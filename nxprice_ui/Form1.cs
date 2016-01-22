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

namespace nxprice_ui
{
    public partial class Form1 : Form
    {
        FileSystemWatcher fileWatcher;

        public Form1()
        {
            InitializeComponent();

            fileWatcher = new FileSystemWatcher(DirectoryHelper.CombineWithCurrentExeDir("ZcbExchange"));
            fileWatcher.Created += new FileSystemEventHandler(FileWatcherOnFileCreated);
            fileWatcher.EnableRaisingEvents = true;

            this.webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
        }

        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
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
            string productId = e.Name;

            this.Invoke((MethodInvoker)(() => 
            {
                this.tbProductId.Text = productId;
                this.webBrowser1.Navigate("https://zhaocaibao.alipay.com/pf/purchase.htm?productId=" + productId);
            }));

            try
            {
                File.Delete(e.FullPath);

            }catch{}
        }

        private void btnGoBuy_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate("https://zhaocaibao.alipay.com/pf/purchase.htm?productId=" + this.tbProductId.Text);
        }
    }
}
