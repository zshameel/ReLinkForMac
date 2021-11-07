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

            InitBrowsers();
            InitMatchTypes();
            InitRuleTable();

            InitSites();
        }

        private void InitRuleTable() {
            RuleDataSource ruleDataSource = new RuleDataSource(RuleInfo.GetInstance().Rules);

            RuleListTableView.DataSource = ruleDataSource;

        }

        private void InitMatchTypes() {
            List<string> matchTypes = new List<string>() { "Contains", "StartsWith", "ExactMatch", "Wildcard", "Regex" };
            StringDataSource matchTypesDataSource = new StringDataSource(matchTypes);

            MatchTypeComboBox.UsesDataSource = true;
            MatchTypeComboBox.DataSource = matchTypesDataSource;
            MatchTypeComboBox.SelectItem(0);
        }

        private void InitBrowsers() {
            //LSCopyAllHandlersForURLScheme();
            List<string> browserList = new List<string>();
            foreach (BrowserInfo browser in BrowserManager.Browsers) {
                browserList.Add(browser.Name);
            }

            StringDataSource browserListDataSource = new StringDataSource(browserList);

            BrowserComboBox.UsesDataSource = true;
            BrowserComboBox.DataSource = browserListDataSource;
            BrowserComboBox.SelectItem(0);

            FallbackBrowserComboBox.UsesDataSource = true;
            FallbackBrowserComboBox.DataSource = browserListDataSource;
            FallbackBrowserComboBox.SelectItem(0);
        }

        private void InitSites() {
            List<string> siteList = new List<string>() { "https://google.com", "https://github.com", "http://shameel.net", "https://amazon.com", "https://microsoft.com" };
 
        }

    }
}