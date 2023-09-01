using System.Net;

namespace StudentMinimalAPI.Models
{
    public class APIResponse
    {
        public APIResponse() {
            ErrorMessages = new List<string>();
        }

        public Boolean IsSuccess { get; set; }
        public Object Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }  
        public List<String> ErrorMessages { get; set; }
    }
}
