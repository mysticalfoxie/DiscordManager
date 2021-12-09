using System;
using System.Collections.Generic;

namespace DCM.Models
{
    public class Command
    {
        public string Name { get; set; }
        public CommandOptions Options { get; set; }
        public Permissions Permissions { get; set; }
        public List<CommandHandler> Handlers { get; } = new();
        internal List<Type> HandlerTypes { get; set; }
    }
}
