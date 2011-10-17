namespace lsight.Extensibility
{
    public interface IFilterAddin
    {
        bool Allow(ITimestampedLine timestampedLine);
    }
}