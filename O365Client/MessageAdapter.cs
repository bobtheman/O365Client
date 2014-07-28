using System;
using Android.Widget;
using Android.App;
using Microsoft.Office365.Exchange;
using System.Collections.Generic;
using System.Linq;
using Android.Views;


namespace O365Client
{
	public class HomeScreenAdapter : BaseAdapter<IMessage> {
		List<IMessage> items;
		Activity context;
		public HomeScreenAdapter(Activity context, IEnumerable<IMessage> items) : base() {
			this.context = context;
			this.items = items.ToList();
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override IMessage this[int position] {  
			get { return items[position]; }
		}
		public override int Count {
			get { return items.Count; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items[position].Subject;
			view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = items[position].From.Address;
			view.Tag = items [position].Id;
			return view;
		}
	}

}

