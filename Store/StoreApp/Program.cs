using StoreApp.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


//builder.Services.AddDbContext<RepositoryContext>(options=>
//{
//    options.UseSqlite(builder.Configuration.GetConnectionString("sqlconnection"),
//    b=>b.MigrationsAssembly("StoreApp"));
//});
builder.Services.ConfigureDbContext(builder.Configuration);

//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(options =>
//{
//    options.Cookie.Name = "Store App Session";
//    options.IdleTimeout= TimeSpan.FromMinutes(10);
//});
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.ConfigureSession();


builder.Services.ConfigureRepositoryRegistration();

builder.Services.ConfigureServiceRegistration();
builder.Services.ConfigureRouting();//urlde lowecase kuralý

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
app.UseHttpsRedirection();
app.UseRouting();


//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapAreaControllerRoute(
//        name: "Admin",
//        areaName: "Admin",
//        pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}"
//        );
//    endpoints.MapControllerRoute("default", "{controller=Home}/{ action = Index}/{id?}");
//    endpoints.MapRazorPages();
//});

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapRazorPages();
app.ConfigureAndCheckMigration();//migrate update otomatik olarak alýnýr. 
app.ConfigureLocalization();
app.Run();
