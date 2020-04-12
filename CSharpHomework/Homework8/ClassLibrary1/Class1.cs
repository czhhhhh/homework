using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClassLibrary1
{
    public class Goods
    {
        public string GoodName { set; get; }
        public double GoodPrice { set; get; }
        public Goods()
        {
            GoodName = "good";
            GoodPrice = 10;
        }
        public Goods(string name, double price)
        {
            GoodName = name;
            GoodPrice = price;
        }
        public override bool Equals(object obj)
        {
            Goods good = (Goods)obj;
            return good.GoodName == GoodName;
        }
        public override int GetHashCode()
        {
            int i = int.Parse(GoodName);
            return i;
        }
        public override string ToString()
        {
            return "Good Name: " + GoodName + " Price: " + GoodPrice;
        }
    }

    public class Customer
    {
        public string Name { set; get; }
        public string TelNumber { set; get; }
        public Customer()
        {
            Name = "zhang";
            TelNumber = "123456";
        }
        public Customer(string name, string tel)
        {
            Name = name;
            TelNumber = tel;
        }
        public override bool Equals(object obj)
        {
            Customer thisCus = (Customer)obj;
            return Name == thisCus.Name || this.TelNumber == thisCus.TelNumber;
        }
        public override int GetHashCode()
        {
            int telNumber = int.Parse(TelNumber);
            return telNumber;
        }
        public override string ToString()
        {
            return "Customer: " + Name + "Tel Number" + TelNumber;
        }
    }

    public class OrderItem
    {
        Goods goods;
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public OrderItem(Goods good, int quantity)
        {
            goods = good;
            Quantity = quantity;
            TotalPrice = goods.GoodPrice * quantity;
        }
        public override bool Equals(object orderItem)
        {
            OrderItem item = (OrderItem)orderItem;
            return item.goods.GoodName == goods.GoodName;//商品名一样就批判定为一样
        }
        public override int GetHashCode()
        {
            int name = int.Parse(goods.GoodName);
            return name;
        }
        public override string ToString()
        {
            return string.Format($"名称：{goods.GoodName}  总数：{Quantity}  单价：{goods.GoodPrice}  总价：{TotalPrice}");
        }
    }

    public class Order : IComparable
    {
        List<OrderItem> orderList;
        Customer Icustomer;
        public List<OrderItem> OrderList { set => value = orderList; get => orderList; }
        public int CompareTo(object obj)
        {
            if (!(obj is Order))
            {
                throw new System.ArgumentException();
            }
            Order order2 = (Order)obj;
            return this.OrderID.CompareTo(order2.OrderID);
        }
        public string Client { get => Icustomer.Name; }
        public int OrderID { get; set; }
        public double TotalCost { get; set; }
        public Order(int ID, Customer client)
        {
            OrderID = ID;
            Icustomer = client;
            TotalCost = 0;
            orderList = new List<OrderItem>();
        }
        //无参构造函数，让xml能够创建对象
        public Order()
        {
            OrderID = 0;
            TotalCost = 0;
            Icustomer = new Customer();
        }
        public void AddItem(Goods goods, int quantity)
        {
            int flag = 0;
            OrderItem NewItem = new OrderItem(goods, quantity);
            for (int i = orderList.Count() - 1; i >= 0; i--)
            {
                if (orderList[i].Equals(NewItem))
                {//如果订单明细中已经有同种商品则修改
                    orderList[i].Quantity += quantity;//增加商品数量
                    flag = 1;
                }
            }
            if (flag == 0)
            {
                orderList.Add(NewItem);
            }
            TotalCost += (goods.GoodPrice * quantity);
        }
        public void DeleteItem(Goods goods, int quantity)
        {
            int flag = 0;
            OrderItem NewItem = new OrderItem(goods, quantity);
            for (int i = orderList.Count() - 1; i >= 0; i--)
            {
                if (orderList[i].Equals(NewItem))
                {
                    flag = 1;
                    if (orderList[i].Quantity < quantity)
                    {
                        throw new InvalidOperation("当前商品数小于删除商品数");
                    }
                    else if (orderList[i].Quantity == quantity)
                    {
                        orderList.Remove(orderList[i]);
                        TotalCost -= (goods.GoodPrice * quantity);
                    }
                    else
                    {
                        orderList[i].Quantity -= quantity;
                        TotalCost -= (goods.GoodPrice * quantity);
                    }
                }
            }
            if (flag == 0)//无匹配商品存在
            {
                throw new InvalidOperation("当前订单中无匹配商品");
            }
        }
        public override bool Equals(object order)
        {
            Order thisOrder = (Order)order;
            return thisOrder.OrderID == OrderID;
        }
        public override int GetHashCode()
        {
            return OrderID;
        }
        public override string ToString()
        {
            return string.Format($"订单号：{OrderID}   客户：{Icustomer.Name}  总价：{TotalCost}");
        }
    }

    public delegate bool Func(Order order);
    public class InvalidOperation : Exception
    {
        public string message { get; }
        public InvalidOperation(string message) { this.message = message; }

    }
    [Serializable()]
    public class OrderService
    {
        List<Order> orders = new List<Order>();

        List<Customer> customers = new List<Customer>();
        List<Goods> goods = new List<Goods>();

        public List<Order> Orders { get => orders; set => value = orders; }
        public List<Customer> Customers { get => customers; set => value = customers; }
        public List<Goods> Goods { get => goods; set => value = goods; }
        public OrderService()
        {
            customers.Add(new Customer("唐雪", "15102724450"));
            customers.Add(new Customer("袁琪", "13971694793"));
            customers.Add(new Customer("张弛", "18071387089"));

            goods.Add(new Goods("水性笔", 3.5));
            goods.Add(new Goods("书包", 70));
            goods.Add(new Goods("一盒白纸", 24));
            goods.Add(new Goods("记号笔3色", 12));
        }


        public void CreateOrder(int id, Customer client)
        {
            //try
            //{
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
            //}
            //catch (InvalidOperation E)
            //{
            //    Console.WriteLine("订单创建异常：" + E.message);
            //}

        }
        public void DeleteOrder(int id)
        {
            //try
            //{
            int flag = 0;
            for (int i = orders.Count() - 1; i >= 0; i--)
            {
                if (id == orders[i].OrderID)
                {
                    orders.Remove(orders[i]);
                    flag = 1;
                }
            }
            if (flag == 0)
                throw new InvalidOperation("不存在该订单！");

            //}catch(InvalidOperation e)
            //{
            //    Console.WriteLine("订单删除异常：" + e.message);
            //}
        }
        public void AddOrderItem(int id, Goods goods, int quantity)
        {
            //try
            //{
            int flag = 0;

            for (int i = orders.Count() - 1; i >= 0; i--)
            {
                if (id == orders[i].OrderID)
                {
                    flag = 1;
                    orders[i].AddItem(goods, quantity);

                }
            }
            if (flag == 0)
            {
                throw new InvalidOperation("不存在该订单！");
            }
            //}catch(InvalidOperation e)
            //{
            //    Console.WriteLine("商品添加异常：" + e.message);
            //}
        }
        public void DeleteOrderItem(int id, Goods goods, int quantity)
        {
            //try
            //{
            int flag = 0;

            for (int i = orders.Count() - 1; i >= 0; i--)
            {
                if (id == orders[i].OrderID)
                {
                    flag = 1;
                    orders[i].DeleteItem(goods, quantity);
                }
            }
            if (flag == 0)
            {
                throw new InvalidOperation("不存在该订单！");
            }
            //}
            //catch (InvalidOperation e)
            //{
            //    Console.WriteLine("商品删除异常：" + e.message);
            //}

        }
        public void OrderSort()
        {
            orders.Sort();
        }
        public List<Order> SearchOrders(Func func)
        {
            List<Order> iorder = new List<Order>();
            var query = from order in orders
                        where func(order)
                        select order;
            foreach (Order i in query) { iorder.Add(i); }
            return iorder;
        }
        public void Export()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream("Orders.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fs, Orders);
            }
            Console.WriteLine("\nSerialized as XML:");
            Console.WriteLine(File.ReadAllText("Orders.xml"));
        }
        public void Import()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream("Orders.xml", FileMode.Open))
            {
                List<Order> orders = (List<Order>)xmlSerializer.Deserialize(fs);
                foreach (Order x in orders) { Console.WriteLine(x.ToString()); }
            }
        }
        public override string ToString()
        {
            return "订单服务：";
        }
    }
}
