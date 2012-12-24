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
    public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var item = items [position];

			View view = convertView;
			if (view == null) // no view to re-use, create new
				view = context.LayoutInflater.Inflate (Resource.Layout.CustomView, null);
			view.FindViewById<TextView> (Resource.Id.Text1).Text = item.firstName + " " + item.lastName;
			view.FindViewById<TextView> (Resource.Id.Text2).Text = item.title;
			if (item.avatarImage == null || item.avatarImage == "") {
				view.FindViewById<ImageView>(Resource.Id.Image).SetImageResource(Resource.Drawable.dafault_avatar);
			} else {
				view.FindViewById<ImageView> (Resource.Id.Image).SetImageBitmap (GetAvatar (item));
			}
			//view.FindViewById<ImageView>(Resource.Id.Image).SetImageResource(item.id);

			ImageView quickCall = view.FindViewById<ImageView> (Resource.Id.quick_call);
			ImageView quickSms = view.FindViewById<ImageView> (Resource.Id.quick_sms);
			ImageView quickMail = view.FindViewById<ImageView> (Resource.Id.quick_mail);

			if (item.mobiPhone.Length == 0 && item.homePhone.Length == 0 && item.workPhone.Length == 0) {
				quickCall.Visibility = Android.Views.ViewStates.Gone;
				quickSms.Visibility = Android.Views.ViewStates.Gone;
			} else {
				quickCall.Visibility = Android.Views.ViewStates.Visible;
				quickSms.Visibility = Android.Views.ViewStates.Visible;
			}
			quickCall.Click += (sender, e) => {  
				//Android.Widget.Toast.MakeText(this.context, item.firstName, Android.Widget.ToastLength.Short).Show();
				var phoneNum = "";
				if (item.mobiPhone != null && item.mobiPhone.Length > 0) {
					phoneNum = item.mobiPhone;
				} else if (item.homePhone != null && item.homePhone.Length > 0) {
					phoneNum = item.homePhone;
				} else if (item.workPhone != null && item.workPhone.Length > 0) {
					phoneNum = item.workPhone;
				} else {
					Android.Widget.Toast.MakeText (this.context, "No data!", Android.Widget.ToastLength.Short).Show ();
					return;
				}

				var uri = Android.Net.Uri.Parse ("tel:" + phoneNum);
				var intent = new Intent (Intent.ActionView, uri); 
				this.context.StartActivity (intent);  
			};

			quickSms.Click += (sender, e) => {  
				//Android.Widget.Toast.MakeText(this.context, item.firstName, Android.Widget.ToastLength.Short).Show();
				var phoneNum = "";
				if (item.mobiPhone != null && item.mobiPhone.Length > 0) {
					phoneNum = item.mobiPhone;
				} else if (item.homePhone != null && item.homePhone.Length > 0) {
					phoneNum = item.homePhone;
				} else if (item.workPhone != null && item.workPhone.Length > 0) {
					phoneNum = item.workPhone;
				} else {
					Android.Widget.Toast.MakeText (this.context, "No data!", Android.Widget.ToastLength.Short).Show ();
					return;
				}
				  
				var smsUri = Android.Net.Uri.Parse ("smsto:" + phoneNum);
				var smsIntent = new Intent (Intent.ActionSendto, smsUri);
				//smsIntent.PutExtra ("sms_body", "hello from Mono for Android");  
				this.context.StartActivity (smsIntent);
			};

			if (item.workEmail.Length == 0 && item.otherEmail.Length == 0) {
				quickMail.Visibility = Android.Views.ViewStates.Gone;
			} else {
				quickMail.Visibility = Android.Views.ViewStates.Visible;
			}

			quickMail.Click += (sender, e) => {  
				//Android.Widget.Toast.MakeText(this.context, item.firstName, Android.Widget.ToastLength.Short).Show();
				var mailAddr = "";
				if (item.workEmail != null && item.workEmail.Length > 0) {
					mailAddr = item.workEmail;
				} else if (item.otherEmail != null && item.otherEmail.Length > 0) {
					mailAddr = item.otherEmail;
				} else {
					Android.Widget.Toast.MakeText (this.context, "No data!", Android.Widget.ToastLength.Short).Show ();
					return;
				}
				
				var email = new Intent (Android.Content.Intent.ActionSend);
				email.PutExtra (Android.Content.Intent.ExtraEmail, 
				                new string[]{mailAddr} );
				/*
				email.PutExtra (Android.Content.Intent.ExtraEmail, 
				                new string[]{"person1@xamarin.com", "person2@xamrin.com"} );
				
				email.PutExtra (Android.Content.Intent.ExtraCc,
				                new string[]{"person3@xamarin.com"} );

				email.PutExtra (Android.Content.Intent.ExtraSubject, "Hello Email");
				
				email.PutExtra (Android.Content.Intent.ExtraText, 
				                "Hello from Mono for Android");
				*/
				//email.SetType ("message/rfc822");

				this.context.StartActivity (email); 
			};





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