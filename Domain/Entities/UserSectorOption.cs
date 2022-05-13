namespace Domain
{
    public class UserSectorOption
    {

        public Guid UserId { get; set; }
        public Guid SectorOptionId { get; set; }
        public User User { get; set; }
        public SectorOption SectorOption { get; set; }
    }
}
