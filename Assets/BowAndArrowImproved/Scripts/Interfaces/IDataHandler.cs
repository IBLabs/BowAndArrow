public interface IDataHandler
{
    public T LoadData<T>(string asset);
    public void SaveData<T>(string fileName, T data);
}