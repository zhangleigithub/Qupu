using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Qupu
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class ViewQuquActivity : AppCompatActivity
    {
        private LinearLayout viewQupuLinearLayout;

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.view_qupu);
            this.viewQupuLinearLayout = FindViewById<LinearLayout>(Resource.Id.linearLayout_ViewQupu);
            string[] qupus = this.GetIntent().GetStringArray("Qupus");

            foreach (var item in qupus)
            {
                try
                {
                    var stream = await GetFileStreamAsync(item);

                    ImageView img = new ImageView(this);
                    img.SetImageBitmap(BitmapFactory.DecodeStream(stream));

                    this.viewQupuLinearLayout.AddView(img);
                }
                catch (Exception ex)
                {
                    TextView msg = new TextView(this);
                    msg.Text = ex.Message;

                    this.viewQupuLinearLayout.AddView(msg);
                }
            }
        }

        public async Task<Stream> GetFileStreamAsync(string url)
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            var fileStream = await client.GetStreamAsync(url);
            return fileStream;
        }
    }
}