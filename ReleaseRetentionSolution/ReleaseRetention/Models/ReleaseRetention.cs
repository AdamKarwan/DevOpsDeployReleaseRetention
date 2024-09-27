namespace ReleaseRetention.Models
{
    /// <summary>
    /// The main class for managing release retention.
    /// </summary>
    public class ReleaseRetention
    {
        public List<Project> Projects { get; set; }
        public List<Environment> Environments { get; set; }
        public List<Deployment> Deployments { get; set; }
        public List<Release> Releases { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseRetention"/> class.
        /// </summary>
        public ReleaseRetention()
        {
            Projects = [];
            Environments = [];
            Deployments = [];
            Releases = [];
        }

        /// <summary>
        /// Retains the specified number of releases for each environment, based on most recent deployment date.
        /// </summary>
        /// <param name="releasesToKeep">The number of releases to keep per environment</param>
        /// <exception cref="NotImplementedException"></exception>
        public void RetainReleases(int releasesToKeep)
        {
            InitializeRelationships();
            // Implement the logic to retain the specified number of releases
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the navigation properties between the entities where necessary.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void InitializeRelationships()
        {
            throw new NotImplementedException();
        }
    }
}
