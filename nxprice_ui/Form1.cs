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
        private List<ZCBRecord> list;

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
            FileDbEngine<List<ZCBRecord>> db = new FileDbEngine<List<ZCBRecord>>(e.FullPath);
            list = db.LoadFileDB();

            this.Invoke((MethodInvoker)(() => 
            {


                this.dataGridView1.DataSource = list;
                
                this.tbProductId.Text = list.First().ProductionID;
                this.webBrowser1.Navigate("https://zhaocaibao.alipay.com/pf/purchase.htm?productId=" + this.tbProductId.Text);
            }));

            try
            {
                File.Delete(e.FullPath);

            }catch{}
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            new Action(() =>
            {
                UnityContainer nx = new UnityContainer();

                var mgr = nx.Resolve<NxPriceMgr>();

                mgr.StartOnce();

            }).BeginInvoke(null, null);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            var selectedItem = this.list[index];
            this.tbProductId.Text = selectedItem.ProductionID;
            this.webBrowser1.Navigate("https://zhaocaibao.alipay.com/pf/purchase.htm?productId=" + this.tbProductId.Text);

        }

      
    }
}
