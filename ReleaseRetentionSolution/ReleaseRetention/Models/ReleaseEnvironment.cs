namespace ReleaseRetention.Models
{
    public class ReleaseEnvironment
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        internal HashSet<Deployment> Deployments { get; set; } = [];
    }
}
