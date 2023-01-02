using CappadociaTour.API.Models;
using CappadociaTour.API.Repositories.Interface;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CappadociaTour.API.Repositories.Concrate
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _configuration = configuration;

        }
        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private string CreateToken(string username,string role)
        {
            
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }



        public async Task<ApiResult> Login(Login model)
        {
            var apiresult = new ApiResult { Success = true };

            using (var connection = new SqlConnection(_connectionString))
            {
                if (model.UsernameOrMail == null  || model.Password == null )
                {
                    apiresult.Success = false;
                    apiresult.ErrorMessages.Add("Bad Request");
                    connection.Close();

                    return apiresult;
                }
                connection.Open();
                var passhash = ComputeSha256Hash(model.Password);

                var parameter = new { username = model.UsernameOrMail,email=model.UsernameOrMail, passwordHash = passhash };
                var result = await connection.QueryAsync<User>("Select Name,Surname,Username,Token,Role from Users where (Username=@username or Email=@email) and PasswordHash=@passwordHash", parameter);

                User User = new User();
                if (result.Count() != 0)
                {
                    User = result.First();
                    var key = CreateToken(User.Username,User.Role);
                    var param = new { key = key, username = model.UsernameOrMail,email=model.UsernameOrMail, passwordHash = passhash };

                    connection.Query("UPDATE Users SET Token = @key WHERE (Username=@username or Email=@email) and  PasswordHash=@passwordHash ", param);
                    User.ApiKey = key;
                }
                else
                {
                    apiresult.Success = false;
                    apiresult.ErrorMessages.Add("Not Found");
                    connection.Close();

                    return apiresult;
                }
                connection.Close();
                apiresult.item = User;
                return apiresult;
            }


        }

        public async Task<ApiResult> CreateUser(CreateUser model)
        {
            var apiresult = new ApiResult { Success = true };

            using (var connection = new SqlConnection(_connectionString))
            {
                if (model.Username == null || model.Email == null || model.Password == null || model.Surname == null || model.Name == null || model.PhoneNumber == null || model.Role == null )
                {
                    apiresult.Success = false;
                    apiresult.ErrorMessages.Add("Bad Request");
                    return apiresult;
                }
                connection.Open();
                var parameter = new { model.Username, model.Email, model.PhoneNumber };
                var result = await connection.QueryAsync<User>("Select * from Users where Username=@username or Email=@email or PhoneNumber=@phonenumber", parameter);

                User User = new User();
                if (result.Count() != 0)
                {
                    apiresult.Success= false;
                    apiresult.ErrorMessages.Add("Kullanıcı kayıtlı");
                    return apiresult;
                }
                else
                {

                    var key = CreateToken(model.Username,model.Role.ToLower());
                    var hashingpass = ComputeSha256Hash(model.Password);
                    var param = new { value1 = model.Name, value2 = model.Surname, value3 = model.Username, value4 = model.Email, value5 = hashingpass, value6 = model.PhoneNumber, value7 = key,value8=model.Role.ToLower() };

                    await connection.QueryAsync("INSERT INTO Users (Name, Surname, Username, Email,PasswordHash,PhoneNumber,Token,Role)VALUES(@value1, @value2, @value3,@value4,@value5,@value6,@value7,@value8)", param);
                    var user = await connection.QueryAsync<User>("Select Name,Surname,Username,Token,Role from Users where Username=@username", new { model.Username });
                    if (user.Count() == 0)
                    {
                        apiresult.Success = false;
                        apiresult.ErrorMessages.Add("Not Found");
                        return apiresult;

                    }
                    User = user.First();
                    User.ApiKey = key;
                }
                connection.Close();
                apiresult.item = User;
                return apiresult;

            }
        }

      
    }
}
