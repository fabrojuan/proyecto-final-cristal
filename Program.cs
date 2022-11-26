using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MVPSA_V2022.Configurations;
using MVPSA_V2022.Interceptors;
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

var authenticationKey = "esta es la clave secreta para autenticar usuarios";

builder.Services.AddSingleton<IReclamoService, ReclamoService>();
builder.Services.AddSingleton<IDenunciaService, DenunciaService>();
builder.Services.AddSingleton<IPagoService, PagoService>();
builder.Services.AddSingleton<IUsuarioService, UsuarioService>();
builder.Services.AddSingleton<IJwtAuthenticationService> (new JwtAuthenticationService(authenticationKey));
builder.Services.AddTransient<IMailService, MailService>();

builder.Services.AddAutoMapper(typeof(ReclamoProfile));
builder.Services.AddAutoMapper(typeof(MobbexPagoProfile));

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters { 
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(authenticationKey)),
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();
app.UseCors(cors);
app.UseSession(); //Habilito el uso de sesiones borraremos si usamos lStorage
app.UseAuthentication();
app.UseAuthorization();
app.UseUserMiddleware();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
