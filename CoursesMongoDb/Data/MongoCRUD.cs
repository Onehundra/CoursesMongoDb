using CoursesMongoDb.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CoursesMongoDb.Data
{
    public class MongoCRUD
    {
        private IMongoDatabase db;


        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        //CREATE - Add
        public async Task<Courses> AddCourse(string collectionToUse, Courses course)
        {
            var collection = db.GetCollection<Courses>(collectionToUse);
            await collection.InsertOneAsync(course);
            return course;
        }
        //READALL - Get
        public async Task<List<Courses>> GetAllCourses(string collectionToUse)
        {
            var collection = db.GetCollection<Courses>(collectionToUse);
            var courses = await collection.AsQueryable().ToListAsync();

            return courses;
        }

        //READ BY ID - Get
        public async Task<Courses> GetCourseById(string collectionToUse, string id)
        {
            var collection = db.GetCollection<Courses>(collectionToUse);
            var course = await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return course;
        }

        //UPDATE - Put
        public async Task<Courses> UpdateCourse(string collectionToUse, string id, Courses updatedCourse)
        {
            var collection = db.GetCollection<Courses>(collectionToUse);
            updatedCourse.Id = id;
            var result = await collection.ReplaceOneAsync(x => x.Id == id, updatedCourse);
            if (result.MatchedCount == 0)
            {
                return null;
            }
            return updatedCourse;
        }

        //DELETE - Delete
        public async Task<string> DeleteCourse(string collectionToUse, string id)
        {
            var collection = db.GetCollection<Courses>(collectionToUse);
            var result = await collection.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount == 0)
            {
                return null;
            }
            return "Course was deleted. . . ";
        }
    }
}
