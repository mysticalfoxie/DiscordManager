namespace DCM.Models
{
    public class Permissions
    {
        public Permissions(RestrictionStrategy strategy, Restrictions restrictions)
        {
            Strategy = strategy;
            Restrictions = restrictions;
        }

        public RestrictionStrategy Strategy { get; }
        public Restrictions Restrictions { get; }
    }
}
