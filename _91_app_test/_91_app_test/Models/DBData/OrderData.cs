using System;
namespace _91_app_test.Models.DBData
{
    public class OrderData
    {
        public OrderData()
        {
        }

        public string OrderID { get; set; }
        public string Account { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int Status { get; set; }

    }
}
