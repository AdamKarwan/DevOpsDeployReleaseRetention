namespace ReleaseRetention.Models
{
    public class Deployment
    {
        public required string Id { get; set; }
        public required string ReleaseId { get; set; }
        public required string EnvironmentId { get; set; }
        public required DateTime DeployedAt { get; set; }

        // Navigation properties
        internal Release? Release { get; set; }
        internal ReleaseEnvironment? Environment { get; set; }
    }
}
