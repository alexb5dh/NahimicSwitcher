using System;
using System.Linq;
using System.Reflection;
using MSI_Command_Center.Controls.TuneMgr.SetFunction;

namespace NahimicSwitcher
{
    internal static class Program
    {
        static Program()
        {
            // Handle MSI Command Center renaming
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                if (args.Name.StartsWith("MSI Command Center", StringComparison.OrdinalIgnoreCase))
                {
                    return Assembly.LoadFrom("Dragon Center.exe");
                }
                return null;
            };
        }

        private static void Main(string[] args)
        {
            var nahimic = new Nahimic2();

            if (args.Length == 0 || new[] { "/?", "-h", "-help", "--help" }.Contains(args.First()))
            {
                Console.WriteLine($"Nahimic is {(nahimic.GetGlobalOnOff() ? "ON" : "OFF")}.");

                Console.WriteLine("Supported profiles:");
                foreach (var profileName in Enum.GetNames(typeof(AudioType))
                    .Except(new[] { "null" }, StringComparer.OrdinalIgnoreCase))
                {
                    Console.WriteLine(profileName);
                }
            }

            else if (string.Equals(args.First(), "on", StringComparison.OrdinalIgnoreCase))
            {
                nahimic.SetGlobalOnOff(true);
                Console.WriteLine("Nahimic turned ON.");
            }

            else if (string.Equals(args.First(), "off", StringComparison.OrdinalIgnoreCase))
            {
                nahimic.SetGlobalOnOff(false);
                Console.WriteLine("Nahimic turned OFF.");
            }

            else if (string.Equals(args.First(), "toogle", StringComparison.OrdinalIgnoreCase))
            {
                var oldProfile = nahimic.GetGlobalOnOff();
                nahimic.SetGlobalOnOff(!oldProfile);
                Console.WriteLine($"Nahimic toogled {(!oldProfile ? "ON" : "OFF")}.");
            }

            else
            {
                var profile = (AudioType) Enum.Parse(typeof(AudioType), args[0], ignoreCase: true);
                nahimic.SetProfile(profile);
                Console.WriteLine($"Nahimic profile set to {profile}.");
            }
        }
    }
}