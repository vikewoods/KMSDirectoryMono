
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace KMSDirectory.iOS
{
	public partial class EmployeeDetailViewController : UIViewController
	{
		protected Employee m_empInfo;

		public EmployeeDetailViewController (Employee empInfo) : base ("EmployeeDetailViewController", null)
		{
			m_empInfo = empInfo;
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			this.Title = @"Details";

			this.avatar.Image = m_empInfo.getAvatar();
			this.name.Text = m_empInfo.getName();
			this.empTitle.Text = m_empInfo.title;

			this.mobilePhone.Text = m_empInfo.mobiPhone;
			this.homePhone.Text = m_empInfo.homePhone;
			this.workPhone.Text = m_empInfo.workPhone;

			this.workEmail.Text = m_empInfo.workEmail;
			this.otherEmail.Text = m_empInfo.otherEmail;

			this.emergencyName.Text = m_empInfo.relaName;
			this.emergencyRelationship.Text = m_empInfo.relationShip;
			this.emergencyPhone.Text = m_empInfo.getRelaPhone();
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

