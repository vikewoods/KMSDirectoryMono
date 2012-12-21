
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace KMSDirectory.iOS
{
	public partial class EmployeeTableViewCell : UITableViewCell
	{
		public string szPhoneNo { get; set; }
		public string szEmailAddress { get; set; }

		public EmployeeTableViewCell () : base()
		{
			szPhoneNo = "";
			szEmailAddress = "";
		}
		
		public EmployeeTableViewCell (IntPtr handle) : base(handle)
		{
			szPhoneNo = "";
			szEmailAddress = "";
		}

		public void UpdateWithData (Employee employee, UIImage imgDefaultAvatar, UIImage imgPhone, UIImage imgSMS, UIImage imgEmail)
		{
			avatar.Image = employee.getAvatar ();

			if (avatar.Image == null) {
				avatar.Image = imgDefaultAvatar;
			}

			name.Text = employee.getName ();
			title.Text = employee.title;

			szPhoneNo = employee.getPhoneNo ();

			if (szPhoneNo.Length > 0) {
				phoneImg.Image = imgPhone;
				smsImg.Image = imgSMS;
			} else {
			}

			szEmailAddress = employee.getEmailAddr ();

			if (szEmailAddress.Length > 0) {
				emailImg.Image = imgEmail;
			} else {
			}
		}
	}
}

