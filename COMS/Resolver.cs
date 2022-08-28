using COMS.Security;
using Core.Common;
using Core.FileStore;
using Core.Repository;
using Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Repository;
using Serilog;
using Service;

namespace COMS
{
    public class Resolver
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<Config>();

            services.AddScoped<IFileStore, LocalFileStore>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IErrorRepository, ErrorRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<IAttachmentTypeRepository, AttachmentTypeRepository>();

            services.AddScoped<IErrorService, ErrorService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAttachmentService, AttachmentService>();

            services.AddSingleton((Serilog.ILogger)Log.Logger);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<MCLDBContext>();
            services.AddScoped<ErrorDbContext>();
            services.AddTransient<PopulateClaimsMiddleware>();

            services.AddScoped<IUserResolverService, UserResolverService>();

        }
    }
}
