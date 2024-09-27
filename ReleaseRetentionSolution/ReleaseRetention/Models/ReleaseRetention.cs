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
        /// Retains the specified number of releases for each environment and project combination, based on most recent deployment date.
        /// </summary>
        /// <param name="numberOfReleases">The number of releases to keep per environment/project</param>
        public List<Release> RetainReleases(int numberOfReleases)
        {
            InitializeRelationships();
            // keep track of releases with the reason for keeping them. we only need to keep one reason per release and dont want the same release in there multiple times
            var releasesToKeep = new Dictionary<Release, string>();
            // we need to know which releases have not been deployed at all for filling in the gaps if there are fewer deployments than the specified number of releases
            var undeployedReleases = Releases.Where(r => r.Deployments.Count == 0);

            // Loop through each environment and project to find the most recent deployments
            foreach (var environment in Environments)
            {
                foreach (var project in Projects)
                {
                    // sort the deployments by most recent
                    var deployments = Deployments
                        .Where(d =>
                            d.EnvironmentId == environment.Id && d?.Release?.ProjectId == project.Id
                        )
                        .OrderByDescending(d => d.DeployedAt);
                    var releases = deployments.Select(d => d.Release).Distinct();
                    // take the most recent releases up to the number specified
                    var releasesToKeepForEnvironmentProject = releases
                        .Take(numberOfReleases)
                        .ToList();

                    foreach (var release in releasesToKeepForEnvironmentProject)
                    {
                        if (release != null && !releasesToKeep.ContainsKey(release))
                        {
                            releasesToKeep.Add(
                                release,
                                $"{release.Id} kept because it was one of the most recent {numberOfReleases} deployments for {environment.Name} and {project.Name}"
                            );
                        }
                    }
                    // if there are extra spaces to fill for any environment/project, add the most recent releases that have not yet been deployed.
                    var numRemainingReleases =
                        numberOfReleases - releasesToKeepForEnvironmentProject.Count;

                    if (numRemainingReleases > 0)
                    {
                        var remainingReleases = undeployedReleases
                            .Where(r => r.ProjectId == project.Id && !releasesToKeep.ContainsKey(r))
                            .OrderByDescending(r => r.Created)
                            .Take(numRemainingReleases);
                        foreach (var release in remainingReleases)
                        {
                            if (!releasesToKeep.ContainsKey(release))
                            {
                                releasesToKeep.Add(
                                    release,
                                    $"{release.Id} kept because less than {numberOfReleases} releases were deployed to {project.Name}/{environment.Name} and this is one of the {numRemainingReleases} most recent releases that have not been deployed"
                                );
                            }
                        }
                    }
                }
            }
            // return the list of releases and log the reason for keeping them
            foreach (var release in releasesToKeep.Keys)
            {
                Console.WriteLine(releasesToKeep[release]);
            }
            return [.. releasesToKeep.Keys];
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
