using System;
using System.Collections.Generic;
using System.Diagnostics;
using AppKit;
using Foundation;
using ReLink;

namespace ReLink {
    public partial class ViewController : NSViewController {
        public ViewController(IntPtr handle) : base(handle) {
        }

        public override void ViewDidLoad() {
            base.ViewDidLoad();
            if (MainClass.NoUI) {
                
            }

            // Do any additional setup after loading the view.
            lblSelected.StringValue = string.Empty;
            LoadBrowsers();
            LoadSites();
        }

        private void LoadBrowsers() {
            //LSCopyAllHandlersForURLScheme();
            List<string> browserList = new List<string>();
            foreach (BrowserInfo browser in BrowserManager.Browsers) {
                browserList.Add(browser.Name);
            }

            cboBrowsers.UsesDataSource = true;
            cboBrowsers.DataSource = new GenericDataSource(browserList);
            cboBrowsers.SelectItem(0);
        }

        private void LoadSites() {
            List<string> siteList = new List<string>() { "https://google.com", "https://github.com", "http://shameel.net", "https://amazon.com", "https://microsoft.com" };
 
            cboSites.UsesDataSource = true;
            cboSites.DataSource = new GenericDataSource(siteList);
            cboSites.SelectItem(0);
        }

        partial void btnOKClicked(NSObject sender) {
            lblSelected.StringValue = cboBrowsers.DataSource.ObjectValueForItem(cboBrowsers, cboBrowsers.SelectedIndex).ToString();

            BrowserInfo browser = BrowserManager.GetBrowserByName(lblSelected.StringValue);
            string url = cboSites.DataSource.ObjectValueForItem(cboSites, cboSites.SelectedIndex).ToString();

            BrowserManager.LaunchUrl(url);
        }
    }
}