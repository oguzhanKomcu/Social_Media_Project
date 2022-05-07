using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SMP.Application.IoC;
using SMP.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("CMSAPI", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "CMS API",
        Version = "v1",
        Description = "RestFul API",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
        {
            Email = "burak.yilmaz@bilgeadam.com",
            Name = "Burak Y�lmaz",
            Url = new Uri("https://github.com/Burakkylmz")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "MIT Licance",
            Url = new Uri("https://opensource.org/licanses/MIT")
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

//CMS.Presentation ve CMS.API ayn� veri taban� ile konu�acaklar. Bu y�zden Connection string burada da yaz�lacak. Lakil migraton gibi bir operasyon yap�lamayacak. Zaten veri taban� hali haz�rda var.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new DependencyResolver());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/CMSAPI/swagger.json", "CMS API");
    });
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.Run();

