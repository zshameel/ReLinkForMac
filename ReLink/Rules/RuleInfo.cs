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
            
            using (StreamReader textStreamReader = File.OpenText(GetRuleFilePath(true))) {
                content = textStreamReader.ReadToEnd();
            }

            var ruleInfo = JsonConvert.DeserializeObject<RuleInfo>(content);
            return ruleInfo;
        }

        private static string GetRuleFilePath(bool create) {
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string reLinkProfile = Path.Combine(userProfile, @"Library/ReLink");
            if (!Directory.Exists(reLinkProfile)) {
                Directory.CreateDirectory(reLinkProfile);
            }

            //If Rule file doesn't exist, create one using the stock file from config folder
            string ruleFilePath = Path.Combine(reLinkProfile, "RulesInfo.json");

            if (create && !File.Exists(ruleFilePath)) {
                string stockRuleFilePath = NSBundle.MainBundle.ResourcePath + @"/Config/RulesInfo.json";
                File.Copy(stockRuleFilePath, ruleFilePath);
            }

            return ruleFilePath;
        }

        internal void SaveRules() {
            int ruleId = 0;
            foreach(RuleItem rule in Rules) {
                rule.RuleId = ++ruleId;
            }

            string stringRuleInfo = JsonConvert.SerializeObject(this);
            File.WriteAllText(GetRuleFilePath(false), stringRuleInfo);
        }
    }

}
