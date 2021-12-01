using DCM.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DCM
{
    public class PermissionsBuilder
    {
        internal Collection<ulong> _channels = new();
        internal Collection<ulong> _roles = new();
        internal Collection<ulong> _guilds = new();
        internal Collection<ulong> _users = new();
        internal RestrictionStrategy _strategy;

        public PermissionsBuilder UseStrategy(RestrictionStrategy strategy)
        {
            _strategy = strategy;
            return this;
        }

        public PermissionsBuilder AddChannel(ulong channel)
            => AddChannels(new ulong[] { channel });
        public PermissionsBuilder AddChannels(IEnumerable<ulong> channels)
        {
            foreach (var channel in channels)
                _channels.Add(channel);

            return this;
        }

        public PermissionsBuilder AddUser(ulong user)
            => AddUsers(new ulong[] { user });
        public PermissionsBuilder AddUsers(IEnumerable<ulong> users)
        {
            foreach (var user in users)
                _users.Add(user);

            return this;
        }

        public PermissionsBuilder AddRole(ulong role)
            => AddRoles(new ulong[] { role });
        public PermissionsBuilder AddRoles(IEnumerable<ulong> roles)
        {
            foreach (var role in roles)
                _roles.Add(role);

            return this;
        }

        public PermissionsBuilder AddGuild(ulong guild)
            => AddGuilds(new ulong[] { guild });
        public PermissionsBuilder AddGuilds(IEnumerable<ulong> guilds)
        {
            foreach (var guild in guilds)
                _guilds.Add(guild);

            return this;
        }

        public Permissions Build()
            => new(_strategy, new(_guilds, _channels, _users, _roles));
    }
}
