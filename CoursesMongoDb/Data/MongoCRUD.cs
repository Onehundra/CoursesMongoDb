using CoursesMongoDb.Models;
using MongoDB.Driver;

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
        //READ - Get
        public async Task<List<Courses>> GetAllCourses(string collectionToUse)
        {
            var collection = db.GetCollection<Courses>(collectionToUse);
            var cursor = await collection.FindAsync(_ => true);
            var courses = await cursor.ToListAsync();
            return courses;
        }
    }
}
