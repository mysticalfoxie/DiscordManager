namespace DCM.Models
{
    internal class CommandConfiguration
    {
        public string Prefix { get; internal set; }
        public bool? IgnoreCasing { get; internal set; }
        public Command HelpCommand { get; internal set; }
    }
}
