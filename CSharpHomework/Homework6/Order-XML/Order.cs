using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWarehouse_XML
{
    public class Order:IComparable
    {
        List<OrderItem> orderList;
        public int CompareTo(object obj)
        {
            if(!(obj is Order))
            {
                throw new System.ArgumentException();
            }
            Order order2 = (Order)obj;
            return this.orderID.CompareTo(order2.orderID);
        }
        private int orderID;
        private string client;
        private double totalCost;
        public int OrderID { get => orderID; set => value = orderID; }
        public string OwnerName { get => client; set => value = client; }
        public double TotalCost{ get => totalCost; set => value = totalCost; }
        public Order(int ID,string client)
        {
            orderID = ID;
            this.client = client;
            totalCost = 0;
            orderList = new List<OrderItem>();
        }
        //无参构造函数，让xml能够创建对象
        public Order()
        {
            OrderID = 0;
            TotalCost = 0;
            OwnerName = "nobody";
        }
        public void AddItem(string item,int quantity,double price)
        {
            int flag = 0;
            OrderItem NewItem = new OrderItem(item, price, quantity);
            for(int i = orderList.Count() - 1; i >= 0; i--)
            {
                if (orderList[i].Equals(NewItem))
                {//如果订单明细中已经有同种商品则修改
                    orderList[i].Quantity += quantity;//增加商品数量
                    flag = 1;
                }
            }
            if(flag==0)
            {
                orderList.Add(NewItem);
            }
            totalCost += (price * quantity);
        }
        public void DeleteItem(string item,int quantity,double price)
        {
            int flag = 0;
            OrderItem NewItem = new OrderItem(item, price, quantity);
            for (int i = orderList.Count() - 1; i >= 0; i--)
            {
                if (orderList[i].Equals(NewItem))
                {
                    flag = 1;
                    if (orderList[i].Quantity < quantity)
                    {
                        throw new InvalidOperation("当前商品数小于删除商品数");
                    }
                    else if(orderList[i].Quantity == quantity)
                    {
                        orderList.Remove(orderList[i]);
                        totalCost -= (price * quantity);
                    }
                    else
                    {
                        orderList[i].Quantity -= quantity;
                        totalCost -= (price * quantity);
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
            return thisOrder.orderID == orderID;
        }
        public override int GetHashCode()
        {
            return orderID;
        }
        public override string ToString()
        {
            return string.Format($"订单号：{orderID}   客户：{client}  总价：{totalCost}");
        }
    }
}
