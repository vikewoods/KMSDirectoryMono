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
using Android.Graphics;

namespace CustomRowView {
public class HomeScreenAdapter : BaseAdapter<Employee> {
    List<Employee> items;
    Activity context;
    public HomeScreenAdapter(Activity context, List<Employee> items)
        : base()
    {
        this.context = context;
        this.items = items;
    }
    public override long GetItemId(int position)
    {
        return position;
    }
    public override Employee this[int position]
    {
        get { return items[position]; }
    }
    public override int Count
    {
        get { return items.Count; }
    }
    public override View GetView(int position, View convertView, ViewGroup parent)
    {
        var item = items[position];

        View view = convertView;
        if (view == null) // no view to re-use, create new
            view = context.LayoutInflater.Inflate(Resource.Layout.CustomView, null);
        view.FindViewById<TextView>(Resource.Id.Text1).Text = item.firstName + " " + item.lastName;
        view.FindViewById<TextView>(Resource.Id.Text2).Text = item.title;
        //view.FindViewById<ImageView>(Resource.Id.Image).SetImageResource(item.id);
		view.FindViewById<ImageView>(Resource.Id.Image).SetImageBitmap(GetAvatar(item));

        return view;
    }

		Bitmap GetAvatar (Employee employee)
		{
			// Default avatar
			if (employee.avatarImage == null || employee.avatarImage == "")
				return null;
			
			// Special avatar
			byte[] byteImg = System.Convert.FromBase64String (employee.avatarImage);
			Bitmap b = BitmapFactory.DecodeByteArray(byteImg, 0, byteImg.Length);
			return b;
		}
}
}