using Microservices.Services.Catalog.Settings;
using Microservices.Shared.Dtos;
using MongoDB.Driver;
using Microservices.Services.Catalog.Models;


namespace Microservices.Services.Catalog.Services.Course
{
    public class CourseService: ICourseService
    {
		private readonly IMongoCollection<Models.Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;

        public CourseService(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _courseCollection = database.GetCollection<Models.Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        }

        public async Task<ResponseDto<List<Models.Course>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();

            if (courses.Any())
            {
                foreach(var course in courses)
                {
                    course.Category = await _categoryCollection.Find(ct => ct.Id == course.CategoryId).FirstAsync();
                }
            }

            return ResponseDto<List<Models.Course>>.Success(courses, 200);
        }

        public async Task<ResponseDto<Models.Course>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if(course != null)
            {
                course.Category = await _categoryCollection.Find(ct => ct.Id == course.CategoryId).FirstAsync();
            }
            

            return ResponseDto<Models.Course>.Success(course, 200);
        }

        public async Task<ResponseDto<List<Models.Course>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find(x => x.UserId == userId).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find(ct => ct.Id == course.CategoryId).FirstAsync();
                }
            }

            return ResponseDto<List<Models.Course>>.Success(courses, 200);
        }

        public async Task<ResponseDto<Models.Course>> CreateAsync(Models.Course course)
        {
            course.CreatedAt = DateTime.Now;
            await _courseCollection.InsertOneAsync(course);

            return ResponseDto<Models.Course>.Success(course, 201);
        }

        public async Task<ResponseDto<NoContentDto>> UpdateAsync(Models.Course course)
        {
            await _courseCollection.FindOneAndReplaceAsync(crs => crs.Id == course.Id, course);

            return ResponseDto<NoContentDto>.Success(204);
        }

        public async Task<ResponseDto<NoContentDto>> DeleteAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(crs => crs.Id == id);

            if(result.DeletedCount < 0)
            {
                return ResponseDto<NoContentDto>.Fail("Course not found", 404);
            }

            return ResponseDto<NoContentDto>.Success(204);
        }
    }
}

