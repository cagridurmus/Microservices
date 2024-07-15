using Microservices.Services.Catalog.Models;
using Microservices.Services.Catalog.Settings;
using Microservices.Shared.Dtos;
using MongoDB.Driver;

namespace Microservices.Services.Catalog.Services
{
    public class CategoryService: ICategoryService
    {
		private readonly IMongoCollection<Category> _categoryCollection;

        public CategoryService(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        }

        public async Task<ResponseDto<List<Category>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();

            return ResponseDto<List<Category>>.Success(categories, 200);
        }

        public async Task<ResponseDto<Category>> CreateAsync(Category category)
        {
            await _categoryCollection.InsertOneAsync(category);

            return ResponseDto<Category>.Success(category, 201);
        }

        public async Task<ResponseDto<Category>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find(category => category.Id == id).FirstOrDefaultAsync();
            return ResponseDto<Category>.Success(category, 200);
        }

    }
}

