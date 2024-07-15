using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcWebIdentity.Data;
using MvcWebIdentity.Policies;
using MvcWebIdentity.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var connection = builder.Configuration.GetConnectionString("ConnectionDefault");
builder.Services.AddDbContext<Contexto>(option => option.UseSqlServer(connection));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<Contexto>();
builder.Services.Configure<IdentityOptions>(option =>
{
    option.Password.RequiredLength = 10;
    option.Password.RequiredUniqueChars = 3;
    option.Password.RequireNonAlphanumeric = false;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.Cookie.Name = "AspNetCore.Cookies";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        option.SlidingExpiration = true;
    });

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("RequireUserAdminGerenteRole", policy => policy.RequireRole("User", "Admin", "Gerente"));
});

//agora vamos definir as politicas
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("IsAdminClaimAccess", policy => policy.RequireClaim("CadastrarEm"));
    option.AddPolicy("IsAdminClaimAccess", policy => policy.RequireClaim("IsAdmin","true"));
    option.AddPolicy("IsFuncionarioClaimAccess", policy => policy.RequireClaim("IsFuncionario", "true"));
    option.AddPolicy("TempoCadastroMinimo", policy =>
    {
        //estou definindo o tempo em 5 anos
        policy.Requirements.Add(new TempoCadastroRequirement(5));
    });
    option.AddPolicy("TesteClaim", policy => policy.RequireClaim("Teste", "teste_claims"));

});

builder.Services.AddScoped<IAuthorizationHandler, TempoCadastroHandler>();
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
builder.Services.AddScoped<ISeedUserClaimsInitial, SeedUserClaimsInitial>();



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
//quando o projeto for executado,esse m�todo vai ser invocado e ele vai invocar os dois m�todos implementados nele
await CriarPerfisUsuarioAsync(app);

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
    );
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

//M�todo para invocar os m�todos que v�o criar os usuarios e roles
async Task CriarPerfisUsuarioAsync(WebApplication app)
{
    //estou obtendo uma instancia do servi�o IServiceScopeFactory do conteiner do servi�o de inje��o de dependencia
    //da aplica��o e essa interface � responsavel por criar e gerenciar scopo de servi�os para nossa aplica�a�,
    //cada scopo de servi�o tem sua propia instancia de servi�o registrado no container de di
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    //aqui estou criando um scopo usando minha instancia do servi�o criada acima
    using (var scope = scopedFactory.CreateScope())
    {

        //var service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();       
        //await service.SeedRolesAsync();
        //await service.SeedUsersAsync();


        //aqui consigo a instancia do metodo ISeedUserClaimsInitial
        var service = scope.ServiceProvider.GetService<ISeedUserClaimsInitial>();
        //em seguida eu invoco o m�todo 
        await service.SeedUserClaims();
    }

}
