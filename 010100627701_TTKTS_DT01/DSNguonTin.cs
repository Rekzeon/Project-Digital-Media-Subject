using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace _010100627701_TTKTS_DT01
{
    public class DSNguonTin
    {
        List<TIN> a = new List<TIN>();

        public List<TIN> A { get => a; set => a = value; }
    
        public void nhap (string file)
        {
            XmlDocument read = new XmlDocument();
            read.Load(file);
            XmlNodeList nodeList = read.SelectNodes("/TINS/TIN");
            foreach(XmlNode node in nodeList)
            {
                TIN temp = new TIN();
                temp.STenTin = node["Ten"].InnerText;
                temp.DXacSuatTin = double.Parse(node["XacXuat"].InnerText);
                temp.IHeCoSo = int.Parse(node["HeSo"].InnerText);
                a.Add(temp);
            }
        }

        public void themTinVaoNguon(string file, Dictionary<string,string> tinMoi)
        {
            var write = XDocument.Load(file);
            write.Element("TINS").Add(new XElement("TIN", new XAttribute("ID", tinMoi["Name"]),
                                                          new XElement("Ten", tinMoi["Name"]),
                                                          new XElement("XacXuat", tinMoi["XacXuat"]),
                                                          new XElement("HeSo", tinMoi["HeSo"])
                                                          ));
            write.Save(file);
        }

        public bool xoaTinTuNguon(string file, string tentin)
        {
            var del = XDocument.Load(file);
            var existElement = del.Element("TINS").Elements("TIN").Where(element => element.Attribute("ID").Value == tentin).FirstOrDefault();
            if (existElement != null)
            {
                existElement.Remove();
                del.Save(file);
                return true;
            }
            else
                return false;
        }

        public void tinhLuongTinRiengTungTIN()
        {
            for(int i=0; i<a.Count;i++)
                a[i].DLuongTinRieng = Math.Log(1/a[i].DXacSuatTin, a[i].IHeCoSo);         
        }

        public double tinhLuongTinTBCuaNguon()
        {
            double dTong = 0;
            foreach (TIN tin in a)
                dTong += tin.DXacSuatTin * tin.DLuongTinRieng;
            return dTong;
        }
        
        
        public void sapXepTTTin()
        {
            a.Sort();
            a.Reverse();
        }
    }
}
