// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace KMSDirectory.iOS
{
	[Register ("EmployeeTableViewCell")]
	partial class EmployeeTableViewCell
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView avatar { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel name { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel title { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView phoneImg { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView smsImg { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView emailImg { get; set; }
		
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

			if (title != null) {
				title.Dispose ();
				title = null;
			}

			if (phoneImg != null) {
				phoneImg.Dispose ();
				phoneImg = null;
			}

			if (smsImg != null) {
				smsImg.Dispose ();
				smsImg = null;
			}

			if (emailImg != null) {
				emailImg.Dispose ();
				emailImg = null;
			}
		}
	}
}
