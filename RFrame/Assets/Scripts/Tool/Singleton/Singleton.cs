public class Singleton<T> where T : new()
{
    private T instance;

    public T Instance => instance ??= new T();
}