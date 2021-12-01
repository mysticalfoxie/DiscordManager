namespace DCM.Models
{
    internal class CommandConfiguration
    {
        public string Prefix { get; set; }
        public Command[] Commands { get; set; }
        public bool? IgnoreCasing { get; set; }
    }
}
