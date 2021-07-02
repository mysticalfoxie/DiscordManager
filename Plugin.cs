using System.Threading.Tasks;

namespace DCM
{
    public class Plugin
    {
        /// <summary>
        /// An indicator for the plugin manager if the plugin is still executed.
        /// If an error occures in the from DCM invoked Methods this Property is automatically false.
        /// </summary>
        public virtual bool IsRunning { get; internal set; } = true;

        /// <summary>
        /// The Initialize method is being called before Discord has started. 
        /// Useful operations would be event registrations, validation, ect. 
        /// 
        /// The Task is being awaited so the discord will not start until all plugins are initialized.
        /// </summary>
        public virtual Task InitializeAsync() => Task.CompletedTask;

        /// <summary>
        /// The Initialize method is being called before Discord has started. 
        /// Useful operations would be event registrations, validation, ect. 
        /// </summary>
        public virtual void Initialize() { }

        /// <summary>
        /// The Start Method gets called when the Discord Client is ready. 
        /// Here you can use all Discord functionalities!
        /// </summary>
        /// <returns></returns>
        public virtual void Start() { }

        /// <summary>
        /// The Start Method gets called when the Discord Client is ready. 
        /// Here you can use all Discord functionalities!
        /// </summary>
        /// <returns></returns>
        public virtual Task StartAsync() => Task.CompletedTask;
    }
}
