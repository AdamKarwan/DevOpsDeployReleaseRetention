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
        private void InitializeRelationships()
        {
            // set up dictionaries for quick lookup of environments, projects and releases by id
            var environmentsById = Environments.ToDictionary(e => e.Id);
            var projectsById = Projects.ToDictionary(p => p.Id);
            var releasesById = Releases.ToDictionary(r => r.Id);

            // set up the relationships between the entities
            // we only need to loop through the deployments to set up most relationships. releases with no deployments will be handled separately
            foreach (var deployment in Deployments)
            {
                deployment.Environment = environmentsById[deployment.EnvironmentId];
                deployment.Environment.Deployments.Add(deployment);
                deployment.Release = releasesById[deployment.ReleaseId];
                deployment.Release.Deployments.Add(deployment);
                deployment.Release.Project = projectsById[deployment.Release.ProjectId];
                deployment.Release.Project.Releases.Add(deployment.Release);
            }
        }
    }
}
