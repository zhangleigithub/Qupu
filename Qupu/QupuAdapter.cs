using Android;
using Android.Support.V7.Widget;
using Android.Views;

namespace Qupu
{
    public class QupuAdapter : RecyclerView.Adapter
    {
        private QupuModel[] qupus;

        public event EventHandler<int> ItemClick;

        public QupuAdapter(QupuModel[] qupus)
        {
            this.qupus = qupus;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.cardView_qupu, parent, false);
            QupuViewHolder vh = new QupuViewHolder(itemView,this.OnClick);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            QupuViewHolder vh = holder as QupuViewHolder;
            vh.Title.Text = qupus[position].Title;
            vh.Type.Text = qupus[position].Type;
            vh.Songwriter.Text = qupus[position].Songwriter;
            vh.Singer.Text = qupus[position].Singer;
            vh.Uploader.Text = qupus[position].Uploader;
            vh.UploadDate.Text = qupus[position].UploadDate;
            vh.QupuCount.Text = qupus[position].Qupus.Count.ToString();
        }

        public override int ItemCount
        {
            get { return qupus.Length; }
        }

        private void OnClick(int position)
        {
            if (this.ItemClick != null)
            {
                this.ItemClick (this, position);
            }
        }
    }
}