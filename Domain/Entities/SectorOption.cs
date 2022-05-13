namespace Domain
{
    public class SectorOption : Entity
    {
        public string Label { get; set; }
        public int Level { get; set; }
        public Guid? ParentId { get; set; }
        public SectorOption Parent { get; set; }
        public IEnumerable<SectorOption> Children { get; set; }
        public ICollection<UserSectorOption> Users { get; set; }
    }
}
