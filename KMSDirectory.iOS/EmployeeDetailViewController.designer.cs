// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace KMSDirectory.iOS
{
	[Register ("EmployeeDetailViewController")]
	partial class EmployeeDetailViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView avatar { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel name { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel empTitle { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel mobilePhone { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel homePhone { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel workPhone { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel workEmail { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel otherEmail { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel emergencyName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel emergencyRelationship { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel emergencyPhone { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (avatar != null) {
				avatar.Dispose ();
				avatar = null;
			}

			if (name != null) {
				name.Dispose ();
				name = null;
			}

			if (empTitle != null) {
				empTitle.Dispose ();
				empTitle = null;
			}

			if (mobilePhone != null) {
				mobilePhone.Dispose ();
				mobilePhone = null;
			}

			if (homePhone != null) {
				homePhone.Dispose ();
				homePhone = null;
			}

			if (workPhone != null) {
				workPhone.Dispose ();
				workPhone = null;
			}

			if (workEmail != null) {
				workEmail.Dispose ();
				workEmail = null;
			}

			if (otherEmail != null) {
				otherEmail.Dispose ();
				otherEmail = null;
			}

			if (emergencyName != null) {
				emergencyName.Dispose ();
				emergencyName = null;
			}

			if (emergencyRelationship != null) {
				emergencyRelationship.Dispose ();
				emergencyRelationship = null;
			}

			if (emergencyPhone != null) {
				emergencyPhone.Dispose ();
				emergencyPhone = null;
			}
		}
	}
}
