namespace QuakerZero
{
    public enum ScreenOption
    {
        None = 0,
        Console = 1,
        Debug = 2,
        QuakerUI = 3
    }
    /// <summary>
    /// Base class for standard Quake logger Logger/Logger.cs
    /// </summary>
    public class LoggerBase
    {
        protected bool IsLogging = false;
        public List<string> Log; //Log collection
        protected ILogger logger;
        /// <summary>
        /// Initialize the logger to its corresponding type
        /// </summary>
        /// <param name="screenOption"></param>
        public void UseLogger(ScreenOption screenOption){
            switch (screenOption){
                case ScreenOption.None: 
                    return;
                case ScreenOption.Console: logger = new ConsoleLogger();
                    break;
                case ScreenOption.Debug: logger = new DebugLogger();
                    break;
            }
            Log = new List<string>(); //Force no null Log when logging
            IsLogging = true;
        }
        public void UseLogger(ScreenOption screenOption, Type loggerType)
        {
            switch (screenOption)
            {
                case ScreenOption.None: 
                    return;
                case ScreenOption.QuakerUI: logger = (ILogger)Activator.CreateInstance(loggerType);
                    break;
            }
            Log = new List<string>(); //Force no null Log when logging
            IsLogging = true;
        }
        /// <summary>
        /// Clear logger logs and reset IsLogging
        /// </summary>
        public void Kill(){
            Log.Clear();
            IsLogging = false;
        }
        /// <summary>
        /// Save Log
        /// </summary>
        public void SaveLog(string path){
            throw new NotImplementedException();
        }
        /// <summary>
        /// Add the data to the Log collection
        /// </summary>
        /// <typeparam name="TId"></typeparam>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="id"></param>
        /// <param name="message"></param>
        protected void Add2Log<TId, TMessage>(TId id, TMessage message) { Log.Add($"<{id}> : {message}"); }
    }
}
