using System;
using System.Linq;
using System.Threading.Tasks;

namespace DCM.Extensions
{
    public static class PluginExtensions
    {
        public static void InvokeMethod(this Plugin plugin, string methodName, object[] parameters = null)
        {
            var method = plugin.GetType().GetMethods().FirstOrDefault(method => method.Name == methodName);
            if (method == null) return; // The methods from the class PluginBase are virtual --> nullable
            else if (method.ReturnType == typeof(void))
                method.Invoke(plugin, parameters);
            else
                throw new NotSupportedException(string.Format("The return type {0} is not supported.", method.ReturnType.Name));
        }

        public static async Task InvokeMethodAsync(this Plugin plugin, string methodName, object[] parameters = null)
        {
            var method = plugin.GetType().GetMethods().FirstOrDefault(method => method.Name == methodName);
            if (method == null) return; // The methods from the class PluginBase are virtual --> nullable
            if (method.ReturnType == typeof(Task))
                await (Task)method.Invoke(plugin, parameters);
            else
                throw new NotSupportedException(string.Format("The return type {0} is not supported.", method.ReturnType.Name));
        }
    }
}
