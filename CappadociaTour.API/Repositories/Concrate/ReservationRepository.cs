using CappadociaTour.API.Models;
using CappadociaTour.API.Repositories.Interface;
using Dapper;
using System.Data.SqlClient;
using System.Transactions;

namespace CappadociaTour.API.Repositories.Concrate
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly string _connectionString;

        public ReservationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");

        }
        public async Task<ApiResult> Post(Reservation model, string apikey)
        {
            var apiresult = new ApiResult { Success = true };
          
                using (var connection = new SqlConnection(_connectionString))
                {

                try
                {

                    if (model.CreatedDate == null || model.ReservationDate == null || model.Customer == null || model.variantGroupId == 0
                            || model.CustomerPayment == 0 || model.currencyTypeId == 0 || model.CustomerRoomNumber == 0 || model.Pax == 0
                            )
                    {
                        apiresult.Success = false;
                        apiresult.ErrorMessages.Add("Bad Request");
                        return apiresult;
                    }
                    connection.Open();

                    var userId = await connection.QueryAsync<int>("Select id from Users where Token=@apikey", new { apikey = apikey });
                    var currentPrice = await connection.QueryAsync<decimal>("Select Price from VariantGroup where id=@variantGroupId", new { variantGroupId = model.variantGroupId });


                    if (userId.Count() == 0)
                    {
                        apiresult.Success = false;
                        connection.Close();

                        apiresult.ErrorMessages.Add("Kullanıcı bulunamadı");
                        return apiresult;
                    }
                    else
                    {

                        var parameter = new
                        {
                            value1 = model.Pax,
                            value2 = model.Status,
                            value3 = model.ReservationDate,
                            value4 = model.CreatedDate,
                            value5 = model.currencyTypeId,
                            value6 = model.variantGroupId,
                            value7 = model.CustomerPayment,
                            value8 = model.Note,
                            value9 = model.CustomerRoomNumber,
                            value10 = model.OperationNote,
                            value11 = userId.First(),
                            value12 = currentPrice.First(),
                        };

                        var reservationId = await connection.QueryAsync<int>("INSERT INTO  Reservation(Pax, Status, ReservationDate, CreatedDate,CurrencyTypeId," +
                            "VariantGroupId,CustomerPayment,Note,CustomerRoomNumber,OperationNote,UserId,CurrentPrice) OUTPUT inserted.id VALUES(@value1, @value2," +
                            " @value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value11,@value12)", parameter);

                        if (reservationId == null)
                        {
                            apiresult.Success = false;
                            connection.Close();

                            apiresult.ErrorMessages.Add("Rezervasyon eklenemedi");
                            return apiresult;

                        }
                        else
                        {

                            foreach (var item in model.Customer)
                            {
                                var param = new
                                {
                                    value1 = item.customerName,
                                    value2 = item.customerSurname,
                                    value3 = item.customerPhone,
                                    value4 = item.customerEmail,
                                    value5 = reservationId,

                                };

                                var customer = await connection.QueryAsync("INSERT INTO CustomerDetail (CustomerName, CustomerSurname, CustomerPhone, CustomerEmail,ReservationId)" +
                                    "VALUES(@value1, @value2, @value3,@value4,@value5)", param);
                                if (customer == null)
                                {

                                    apiresult.Success = false;
                                    apiresult.ErrorMessages.Add("Rezervasyon eklenemedi");

                                    connection.Close();

                                    return apiresult;
                                }
                            }
                        }





                    }


                    connection.Close();

                    return apiresult;
                }
                catch (Exception)
                {
                    apiresult.Success = false;
                    apiresult.ErrorMessages.Add("Rezervasyon eklenemedi");

                    connection.Close();

                    return apiresult;
                }

                
            }
        }
    
    
    
    }
}
