using System;
using System.Collections.Generic;
using AppKit;
using Foundation;

namespace ReLink {
    public class RuleDataSource : NSTableViewDataSource {
        readonly List<RuleItem> source;

        public RuleDataSource(List<RuleItem> source) {
            this.source = source;
        }

        [Export("numberOfRowsInTableView:")]
        public override nint GetRowCount(NSTableView tableView) {
            return source.Count;
        }

        [Export("tableView:objectValueForTableColumn:row:")]
        public override NSObject GetObjectValue(NSTableView tableView, NSTableColumn tableColumn, nint row) {
            switch(tableColumn.HeaderCell.Title) {
                case "#":
                default:
                    return new NSString(source[(int)row].RuleId.ToString());
                case "Match":
                    return new NSString(source[(int)row].MatchType.ToString());
                case "Url":
                    return new NSString(source[(int)row].Url);
                case "Browser":
                    return new NSString(source[(int)row].BrowserName);
            }

        }
        
    }
}
