using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWarehouse
{
    class OrderItem
    {
        private string itemName;
        private double price;
        private int quantity;
        //商品名称和价格不可修改
        public string ItemName { get => itemName; set => value = itemName; }
        public double Price { get => price; set => value = price; }
        public int Quantity { get => quantity; set => value = quantity; }
        public double TotalCost { get => price * quantity; }//本商品的总价，区别于订单总价
        public OrderItem(string itemName,double price,int quantity)
        {
            this.itemName = itemName;
            this.price = price;
            this.quantity = quantity;
        }
        public override bool Equals(object orderItem)
        {
            OrderItem item = (OrderItem)orderItem;
            return item.itemName == itemName && item.price == price;//判断是否为价格和名字相同的商品，数量可以修改，所以不作为判断标准
        }
        public override int GetHashCode()
        {
            int name = int.Parse(itemName);
            return name + quantity;
        }
        public override string ToString()
        {
            return string.Format($"名称：{itemName}  总数：{quantity}  单价：{price}  总价：{TotalCost}");
        }
    }
}
