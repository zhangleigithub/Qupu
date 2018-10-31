using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;
using ImageViews.Photo;
using System;
using System.Collections.Generic;
using System.IO;
using Android.Content;
using System.Threading.Tasks;
using static Android.Widget.ImageView;
using Android.Views;

namespace Qupu
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class ViewQuquActivity : AppCompatActivity
    {
        private LinearLayout viewQupuLinearLayout;

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            List<Bitmap> result = new List<Bitmap>();

            IList<string> qupus = this.Intent.GetStringArrayListExtra("Qupus");

            foreach (var item in qupus)
            {
                try
                {
                    var stream = await GetFileStreamAsync(item);

                    result.Add(BitmapFactory.DecodeByteArray(stream, 0, stream.Length));
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
            }

            var viewPager = new HackyViewPager(this);
            SetContentView(viewPager);
            viewPager.Adapter = new SamplePagerAdapter(result);
        }

        public async Task<byte[]> GetFileStreamAsync(string url)
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            var fileStream = await client.GetByteArrayAsync(url);
            return fileStream;
        }
    }

    public class HackyViewPager : ViewPager
    {
        public HackyViewPager(Context context)
            : base(context)
        {
        }

        public HackyViewPager(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            try
            {
                return base.OnInterceptTouchEvent(ev);
            }
            catch (Java.Lang.IllegalArgumentException ex)
            {
                ex.PrintStackTrace();
                return false;
            }
        }
    }

    public class SamplePagerAdapter : PagerAdapter
    {
        private List<Bitmap> bitmaps;

        public SamplePagerAdapter(List<Bitmap> bitmaps)
        {
            this.bitmaps = new List<Bitmap>(bitmaps);
        }

        public override int Count
        {
            get
            {
                return this.bitmaps.Count;
            }
        }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            var photoView = new PhotoView(container.Context);
            photoView.SetImageBitmap(bitmaps[position]);

            container.AddView(photoView, ViewPager.LayoutParams.MatchParent, ViewPager.LayoutParams.MatchParent);

            return photoView;
        }

        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object objectValue)
        {
            container.RemoveView((View)objectValue);
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object objectValue)
        {
            return view == objectValue;
        }
    }
}