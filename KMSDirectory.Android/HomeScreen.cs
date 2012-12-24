using System;
using System.Net;
using System.IO;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CustomRowView {
    [Activity(Label = "KMSDirectory.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class HomeScreen : Activity{//, View.IOnClickListener {

        List<Employee> employeeItems = new List<Employee>();
        ListView listView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
			employeeItems.Clear();
			loadData();

            SetContentView(Resource.Layout.HomeScreen);
            listView = FindViewById<ListView>(Resource.Id.List);
			/*
			employeeItems.Add(new Employee() { firstName = "Vegetables", lastName="test", title = "65 items", id = Resource.Drawable.Vegetables });
			employeeItems.Add(new Employee() { firstName = "Fruits", lastName="test", title = "17 items", id = Resource.Drawable.Fruits });
			employeeItems.Add(new Employee() { firstName = "Flower Buds", lastName="test", title = "5 items", id = Resource.Drawable.FlowerBuds });
			employeeItems.Add(new Employee() { firstName = "Legumes", lastName="test", title = "33 items", id = Resource.Drawable.Legumes });
			employeeItems.Add(new Employee() { firstName = "Bulbs", lastName="test", title = "18 items", id = Resource.Drawable.Bulbs });
			employeeItems.Add(new Employee() { firstName = "Tubers", lastName="test", title = "43 items", id = Resource.Drawable.Tubers });
			*/
			listView.Adapter = new HomeScreenAdapter(this, employeeItems);

            listView.ItemClick += OnListItemClick;
        }

        protected void OnListItemClick(object sender, Android.Widget.AdapterView.ItemClickEventArgs e)
        {
            /* var listView = sender as ListView;
            var t = tableItems[e.Position];
            Android.Widget.Toast.MakeText(this, t.Heading, Android.Widget.ToastLength.Short).Show();
            Console.WriteLine("Clicked on " + t.Heading);
            */
			var employee = employeeItems[e.Position];
			var intent = new Intent(this, typeof(DetailScreen));
			intent.PutExtra("id", employee.id);
			intent.PutExtra("firstName", employee.firstName);
			intent.PutExtra("lastName", employee.lastName);
			intent.PutExtra("title", employee.title);
			intent.PutExtra("mobiPhone", employee.mobiPhone);
			intent.PutExtra("homePhone", employee.homePhone);
			intent.PutExtra("workPhone", employee.workPhone);
			intent.PutExtra("workEmail", employee.workEmail);
			intent.PutExtra("otherEmail", employee.otherEmail);
			intent.PutExtra("relaName", employee.relaName);
			intent.PutExtra("relationShip", employee.relationShip);
			intent.PutExtra("relaMobiPhone", employee.relaMobiPhone);
			intent.PutExtra("relaHomePhone", employee.relaHomePhone);
			intent.PutExtra("relaWorkPhone", employee.relaWorkPhone);
			intent.PutExtra("avatarImage", employee.avatarImage);
			StartActivity (intent);
        }

		protected void loadData ()
		{
			var url = "http://192.168.1.10/RestServiceImpl.svc/json/456";
			// Now handle the result from the WebClient
			var request = (HttpWebRequest)WebRequest.Create (url);
			request.ContentType = "application/json";
			request.Method = "GET";
			request.Timeout = 600000;
			
			using (var response = (HttpWebResponse) request.GetResponse ()) {
				if (response.StatusCode != HttpStatusCode.OK) {
					Android.Widget.Toast.MakeText(this, "Malformed JSON was found in the request.", Android.Widget.ToastLength.Short).Show();
				} else {
					using (var reader = new StreamReader(response.GetResponseStream ())) {
						//JsonValue root = JsonValue.Load (streamReader);
						//List<Employee> questions = ParseJsonAndLoadQuestions ((JsonObject)root);
						
						var content = reader.ReadToEnd ();
						
						if (string.IsNullOrWhiteSpace (content)) {
							Console.Out.WriteLine ("Response contained empty body...");
							Android.Widget.Toast.MakeText(this, "Response contained empty body...", Android.Widget.ToastLength.Short).Show();
						} else {
							List<Employee> list = JsonConvert.DeserializeObject<List<Employee>> (content);
							//var deserializer = new DataContract DataContractJsonSerializer(); // Xamarin's api


							foreach (var item in list)
							{
								//Console.WriteLine(item.firstName);
								employeeItems.Add(new Employee() { id = item.id, firstName = item.firstName, lastName= item.lastName, title = item.title,  
																	mobiPhone = item.mobiPhone, homePhone = item.homePhone, workPhone = item.workPhone,
																	workEmail = item.workEmail, otherEmail = item.otherEmail, relaName = item.relaName,
																	relationShip = item.relationShip, relaMobiPhone = item.relaMobiPhone, 
																	relaHomePhone = item.relaHomePhone, relaWorkPhone = item.relaWorkPhone,
																	avatarImage = item.avatarImage, avatarImageType = item.avatarImageType, gender = item.gender});
							}
						}
					
					}
				}
			}
		}
    }
}