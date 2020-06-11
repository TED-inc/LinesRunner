namespace TEDinc.LinesRunner
{
    /// <summary>
    /// hold data and return random by its weight(as bigger weight -> bigger chanse to get it)
    /// </summary>
    public interface IWeightHolder
    {
        object GetNextByWeigth();
    }
}