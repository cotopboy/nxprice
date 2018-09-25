using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities.DataTypes.ExtensionMethods;
using Utilities.IO.ExtensionMethods;
using Utilities.Reflection.ExtensionMethods;
using Utilities.Math.ExtensionMethods;

namespace nxprice_lib.Robot.DBStation.Ps
{
    public class PsInfoExtractor
    {
        internal List<PsRecord> GetPsRecords(byte[] fileBytes)
        {
            var textContent = Encoding.UTF8.GetString(fileBytes);

            var tableBody = textContent.SubStringBetween("<tbody>", "</tbody>");

            tableBody = tableBody.Replace("<tr>", "");

            var rowsHtml = tableBody.Split(new string[] { "</tr>" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            List<PsRecord> retList = new List<PsRecord>();

            foreach (var item in rowsHtml)
            {
                if (item.Trim().Length > 0)
                {
                    retList.Add(Parse(item));
                }
            }

            return retList;
        }

        private PsRecord Parse(string htmlRow)
        {
            PsRecord ret = new PsRecord();
            var content = htmlRow.Trim();
            content = content.Replace("<td>", "");

            var columns = content.Split(new string[] { "</td>" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            ret.OrderId = GetOrderId(columns[1]);
            ret.DHLTracingNo = DHLTracingNo(columns[4]);
            ret.RecipientName = GetRecipientName(columns[6]);
            ret.CnAddress = GetCnAddress(columns[8]);
            ret.EMSTracingNo = GetEMSTracingNo(columns[9]);

            ret.Remark = GetRemark(columns[10]);

            var remarkLines = ret.Remark.ToLines();

            ret.AgentName = GetAgentName(remarkLines);

            ret.Email = GetEmail(remarkLines);
            return ret;

        }

        private string GetAgentName(string[] remarkLines)
        {
            if (remarkLines.Length >= 3)
            {
                return remarkLines[2].Trim();
            }
            else
            {
                return "";
            }
        }

        private string GetEmail(string[] remarkLines)
        {
            if (remarkLines.Length < 4) return "";

            var input = remarkLines[3];

            if (input.Contains("@"))
            {
                return input.Trim();
            }
            else
            {
                return input + "@qq.com";
            }
        }

        private string GetRemark(string input)
        {
            return input.SubStringAfter("<span>", 1)
                        .SubStringBefore("</div>")
                        .Replace("<br/>", "")
                        .Trim();
        }

        private string GetEMSTracingNo(string input)
        {
            return input.Trim().SubStringAfter("<br/>", 1);
        }

        private string GetCnAddress(string input)
        {
            return input.Trim();
        }

        private string GetRecipientName(string input)
        {
            return input.Trim();
        }

        private string DHLTracingNo(string input)
        {
            return input.TrimStart().SubStringBefore("<br />");
        }

        private string GetOrderId(string input)
        {
            return input.SubStringAfter(">", 1).SubStringBefore("<").Trim();
        }
    }
}
