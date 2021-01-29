using System;
using System.Collections.Generic;
using _91_app_test.Common;
using _91_app_test.Models;
using _91_app_test.Models.DBData;
using _91_app_test.Models.DBData.DataType;
using _91_app_test.Models.ViewModel;
using Dapper;

namespace _91_app_test.Services
{
    public interface IOrderService
    {
        List<OrderVM> GetOrders(string account);
        ProductVM GetOrderItem(int productId);
        int UpdateOrderShip(string account, List<string> orders);
    }
    public class OrderService : IOrderService
    {

        private IDatabaseService _databaseService;
        private IDBConfig _dbConfig;

        public OrderService(IDatabaseService databaseService, IDBConfig dbConfig)
        {
            _databaseService = databaseService;
            _dbConfig = dbConfig;

        }

        /// <summary>
        /// 取得商品資訊
        /// </summary>
        /// <param name="productId">商品編號</param>
        /// <returns></returns>
        public ProductVM GetOrderItem(int productId)
        {
            string sqlStatement = "select Name, Price, Cost, ProductDesc from TB_Product " +
                " where ProductID = @in_prodid";
            var data = new DynamicParameters();
            data.Add("@in_prodid", productId, System.Data.DbType.Int32);

            return _databaseService.SingleQuery<ProductVM>(new DatabaseObject(_dbConfig.GetLocalDBConnStr(), sqlStatement, data));

        }

        /// <summary>
        /// 訂單列表
        /// </summary>
        /// <param name="account">會員帳號</param>
        /// <returns></returns>
        public List<OrderVM> GetOrders(string account)
        {
            string sqlStatement = "select to2.OrderID , tp.Name OrderItem, tp.ProductId,tp.Price ," +
                " tp.Cost, to2.Status as OrderStatus " +
                " from TB_Order to2 " +
                " join TB_OrderItem toi on(to2.OrderID = toi.OrderID) " +
                " join TB_Product tp on(toi.ProductID = tp.ProductID) " +
                " where to2.Account = @in_account";

            var data = new DynamicParameters();
            data.Add("@in_account", account, System.Data.DbType.String, size:30);
             
            return _databaseService.Query<OrderVM>(new DatabaseObject(_dbConfig.GetLocalDBConnStr(), sqlStatement, data));
        }


        /// <summary>
        /// 更新訂單狀態
        /// </summary>
        /// <param name="account"></param>
        /// <param name="orders"></param>
        /// <returns>更新筆數</returns>
        public int UpdateOrderShip(string account, List<string> orders)
        {
            // 驗證訂單內容
            string queryStatement = "select OrderId, Status from TB_Order " +
                " where Account = @in_account and OrderId in @in_orders and status = @in_status";
            var updateData = new DynamicParameters();
            updateData.Add("@in_account", account, System.Data.DbType.String, size: 30);
            updateData.Add("@in_orders", orders.ToArray());
            updateData.Add("@in_status", (int)OrderStatus.PaymentCompleted, System.Data.DbType.Int16);


            var orderDatas = _databaseService.Query<OrderData>(new DatabaseObject(_dbConfig.GetLocalDBConnStr(), queryStatement, updateData));

            if (orderDatas == null || orderDatas.Count == 0) return 0;
            foreach (var item in orderDatas)
            {
                item.Status = (int)OrderStatus.ToBeShipped;
                item.UpdateDate = DateTime.Now;
            } 
            string updateStatement = "update TB_Order set Status = @Status, UpdateDate = @UpdateDate where OrderId = @OrderId";

           
            return _databaseService.Update(new DatabaseObject(_dbConfig.GetLocalDBConnStr(), updateStatement, orderDatas));

        }
    }
}
