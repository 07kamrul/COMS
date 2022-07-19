using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MCLDBContext : DbContext
    {
        public MCLDBContext(DbContextOptions<MCLDBContext> options) :base(options)
        {

        }

        public virtual DbSet<Attachments> Attachment { get; set; }
        public virtual DbSet<AttachmentTypes> AttachmentType { get; set; }
        public virtual DbSet<AuditLogs> AuditLogs { get; set; }
        public virtual DbSet<Amounts> Amounts { get; set; }
        public virtual DbSet<Deposites> Deposites { get; set; }
        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<RoleUser> RoleUser { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasMany(u => u.Roles).WithMany(x => x.Users).UsingEntity<RoleUser>(
                x => x.HasOne(xs => xs.Role).WithMany(),
                x => x.HasOne(xs => xs.User).WithMany()
                ).HasKey( x => new {x.UserId, x.RoleId});
        }

        protected virtual long GetPrimaryKeyValue<T>(T entity)
        {
            var test = entity;
            var test2 = test.GetType();
            var keyName = this.Model.FindEntityType(test2).FindPrimaryKey().Properties.Select(x => x.Name).Single();

            object obj = entity.GetType().GetProperty(keyName).GetValue(entity, null);
            var result = (obj is int) ? (int)obj : (long)obj;
            if(result < 0)
            {
                return -1;
            }
            return result;
        }

        public int SaveChanges(string userId)
        {
            if(userId == null)
            {
                return base.SaveChanges();
            }

            try
            {
                ChangeTracker.DetectChanges();
                IList<AuditLogs> auditLogChanges = new List<AuditLogs>();
                var modifiedEntities = ChangeTracker.Entries().Where( p => p.State == EntityState.Modified || p.State == EntityState.Detached || p.State == EntityState.Detached).ToList();

                foreach(var change in modifiedEntities)
                {
                    TableAttribute tableAttribute = change.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;
                    string tableName = tableAttribute != null ? tableAttribute.Name : change.Entity.GetType().Name;
                    var entityName = change.Entity.GetType().Name;
                    var primaryKeyValue = GetPrimaryKeyValue(change.Entity);

                    if (change.State == EntityState.Added)
                    {
                        var changeLoged = new AuditLogs
                        {
                            ColumnName = "All",
                            RecordID = primaryKeyValue.ToString(),
                            EventDateUTC = DateTime.UtcNow,
                            OriginalValue = "",
                            NewValue = Newtonsoft.Json.JsonConvert.SerializeObject(change.Entity),
                            UserID = userId,
                            EventType = change.State.ToString().Substring(0, 1),
                            TableName = tableName
                        };
                        AuditLogs.Add(changeLoged);
                    }
                    else
                    {
                        foreach(var prop in change.Entity.GetType().GetTypeInfo().GetRuntimeProperties())
                        {
                            if(prop.Name == "ModifiedBy" || prop.Name == "ModifiedDate")
                            {
                                continue;
                            }

                            if(!prop.GetGetMethod().IsVirtual && prop.CustomAttributes.Where(x => x.AttributeType.Name == "NotMappedAttribute").Count() == 0)
                            {
                                try
                                {
                                    var currentValue = change.Property(prop.Name).CurrentValue ?? string.Empty;
                                    var originalValue = change.Property(prop.Name).OriginalValue ?? string.Empty;

                                    if(change.Property(prop.Name).CurrentValue != null && change.Property(prop.Name).OriginalValue != null 
                                        && change.Property(prop.Name).CurrentValue.GetType() == typeof(decimal))
                                    {
                                        currentValue = ((decimal)change.Property(prop.Name).CurrentValue).ToString("0.00");
                                        originalValue = ((decimal)change.Property(prop.Name).OriginalValue).ToString("0.00");
                                    }

                                    if(originalValue.ToString() != currentValue.ToString() || change.State == EntityState.Added || change.State == EntityState.Deleted)
                                    {
                                        var changeLoged = new AuditLogs
                                        {
                                            ColumnName = prop.Name,
                                            RecordID = primaryKeyValue.ToString(),
                                            EventDateUTC = DateTime.UtcNow,
                                            OriginalValue = originalValue.ToString(),
                                            NewValue = currentValue.ToString(),
                                            UserID = userId,
                                            EventType = change.State.ToString().Substring(0, 1),
                                            TableName = tableName
                                        };
                                        auditLogChanges.Add(changeLoged);
                                    }
                                }
                                catch
                                {

                                }
                            }
                        }
                    }
                }
                if(auditLogChanges.Count > 0)
                {
                    AuditLogs.AddRange(auditLogChanges);
                }
                return base.SaveChanges();
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
    }
}
