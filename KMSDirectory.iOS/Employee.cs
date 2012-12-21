using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace KMSDirectory.iOS
{
	public class Employee
	{
		public Int64 id { get; set; }		
		public string firstName { get; set; }
		public string lastName { get; set; }
		public string title { get; set; }
		public string mobiPhone { get; set; }
		public string homePhone { get; set; }
		public string workPhone { get; set; }
		public string workEmail { get; set; }
		public string otherEmail { get; set; }
		public string relaName { get; set; }
		public string relationShip { get; set; }
		public string relaMobiPhone { get; set; }
		public string relaHomePhone { get; set; }
		public string relaWorkPhone { get; set; }
		public string avatarImage { get; set; }
		public string avatarImageType { get; set; }
		public string gender { get; set; }

		public string getName ()
		{
			return firstName + " " + lastName;
		}

		public UIImage getAvatar ()
		{
			// Default avatar
			if (avatarImage == null || avatarImage == "")
				return null;
			
			// Special avatar
			byte[] byteImg = System.Convert.FromBase64String (avatarImage);
			NSData data = NSData.FromArray(byteImg);
			
			return UIImage.LoadFromData (data);
		}

		public string getPhoneNo()
		{
			if (mobiPhone.Length > 0)
				return mobiPhone;

			if (homePhone.Length > 0)
				return homePhone;

			if (workPhone.Length > 0)
				return workPhone;

			return "";
		}

		public string getEmailAddr()
		{
			if (workEmail.Length > 0)
				return workEmail;

			if (otherEmail.Length > 0)
				return otherEmail;

			return "";
		}
	}
}

