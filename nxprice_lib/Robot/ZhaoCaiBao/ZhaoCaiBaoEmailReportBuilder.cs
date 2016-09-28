using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nxprice_data;

namespace nxprice_lib.Robot.ZhaoCaiBao
{
    public class ZhaoCaiBaoEmailReportBuilder
    {


        public EmailMsg Build(ZCBRecord first, ZCBRecord second)
        {
            string sender = string.Format("@{0:F2}%  |  @{1:F2}%",
                                        first.BuyIndex,
                                        second.BuyIndex
                                       );

            string subject = string.Format("{0}天 @{1}%  || {2}天 @{3}%",
                                            first.DayLeft,
                                            first.YearRate,
                                            second.DayLeft,
                                            second.YearRate
                                          );

            string url1 = "https://zhaocaibao.alipay.com/pf/purchase.htm?productId=" + first.ProductionID + Environment.NewLine;
            string part1 = string.Format("\n位置: {0} 页 {1} 项 \n" +
                                         "利率: {2:F2}%             折算利率: {3:F2}% \n" + 
                                         "天数: {4}  \n" + 
                                         "成交数: {5} \n" + 
                                         "最小额度: {6} \n" +
                                         "链接: {7}   \n" , 
                         first.PageIndex,
                         first.ItemIndex,
                         first.YearRate,
                         first.BuyIndex,
                         first.DayLeft,
                         first.DealCount,
                         first.MinMount,
                         url1
                );


            string url2 = "https://zhaocaibao.alipay.com/pf/purchase.htm?productId=" + second.ProductionID + Environment.NewLine;
            string part2 = string.Format("\n位置: {0} 页 {1} 项 \n" +
                                         "利率: {2:F2}%             折算利率: {3:F2}% \n" + 
                                         "天数: {4}  \n" +
                                         "成交数: {5}  \n" +
                                         "最小额度: {6} \n" +
                                         "链接: {7}   \n", 
                                             second.PageIndex,
                                             second.ItemIndex,
                                             second.YearRate,
                                             second.BuyIndex,
                                             second.DayLeft,
                                             second.DealCount,
                                             second.MinMount,
                                             url2
                                    );

            string body = "\n" + sender + "\n"+ subject + "\n" + part1 + part2;


            return new EmailMsg { Sender = sender, Subject = subject, Body = body };
        }
    }


    public class EmailMsg
    {
        public string Sender { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
