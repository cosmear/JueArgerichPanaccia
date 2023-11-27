using MyGame;
using System.Collections.Generic;
using System.Linq;

public interface IPooleable
{
    void Reset();
}
public class GenericPool<T> where T : IPooleable, new()
{
    private List<T> available = new List<T>();
    private List<T> inUse = new List<T>();

    public T GetObject()
    {
        T obj;
        if (available.Count > 0)
        {
            obj = available[0];
            available.RemoveAt(0);
        }
        else
        {
            obj = new T();
        }

        obj.Reset();
        inUse.Add(obj);
        return obj;
    }

    public void ReleaseObject(T obj)
    {
        inUse.Remove(obj);
        available.Add(obj);
    }
}
