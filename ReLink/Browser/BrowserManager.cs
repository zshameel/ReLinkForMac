using System;
using System.IO;
using System.Linq;
using AppKit;
using Foundation;
using Newtonsoft.Json;

namespace ReLink {
    internal class BrowserManager {
        internal static BrowserInfo[] Browsers { get; private set; }

        static BrowserManager() {
            InitBrowsers();
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
                BrowserInfo browser = GetBrowserForUrl(url);

                NSWorkspace.SharedWorkspace.OpenUrls(
                    new NSUrl[] { new NSUrl(url) },
                    browser.BundleId,
                    NSWorkspaceLaunchOptions.Default,
                    new NSAppleEventDescriptor(),
                    new string[0]
                );

                var notification = new NSUserNotification {
                    Title = "Re:Link",
                    InformativeText = $"{url} was opened with {browser.Name}",
                    SoundName = NSUserNotification.NSUserNotificationDefaultSoundName,
                    HasActionButton = false
                };
                NSUserNotificationCenter.DefaultUserNotificationCenter.DeliverNotification(notification);

            } catch(Exception) {

            }
        }

        static internal void LaunchUrlWithBrowser(string url, string browserName) {
            url = GetSanitizedUrl(url);
        }

        static BrowserInfo GetBrowserForUrl(string url) {
            url = GetSanitizedUrl(url);
            RuleInfo rulesManager = RuleInfo.GetInstance();

            try {
                if (!rulesManager.UseFallbackBrowserForAllLinks) {
                    RuleItem rule = rulesManager.Rules.FirstOrDefault(r => r.IsMatch(url));

                    if (rule != null) {
                        BrowserInfo browser = GetBrowserByName(rule.BrowserName);
                        if (browser != null) {
                            return browser;
                        }
                    }
                }
            } catch (Exception ex) {
                NSAlert alert = new NSAlert() {
                    InformativeText = ex.Message,
                    MessageText = "An error occured while resolving a Browser for the url." };
                alert.RunModal();
            }

            return GetBrowserByName(rulesManager.FallbackBrowserName);
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