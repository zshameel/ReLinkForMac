using System;
using System.Collections.Generic;
using System.Diagnostics;
using AppKit;
using Foundation;
using ReLink;

namespace ReLink {
    public partial class ViewController : NSViewController {

        RuleInfo _ruleInfo;

        public ViewController(IntPtr handle) : base(handle) {
        }

        public override void ViewDidLoad() {
            base.ViewDidLoad();
            if (MainClass.NoUI) {

            }

            _ruleInfo = RuleInfo.GetInstance();

            InitBrowsers();
            InitMatchTypes();
            InitRuleTable();
            InitControls();
        }

        private void InitControls() {
            FallbackBrowserComboBox.ToolTip = "The Browser to use when no Rule match the url";
            BrowserComboBox.ToolTip = "Select the Browser to use with the Rule";
            UrlTextField.ToolTip = "Enter the url or url pattern here";
            MatchTypeComboBox.ToolTip = "Select the Url Match type";
            UseFallbackBrowserForAllUrlsCheckBox.ToolTip = "Check this to bypass rules and open all urls with the Fallback Browser";
            AddRuleButton.ToolTip = "Add Rule";
            DeleteRuleButton.ToolTip = "Delete selected Rule";
            MoveRuleUpButton.ToolTip = "Move selected Rule up";
            MoveRuleDownButton.ToolTip = "Move selected Rule down";
        }

        private void InitRuleTable() {
            RuleDataSource ruleDataSource = new RuleDataSource(_ruleInfo.Rules);

            RuleListTableView.DataSource = ruleDataSource;

            if (RuleListTableView.RowCount > 0) {
                RuleListTableView.SelectRow(0, false);
            }
        }

        private void SaveRules() {
            _ruleInfo.SaveRules();
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

        partial void AddButtonClicked(Foundation.NSObject sender) {
            RuleItem rule = _ruleInfo.Rules.Find(r => r.Url.Equals(UrlTextField.StringValue, StringComparison.OrdinalIgnoreCase));
            nint selectedRow = 0;

            if (rule == null) {
                _ruleInfo.Rules.Add(new RuleItem() {
                    MatchType = Enum.Parse<MatchType>(MatchTypeComboBox.StringValue),
                    RuleId = _ruleInfo.Rules.Count + 1,
                    Url = UrlTextField.StringValue,
                    BrowserName = BrowserComboBox.StringValue
                });
                selectedRow = RuleListTableView.RowCount;
            } else {
                rule.MatchType = Enum.Parse<MatchType>(MatchTypeComboBox.StringValue);
                rule.BrowserName = BrowserComboBox.StringValue;
                selectedRow = rule.RuleId-1;
            }

            SaveRules();
            InitRuleTable();

            RuleListTableView.SelectRow(selectedRow, false);
            RuleListTableView.ScrollRowToVisible(selectedRow);
        }

    }
}