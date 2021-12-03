using System;

namespace DCM.Models
{
    internal class CommandConfiguration
    {
        public string Prefix { get; set; }
        public Command[] Commands { get; set; } = Array.Empty<Command>();
        public bool? IgnoreCasing { get; set; }
    }
}
