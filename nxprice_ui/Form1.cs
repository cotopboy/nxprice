﻿using System;
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
using Utilities.IO.Logging;

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
            if (e.FullPath.Contains(".temp")) return;
                
            string temp = e.FullPath + ".temp";

            try
            {
                if(File.Exists(temp)) File.Delete(temp);
                File.Copy(e.FullPath, temp);
            }catch{}

            try
            {
                FileDbEngine<ExchangeDataContainer> exContainerEngine = new FileDbEngine<ExchangeDataContainer>(temp);
                this.container = exContainerEngine.LoadFileDB();
            }
            catch { MessageBox.Show("load failed."); }

            this.Invoke((MethodInvoker)(() => 
            {
                if (this.cbIgnoreRefresh.Checked) return;
                
                try
                {
                    this.dataGridView1.DataSource = container.FisrtList;
                    this.dataGridView2.DataSource = container.SecondList;

                    this.tbProductId.Text = container.FisrtList.First().ProductionID;
                    this.webBrowser1.Navigate("https://zhaocaibao.alipay.com/pf/purchase.htm?productId=" + this.tbProductId.Text);

                    
                    File.Delete(e.FullPath);
                    File.Delete(temp);
                }
                catch { }

            }));

           
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            ZCBRecord.HalfYearRate = this.numericUpDown1.Value;

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

            if (index <= 0) return;

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
            string url = "https://zhaocaibao.alipay.com/pf/purchase.htm?productId=" + this.tbProductId.Text;
            this.webBrowser1.Navigate(url);
            System.Diagnostics.Process.Start(url);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = DirectoryHelper.CombineWithCurrentExeDir("ZcbListExchange\\list.xml");
                FileDbEngine<ExchangeDataContainer> exContainerEngine = new FileDbEngine<ExchangeDataContainer>(filePath);
                this.container = exContainerEngine.LoadFileDB();

                this.dataGridView1.DataSource = container.FisrtList;
                this.dataGridView2.DataSource = container.SecondList;

                var first = container.FisrtList.FirstOrDefault();

                if (first != null)
                {
                    this.tbProductId.Text = container.FisrtList.First().ProductionID;
                    this.webBrowser1.Navigate("https://zhaocaibao.alipay.com/pf/purchase.htm?productId=" + this.tbProductId.Text);
                }
            }
            catch { }
        }

        private void btnOpenByDefaultBrowser_Click(object sender, EventArgs e)
        {
            string url = "https://zhaocaibao.alipay.com/pf/purchase.htm?productId=" + this.tbProductId.Text;
            System.Diagnostics.Process.Start(url);
        }

        private void cbConsole_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbConsole.Checked)
            {
                ConsoleManager.AllocConsole();
            }
            else
            {
                ConsoleManager.FreeConsole();
            }
        }



      
    }
}
