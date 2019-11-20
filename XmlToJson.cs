using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Schema;
using Newtonsoft.Json;
using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace XmlReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string URLString = @"D:\log\Z_BAPI20191120065500Aoutput.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(URLString);
            // 讀取HEADER節點
            XmlNode responseNode = xmlDoc.SelectSingleNode("/HEADER");
            // 透過Json.NET將XmlNode轉為Json格式字串
            string jsonText = JsonConvert.SerializeXmlNode(responseNode);
            // 透過Json.NET反序列化為物件
            Response responseObj = JsonConvert.DeserializeObject<Response>(jsonText);
            Header Hr = responseObj.HEADER;
            Family Fr = Hr.Family;
            DataTable dt = new DataTable();
            dt.Columns.Add("Member");
            foreach (string ff in Fr.Member)
            {
                DataRow row = dt.NewRow();
                row["Member"] = ff;
                dt.Rows.Add(row);
            }
            System.IO.FileInfo filePath = new System.IO.FileInfo("D:\\log\\test.xlsx");
            using (ExcelPackage pck = new ExcelPackage(filePath))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Accounts");
                ws.Cells["A1"].LoadFromDataTable(dt, true);
                pck.Save();
                //string json = JsonConvert.SerializeObject(responseObj);
            }
        }
        public class Response
        {
            public Header HEADER { get; set; }
        }

        public class Header
        {
            public string UserID { get; set; }
            public string UserName { get; set; }
            public string UserDesc { get; set; }
            public Family Family { get; set; }
        }

        public class Family
        {
            public List<string> Member { get; set; }
        }
    }
}
