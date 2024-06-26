using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Infraestructure.Identity.Seeds;
using RealEstateApp.Presentation.WebAPI.Extensions;
using RealEstateApplication.Identity;
using RealEstateApplication.Identity.Entities;
using RealEstateApplication.Identity.Seeds;
using RealEstateApplication.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressInferBindingSourcesForParameters = true;
    options.SuppressMapClientErrors = true;
});

builder.Services.AddIdentityInfrastructure(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(rolesManager);
        await DefaultAdminUser.SeedAsync(userManager, rolesManager);
        await DefaultClientUser.SeedAsync(userManager, rolesManager);
        await DefaultAgentUser.SeedAsync(userManager, rolesManager);
    }
    catch (Exception ex)
    {
        throw new Exception(ex.Message);
    }
}




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHealthChecks("/health");
app.UseSession();
app.UseSwaggerExtension();
app.UseErrorHandlingMiddleware();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
