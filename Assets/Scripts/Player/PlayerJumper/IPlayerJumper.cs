namespace TEDinc.LinesRunner
{
    /// <summary>
    /// controls player veritcal movement
    /// </summary>
    public interface IPlayerJumper
    {
        /// <summary>
        /// vertical position of player
        /// </summary>
        float addHeight { get; }
    }
}