using System;
using System.Text.Json.Serialization;
using _91_app_test.Common.Tool;
using _91_app_test.Models.DBData.DataType;

namespace _91_app_test.Models.ViewModel
{
    public class OrderVM
    {
        public string OrderID { get; set; }
        public string OrderItem { get; set; }
        public int ProductId { get; set; }
        public int Price { get; set; }
        public int Cost { get; set; }

        public string Status
        {
            get { return EnumExtenstions.GetEnumDescription((OrderStatus)OrderStatus); }
        }

        [JsonIgnore]
        public int OrderStatus { get; set; }
    }
}
