using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace App1
{
    public class CustomAdapter : BaseAdapter
    {
        List<Note> items;
        Activity context;

        public CustomAdapter(Activity context, List<Note> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.NotesListview, null);

            //position = (items.Count - 1) - position;

            view.FindViewById<TextView>(Resource.Id.title).Text = items[position].Title;
            view.FindViewById<TextView>(Resource.Id.date).Text = items[position].Date.ToLocalTime().ToString("HH:mm dd.MM.yyyy");
            view.FindViewById<TextView>(Resource.Id.content).Text = items[position].Content;
            return view;
        }
    }
}