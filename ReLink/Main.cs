using System;
using AppKit;

namespace ReLink
{
    static class MainClass {
        internal static bool NoUI { get; set; }

        static void Main(string[] args)
        {
            NSApplication.Init();
            NSApplication.Main(args);
        }
    }
}
