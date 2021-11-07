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
		AppKit.NSButton AddRuleButton { get; set; }

		[Outlet]
		AppKit.NSComboBox BrowserComboBox { get; set; }

		[Outlet]
		AppKit.NSButton DeleteRuleButton { get; set; }

		[Outlet]
		AppKit.NSComboBox FallbackBrowserComboBox { get; set; }

		[Outlet]
		AppKit.NSComboBox MatchTypeComboBox { get; set; }

		[Outlet]
		AppKit.NSButton MoveRuleDownButton { get; set; }

		[Outlet]
		AppKit.NSButton MoveRuleUpButton { get; set; }

		[Outlet]
		AppKit.NSScrollView RuleListScrollView { get; set; }

		[Outlet]
		AppKit.NSTableView RuleListTableView { get; set; }

		[Outlet]
		AppKit.NSTextField UrlTextField { get; set; }

		[Outlet]
		AppKit.NSButton UseFallbackBrowserForAllUrlsCheckBox { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AddRuleButton != null) {
				AddRuleButton.Dispose ();
				AddRuleButton = null;
			}

			if (BrowserComboBox != null) {
				BrowserComboBox.Dispose ();
				BrowserComboBox = null;
			}

			if (DeleteRuleButton != null) {
				DeleteRuleButton.Dispose ();
				DeleteRuleButton = null;
			}

			if (FallbackBrowserComboBox != null) {
				FallbackBrowserComboBox.Dispose ();
				FallbackBrowserComboBox = null;
			}

			if (MatchTypeComboBox != null) {
				MatchTypeComboBox.Dispose ();
				MatchTypeComboBox = null;
			}

			if (MoveRuleDownButton != null) {
				MoveRuleDownButton.Dispose ();
				MoveRuleDownButton = null;
			}

			if (MoveRuleUpButton != null) {
				MoveRuleUpButton.Dispose ();
				MoveRuleUpButton = null;
			}

			if (RuleListScrollView != null) {
				RuleListScrollView.Dispose ();
				RuleListScrollView = null;
			}

			if (UrlTextField != null) {
				UrlTextField.Dispose ();
				UrlTextField = null;
			}

			if (RuleListTableView != null) {
				RuleListTableView.Dispose ();
				RuleListTableView = null;
			}

			if (UseFallbackBrowserForAllUrlsCheckBox != null) {
				UseFallbackBrowserForAllUrlsCheckBox.Dispose ();
				UseFallbackBrowserForAllUrlsCheckBox = null;
			}
		}
	}
}
