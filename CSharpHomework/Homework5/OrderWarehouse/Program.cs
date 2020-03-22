using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWarehouse
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderService orderService = new OrderService();
            string[] customer = { "Lisa", "Tom", "Ivery" };
            string[] goods = { "pencil", "box", "ruler" };
            orderService.CreateOrder(0,customer[0]);
            orderService.AddOrderItem(1, goods[0], 2, 2);
            orderService.AddOrderItem(0, goods[0], 2, 2);
            orderService.AddOrderItem(0, goods[1], 3, 4);
            orderService.CreateOrder(1, customer[1]);
            orderService.CreateOrder(2, customer[2]);
            orderService.CreateOrder(4, customer[0]);
            orderService.AddOrderItem(2, goods[2], 5, 10);
            orderService.OrderSort();
            Console.WriteLine("查找Lisa的订单");
            orderService.SearchOrders(x => x.OwnerName == customer[0]);
        }
    }
}
