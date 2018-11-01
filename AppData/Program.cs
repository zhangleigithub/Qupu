using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData
{
    class Program
    {
        static void Main(string[] args)
        {

            WebResourceAnalysisService web = new WebResourceAnalysisService();
            //QupuModel[] qupu = web.Parse("http://www.qupu123.com/", "qiyue/dianziqin");

            QupuModel[] qupu = web.Parse2("http://www.qupu123.com/", "qiyue/dianziqin");

            string str = Newtonsoft.Json.JsonConvert.SerializeObject(qupu);

            Console.Read();
        }
    }
}
