﻿using System;
using System.Collections.Generic;
using System.Linq;
using LibYear.Lib.FileTypes;
using NuGet.Versioning;
using Xunit;

namespace LibYear.Lib.Tests.FileTypes
{
    public class ProjectJsonTests
    {
        [Fact]
        public void CanLoadProjectJsonFile()
        {
            //arrange
            const string filename = "FileTypes\\project.json";

            //act
            var file = new ProjectJsonFile(filename);

            //assert
            Assert.Equal("test1", file.Packages.First().Key);
            Assert.Equal("test2", file.Packages.Skip(1).First().Key);
        }

        [Fact]
        public void CanUpdateProjectJsonFile()
        {
            //arrange
            const string filename = "FileTypes\\project.json";
            var file = new ProjectJsonFile(filename);
            var results = new List<Result>
            {
                new Result("test1", new VersionInfo(new SemanticVersion(0, 1, 0), DateTime.Today), new VersionInfo(new SemanticVersion(1, 2, 3), DateTime.Today)),
                new Result("test2", new VersionInfo(new SemanticVersion(0, 2, 0), DateTime.Today), new VersionInfo(new SemanticVersion(2, 3, 4), DateTime.Today))
            };

            //act
            file.Update(results);

            //assert
            var newFile = new ProjectJsonFile(filename);
            Assert.Equal("1.2.3", newFile.Packages.First().Value.ToString());
            Assert.Equal("2.3.4", newFile.Packages.Skip(1).First().Value.ToString());
        }
    }
}