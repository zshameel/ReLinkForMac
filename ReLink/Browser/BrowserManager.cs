using System;
using System.IO;
using System.Linq;
using AppKit;
using Foundation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ReLink {
    internal class BrowserManager {
        private static RulesManager RulesManager { get; set; }
        internal static BrowserInfo[] Browsers { get; private set; }

        static BrowserManager() {
            InitBrowsers();
            RulesManager = new RulesManager();
        }

        private static void InitBrowsers() {
            var content = string.Empty;

            using (var textStreamReader = File.OpenText(NSBundle.MainBundle.ResourcePath + @"/Config/Browsers.json")) {
                content = textStreamReader.ReadToEnd();
            }

            Browsers = JsonConvert.DeserializeObject<BrowserInfo[]>(content);
        }

        static internal void LaunchUrl(string url) {
            try {
                NSWorkspace.SharedWorkspace.OpenUrls(
                    new Foundation.NSUrl[] { new Foundation.NSUrl(url) },
                    GetBrowserForUrl(url).BundleId,
                    NSWorkspaceLaunchOptions.WithoutActivation | NSWorkspaceLaunchOptions.Hide,
                    new Foundation.NSAppleEventDescriptor(),
                    new string[0]
                );
            } catch(Exception) {

            }
        }

        static internal void LaunchUrlWithBrowser(string url, string browserName) {
            url = GetSanitizedUrl(url);
        }

        static BrowserInfo GetBrowserForUrl(string url) {
            if (BrowserSettings.UseDefaultBrowserForAllLinks) {
                return GetBrowserByName(BrowserSettings.DefaultBrowserName);
            }

            url = GetSanitizedUrl(url);

            Rule rule = RulesManager.Rules.FirstOrDefault(r => r.IsMatch(url));

            if (rule != null) {
                BrowserInfo browser = GetBrowserByName(rule.BrowserName);
                if (browser == null) {
                    return GetBrowserByName(BrowserSettings.DefaultBrowserName);
                } else {
                    return browser;
                }
            } else {
                return GetBrowserByName(BrowserSettings.DefaultBrowserName); ;
            }
        }

        internal static BrowserInfo GetBrowserByName(string browserName) {
            return Browsers.FirstOrDefault(b => b.Name.Equals(browserName, StringComparison.OrdinalIgnoreCase));
        }

        internal static string GetSanitizedUrl(string url) {
            url = url.Trim();
            while (url.EndsWith("/")) {
                url = url.Substring(0, url.Length - 1);
            }

            return url;
        }
    }
}