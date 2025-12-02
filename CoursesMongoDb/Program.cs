using CoursesMongoDb.Data;
using CoursesMongoDb.Models;
using Microsoft.AspNetCore.Builder;

namespace CoursesMongoDb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            MongoCRUD db = new MongoCRUD("CourseCatalog");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapPost("/course", async (Courses course) =>
            {
                var testDB = await db.AddCourse("Courses", course);
                return Results.Ok(testDB);

            });

            app.Run();
        }
    }
}
