namespace ReleaseRetention.Models
{
    public class Release
    {
        public required string Id { get; set; }
        public required string ProjectId { get; set; }
        public required string Version { get; set; }
        public required DateTime Created { get; set; }

        // Navigation property
        internal Project? Project { get; private set; }

        // Setter for navigation property
        internal void SetProject(Project project)
        {
            Project = project;
        }
    }
}
