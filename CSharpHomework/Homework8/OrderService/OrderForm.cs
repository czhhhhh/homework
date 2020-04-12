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
    public partial class OrderForm : Form
    {
        private string Op;
        public OrderService orderForm;
        public OrderForm(string operation,OrderService service)
        {
            InitializeComponent();
            Op = operation;
            orderForm = service;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            comboBox3.DataSource = orderForm.Customers.Where(s => s.Name == comboBox2.SelectedText);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.DataSource = orderForm.Customers.Where(s => s.TelNumber == comboBox3.SelectedText);
        }
    }
}
