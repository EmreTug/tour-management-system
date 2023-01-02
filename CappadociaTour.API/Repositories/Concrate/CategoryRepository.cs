using CappadociaTour.API.Models;
using CappadociaTour.API.Repositories.Interface;
using Dapper;
using System.Data.SqlClient;

namespace CappadociaTour.API.Repositories.Concrate
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");

        }
        public async Task<ApiResult> Get()
        {
            var apiresult=new ApiResult { Success=true};
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var category = await connection.QueryAsync<Category>("Select * from Category");
                connection.Close();
                apiresult.item = category;
                return apiresult;
            }
            
            
        }
    }
}
