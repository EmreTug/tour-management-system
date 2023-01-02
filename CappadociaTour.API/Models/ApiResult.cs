namespace CappadociaTour.API.Models
{
    public class ApiResult
    {
        public ApiResult()
        {
            ErrorMessages = new List<string>();
        }
        public bool Success { get; set; }
        public object item { get; set; }
        public List<string> ErrorMessages { get; set; }

    }

}
