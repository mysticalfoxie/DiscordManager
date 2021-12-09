using System;

namespace DCM.Models
{
    public class CommandOptions
    {
        public CommandOptions(
            string[] aliases, 
            // bool hidden, 
            bool disabled, 
            bool? requiresPrefixOverride, 
            bool? ignoreCasingOverride)
        {
            Aliases = aliases ?? Array.Empty<string>();
            // Hidden = hidden;
            Disabled = disabled;
            RequiresPrefixOverride = requiresPrefixOverride;
            IgnoreCasingOverride = ignoreCasingOverride;
        }

        public string[] Aliases { get; }
        // public bool Hidden { get; }
        public bool Disabled { get; }
        public bool? RequiresPrefixOverride { get; }
        public bool? IgnoreCasingOverride { get; }
    }
}
