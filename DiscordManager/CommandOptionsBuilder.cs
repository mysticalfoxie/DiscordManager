using DCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DCM
{
    public class CommandOptionsBuilder
    {
        private readonly CommandOptions _options = new();

        internal CommandOptionsBuilder() { }

        public CommandOptionsBuilder AddAlias(string alias)
        {
            _options.Aliases = _options.Aliases.Append(alias).ToArray();
            return this;
        }
        public CommandOptionsBuilder AddAliases(IEnumerable<string> aliases)
        {
            _options.Aliases = _options.Aliases.Concat(aliases).ToArray();
            return this;
        }

        public CommandOptionsBuilder Disabled(bool disabled = true)
        {
            _options.Disabled = disabled;
            return this;
        }

        public CommandOptionsBuilder NoPrefix()
        {
            _options.NoPrefix = true;
            return this;
        }

        public CommandOptionsBuilder OverrideIgnoreCasing(bool ignoreCasing = true)
        {
            _options.IgnoreCasingOverride = ignoreCasing;
            return this;
        }

        public CommandOptionsBuilder SetPermissions(Action<PermissionsBuilder> configure)
        {
            var permissionsBuilder = new PermissionsBuilder();
            configure(permissionsBuilder);
            _options.Permissions = permissionsBuilder.Build();
            return this;
        }

        public CommandOptions Build() => _options;
    }
}
