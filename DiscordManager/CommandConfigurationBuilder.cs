using DCM.Models;
using System;

namespace DCM
{
    public class CommandConfigurationBuilder
    {
        private readonly CommandConfiguration _configuration;

        internal CommandConfigurationBuilder(CommandConfiguration instance) 
        {
            _configuration = instance;
        }

        public CommandConfigurationBuilder UsePrefix(char prefix)
            => UsePrefix(prefix.ToString());
        public CommandConfigurationBuilder UsePrefix(string prefix)
        {
            _configuration.Prefix = prefix;
            return this;
        }

        public CommandConfigurationBuilder IgnoreCasing(bool ignoreCasing = true)
        {
            _configuration.IgnoreCasing = ignoreCasing;
            return this;
        }

        public CommandConfigurationBuilder ConfigureHelpCommand(Action<HelpCommandBuilder> configure)
        {
            var builder = new HelpCommandBuilder();
            configure(builder);
            _configuration.HelpCommand = builder.Build();
            return this;
        }

        internal CommandConfiguration Build() => _configuration;
    }
}
