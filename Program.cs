using MVPSA_V2022.Mappers;
using MVPSA_V2022.Services;
    
var builder = WebApplication.CreateBuilder(args);
string cors = "ConfigurarCors";
// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: cors, builder =>
   {
       builder.WithOrigins("*");
   });

});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1200);   //TIMOUT DE LA SESION
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton<IReclamoService, ReclamoService>();
builder.Services.AddSingleton<IPagoService, PagoService>();
builder.Services.AddAutoMapper(typeof(ReclamoProfile));
builder.Services.AddAutoMapper(typeof(MobbexPagoProfile));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();
app.UseCors(cors);
app.UseSession(); //Habilito el uso de sesiones borraremos si usamos lStorage


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
