using DCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DCM
{
    public class CommandOptionsBuilder
    {
        private readonly ICollection<string> _aliases = Array.Empty<string>();
        private bool _hidden;
        private bool _disabled;
        private bool _requiresPrefixOverride;
        private bool _ignoreCasingOverride;

        public CommandOptionsBuilder AddAlias(string alias)
            => AddAliases(new string[] { alias });
        public CommandOptionsBuilder AddAliases(IEnumerable<string> aliases)
        {
            foreach (var alias in aliases)
                _aliases.Add(alias);
            return this;
        }

        public CommandOptionsBuilder Hidden(bool hidden = true)
        {
            _hidden = hidden;
            return this;
        }

        public CommandOptionsBuilder Disabled(bool disabled = true)
        {
            _disabled = disabled;
            return this;
        }

        public CommandOptionsBuilder OverrideRequiresPrefix(bool requiresPrefix = true)
        {
            _requiresPrefixOverride = requiresPrefix;
            return this;
        }

        public CommandOptionsBuilder OverrideIgnoreCasing(bool ignoreCasing = true)
        {
            _ignoreCasingOverride = ignoreCasing;
            return this;
        }

        public CommandOptions Build()
            => new(_aliases.ToArray(), _hidden, _disabled, _requiresPrefixOverride, _ignoreCasingOverride);
    }
}
