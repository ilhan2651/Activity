using App.Services.Services.ApiServices.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:44344/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Servisleri DI container'a ekle, ortak HttpClient kullan
builder.Services.AddScoped<EventApiService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    return new EventApiService(httpClientFactory.CreateClient("ApiClient"));
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
