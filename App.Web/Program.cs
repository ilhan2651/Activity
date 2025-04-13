using App.Services.Services.ApiServices.Concrete;
using App.Services.Validation;
using FluentValidation.AspNetCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:44344/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<CreateContactValidator>();
builder.Services.AddValidatorsFromAssembly(typeof(CreateContactValidator).Assembly);

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.Cookie.Name = "MyApp.Auth";
        options.Cookie.HttpOnly = true;
    });

// Servisleri DI container'a ekle, ortak HttpClient kullan
builder.Services.AddScoped<EventApiService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();

    return new EventApiService(httpClientFactory.CreateClient("ApiClient"), httpContextAccessor);
});

builder.Services.AddScoped<EventParticipantApiService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    return new EventParticipantApiService(httpClientFactory.CreateClient("ApiClient"));
});
builder.Services.AddScoped<UserApiService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    return new UserApiService(httpClientFactory.CreateClient("ApiClient"));
});
builder.Services.AddScoped<CommentApiService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    return new CommentApiService(httpClientFactory.CreateClient("ApiClient"));
});
builder.Services.AddScoped<AuthApiService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();

    return new AuthApiService(httpClientFactory.CreateClient("ApiClient"), httpContextAccessor);
});
builder.Services.AddScoped<ContactApiService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();

    return new ContactApiService(httpClientFactory.CreateClient("ApiClient"), httpContextAccessor);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

