using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LandLeaser.Shared.Models
{
    public class ResponseMessage
    {
        public Object ResponseItem { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessful { get; set; }

        public ResponseMessage(object responseItem, HttpStatusCode statusCode, bool isSuccessful)
        {
            ResponseItem = responseItem;
            StatusCode = statusCode;    
            IsSuccessful = isSuccessful;
        }

    }
}
