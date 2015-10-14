using UnityEngine;

public abstract class Script : MonoBehaviour
{
    public readonly int Id = GetId();
  
    public T Get<T>() where T : MonoBehaviour
    {
        return GetComponent<T>();
    }

    public static T Find<T>() where T : MonoBehaviour
    {
        return FindObjectOfType<T>();
    }

    private static int _id = 55555;

    private static int GetId()
    {
        return ++_id;
    }
}