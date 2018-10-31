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
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://www.qupu123.com/qiyue/dianziqin");

            var vv = driver.FindElementByClassName("opern_list");

            WebResourceAnalysisService web = new WebResourceAnalysisService();
            QupuModel[] qupu = web.Parse("http://www.qupu123.com/", "qiyue/dianziqin");

            string str = Newtonsoft.Json.JsonConvert.SerializeObject(qupu);
  
            Console.Read();
        }
    }
}
