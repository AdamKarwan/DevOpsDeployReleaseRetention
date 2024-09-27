using System;
using System.Collections.Generic;
using System.Linq;
using ReleaseRetention.Models;
using Xunit;

namespace ReleaseRetention.Tests
{
    public class ReleaseRetentionTests
    {
        [Fact]
        public void RetainReleases_ShouldRetainSpecifiedNumberOfReleases()
        {
            // Arrange
            var releaseRetention = new ReleaseRetainer
            {
                Projects = [new() { Id = "1", Name = "Project1" }],
                Environments = [new() { Id = "1", Name = "Environment1" }],
                Releases =
                [
                    new()
                    {
                        Id = "1",
                        ProjectId = "1",
                        Version = "1.0",
                        Created = DateTime.Now.AddDays(-10),
                    },
                    new()
                    {
                        Id = "2",
                        ProjectId = "1",
                        Version = "1.1",
                        Created = DateTime.Now.AddDays(-5),
                    },
                    new()
                    {
                        Id = "3",
                        ProjectId = "1",
                        Version = "1.2",
                        Created = DateTime.Now.AddDays(-1),
                    },
                ],
                Deployments =
                [
                    new()
                    {
                        Id = "1",
                        ReleaseId = "1",
                        EnvironmentId = "1",
                        DeployedAt = DateTime.Now.AddDays(-9),
                    },
                    new()
                    {
                        Id = "2",
                        ReleaseId = "2",
                        EnvironmentId = "1",
                        DeployedAt = DateTime.Now.AddDays(-4),
                    },
                    new()
                    {
                        Id = "3",
                        ReleaseId = "3",
                        EnvironmentId = "1",
                        DeployedAt = DateTime.Now.AddDays(-2),
                    },
                ],
            };

            // Act
            var retainedReleases = releaseRetention.RetainReleases(2);

            // Assert
            Assert.Equal(2, retainedReleases.Count);
            Assert.Contains(retainedReleases, r => r.Id == "2");
            Assert.Contains(retainedReleases, r => r.Id == "3");
        }

        [Fact]
        public void RetainReleases_ShouldIncludeUndeployedReleasesIfNeeded()
        {
            // Arrange
            var releaseRetention = new ReleaseRetainer
            {
                Projects = [new() { Id = "1", Name = "Project1" }],
                Environments = [new() { Id = "1", Name = "Environment1" }],
                Releases =
                [
                    new()
                    {
                        Id = "1",
                        ProjectId = "1",
                        Version = "1.0",
                        Created = DateTime.Now.AddDays(-10),
                    },
                    new()
                    {
                        Id = "2",
                        ProjectId = "1",
                        Version = "1.1",
                        Created = DateTime.Now.AddDays(-5),
                    },
                    new()
                    {
                        Id = "3",
                        ProjectId = "1",
                        Version = "1.2",
                        Created = DateTime.Now.AddDays(-1),
                    },
                ],
                Deployments =
                [
                    new()
                    {
                        Id = "1",
                        ReleaseId = "1",
                        EnvironmentId = "1",
                        DeployedAt = DateTime.Now.AddDays(-9),
                    },
                ],
            };

            // Act
            var retainedReleases = releaseRetention.RetainReleases(2);

            // Assert
            Assert.Equal(2, retainedReleases.Count);
            Assert.Contains(retainedReleases, r => r.Id == "1");
            Assert.Contains(retainedReleases, r => r.Id == "3");
        }

