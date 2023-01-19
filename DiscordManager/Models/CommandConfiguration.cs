namespace DCM.Models
{
    public class CommandConfiguration
    {
        public string Prefix { get; set; }
        public bool? IgnoreCasing { get; set; }
        public Command HelpCommand { get; set; }
    }
}
