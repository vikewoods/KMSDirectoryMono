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


namespace CustomRowView
{
	[Activity (Label = "Employee")]            
	public class DetailScreen : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			// Load the UI defined in Second.axml
			SetContentView (Resource.Layout.DetailScreen);

			//Get detail data from parent activity
			var firstNameStr = Intent.GetStringExtra("firstName") ?? "";
			var lastNameStr = Intent.GetStringExtra("lastName") ?? "";
			var titleStr = Intent.GetStringExtra("title") ?? "";
			var mobiPhoneStr = Intent.GetStringExtra("mobiPhone") ?? "";
			var homePhoneStr = Intent.GetStringExtra("homePhone") ?? "";
			var workPhoneStr = Intent.GetStringExtra("workPhone") ?? "";
			var workEmailStr = Intent.GetStringExtra("workEmail") ?? "";
			var otherEmailStr = Intent.GetStringExtra("otherEmail") ?? "";
			var relaNameStr = Intent.GetStringExtra("relaName") ?? "";
			var relationShipStr = Intent.GetStringExtra("relationShip") ?? "";
			var relaMobiPhoneStr = Intent.GetStringExtra("relaMobiPhone") ?? "";
			var relaHomePhoneStr = Intent.GetStringExtra("relaHomePhone") ?? "";
			var relaWorkPhoneStr = Intent.GetStringExtra("relaWorkPhone") ?? "";
			var avatarImageStr = Intent.GetStringExtra("avatarImage") ?? "";
			
			// Get a reference to the TextView
			var avatar = FindViewById<ImageView> (Resource.Id.avatar);
			var name = FindViewById<TextView> (Resource.Id.employee_name);
			var title = FindViewById<TextView> (Resource.Id.employee_title);
			var mobiPhone = FindViewById<TextView> (Resource.Id.homephone_val);
			var homePhone = FindViewById<TextView> (Resource.Id.mobiphone_val);
			var workPhone = FindViewById<TextView> (Resource.Id.workphone_val);
			var workEmail = FindViewById<TextView> (Resource.Id.workmail_val);
			var otherEmail = FindViewById<TextView> (Resource.Id.othermail_val);
			var relaName = FindViewById<TextView> (Resource.Id.rela_name);
			var relationship = FindViewById<TextView> (Resource.Id.relationship);
			var relaMobiPhone = FindViewById<TextView> (Resource.Id.relamobiphone_val);
			var relaHomePhone = FindViewById<TextView> (Resource.Id.relahomephone_val);
			var relaWorkPhone = FindViewById<TextView> (Resource.Id.relaworkphone_val);
			var mobiPhoneLayout = FindViewById<RelativeLayout> (Resource.Id.mobiphone_layout);
			var homePhoneLayout = FindViewById<RelativeLayout> (Resource.Id.homephone_layout);
			var workPhoneLayout = FindViewById<RelativeLayout> (Resource.Id.workphone_layout);
			var workEmailLayout = FindViewById<RelativeLayout> (Resource.Id.workmail_layout);
			var otherEmailLayout = FindViewById<RelativeLayout> (Resource.Id.othermail_layout);
			var relaMobiPhoneLayout = FindViewById<RelativeLayout> (Resource.Id.relamobiphone_layout);
			var relaHomePhoneLayout = FindViewById<RelativeLayout> (Resource.Id.relahomephone_layout);
			var relaWorkPhoneLayout = FindViewById<RelativeLayout> (Resource.Id.relaworkphone_layout);

			var mobiSms = FindViewById<ImageView> (Resource.Id.mobiphone_chat);
			var mobiCall = FindViewById<ImageView> (Resource.Id.mobiphone_call);
			var workEmailSend = FindViewById<ImageView>(Resource.Id.workmail_send);


			name.Text = firstNameStr + " " + lastNameStr;
			title.Text = titleStr;

			if (mobiPhoneStr == "") {
				mobiPhoneLayout.Visibility = Android.Views.ViewStates.Gone;
			} else {
				mobiPhone.Text = mobiPhoneStr;
			}

			if (homePhoneStr == "") {
				homePhoneLayout.Visibility = Android.Views.ViewStates.Gone;
			} else {
				homePhone.Text = homePhoneStr;
			}

			if (workPhoneStr == "") {
				workPhoneLayout.Visibility = Android.Views.ViewStates.Gone;
			} else {
				workPhone.Text = workPhoneStr;
			}

			if (workEmailStr == "") {
				workEmailLayout.Visibility = Android.Views.ViewStates.Gone;
			} else {
				workEmail.Text = workEmailStr;
			}

			if (otherEmailStr == "") {
				otherEmailLayout.Visibility = Android.Views.ViewStates.Gone;
			} else {
				otherEmail.Text = otherEmailStr;
			}

			relaName.Text = "Name: " + relaNameStr;
			relationship.Text = "Relationship: " + relationShipStr;

			if (relaMobiPhoneStr == "") {
				relaMobiPhoneLayout.Visibility = Android.Views.ViewStates.Gone;
			} else {
				relaMobiPhone.Text = relaMobiPhoneStr;
			}

			if (relaHomePhoneStr == "") {
				relaHomePhoneLayout.Visibility = Android.Views.ViewStates.Gone;
			} else {
				relaHomePhone.Text = relaHomePhoneStr;
			}

			if (relaWorkPhoneStr == "") {
				relaWorkPhoneLayout.Visibility = Android.Views.ViewStates.Gone;
			} else {
				relaWorkPhone.Text = relaWorkPhoneStr;
			}
			avatar.SetImageBitmap(GetAvatar(avatarImageStr));

			// Populate the TextView with the data that was added to the intent in FirstActivity 
			//label.Text = Intent.GetStringExtra("FirstData") ?? "Data not available";
			mobiSms.Click += (sender, e) => {           
				//Android.Widget.Toast.MakeText(this, "just test here", Android.Widget.ToastLength.Short).Show();
				var smsUri = Android.Net.Uri.Parse("smsto:1234567890");
				var smsIntent = new Intent (Intent.ActionSendto, smsUri);
				//smsIntent.PutExtra ("sms_body", "hello from Mono for Android");  
				StartActivity (smsIntent);
			};

			mobiCall.Click += (sender, e) => {           
				var uri = Android.Net.Uri.Parse ("tel:1112223333");
				var intent = new Intent (Intent.ActionView, uri); 
				StartActivity (intent);  
			};

			workEmailSend.Click += (sender, e) => {           
				//Android.Widget.Toast.MakeText(this, "just test here", Android.Widget.ToastLength.Short).Show();

				var email = new Intent (Android.Content.Intent.ActionSend);
				email.PutExtra (Android.Content.Intent.ExtraEmail, 
				                new string[]{"person1@xamarin.com", "person2@xamrin.com"} );
				
				email.PutExtra (Android.Content.Intent.ExtraCc,
				                new string[]{"person3@xamarin.com"} );
				
				email.PutExtra (Android.Content.Intent.ExtraSubject, "Hello Email");
				
				email.PutExtra (Android.Content.Intent.ExtraText, 
				                "Hello from Mono for Android");

				//email.SetType ("message/rfc822");

				StartActivity (email); 
			};
		}

		Bitmap GetAvatar (String avatar)
		{
			// Default avatar
			if (avatar == null || avatar == "")
				return null;
			
			// Special avatar
			byte[] byteImg = System.Convert.FromBase64String (avatar);
			Bitmap b = BitmapFactory.DecodeByteArray(byteImg, 0, byteImg.Length);
			return b;
		}

	}
}

