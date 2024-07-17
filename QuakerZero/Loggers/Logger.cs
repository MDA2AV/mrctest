namespace QuakerZero
{
    /// <summary>
    /// Logger singleton
    /// </summary>
    public class Logger : LoggerBase{
        #region Singleton boilerplate
        private Logger(){ } //private constructor
        private static Logger _instance; //singleton instance
        private static readonly object _lock = new object(); //locker
        /// <summary>
        /// Get Logger single instance or create a new one if none exists
        /// </summary>
        /// <returns>Logger</returns>
        public static Logger GetInstance(){
            if (_instance == null){
                lock (_lock){
                    if (_instance == null){ _instance = new Logger(); }
                }
            }
            return _instance;
        }
        #endregion
        /// <summary>
        /// Display message to the screen
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        public void Screen(Guid id, string message){
            if (!IsLogging) return; //Not logging
            logger.WriteLine(id, message);
            Add2Log(id, message);
        }
        /// <summary>
        /// Display message to the screen
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        public void Screen(string id, string message){
            if (!IsLogging) return; //Not logging
            logger.WriteLine(id, message);
            Add2Log(id, message);
        }
        /// <summary>
        /// Display message to the screen
        /// </summary>
        /// <param name="message"></param>
        public void Screen(string message){
            if (!IsLogging) return; //Not logging
            logger.WriteLine("", message);
            Add2Log("", message);
        }
    }
}
