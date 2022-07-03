namespace DataAccess
{
    public interface IDataSaver
    {
        void SaveData(object data);

        bool TrySaveData(object data);
    }
}
