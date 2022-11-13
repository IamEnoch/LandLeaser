using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

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
