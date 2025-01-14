﻿using Microsoft.AspNetCore.Hosting;

namespace Application.Commons.UploadHandlers.UploadHelpers
{
    public interface IPathFinder
    {
        string GetAssetsFolder();
    }
    public class PathFinder : IPathFinder
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public PathFinder(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public string GetAssetsFolder()
        {
            return _hostingEnvironment.WebRootPath;
        }
    }
    public interface IPathHandler
    {
        string MakePath(string directory);
        string CombineRootAndDirectory(string directory);
        string CombineRootAndSubDirectories(string root, List<string> subDirectories);
    }

    public class PathHandler : IPathHandler
    {
        private readonly IPathFinder _pathFinder;

        public PathHandler(IPathFinder pathFinder)
        {
            _pathFinder = pathFinder;
        }

        public string MakePath(string directory)
        {
            var assets = _pathFinder.GetAssetsFolder();
            return Path.Combine(assets, directory);
        }

        public string CombineRootAndDirectory(string directory)
        {

            return Path.Combine(_pathFinder.GetAssetsFolder(), directory);
        }

        public string CombineRootAndSubDirectories(string root, List<string> subDirectories)
        {
            if (!subDirectories.Any())
            {
                return Path.Combine(_pathFinder.GetAssetsFolder(), root);
            }

            var path = subDirectories.Aggregate(root, Path.Combine);
            var assets = _pathFinder.GetAssetsFolder();
            return Path.Combine(assets, path);
        }

    }
}
