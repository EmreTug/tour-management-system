using CappadociaTour.API.Models;
using CappadociaTour.API.Repositories.Interface;
using Dapper;
using System.Data.SqlClient;

namespace CappadociaTour.API.Repositories.Concrate
{
    public class VariantGroupRepository : IVariantGroupRepository
    {
        private readonly string _connectionString;
        public VariantGroupRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");

        }
        public async Task<ApiResult> Get(int id)
        {
            var apiresult = new ApiResult { Success = true };

            using (var connection = new SqlConnection(_connectionString))
            {
                if (id==0)
                {
                    apiresult.Success = false;
                    apiresult.ErrorMessages.Add("Bad Request");
                    connection.Close();
                    return apiresult;
                }
                connection.Open();

                var variatGroup =await connection.QueryAsync<VariantGroup>("Select ReservationType.TypeName," +
                    "ReservationVariant.VariantName from" +
                    " ReservationTypeVariantCategory,ReservationType,ReservationVariant" +
                    "  where ReservationTypeVariantCategory.ReservationTypeId=ReservationType.id" +
                    " and ReservationTypeVariantCategory.ReservationVariantId=ReservationVariant.id" +
                    " and ReservationTypeVariantCategory.CategoryId=@id", new { id = id });
                if (variatGroup.Count() == 0)
                {
                    apiresult.Success = true;
                    apiresult.ErrorMessages.Add("Not Found");
                    connection.Close();

                    return apiresult;
                }
                connection.Close();
                apiresult.item= variatGroup;
                return apiresult;
            }
        }
    }
}
