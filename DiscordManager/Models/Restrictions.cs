using System.Collections.Generic;
using System.Linq;

namespace DCM.Models
{
    public class Restrictions
    {
        public Restrictions(
            IEnumerable<ulong> guilds,
            IEnumerable<ulong> channels,
            IEnumerable<ulong> users,
            IEnumerable<ulong> roles)
        {
            Guilds = guilds.ToArray();
            Channels = channels.ToArray();
            Users = users.ToArray();
            Roles = roles.ToArray();
        }

        public IReadOnlyCollection<ulong> Guilds { get; set; }
        public IReadOnlyCollection<ulong> Channels { get; set; }
        public IReadOnlyCollection<ulong> Users { get; set; }
        public IReadOnlyCollection<ulong> Roles { get; set; }
    }
}
