# DevOpsDeployReleaseRetention

This library provides functionality for managing release retention in a DevOps environment. It helps in retaining a specified number of releases for each environment and project combination based on the most recent deployment date.

It returns a list of which releases to keep, and logs to the console the reason for keeping them.

## Usage

To retain a specified number of releases, use the `RetainReleases` method on an instance of the `ReleaseRetainer` class. This method will ensure that only the specified number of most recent releases are kept for each environment and project combination.

Example:

```csharp
var releaseRetainer = new ReleaseRetainer
{
    Projects = new List<Project>
    {
        new Project { Id = "1", Name = "Project1" }
    },
    Environments = new List<Environment>
    {
        new Environment { Id = "1", Name = "Environment1" }
    },
    Releases = new List<Release>
    {
        new Release { Id = "1", ProjectId = "1", Version = "1.0", Created = DateTime.Now.AddDays(-10) },
        new Release { Id = "2", ProjectId = "1", Version = "1.1", Created = DateTime.Now.AddDays(-5) },
        new Release { Id = "3", ProjectId = "1", Version = "1.2", Created = DateTime.Now.AddDays(-1) }
    },
    Deployments = new List<Deployment>
    {
        new Deployment { Id = "1", ReleaseId = "1", EnvironmentId = "1", DeployedAt = DateTime.Now.AddDays(-9) },
        new Deployment { Id = "2", ReleaseId = "2", EnvironmentId = "1", DeployedAt = DateTime.Now.AddDays(-4) }
    }
};

var retainedReleases = releaseRetainer.RetainReleases(2);
```

In this example, the RetainReleases method is called on an instance of the ReleaseRetainer class to retain the specified number of releases.

## Assumptions

- assuming that the deployments, releases, projects and environments are to be supplied as the Classes I created (rather than reading directly from the json files).
- assuming that if an environment does not have any deployments, the most recently created releases are kept.
- assuming that if a release was the most recently deployed in multiple environments, the reason only needs to be logged once.
- assuming that the excess releases don't actually need to be removed by this class, it just returns and logs which ones to keep.
