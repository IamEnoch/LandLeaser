using System.Net;

namespace LandLeaser.Shared.Models
{
    public class ResponseMessage
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessful { get; set; }

        public ResponseMessage(HttpStatusCode statusCode, bool isSuccessful)
        {
            StatusCode = statusCode;    
            IsSuccessful = isSuccessful;
        }

    }
}
