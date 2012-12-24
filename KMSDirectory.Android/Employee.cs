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

namespace CustomRowView
{
	public class Employee
	{
		public int id { get; set; }		
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
	}
}

