using System;
using System.ComponentModel;

namespace _91_app_test.Models.DBData.DataType
{
    public enum OrderStatus
    {
        [Description("Payment Completed")]
        PaymentCompleted = 1,
        [Description("To Be Shipped")]
        ToBeShipped = 2
    }
}
