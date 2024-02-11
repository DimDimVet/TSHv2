namespace Registrator
{
    public interface IConstruction
    {
        public int Hash { get; set; }
        public bool IsDead { get; set; }
        public int[] ChildrenHash { get; set; }
        public TypeObject TypeObject { get; set; }
    }
}

