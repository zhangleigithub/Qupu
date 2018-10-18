using HtmlAgilityPack;
using Summer.Qupu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summer.Qupu.Service
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
                    OpernModel model = new OpernModel();
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
    }
}
