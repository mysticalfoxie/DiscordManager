using DCM.Events.Logging;
using DCM.Exceptions;
using DCM.Extensions;
using DCM.Interfaces;
using DCM.Models;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DCM;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace DCM
{
    internal interface IPluginManager
    {
        void LoadAll();
        Task InvokeInitialize();
        Task InvokeStart();
    }

    internal class PluginManager : IPluginManager
    {
        private readonly AssemblyLoader _assemblyLoader;
        private readonly Discord _discord;
        private readonly IList<FileInfo> _pluginLibraries;
        private readonly IList<Type> _pluginTypes;
        private readonly IList<Plugin> _plugins;
        private readonly DependencyContainer _dependencies;
        private readonly IEventEmitter _eventEmitter;

        public PluginManager(
            IList<FileInfo> pluginLibraries,
            IList<Type> pluginTypes,
            IList<Plugin> plugins,
            DependencyContainer dependencies,
            IEventEmitter eventEmitter,
            AssemblyLoader assemblyLoader,
            Discord discord)
        {
            _assemblyLoader = assemblyLoader;
            _pluginLibraries = pluginLibraries;
            _pluginTypes = pluginTypes;
            _plugins = plugins;
            _dependencies = dependencies;
            _eventEmitter = eventEmitter;
            _discord = discord;

            _assemblyLoader.AssemblyLoad += AssemblyLoader_AssemblyLoad;
            _assemblyLoader.Error += AssemblyLoader_Error;

            _dependencies.Services.AddSingleton<IDiscordClient>(prov => _discord.Client);
            _dependencies.Services.AddSingleton<IEventEmitter>(prov => _eventEmitter);
        }

        public void LoadAll()
        {
            // Locating
            var libraries = _pluginLibraries.ToArray();
            var types = _pluginTypes.ToArray();

            // Extraction
            var assemblies = _assemblyLoader.LoadAll(libraries);
            var pluginTypes = assemblies
                .SelectMany(assembly => ExtractPluginTypes(assembly))
                .Concat(types)
                .ToArray();

            // Loading
            var provider = RegisterPlugins(pluginTypes);
            foreach (var plugin in InstantiatePlugins(pluginTypes, provider))
                _plugins.Add(plugin);
        }

        public async Task InvokeInitialize()
        {
            foreach (var plugin in _plugins)
            {
                ExecuteMethodHandled(plugin, nameof(Plugin.Initialize));
                await ExecuteMethodHandledAsync(plugin, nameof(Plugin.InitializeAsync));
            }
        }

        public async Task InvokeStart()
        {
            foreach (var plugin in _plugins)
            {
                ExecuteMethodHandled(plugin, nameof(Plugin.Start));
                await ExecuteMethodHandledAsync(plugin, nameof(Plugin.StartAsync));
            }
        }

        private IEnumerable<Plugin> InstantiatePlugins(IEnumerable<Type> pluginTypes, IServiceProvider provider)
        {
            foreach (var pluginType in pluginTypes)
            {
                Plugin plugin;
                try
                {
                    plugin = (Plugin)provider.GetRequiredService(pluginType);
                }
                catch (Exception ex)
                {
                    _eventEmitter.Emit<ErrorEvent>(new(new PluginException($"An error occured when trying to instantiate the plugin '{pluginType.Name}'.", ex)));
                    continue;
                }
                yield return plugin;
            }
        }

        private IServiceProvider RegisterPlugins(IEnumerable<Type> pluginTypes)
        {
            foreach (var pluginType in pluginTypes)
                _dependencies.Services.AddSingleton(pluginType);

            return _dependencies.Services.BuildServiceProvider();
        }


        private void AssemblyLoader_AssemblyLoad(string filepath)
            => _eventEmitter?.Emit<TraceEvent>(new($"Loaded the assembly '{filepath}'."));

        private void AssemblyLoader_Error(Exception error)
            => _eventEmitter?.Emit<ErrorEvent>(new(error));

        private void ExecuteMethodHandled(Plugin plugin, string methodName, object[] parameters = null)
        {
            try
            {
                plugin.InvokeMethod(methodName, parameters);
            }
            catch (Exception ex)
            {
                _eventEmitter.Emit<ErrorEvent>(new(new PluginException($"An error occured in the plugin '{plugin.GetType().Name}' when invoking the method '{methodName}'.", ex)));
                plugin.IsRunning = false;
            }
        }

        private async Task ExecuteMethodHandledAsync(Plugin plugin, string methodName, object[] parameters = null)
        {
            try
            {
                await plugin.InvokeMethodAsync(methodName, parameters);
            }
            catch (Exception ex)
            {
                _eventEmitter.Emit<ErrorEvent>(new(new PluginException($"An error occured in the plugin '{plugin.GetType().Name}' when invoking the method '{methodName}'.", ex)));
                plugin.IsRunning = false;
            }
        }

        private static IEnumerable<Type> ExtractPluginTypes(Assembly assembly)
            => assembly.GetTypes()
                       .Where(type => type.IsClass)
                       .Where(@class => @class.IsPublic)
                       .Where(@class => !@class.IsAbstract)
                       .Where(@class => @class.BaseType == typeof(Plugin));
    }
}