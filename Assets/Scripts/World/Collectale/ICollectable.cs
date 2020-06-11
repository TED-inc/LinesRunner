namespace TEDinc.LinesRunner
{
    /// <summary>
    /// collecatable items in game
    /// </summary>
    public interface ICollectable
    {
        string playerPrefsName { get; }
        bool collected { get; }
        /// <summary>
        /// call when item collected
        /// </summary>
        void Collect();  
    }
}