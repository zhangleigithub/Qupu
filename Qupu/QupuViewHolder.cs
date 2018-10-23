using Android;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Qupu
{
    public class QupuViewHolder : RecyclerView.ViewHolder
    {
        public TextView Title { get; private set; }
        public TextView Type { get; private set; }
        public TextView Songwriter { get; private set; }
        public TextView Singer { get; private set; }
        public TextView Uploader { get; private set; }
        public TextView UploadDate { get; private set; }
        public TextView QupuCount { get; private set; }

        public QupuViewHolder(View itemView) : base(itemView)
        {
            this.Title = itemView.FindViewById<TextView>(Resource.Id.Title);
            this.Type = itemView.FindViewById<TextView>(Resource.Id.Type);
            this.Songwriter = itemView.FindViewById<TextView>(Resource.Id.Songwriter);
            this.Singer = itemView.FindViewById<TextView>(Resource.Id.Singer);
            this.Uploader = itemView.FindViewById<TextView>(Resource.Id.Uploader);
            this.UploadDate = itemView.FindViewById<TextView>(Resource.Id.UploadDate);
            this.QupuCount = itemView.FindViewById<TextView>(Resource.Id.QupuCount);
        }
    }
}