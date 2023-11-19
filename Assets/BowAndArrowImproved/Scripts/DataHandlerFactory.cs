public class DataHandlerFactory
{
    public static IDataHandler get()
    {
        return new JsonDataHandler();
    }
}