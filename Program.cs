using System;
using System.Security.AccessControl;
using System.IO;
using System.Data.Common;
using Microsoft.Extensions.FileProviders;
using appointmentApi.Models.data;
using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppointmentDbContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("hospitalCon"))
);
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddTransient<IUserServices, UserServices>();

var app = builder.Build();
app.UseCors(options=>{
    options.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Images")),
    RequestPath="/Images"
});
// app.UseStaticFiles(new StaticFileOptions
// {
//    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Images")), 
//    RequestPath = "/Images"
// });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger( options=>{
        options.SerializeAsV2 = true;
    } );
    app.UseSwaggerUI(options=>{
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    } );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
