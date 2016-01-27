using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Utilities.IO;
using mshtml;
using Microsoft.Practices.Unity;
using nxprice_lib;
using nxprice_data;

namespace nxprice_ui
{
    public partial class Form1 : Form
    {
        FileSystemWatcher fileWatcher;        
        private ExchangeDataContainer container;

        public Form1()
        {
            InitializeComponent();

            fileWatcher = new FileSystemWatcher(DirectoryHelper.CombineWithCurrentExeDir("ZcbListExchange"));
            fileWatcher.Created += new FileSystemEventHandler(FileWatcherOnFileCreated);
            fileWatcher.EnableRaisingEvents = true;

            this.webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.AbsoluteUri.Contains("zhaocaibao.alipay.com/pf/purchase.htm?productId="))
            {
                HtmlElement head = webBrowser1.Document.GetElementsByTagName("head")[0];
                HtmlElement scriptEl = webBrowser1.Document.CreateElement("script");
                IHTMLScriptElement element = (IHTMLScriptElement)scriptEl.DomElement;
                element.text = @"function GetIt() 
                {
                    document.getElementById('J_pfPurchaseAmt').value = '20000';                    
                }";
                head.AppendChild(scriptEl);
                webBrowser1.Document.InvokeScript("GetIt");
            
            }
        }

        private void FileWatcherOnFileCreated(object sender, FileSystemEventArgs e)
        {

            FileDbEngine<ExchangeDataContainer> exContainerEngine = new FileDbEngine<ExchangeDataContainer>(e.FullPath);
            this.container = exContainerEngine.LoadFileDB();

            this.Invoke((MethodInvoker)(() => 
            {
                if (this.cbIgnoreRefresh.Checked) return;

                this.dataGridView1.DataSource = container.FisrtList;
                this.dataGridView2.DataSource = container.SecondList;

                this.tbProductId.Text = container.FisrtList.First().ProductionID;
                this.webBrowser1.Navigate("https://zhaocaibao.alipay.com/pf/purchase.htm?productId=" + this.tbProductId.Text);

                try
                {
                    File.Delete(e.FullPath);
                }
                catch { }

            }));

           
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = DirectoryHelper.CombineWithCurrentExeDir("ZcbListExchange\\list.xml");
                FileDbEngine<ExchangeDataContainer> exContainerEngine = new FileDbEngine<ExchangeDataContainer>(filePath);
                this.container = exContainerEngine.LoadFileDB();

                this.dataGridView1.DataSource = container.FisrtList;
                this.dataGridView2.DataSource = container.SecondList;

                this.tbProductId.Text = container.FisrtList.First().ProductionID;
                this.webBrowser1.Navigate("https://zhaocaibao.alipay.com/pf/purchase.htm?productId=" + this.tbProductId.Text);
            }
            catch { }

            new Action(() =>
            {
                UnityContainer nx = new UnityContainer();

                var mgr = nx.Resolve<NxPriceMgr>();

                mgr.StartOnce();

            }).BeginInvoke(null, null);


        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            DataGridView dg = sender as DataGridView;

            List<ZCBRecord> list = null;
            if ((string)dg.Tag == "1")
            {
                list = this.container.FisrtList;
            }
            else
            {
                list = this.container.SecondList;
            }

            var selectedItem = list[index];
            this.tbProductId.Text = selectedItem.ProductionID;
            this.webBrowser1.Navigate("https://zhaocaibao.alipay.com/pf/purchase.htm?productId=" + this.tbProductId.Text);

        }



      
    }
}
