using App.Repositories.Repositories.Abstract;
using App.Repositories.Repositories.Concrete;
using App.Services.Services.Abstract;
using App.Services.Services.Concrete;

namespace App.Api.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {


            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEventRepository, EventRepository>();

            services.AddScoped<IEventParticipantService, EventParticipantService>();
            services.AddScoped<IEventParticipantRepository, EventParticipantRepository>();

            services.AddScoped<IUserService, UserService>();
            

            services.AddScoped<IAuthService, AuthService>();
            
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IContactRepository, ContactRepository>();




            return services;
        }

    }
}
