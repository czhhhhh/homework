using System;

namespace OrderWarehouse_XML
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderService MyOrders = new OrderService();
            MyOrders.CreateOrder(00, "meili");
            MyOrders.CreateOrder(01, "dongren");
            MyOrders.AddOrderItem(00, "pen", 3, 5);
            
            //MyOrders.Export();
            //MyOrders.Import();
            //MyOrders.Export();
        }
    }
}
