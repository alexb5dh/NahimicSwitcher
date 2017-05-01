using System;
using System.Linq;

namespace NahimicSwitcher
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var nahimic = new Nahimic();

            if (args.Length == 0 || new[] { "/?", "-h", "-help", "--help" }.Contains(args[0]))
            {
                Console.WriteLine($"Nahimic is {(nahimic.Enabled ? "ON" : "OFF")}.");
                Console.WriteLine($"Current profile is \"{nahimic.CurrentProfile.Name}\".");

                Console.WriteLine("\nSupported profiles:");
                foreach (var profile in nahimic.Profiles)
                {
                    Console.WriteLine($"  {profile.Name}");
                }
            }

            else if (string.Equals(args[0], "on", StringComparison.OrdinalIgnoreCase))
            {
                nahimic.Enabled = true;
                Console.WriteLine("Nahimic turned ON.");
            }

            else if (string.Equals(args[0], "off", StringComparison.OrdinalIgnoreCase))
            {
                nahimic.Enabled = false;
                Console.WriteLine("Nahimic turned OFF.");
            }

            else if (string.Equals(args[0], "toogle", StringComparison.OrdinalIgnoreCase))
            {
                var enabled = (nahimic.Enabled = !nahimic.Enabled);
                Console.WriteLine($"Nahimic toogled {(enabled ? "ON" : "OFF")}.");
            }

            else
            {
                var profile = nahimic.Profiles.FirstOrDefault(p => string.Equals(p.Name, args[0], StringComparison.OrdinalIgnoreCase));
                if (profile == null)
                {
                    Console.WriteLine($"Profile \"{args[0]}\" is not present.");
                }
                else
                {
                    nahimic.CurrentProfile = profile;
                    Console.WriteLine($"Nahimic profile set to \"{profile.Name}\".");
                }
            }
        }
    }
}