using AppTemplate.Database;
using AppTemplate.Net8.Database;
using AppTemplate.Users;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddUsersModule(builder.Configuration);

builder.Services.AddDbContext<DataContext>(
    options =>
    {
        var connection = builder.Configuration.GetConnectionString("DefaultConnection");
        options.UseSqlServer(connection,c=>c.MigrationsAssembly("AppTemplate.Database")); 
    });

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DataContext>();
    context.Database.Migrate();
}

app.Run();

