using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary1;

namespace Order_Service
{
    
    public partial class Form1 : Form
    {
        public static OrderService MyOrders = new OrderService();
        public Form1()
        {
            InitializeComponent();
            OrderSource.DataSource=MyOrders.Orders;
            OrderdataGridView.DataSource = OrderSource;
            GoodsSource.DataSource = MyOrders.Goods;
            OrderIDList.DataSource = OrderSource;
            OrderIDList.DisplayMember = "OrderID";
            CustomerList.DataSource = MyOrders.Customers;
            CustomerList.DisplayMember = "Name";
            MyOrders.CreateOrder(1, MyOrders.Customers[0]);
            MyOrders.CreateOrder(2, MyOrders.Customers[1]);
            MyOrders.AddOrderItem(1, MyOrders.Goods[1], 10);
            
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrderForm form2 = new OrderForm("new", MyOrders);
            if(form2.ShowDialog()==DialogResult.Cancel) form2.Dispose();
            if (form2.ShowDialog() == DialogResult.OK)
            {
                
            }

        }

        private void OrderdataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = OrderdataGridView.CurrentRow;
            int txt = int.Parse(row.Cells[1].Value.ToString());
            Order order=new Order();
            foreach (Order i in MyOrders.Orders) { if (i.OrderID == txt) order = i;break; }
            GooddataGridView.DataSource = order.OrderList;
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            string thisID = OrderIDList.SelectedValue.ToString();
            string thisCus = CustomerList.SelectedItem.ToString();
            

            List<Order> iorder ;
            if (thisID != null)
            {
                iorder = MyOrders.SearchOrders(x => x.OrderID == int.Parse(thisID));
                if (iorder == null) { MessageBox.Show("无相关信息"); }
                else OrderdataGridView.DataSource = iorder;
                return;
            }

            if (thisCus != null)
            {
                iorder = MyOrders.SearchOrders(x => x.Client == thisCus);
                if (iorder == null) { MessageBox.Show("无相关信息"); }
                else OrderdataGridView.DataSource = iorder;
            }

        }
    }
}
