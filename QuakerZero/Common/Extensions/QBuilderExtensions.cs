namespace QuakerZero
{
    public static class IQuakerBuilderExtensions
    {
        /// <summary>
        /// Set logger!
        /// </summary>
        /// <param name="app"></param>
        /// <param name="screenOption"></param>
        public static void UseLogger(this QBuilder builder, ScreenOption screenOption) =>
            Logger.GetInstance().UseLogger(screenOption);
    }
}
