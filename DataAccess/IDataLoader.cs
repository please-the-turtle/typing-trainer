namespace DataAccess
{
    public interface IDataLoader<T>
    {
        T LoadData();

        bool TryLoadData(out T loadedData);
    }
}
