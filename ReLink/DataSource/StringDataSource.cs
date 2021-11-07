using System;
using System.Collections.Generic;
using AppKit;
using Foundation;

namespace ReLink {
    public class StringDataSource : NSComboBoxDataSource {
        readonly List<string> source;

        public StringDataSource(List<string> source) {
            this.source = source;
        }

        public override string CompletedString(NSComboBox comboBox, string uncompletedString) {
            return source.Find(n => n.StartsWith(uncompletedString, StringComparison.InvariantCultureIgnoreCase));
        }

        public override nint IndexOfItem(NSComboBox comboBox, string value) {
            return source.FindIndex(n => n.Equals(value, StringComparison.InvariantCultureIgnoreCase));
        }

        public override nint ItemCount(NSComboBox comboBox) {
            return source.Count;
        }

        public override NSObject ObjectValueForItem(NSComboBox comboBox, nint index) {
            return NSObject.FromObject(source[(int)index]);
        }
    }
}
