namespace GitlabInfo.Models
{
    public class JoinRequest
    {
        public int Id { get; set; }
        public User Requestee { get; set; }
        public Group RequestedGroup { get; set; }
    }
}
