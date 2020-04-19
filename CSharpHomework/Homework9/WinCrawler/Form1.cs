using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinCrawler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            crawler.PageDownloaded += Crawler_PageDownloaded;
            textBox1.Text = "http://www.cnblogs.com/dstang2000/";
        }

            SimpleCrawler crawler = new SimpleCrawler();


        private void button1_Click(object sender, EventArgs e)
        {
            crawler.Clear();
            crawler.AddStartUrl(textBox1.Text);
            if (comboBox1.SelectedItem != null)
                crawler.UrlNum = Int32.Parse(comboBox1.SelectedItem.ToString());
            listBox1.Items.Clear();
            new Thread(crawler.Crawl).Start();
        }

        private void Crawler_PageDownloaded(string obj, DownloadEventArgs args)
        {
            if (listBox1.InvokeRequired)
            {
                Action<String> action = AddUrl;
                this.Invoke(action, new object[] { obj });
            }
            else
            {
                this.AddUrl(obj);
            }
        }

        private void AddUrl(string url)
        {
            listBox1.Items.Add(url);
        }
    }
}
