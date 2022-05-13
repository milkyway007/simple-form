namespace Domain
{
    public class User : Entity
    {
        public User()
        {
            SectorOptions = new List<UserSectorOption>();
        }

        public string Name { get; set; }
        public bool IsAgreed { get; set; }
        public ICollection<UserSectorOption> SectorOptions { get; set; }
    }
}