        [Fact]
        public void RetainReleases_ShouldNotRetainMoreThanSpecifiedNumber()
        {
            // Arrange
            var releaseRetention = new ReleaseRetainer
            {
                Projects = [new() { Id = "1", Name = "Project1" }],
                Environments = [new() { Id = "1", Name = "Environment1" }],
                Releases =
                [
                    new()
                    {
                        Id = "1",
                        ProjectId = "1",
                        Version = "1.0",
                        Created = DateTime.Now.AddDays(-10),
                    },
                    new()
                    {
                        Id = "2",
                        ProjectId = "1",
                        Version = "1.1",
                        Created = DateTime.Now.AddDays(-5),
                    },
                    new()
                    {
                        Id = "3",
                        ProjectId = "1",
                        Version = "1.2",
                        Created = DateTime.Now.AddDays(-1),
                    },
                ],
                Deployments =
                [
                    new()
                    {
                        Id = "1",
                        ReleaseId = "1",
                        EnvironmentId = "1",
                        DeployedAt = DateTime.Now.AddDays(-9),
                    },
                    new()
                    {
                        Id = "2",
                        ReleaseId = "2",
                        EnvironmentId = "1",
                        DeployedAt = DateTime.Now.AddDays(-4),
                    },
                    new()
                    {
                        Id = "3",
                        ReleaseId = "3",
                        EnvironmentId = "1",
                        DeployedAt = DateTime.Now.AddDays(-2),
                    },
                ],
            };

            // Act
            var retainedReleases = releaseRetention.RetainReleases(2);

            // Assert
            Assert.Equal(2, retainedReleases.Count);
        }

        [Fact]
        public void RetainReleases_ShouldKeepReleasesPerEnvironmentAndProject()
        {
            // Arrange
            var releaseRetention = new ReleaseRetainer
            {
                Projects =
                [
                    new() { Id = "1", Name = "Project1" },
                    new() { Id = "2", Name = "Project2" },
                ],
                Environments =
                [
                    new() { Id = "1", Name = "Environment1" },
                    new() { Id = "2", Name = "Environment2" },
                ],
                Releases =
                [
                    new()
                    {
                        Id = "1",
                        ProjectId = "1",
                        Version = "1.0",
                        Created = DateTime.Now.AddDays(-10),
                    },
                    new()
                    {
                        Id = "2",
                        ProjectId = "1",
                        Version = "1.1",
                        Created = DateTime.Now.AddDays(-5),
                    },
                    new()
                    {
                        Id = "3",
                        ProjectId = "1",
                        Version = "1.2",
                        Created = DateTime.Now.AddDays(-1),
                    },
                    new()
                    {
                        Id = "4",
                        ProjectId = "2",
                        Version = "1.0",
                        Created = DateTime.Now.AddDays(-10),
                    },
                    new()
                    {
                        Id = "5",
                        ProjectId = "2",
                        Version = "1.1",
                        Created = DateTime.Now.AddDays(-5),
                    },
                    new()
                    {
                        Id = "6",
                        ProjectId = "2",
                        Version = "1.2",
                        Created = DateTime.Now.AddDays(-1),
                    },
                ],
                Deployments =
                [
                    new()
                    {
                        Id = "1",
                        ReleaseId = "1",
                        EnvironmentId = "1",
                        DeployedAt = DateTime.Now.AddDays(-9),
                    },
                    new()
                    {
                        Id = "2",
                        ReleaseId = "2",
                        EnvironmentId = "1",
                        DeployedAt = DateTime.Now.AddDays(-4),
                    },
                    new()
                    {
                        Id = "3",
                        ReleaseId = "3",
                        EnvironmentId = "1",
                        DeployedAt = DateTime.Now.AddDays(-2),
                    },
                    new()
                    {
                        Id = "4",
                        ReleaseId = "4",
                        EnvironmentId = "2",
                        DeployedAt = DateTime.Now.AddDays(-9),
                    },
                    new()
                    {
                        Id = "5",
                        ReleaseId = "5",
                        EnvironmentId = "2",
                        DeployedAt = DateTime.Now.AddDays(-4),
                    },
                    new()
                    {
                        Id = "6",
                        ReleaseId = "6",
                        EnvironmentId = "2",
                        DeployedAt = DateTime.Now.AddDays(-2),
                    },
                ],
            };
            var numberOfReleases = 2;
            var retainedReleases = releaseRetention.RetainReleases(numberOfReleases);
            Assert.Equal(4, retainedReleases.Count);
            Assert.Contains(retainedReleases, r => r.Id == "2");
            Assert.Contains(retainedReleases, r => r.Id == "3");
            Assert.Contains(retainedReleases, r => r.Id == "5");
            Assert.Contains(retainedReleases, r => r.Id == "6");
        }

