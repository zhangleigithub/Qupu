using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData
{
    public class QupuModel
    {
        public string Title { get; set; }

        public string Type { get; set; }

        public string Songwriter { get; set; }

        public string Singer { get; set; }

        public string Uploader { get; set; }

        public string UploadDate { get; set; }

        public string PageLink { get; set; }

        public List<string> Qupus { get; private set; }

        public QupuModel()
        {
            this.Qupus = new List<string>();
        }
    }
}
