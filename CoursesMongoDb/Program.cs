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
            //CREATE - Post
            app.MapPost("/course", async (Courses course) =>
            {
                var record = await db.AddCourse("Courses", course);
                return Results.Ok(record);

            });
            //READALL - Get
            app.MapGet("/courses", async () =>
            {
                var courses = await db.GetAllCourses("Courses");
                return Results.Ok(courses);
            });
            //GET BY ID - Get
            app.MapGet("/course/id/{id}", async (string id) =>
            {
                var course = await db.GetCourseById("Courses", id);

                if(course == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(course);
            });
            //UPDATE - Put
            app.MapPut("course/{id}", async (string id, Courses updatedCourse) =>
            {
                var existingCourse = await db.GetCourseById("Courses", id);

                if(existingCourse == null)
                {
                    return Results.NotFound();
                }
                updatedCourse.Id = existingCourse.Id;

                var result = await db.UpdateCourse("Courses", id, updatedCourse);
                return Results.Ok(result);
            });


            //Delete

            app.MapDelete("/course/{id}", async (string id) =>
            {
                var course = await db.DeleteCourse("Courses", id);
                return Results.Ok(course);
            });

            app.Run(); 
        }
    }
}
