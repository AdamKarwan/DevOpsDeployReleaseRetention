namespace ReleaseRetention.Models
{
    public class Project
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        internal HashSet<Release> Releases { get; set; } = [];
    }
}
