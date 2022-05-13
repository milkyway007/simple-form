namespace Application.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsAgreed { get; set; }
        public IEnumerable<Guid> SectorOptionIds { get; set; }
    }
}
