using Microsoft.EntityFrameworkCore;
using PokemonReviewApp;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//sql server
builder.Services.AddDbContext<AppDbContext>
   (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefultConnectionstring")));

builder.Services.AddControllers();

// DI
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
//seed Db
builder.Services.AddTransient<Seeding>();
// AutoMapper Config
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


 

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seeding>();
        service.SeedData();
    }
}




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

