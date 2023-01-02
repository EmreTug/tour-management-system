using CappadociaTour.API.Models;
using CappadociaTour.API.Repositories.Interface;
using Dapper;
using System.Data.SqlClient;

namespace CappadociaTour.API.Repositories.Concrate
{
    public class CurrencyTypeRepository : ICurrencyTypeRepository
    {
        private readonly string _connectionString;
        public CurrencyTypeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }
        public async Task<ApiResult> Get()
        {
            var apiresult=new ApiResult { Success=true};
            using (var connection= new SqlConnection(_connectionString))
            {

                connection.Open();
                var currencyTypes =await connection.QueryAsync<CurrencyType>("Select * from CurrencyType");
                connection.Close();
                apiresult.item = currencyTypes;
                return apiresult;
            }
        }
    }
}
