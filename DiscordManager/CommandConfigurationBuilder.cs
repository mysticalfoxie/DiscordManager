using DCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DCM
{
    public class CommandConfigurationBuilder
    {
        private readonly CommandConfiguration _configuration = new();

        internal CommandConfigurationBuilder() { }

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
