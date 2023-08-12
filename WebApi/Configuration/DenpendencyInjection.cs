using Microsoft.EntityFrameworkCore;
using Application.Common.Security.HashPassword;
using Application.Common.Security.Token;
using Application.Implement;
using Application.IRepository;
using Application.IRepository.IGeneric;
using Application.IRepository.IUnitOfWork;
using Application.Service;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Implement;
using Infrastructure.Mapper;
using Infrastructure.RepositoryImp;
using Infrastructure.RepositoryImp.UnitOfWork;
using Infrastructure.Service;
using Application.Security.HashPassword;
using Application.Security.Token;
using Infrastructure.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Configuration
{
    public static class DenpendencyInjection
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, string databaseConnection, string azureBlobStorage, string containerName)
        {
            // CONNECT DB
            services.AddDbContext<FootBall_PitchContext>(optionsAction: options => { options.UseSqlServer(databaseConnection); });
            
            // SIGN UP UNIT OF WORK FOR REPO
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // SIGN UP REPO
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IFeedBackRepository, FeedBackRepository>();
            services.AddTransient<ILandRepository, LandRepository>();
            services.AddTransient<IOwnerRepository, OwnerRepository>();
            services.AddTransient<IPitchRepository, PitchRepository>();
            services.AddTransient<IPitchImageRepository, PitchImageRepository>();
            services.AddTransient<IPriceRepository, PriceRepository>();
            services.AddTransient<IScheduleRepository, ScheduleRepository>();


            // SIGN UP SERVICE
            services.AddTransient<AccountService, AccountImplement>();
            services.AddTransient<AdminService, AdminImplement>();
            services.AddTransient<BookingService, BookingImplement>();
            services.AddTransient<CustomerService, CustomerImplement>();
            services.AddTransient<FeedbackService, FeedbackImplement>();
            services.AddTransient<LandService, LandImplement>();
            services.AddTransient<OwnerService, OwnerImplement>();
            services.AddTransient<PitchService,PitchImplement>();
            services.AddTransient<PitchImageService, PitchImageImplement>();
            services.AddTransient<PriceService, PriceImplement>();
            services.AddTransient<ScheduleService, ScheduleImplement>();

            // SIGN UP JWTOKEN 
            services.AddScoped<ITokensHandler, TokensHandler>(); // dùng imemory cache save trong suốt quá trình 
            services.AddTransient<IPasswordHasher, PasswordHasher>(); // không dùng lưu trữ vì đã lưu hash trong DB và sử dụng 1 lần
            services.AddScoped<AuthenService, AuthenImplement>();


            //AUTOMAPPER
            services.AddAutoMapper(typeof(ApplicationMapper).Assembly);
            return services;
        }
    }
}
