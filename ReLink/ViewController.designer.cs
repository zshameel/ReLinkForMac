// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ReLink
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSComboBox cboBrowsers { get; set; }

		[Outlet]
		AppKit.NSComboBox cboSites { get; set; }

		[Outlet]
		AppKit.NSTextField lblSelected { get; set; }

		[Action ("btnOKClicked:")]
		partial void btnOKClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (cboBrowsers != null) {
				cboBrowsers.Dispose ();
				cboBrowsers = null;
			}

			if (cboSites != null) {
				cboSites.Dispose ();
				cboSites = null;
			}

			if (lblSelected != null) {
				lblSelected.Dispose ();
				lblSelected = null;
			}
		}
	}
}
