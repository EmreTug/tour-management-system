using CappadociaTour.API.Models;
using CappadociaTour.API.Repositories.Interface;
using Dapper;
using System.Data.SqlClient;

namespace CappadociaTour.API.Repositories.Concrate
{
    public class PriceRepository : IPriceRepository
    {
        private readonly string _connectionString;
        public PriceRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");

        }
        public async Task<ApiResult> Get(Price model)
        {
            var apiresult = new ApiResult { Success = true };

            using (var connection = new SqlConnection(_connectionString))
            {
                if (model.CategoryId==0||model.ReservationTypeId==0||model.ReservationVariantId==0)
                {
                    apiresult.Success = false;
                    apiresult.ErrorMessages.Add("Bad Request");
                    connection.Close();

                    return apiresult;
                }
                connection.Open();
                var param = new { model.CategoryId, model.ReservationTypeId, model.ReservationVariantId };
                var price =await connection.QueryAsync<PriceViewModel>("Select VariantGroup.price,CurrencyType.Type from VariantGroup,CurrencyType where" +
                    " VariantGroup.id=(select VariantGroupId from ReservationTypeVariantCategory where CategoryId=@categoryId and" +
                    " ReservationTypeId=@reservationTypeId and ReservationVariantId=@reservationVariantId) and CurrencyType.id=VariantGroup.CurrencyTypeId", param);
                if (price.Count() == 0)
                {
                    apiresult.Success=true;
                    apiresult.ErrorMessages.Add("Not Found");
                    connection.Close();

                    return apiresult;
                }
                connection.Close();
                apiresult.item = price;
                return apiresult;
                
            }

        }
    }
}
