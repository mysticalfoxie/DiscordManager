namespace DCM.Models
{
    public class CommandOptions
    {
        public string[] Aliases { get; internal set; }
        public bool Disabled { get; internal set; }
        public bool NoPrefix { get; internal set; }
        public bool? IgnoreCasingOverride { get; internal set; }
        public Permissions Permissions { get; internal set; }
    }
}
