// 
// AppDelegate.cs
//  
// Author:
//       Alan McGovern <alan@xamarin.com>
// 
// Copyright 2011, Xamarin Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Net;
using System.Collections.ObjectModel;
using System.Json;
using System.IO;
using Newtonsoft.Json;

namespace KMSDirectory.iOS
{
	/// <summary>
	/// The UIApplicationDelegate for the application. This class is responsible for launching the 
	/// User Interface of the application, as well as listening(and optionally responding) to 
	/// application events from iOS.
	/// </summary>
	[Register("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		static readonly Uri m_RssFeedUrl = new Uri("http://phobos.apple.com/WebObjects/MZStoreServices.woa/ws/RSS/toppaidapplications/limit=75/xml");
		static readonly string m_szRequestUrl = @"http://192.168.30.72/RestServiceImpl.svc/json/456";
		
		UINavigationController m_NavigationController { get; set; }
		EmployeeMngrView m_RootController { get; set; }
		public UIWindow m_Window { get; set; }
		
		/// <summary>
		/// This method is invoked when the application has loaded and is ready to run. In this 
		/// method you should instantiate the window, load the UI into it and then make the window
		/// visible.
		/// </summary>
		/// <remarks>
		/// You have 5 seconds to return from this method, or iOS will terminate your application.
		/// </remarks>
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			m_Window = new UIWindow (UIScreen.MainScreen.Bounds);
			m_RootController = new EmployeeMngrView ("EmployeeMngrView", null);
			m_NavigationController = new UINavigationController (m_RootController);
			m_Window.RootViewController = m_NavigationController;
			
			// make the window visible
			m_Window.MakeKeyAndVisible ();
			
			BeginDownloading ();
			return true;
		}
		
		void BeginDownloading ()
		{
			// Show the user that data is about to be downloaded
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;

			/*
			// XML data
			// Retrieve the rss feed from the server
			var downloader = new GzipWebClient();

			downloader.DownloadStringCompleted += DownloadCompleted;
			downloader.DownloadStringAsync(m_RssFeedUrl);
			/**/

			// JSON data
			FetchEmployees(m_szRequestUrl);
		}

		void FetchEmployees(string szRequestedURL)
		{
			UIApplication.SharedApplication.BeginInvokeOnMainThread (() => {
				// First disable the download indicator
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
				
				// Now handle the result from the WebClient
				var request = (HttpWebRequest)WebRequest.Create (szRequestedURL);
				request.ContentType = "application/json";
				request.Method = "GET";
				request.Timeout = 600000;
				
				using (var response = (HttpWebResponse) request.GetResponse ()) {
					if (response.StatusCode != HttpStatusCode.OK) {
						UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
						DisplayError ("Error", "Malformed JSON was found in the request.");
					} else {
						using (var reader = new StreamReader(response.GetResponseStream ())) {
							//JsonValue root = JsonValue.Load (streamReader);
							//List<Employee> questions = ParseJsonAndLoadQuestions ((JsonObject)root);
							
							var content = reader.ReadToEnd ();
							
							if (string.IsNullOrWhiteSpace (content)) {
								Console.Out.WriteLine ("Response contained empty body...");
							} else {
								List<Employee> list = JsonConvert.DeserializeObject<List<Employee>> (content);
								//var deserializer = new DataContract DataContractJsonSerializer(); // Xamarin's api

								m_RootController.m_arrEmployee.Clear ();

								foreach (var item in list)
								{
									m_RootController.m_arrEmployee.Add (item);
								}
							}

							UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
						}
					}
				}

				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			});
		}

		void DownloadCompleted (object sender, DownloadStringCompletedEventArgs e)
		{
			/*
			// The WebClient will invoke the DownloadStringCompleted event on a
			// background thread. We want to do UI updates with the result, so process
			// the result on the main thread.
			UIApplication.SharedApplication.BeginInvokeOnMainThread (() => {
				// First disable the download indicator
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;

				// Now handle the result from the WebClient
				if (e.Error != null) {
					DisplayError ("Warning", "The rss feed could not be downloaded: " + e.Error.Message);
				} else {
					try {
						m_RootController.m_Apps.Clear ();
						foreach (var v in RssParser.Parse(e.Result))
							m_RootController.m_Apps.Add (v);
					} catch {
						DisplayError ("Warning", "Malformed Xml was found in the Rss Feed.");
					}
				}
			});
			/**/
		}
		
		void DisplayError (string szTitle, string szErrorMessage, params object[] paraFormat)
		{
			var alert = new UIAlertView (szTitle, string.Format (szErrorMessage, paraFormat), null, "ok", null);

			alert.Show ();
		}
	}
}
