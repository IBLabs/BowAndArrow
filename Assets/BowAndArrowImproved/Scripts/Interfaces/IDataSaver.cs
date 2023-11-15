public interface IDataSaver
{
    void SaveData<T>(string fileName, T data);
}