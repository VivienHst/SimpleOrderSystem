using System;
namespace _91_app_test.Models
{
    public class ResponseModel
    {
        public ResponseModel(int code, string message)
        {
            this.code = code;
            this.message = message;
        }
        public ResponseModel(object data)
        {
            this.code = 1;
            this.message = "success";
            this.data = data;
        }

        public int code { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
