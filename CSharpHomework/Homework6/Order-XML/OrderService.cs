using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OrderWarehouse_XML
{
    delegate bool Func(Order order);
    class InvalidOperation: Exception
    {
        public string message { get; }
        public InvalidOperation(string message) { this.message = message; }

    }
    [Serializable()]
    class OrderService
    {
        List<Order> orders = new List<Order>();
        public List<Order> Orders { get=>orders ; set=>value=orders ; }
        public void CreateOrder(int id,string client)
        {
            try
            {
                int flag = 0;
                foreach (Order i in orders) { if (id == i.OrderID) flag = 1; }//检测是否有重复订单号
                if (flag == 0)
                {
                    Order NewOrder = new Order(id, client);
                    //添加到订单序列
                    orders.Add(NewOrder);
                }
                else
                {
                    throw new InvalidOperation("重复的订单号");
                }
            }
            catch (InvalidOperation E)
            {
                Console.WriteLine("订单创建异常：" + E.message);
            }
            
        }
        public void DeleteOrder(int id)
        {
            try
            {
                int flag = 0;
                for (int i = orders.Count() - 1; i >= 0; i--)
                {
                    if (id == orders[i].OrderID)
                    {
                        orders.Remove(orders[i]);
                        flag = 1;
                    }
                }
                if (flag == 1)
                    Console.WriteLine("订单删除成功！");
                else
                {
                    throw new InvalidOperation("不存在该订单！");
                }
            }catch(InvalidOperation e)
            {
                Console.WriteLine("订单删除异常：" + e.message);
            }
        }
        public void AddOrderItem(int id ,string item ,int quantity ,double price)
        {
            try
            {
                int flag = 0;
                
                for(int i = orders.Count() - 1; i >= 0; i--)
                {
                    if (id == orders[i].OrderID)
                    {
                        flag = 1;
                        orders[i].AddItem(item, quantity, price);
                        Console.WriteLine("成功添加商品！");
                    }
                }
                if (flag == 0)
                {
                    throw new InvalidOperation("不存在该订单！");
                }
            }catch(InvalidOperation e)
            {
                Console.WriteLine("商品添加异常：" + e.message);
            }
        }
        public void DeleteOrderItem(int id , string item,int quantity,double price)
        {
            try
            {
                int flag = 0;

                for (int i = orders.Count() - 1; i >= 0; i--)
                {
                    if (id == orders[i].OrderID)
                    {
                        flag = 1;
                        orders[i].DeleteItem(item, quantity, price);
                    }
                }
                if (flag == 0)
                {
                    throw new InvalidOperation("不存在该订单！");
                }
            }
            catch (InvalidOperation e)
            {
                Console.WriteLine("商品删除异常：" + e.message);
            }
        }
        public void OrderSort()
        {
            orders.Sort();
        }
        public void SearchOrders(Func func)
        {
            var query = from order in orders
                        where func(order)
                        select order;
            foreach(Order i in query) { Console.WriteLine(i.ToString()); }
        }
        public void Export()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
            using(FileStream fs=new FileStream("Orders.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fs, Orders);
            }
            Console.WriteLine("\nSerialized as XML:");
            Console.WriteLine(File.ReadAllText("Orders.xml"));
        }
        public void Import()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs=new FileStream("Orders.xml", FileMode.Open))
            {
                List<Order> orders = (List<Order>)xmlSerializer.Deserialize(fs);
                foreach(Order x in orders) { Console.WriteLine(x.ToString()); }
            }
        }
    }
}
