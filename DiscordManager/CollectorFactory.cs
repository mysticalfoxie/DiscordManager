using DCM.Collectors;
using DCM.Interfaces;
using Discord;
using System;

namespace DCM
{
    internal class CollectorFactory
    {
        private readonly Discord _discord;

        public CollectorFactory(Discord discord)
        {
            _discord = discord;
        }

        public ICollector<TSource, TEvent> GetCollector<TSource, TEvent>(TSource source) 
            where TEvent : Event 
            where TSource : class
        {
            var collectorType = GetCollectorType(typeof(TSource));
            var collectorCtor = collectorType.GetConstructor(new Type[] { typeof(TSource), _discord.Client.GetType() });
            return (ICollector<TSource, TEvent>)collectorCtor.Invoke(new object[] { source, _discord.Client });
        }

        private static Type GetCollectorType(Type source)
        {
            if (typeof(IMessage).IsAssignableTo(source))
                return typeof(ReactionCollector);
            else
                throw new NotSupportedException($"Cannot build a collector for a source of type '{source.FullName}'.");
        }
    }
}
