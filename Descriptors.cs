namespace XMLTypeFinder
{
    public sealed class ItemDesc
    {
        public readonly string Id;
        public readonly ushort Type;
        public ItemDesc(string id, ushort type)
        {
            Id = id;
            Type = type;
        }
    }
}
