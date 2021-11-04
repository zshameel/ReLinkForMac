using System;
using System.Collections.Generic;
using System.IO;
using Foundation;
using Newtonsoft.Json;

namespace ReLink {

    public class RuleInfo {
        public string FallbackBrowserName { get; set; }
        public bool UseFallbackBrowserForAllLinks { get; set; }
        public List<RuleItem> Rules { get; set; }

        public static RuleInfo GetInstance() {
            var content = string.Empty;

            using (var textStreamReader = File.OpenText(NSBundle.MainBundle.ResourcePath + @"/Config/RulesInfo.json")) {
                content = textStreamReader.ReadToEnd();
            }

            var ruleInfo = JsonConvert.DeserializeObject<RuleInfo>(content);
            return ruleInfo;
        }

    }

}
