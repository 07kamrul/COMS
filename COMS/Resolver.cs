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
            services.AddScoped<IDepositRepository, DepositRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IAmountRepository, AmountRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<IAttachmentTypeRepository, AttachmentTypeRepository>();

            services.AddScoped<IErrorService, ErrorService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAmountService, AmountService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IDepositService, DepositService>();

            services.AddSingleton((Serilog.ILogger)Log.Logger);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<MCLDBContext>();
            services.AddScoped<ErrorDbContext>();
            services.AddTransient<PopulateClaimsMiddleware>();
        }
    }
}
