namespace ReleaseRetention.Models
{
    public class Release
    {
        public required string Id { get; set; }
        public required string ProjectId { get; set; }
        public string? Version { get; set; }
        public required DateTime Created { get; set; }

        // Navigation properties
        internal Project? Project { get; set; }
        internal HashSet<Deployment> Deployments { get; set; } = [];
    }
}
