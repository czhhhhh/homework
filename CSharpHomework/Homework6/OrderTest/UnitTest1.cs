using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OrderWarehouse_XML
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidParameter))]
        public void Test_CreateOrder_1()
        {
            //arrange
            //null is invalid information for customers
            List<string> customers = null;
            OrderService MyOrder = new OrderService();

            //throw Exception:InvalidParameter
            MyOrder.CreateOrder(0, customers[0]);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperation))]
        public void Test_CreateOrder_2()
        {
            //arrange
            //orderID should not be same to each other
            OrderService MyOrder = new OrderService();
            MyOrder.CreateOrder(0, "won");

            //throw Exception:InvalidOperation
            MyOrder.CreateOrder(0, "two");
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidParameter))]
        public void Test_DeleteOrder()
        {
            //arrange
            OrderService MyOrder = new OrderService();
            MyOrder.CreateOrder(0, "won");
            //throw Exception:InvalidParameter
            MyOrder.DeleteOrder(1);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidParameter))]
        public void Test_AddOrderItem()
        {
            OrderService MyOrder = new OrderService();
            MyOrder.CreateOrder(0, "won");
            MyOrder.AddOrderItem(0, "pen", -2, 9);
        }

        [TestMethod()]
        public void Test_SearchOrder()
        {
            OrderService MyOrder = new OrderService();
            MyOrder.CreateOrder(0, "won");
            MyOrder.CreateOrder(1, "won");
            MyOrder.CreateOrder(2, "two");
            var query = MyOrder.SearchOrders(X => (X.OrderID == 1 || X.OrderID == 0));
            Assert.AreEqual(query.ToString(), MyOrder.SearchOrders(X => X.OwnerName == "won").ToString());
        }


    }
}
