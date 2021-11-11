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
                View.Window.Close();
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

        private void DeleteRule(nint selectedRow) {

            _ruleInfo.Rules.RemoveAt((int)selectedRow);

            RefreshRules(selectedRow);
        }

        private int CompareRulesByRuleId(RuleItem x, RuleItem y) {
            if (x == null) {
                if (y == null) {
                    return 0;
                } else {
                    return -1;
                }
            } else {
                if (y == null) {
                    return 1;
                } else {
                    int retval = x.CompareTo(y);

                    if (retval != 0) {
                        return retval;
                    } else {
                        return x.CompareTo(y);
                    }
                }
            }
        }

        private void RefreshRules(nint selectedRow) {
            SaveRules();
            InitRuleTable();

            if (RuleListTableView.RowCount > 0) {
                RuleListTableView.SelectRow(selectedRow, false);
                RuleListTableView.ScrollRowToVisible(selectedRow);
            }
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

            RefreshRules(selectedRow);
        }

        partial void DeleteButtonClicked(Foundation.NSObject sender) {
            nint selectedRow = RuleListTableView.SelectedRow;

            var alert = new NSAlert() {
                AlertStyle = NSAlertStyle.Warning,
                MessageText = "Delete Rule?",
                InformativeText = $"Are you sure you want to delete Rule # {selectedRow + 1}?"
            };
            alert.AddButton("Yes");
            alert.AddButton("No");
            var result = alert.RunModal();

            if (result == 1000) {
                DeleteRule(selectedRow);
            }
        }

        partial void MoveUpButtonClicked(Foundation.NSObject sender) {
            nint selectedRow = RuleListTableView.SelectedRow;

            if (selectedRow > 0) {
                int ruleId = (int)selectedRow + 1;
                int swapRuleId = (int)selectedRow;

                RuleItem rule = _ruleInfo.Rules.Find(r => r.RuleId == ruleId);
                RuleItem swapRule = _ruleInfo.Rules.Find(r => r.RuleId == swapRuleId);

                rule.RuleId = swapRuleId;
                swapRule.RuleId = ruleId;

                _ruleInfo.Rules.Sort(CompareRulesByRuleId);

                if (selectedRow > 0) {
                    selectedRow--;
                }

                RefreshRules(selectedRow);
            }
        }

        partial void MoveDownButtonClicked(Foundation.NSObject sender) {
            nint selectedRow = RuleListTableView.SelectedRow;

            if (selectedRow+1 < _ruleInfo.Rules.Count) {
                int ruleId = (int)selectedRow + 1;
                int swapRuleId = (int)selectedRow+2;

                RuleItem rule = _ruleInfo.Rules.Find(r => r.RuleId == ruleId);
                RuleItem swapRule = _ruleInfo.Rules.Find(r => r.RuleId == swapRuleId);

                rule.RuleId = swapRuleId;
                swapRule.RuleId = ruleId;

                _ruleInfo.Rules.Sort(CompareRulesByRuleId);

                if (selectedRow+1 < _ruleInfo.Rules.Count) {
                    selectedRow++;
                }

                RefreshRules(selectedRow);
            }
        }
    }
}