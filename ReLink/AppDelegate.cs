using AppKit;
using Foundation;

namespace ReLink {
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        public AppDelegate()
        {
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            // Construct menu that will be displayed when tray icon is clicked
            var notifyMenu = new NSMenu();
            var exitMenuItem = new NSMenuItem("Quit", (a, b) => { System.Environment.Exit(0); });
            notifyMenu.AddItem(exitMenuItem);

            // Display tray icon in upper-right-hand corner of the screen
            var sItem = NSStatusBar.SystemStatusBar.CreateStatusItem(30);
            sItem.Menu = notifyMenu;
            sItem.Image = NSImage.FromStream(System.IO.File.OpenRead(NSBundle.MainBundle.ResourcePath + @"/app_icon.png"));
            sItem.HighlightMode = true;

            // Remove the system tray icon from upper-right hand corner of the screen
            // (works without adjusting the LSUIElement setting in Info.plist)
            NSApplication.SharedApplication.ActivationPolicy = NSApplicationActivationPolicy.Accessory;
        }

        [Export("application:openURLs:")]
        public override void OpenUrls(NSApplication application, NSUrl[] urls) {
            MainClass.NoUI = true;

            foreach (NSUrl url in urls) {
                BrowserManager.LaunchUrl(url.ToString());
            }
        }

        public override void WillTerminate(NSNotification notification) {
            // Insert code here to tear down your application
        }

    }
}
