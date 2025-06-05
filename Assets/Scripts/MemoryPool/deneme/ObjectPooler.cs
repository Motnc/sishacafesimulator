using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    private Queue<GameObject> _pool = new();

    private void Awake() => Instance = this;

    public GameObject Get(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (_pool.Count > 0)
        {
            var obj = _pool.Dequeue();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return Instantiate(prefab, position, rotation);
        }
    }

    public void Release(GameObject obj)
    {
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }
}

