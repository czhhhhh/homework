using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformCalculate
{
    public partial class 计算器 : Form
    {
        public 计算器()
        {
            InitializeComponent();
          
        }
        
        private void 计算器_Load(object sender, EventArgs e)
        {
            List<MyChoice> list = new List< MyChoice > ();
            list.Add(new MyChoice('+'));
            list.Add(new MyChoice('-'));
            list.Add(new MyChoice('*'));
            list.Add(new MyChoice('/'));
            //绑定list为comboBox的信息源
            comboBox1.DataSource = list;
            //在comboBox中显示Choice属性
            comboBox1.DisplayMember = "Choice";

        }

       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyChoice item = comboBox1.SelectedItem as MyChoice;
        }

        


        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            double answer = 0;
            double first, second;
            first = double.Parse(textBox1.Text);
            second = double.Parse(textBox3.Text);
            char selected = char.Parse(comboBox1.Text);
            switch (selected)
            {
                case '+':
                    answer = first + second;
                    break;
                case '-':
                    answer = first - second;
                    break;
                case '*':
                    answer = first * second;
                    break;
                case '/':
                    answer = first / second;
                    break;
                default:
                    break;
            }
            textBox4.Text=answer.ToString();
        }
    }
}
