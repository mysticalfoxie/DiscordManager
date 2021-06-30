using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DCM.Extensions
{
    public static class AssemblyLocationInfoExtensions
    {
        public static IEnumerable<FileInfo> FilterForPluginAssemblies(IEnumerable<FileInfo> assemblyFiles)
        {
            // TODO: Upgrade to this! :D
            // https://docs.microsoft.com/en-us/dotnet/standard/assembly/unloadability

            var domain = AppDomain.CreateDomain("assembly-plugin-filter");

            foreach (var assemblyFile in assemblyFiles)
            {
                var assemblyInfo = new AssemblyName(assemblyFile.Name) { CodeBase = assemblyFile.FullName };
                var assembly = domain.Load(assemblyInfo);
                if (assembly.GetTypes().Any(type => type.BaseType == typeof(Plugin)))
                    yield return assemblyFile;
            }

            AppDomain.Unload(domain);
        }
    }
}
