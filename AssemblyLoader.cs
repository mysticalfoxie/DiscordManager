using DCM.Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DCM
{
    public class AssemblyLoader
    {
        public event Action<string> AssemblyLoad;
        public event Action<Exception> Error;

        public bool TryLoad(AssemblyName assemblyName, out Assembly assembly)
        {
            try
            {
                assembly = Assembly.LoadFrom(assemblyName.FullName);
                AssemblyLoad?.Invoke(assembly.FullName);

                return true;
            }
            catch (Exception ex)
            {
                Error?.Invoke(new PluginLoaderException($"Could not load the assembly '{assemblyName.FullName}'.", ex));
                assembly = null;

                return false;
            }
        }

        public IEnumerable<Assembly> LoadAll(AssemblyName[] assemblyFiles)
        {
            foreach (var file in assemblyFiles ?? Array.Empty<AssemblyName>())
                if (TryLoad(file, out var assembly))
                    yield return assembly;
        }
    }
}
