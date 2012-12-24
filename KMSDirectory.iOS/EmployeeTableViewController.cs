// 
// RootViewController.cs
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
using System.Drawing;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using MonoTouch.ObjCRuntime;

namespace KMSDirectory.iOS {
	public partial class EmployeeTableViewController : UITableViewController {
		//public ObservableCollection<App> m_Apps { get; private set; }
		public ObservableCollection<Employee> m_arrEmployee { get; private set; }
		
		public EmployeeTableViewController(string nibName, NSBundle bundle) : base(nibName, bundle)
		{
			m_arrEmployee = new ObservableCollection<Employee>();

			Title = NSBundle.MainBundle.LocalizedString("Employees", "Master");
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
			TableView.Source = new UITableViewSourceEx(this);
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
		{
			return(toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
		
		public override void DidReceiveMemoryWarning()
		{
			// Release all cached images. This will cause them to be redownloaded
			// later as they're displayed.
			foreach(var employee in m_arrEmployee)
				employee.avatarImage = null;
		}

		class UITableViewSourceEx : UITableViewSource {
			EmployeeTableViewController m_Controller { get; set; }
			Task m_DownloadTask { get; set; }
			UIImage m_imgDefaultAvatar { get; set; }
			UIImage m_imgPhone { get; set; }
			UIImage m_imgSMS { get; set; }
			UIImage m_imgEmail { get; set; }

			public UITableViewSourceEx(EmployeeTableViewController controller)
			{
				m_Controller = controller;
				
				// Listen for changes to the Apps collection so the TableView can be updated
				m_Controller.m_arrEmployee.CollectionChanged += HandleEmployeeCollectionChanged;

				// Initialise DownloadTask with an empty and complete task
				m_DownloadTask = Task.Factory.StartNew(() => { });

				// Load the Placeholder image so it's ready to be used immediately
				m_imgDefaultAvatar = UIImage.FromFile("Images/dafault_avatar.png");
				m_imgPhone = UIImage.FromFile("Images/btn_call.png");
				m_imgSMS = UIImage.FromFile("Images/btn_chat.png");
				m_imgEmail = UIImage.FromFile("Images/btn_mail.png");
				
				// If either a download fails or the image we download is corrupt, ignore the problem.
				TaskScheduler.UnobservedTaskException += delegate(object sender, UnobservedTaskExceptionEventArgs e) {
					e.SetObserved();
				};
			}

			void HandleEmployeeCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
			{
				// Whenever the Items change, reload the data.
				m_Controller.TableView.ReloadData();
			}
			
			public override int NumberOfSections(UITableView tableView)
			{
				return 1;
			}
			
			public override int RowsInSection(UITableView tableview, int section)
			{
				//return Controller.m_Apps.Count;
				return m_Controller.m_arrEmployee.Count;
			}

			public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 135;
			}
			
			// Customize the appearance of table view cells.
			public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				// Reuse a cell if one exists
				EmployeeTableViewCell cell = tableView.DequeueReusableCell ("EmployeeCell") as EmployeeTableViewCell;

				if (cell == null) {
					// We have to allocate a cell
					var views = NSBundle.MainBundle.LoadNib ("EmployeeTableViewCell", tableView, null);
					cell = Runtime.GetNSObject (views.ValueAt (0)) as EmployeeTableViewCell;
				}
				
				// Set the tag of each cell to the index of the App that
				// it's displaying. This allows us to directly match a cell
				// with an item when we're updating the Image
				//var app = Controller.m_Apps [indexPath.Row];
				var employee = m_Controller.m_arrEmployee [indexPath.Row];

				cell.UpdateWithData(employee, m_imgDefaultAvatar, m_imgPhone, m_imgSMS, m_imgEmail);

				/*
				cell.Tag = indexPath.Row;
				cell.TextLabel.Text = MakeEmployeeName (employee);
				cell.DetailTextLabel.Text = employee.title;

				// If the Image for this App has not been downloaded,
				// use the Placeholder image while we try to download
				// the real image from the web.
				//if (employee.avatarImage == null) {
				//	employee.avatarImage = m_imgDefaultAvatar;
				//	BeginDownloadingImage (employee, indexPath);
				//} else {
				cell.ImageView.Image = GetAvatar(employee);//employee.avatarImage;
				//}
				/**/

				return cell;
			}

			public override void RowSelected (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				var employee = m_Controller.m_arrEmployee [indexPath.Row];

				EmployeeDetailViewController details = new EmployeeDetailViewController(employee);
				m_Controller.NavigationController.PushViewController(details, true);
			}

			UIImage GetAvatar (Employee employee)
			{
				// Default avatar
				if (employee.avatarImage == null || employee.avatarImage == "")
					return m_imgDefaultAvatar;

				// Special avatar
				byte[] byteImg = System.Convert.FromBase64String (employee.avatarImage);
				NSData data = NSData.FromArray(byteImg);

				return UIImage.LoadFromData (data);
			}

			string MakeEmployeeName (Employee employee)
			{
				return employee.firstName + " " + employee.lastName;
			}

			void BeginDownloadingImage(App app, NSIndexPath path)
			{
				/*
				// Queue the image to be downloaded. This task will execute
				// as soon as the existing ones have finished.
				byte[] data = null;
				DownloadTask = DownloadTask.ContinueWith(prevTask => {
					try {
						UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
						using(var c = new GzipWebClient())
							data = c.DownloadData(app.ImageUrl);
					} finally {
						UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
					}
				});
				
				// When the download task is finished, queue another task to update the UI.
				// Note that this task will run only if the download is successful and it
				// uses the CurrentSyncronisationContext, which on MonoTouch causes the task
				// to be run on the main UI thread. This allows us to safely access the UI.
				DownloadTask = DownloadTask.ContinueWith(t => {
					// Load the image from the byte array.
					app.Image = UIImage.LoadFromData(NSData.FromArray(data));
					
					// Retrieve the cell which corresponds to the current App. If the cell is null, it means the user
					// has already scrolled that app off-screen.
					var cell = Controller.TableView.VisibleCells.Where(c => c.Tag == Controller.m_Apps.IndexOf(app)).FirstOrDefault();
					if(cell != null)
						cell.ImageView.Image = app.Image;
				}, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
				/**/
			}
		}
	}
}
