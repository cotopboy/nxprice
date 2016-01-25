using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace nxprice_lib.Robot
{
    public class PageLoader
    {
        public string GetPageHtml(string url, bool isUseProxy,string proxyUserName,string proxyPassowrd, Encoding encoding = null)
        {

            bool isNeedRetry = true;
            int tryCount = 0;

            while (isNeedRetry && tryCount <= 3)
            {
                try
                {
                    WebClient MyWebClient = new WebClient();

                    MyWebClient.Credentials = CredentialCache.DefaultCredentials;

                    if (isUseProxy)
                    {
                        WebProxy myProxy = new WebProxy();
                        Uri newUri = new Uri("http://node-fr.vnet.link:210");
                        myProxy.Address = newUri;
                        myProxy.Credentials = new NetworkCredential(proxyUserName, proxyPassowrd);
                        MyWebClient.Proxy = myProxy;

                    }

                    Byte[] pageData = MyWebClient.DownloadData(url);

                    if (encoding == null) encoding = Encoding.UTF8;

                    string pageHtml = encoding.GetString(pageData);

                    return pageHtml;
                }
                catch
                {
                    tryCount++;
                }
            }

            return "";
            
        }

    }
}
