using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData
{
    public class WebResourceAnalysisService
    {
        public QupuModel[] Parse(string baseUrl, string resource)
        {
            var web = new HtmlWeb();
            var doc = web.Load(string.Format("{0}{1}", baseUrl, resource));
            var table = doc.DocumentNode.SelectSingleNode("//table[@class='opern_list']");
            var tr = table.SelectNodes("tr");

            List<QupuModel> result = new List<QupuModel>();

            foreach (var item in tr)
            {
                var trTds = item.SelectNodes("td");

                if (trTds.Count == 7)
                {
                    QupuModel model = new QupuModel();
                    model.Title = trTds[1].InnerText;
                    model.Type = trTds[2].InnerText;
                    model.Songwriter = trTds[3].InnerText;
                    model.Singer = trTds[4].InnerText;
                    model.Uploader = trTds[5].InnerText;
                    model.UploadDate = trTds[6].InnerText;
                    model.PageLink = trTds[1].SelectSingleNode("a").Attributes["href"].Value;

                    result.Add(model);
                }
            }

            foreach (var item in result)
            {
                doc = web.Load(string.Format("{0}{1}", baseUrl, item.PageLink));
                var imageList = doc.DocumentNode.SelectSingleNode("//div[@class='imageList']");

                var hrefs = imageList.SelectNodes("a");

                foreach (var itemC in hrefs)
                {
                    item.Qupus.Add(string.Format("{0}{1}", baseUrl, itemC.Attributes["href"].Value));
                }
            }

            return result.ToArray();
        }

        public QupuModel[] Parse2(string baseUrl, string resource)
        {
            List<QupuModel> result = new List<QupuModel>();

            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(string.Format("{0}{1}", baseUrl, resource));

            var lst = driver.FindElementByClassName("opern_list");
            var trs = lst.FindElements(By.TagName("tr"));

            foreach (var item in trs)
            {
                var tds = item.FindElements(By.TagName("td"));

                if (tds.Count == 7)
                {
                    QupuModel model = new QupuModel();
                    model.Title = tds[1].Text;
                    model.Type = tds[2].Text;
                    model.Songwriter = tds[3].Text;
                    model.Singer = tds[4].Text;
                    model.Uploader = tds[5].Text;
                    model.UploadDate = tds[6].Text;
                    model.PageLink = tds[1].FindElement(By.TagName("a")).GetAttribute("href");

                    result.Add(model);
                }
            }

            foreach (var item in result)
            {
                driver.Navigate().GoToUrl(item.PageLink);
                var imgLst = driver.FindElementByClassName("imageList");
                var imgs = imgLst.FindElements(By.TagName("a"));

                foreach (var itemC in imgs)
                {
                    item.Qupus.Add(itemC.GetAttribute("href"));
                }
            }

            return result.ToArray();
        }
    }
}
