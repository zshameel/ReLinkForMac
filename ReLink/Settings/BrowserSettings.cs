using System.Collections;
using System.Collections.Generic;

namespace ReLink {
    static class BrowserSettings {

        public static string DefaultBrowserName {
            get {
                string defaultBrowser = "Safari";
                return defaultBrowser;
            }
            set {

            }
        }

        public static bool UseDefaultBrowserForAllLinks {
            get {
                return false;
            }
            set {

            }
        }

        public static List<Rule> Rules {
            get {
                return new List<Rule>() {
                    new Rule() { RuleId = 1, Url="https://google.com", MatchType=MatchType.Contains, BrowserName="Chrome" },
                    new Rule() { RuleId = 2, Url="https://github.com", MatchType=MatchType.Contains, BrowserName="Firefox" },
                    new Rule() { RuleId = 3, Url="http://shameel.net", MatchType=MatchType.Contains, BrowserName="Brave" },
                    new Rule() { RuleId = 4, Url="https://amazon.com", MatchType=MatchType.Contains, BrowserName="Safari" },
                    new Rule() { RuleId = 5, Url="https://microsoft.com", MatchType=MatchType.Contains, BrowserName="Edge" },
                    new Rule() { RuleId = 6, Url="https://ndtv.com", MatchType=MatchType.Contains, BrowserName="Edge" }
                };
            }
            set {

            }
        }

    }
}