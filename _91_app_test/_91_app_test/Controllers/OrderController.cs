using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _91_app_test.Models;
using _91_app_test.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _91_app_test.Controllers
{
    [Route("api/[controller]/[action]")]
    public class OrderController : Controller
    {
        private IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        /// <summary>
        /// 取得訂單
        /// </summary>
        /// <param name="account"></param>
        /// <returns>訂單列表</returns>
        [HttpGet]
        public IActionResult Orders(string account)
        {
            return Json(_orderService.GetOrders(account));
        }

        /// <summary>
        /// 訂單物品資訊
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns>物品資訊</returns>
        [HttpGet]
        public IActionResult OrderItem(int itemid)
        {
            return Json(_orderService.GetOrderItem(itemid));
        }

        /// <summary>
        /// 更改訂單狀態
        /// </summary>
        /// <param name="account"></param>
        /// <param name="orders"></param>
        /// <returns>更新筆數</returns>
        [HttpPost]
        public IActionResult ShipOrders([FromHeader] string account, [FromBody]List<string> orders)
        {
            return Json(new { count = _orderService.UpdateOrderShip(account, orders) });
        }
    }
}