        [Fact]
        public void RetainReleases_ShouldNotReturnDuplicateReleases()
        {
            // 1 release, deployed to 2 environments
            // Arrange
            var releaseRetention = new ReleaseRetainer
            {
                Projects = [new() { Id = "1", Name = "Project1" }],
                Environments =
                [
                    new() { Id = "1", Name = "Environment1" },
                    new() { Id = "2", Name = "Environment2" },
                ],
                Releases =
                [
                    new()
                    {
                        Id = "1",
                        ProjectId = "1",
                        Version = "1.0",
                        Created = DateTime.Now.AddDays(-10),
                    },
                ],
                Deployments =
                [
                    new()
                    {
                        Id = "1",
                        ReleaseId = "1",
                        EnvironmentId = "1",
                        DeployedAt = DateTime.Now.AddDays(-9),
                    },
                    new()
                    {
                        Id = "2",
                        ReleaseId = "1",
                        EnvironmentId = "2",
                        DeployedAt = DateTime.Now.AddDays(-4),
                    },
                ],
            };
            var numberOfReleases = 1;
            var retainedReleases = releaseRetention.RetainReleases(numberOfReleases);
            Assert.Single(retainedReleases);
        }

        [Fact]
        public void RetainReleases_ShouldReturnEmptyListIfNoReleases()
        {
            // Arrange
            var releaseRetention = new ReleaseRetainer
            {
                Projects = [new() { Id = "1", Name = "Project1" }],
                Environments = [new() { Id = "1", Name = "Environment1" }],
            };
            var numberOfReleases = 1;
            var retainedReleases = releaseRetention.RetainReleases(numberOfReleases);
            Assert.Empty(retainedReleases);
        }

        [Fact]
        public void RetainReleases_ShouldLogReasonsForRetaining()
        {
            // Arrange
            var releaseRetention = new ReleaseRetainer
            {
                Projects = [new() { Id = "1", Name = "Project1" }],
                Environments = [new() { Id = "1", Name = "Environment1" }],
                Releases =
                [
                    new()
                    {
                        Id = "1",
                        ProjectId = "1",
                        Version = "1.0",
                        Created = DateTime.Now.AddDays(-10),
                    },
                    new()
                    {
                        Id = "2",
                        ProjectId = "1",
                        Version = "1.1",
                        Created = DateTime.Now.AddDays(-5),
                    },
                    new()
                    {
                        Id = "3",
                        ProjectId = "1",
                        Version = "1.2",
                        Created = DateTime.Now.AddDays(-1),
                    },
                ],
                Deployments =
                [
                    new()
                    {
                        Id = "1",
                        ReleaseId = "1",
                        EnvironmentId = "1",
                        DeployedAt = DateTime.Now.AddDays(-9),
                    },
                    new()
                    {
                        Id = "2",
                        ReleaseId = "2",
                        EnvironmentId = "1",
                        DeployedAt = DateTime.Now.AddDays(-4),
                    },
                    new()
                    {
                        Id = "3",
                        ReleaseId = "3",
                        EnvironmentId = "1",
                        DeployedAt = DateTime.Now.AddDays(-2),
                    },
                ],
            };

            // Capture the console output
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            var retainedReleases = releaseRetention.RetainReleases(2);

            // make sure the console output is not empty
            Assert.NotEmpty(stringWriter.ToString());

            // Make sure there are exactly 2 lines printed
            var lines = stringWriter
                .ToString()
                .Split(Environment.NewLine)
                .Select(l => l.Trim())
                .Where(l => !string.IsNullOrEmpty(l))
                .ToArray();
            Assert.Equal(2, lines.Length);
            // Make sure each line contains 'kept because'
            Assert.All(lines, l => Assert.Contains("kept because", l));
        }
    }
}
