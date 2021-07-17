using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DCM
{
    public class AssemblyCollector
    {
        public AssemblyCollector(DirectoryInfo pluginFolder)                         : this(new DirectoryInfo[] { pluginFolder }, Array.Empty<FileInfo>()) { }
        public AssemblyCollector(params DirectoryInfo[] pluginFolders)               : this(pluginFolders, Array.Empty<FileInfo>()) { }
        public AssemblyCollector(params FileInfo[] pluginFiles)                      : this(Array.Empty<DirectoryInfo>(), pluginFiles) { }
        public AssemblyCollector(DirectoryInfo pluginFolder, FileInfo[] pluginFiles) : this(new DirectoryInfo[] { pluginFolder }, pluginFiles) { }
        public AssemblyCollector(DirectoryInfo[] pluginFolders, FileInfo[] pluginFiles)
        {
            var files = new List<FileInfo>();

            foreach (var pluginFolder in pluginFolders)
                files.AddRange(GetAssembliesRecursively(pluginFolder));

            files.AddRange(pluginFiles);

            Files = Treeshake(files);
        }

        public FileInfo[] Files { get; }
        public bool FilterRequired { get; set; } = true;

        private static FileInfo[] Treeshake(List<FileInfo> files)
        {
            var fileCollection = new Dictionary<string, FileInfo>();

            foreach (var file in files)
                if (!fileCollection.TryGetValue(file.FullName, out _))
                    fileCollection.Add(file.FullName, file);

            var filteredFiles = new FileInfo[fileCollection.Count];
            fileCollection.Values.CopyTo(filteredFiles, 0);

            return filteredFiles;
        }

        private static List<FileInfo> GetAssembliesRecursively(DirectoryInfo directory)
        {
            var assemblies = new List<FileInfo>();
            foreach (var subDirectory in directory.EnumerateDirectories())
                assemblies.AddRange(GetAssembliesRecursively(subDirectory));

            var directoriesAssemblies = directory.EnumerateFiles().Where(file => Path.GetExtension(file.FullName) == ".dll");
            assemblies.AddRange(directoriesAssemblies);

            return assemblies;
        }
    }
}
