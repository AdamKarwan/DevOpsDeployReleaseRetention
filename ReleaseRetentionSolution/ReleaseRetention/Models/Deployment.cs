namespace ReleaseRetention.Models
{
    public class Deployment
    {
        public required string Id { get; set; }
        public required string ReleaseId { get; set; }
        public required string EnvironmentId { get; set; }
        public required DateTime DeployedAt { get; set; }

        // Navigation properties
        internal Release? Release { get; private set; }
        internal Environment? Environment { get; private set; }

        // Setters for navigation properties
        internal void SetRelease(Release release)
        {
            Release = release;
        }

        internal void SetEnvironment(Environment environment)
        {
            Environment = environment;
        }
    }
}
