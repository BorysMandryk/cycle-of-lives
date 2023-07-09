using System.Collections.Generic;
using UnityEngine;

public class FreezeTracker : MonoBehaviour
{
    [SerializeField] private int _maxFreezeNum;
    private Queue<GameObject> _freezes = new Queue<GameObject>();

    public int Count => _freezes.Count;

    public void AddFreeze(GameObject gameObject)
    {
        _freezes.Enqueue(gameObject);
        if (Count > _maxFreezeNum)
        {
            DequeueFreeze();
        }
    }

    public void DequeueFreeze()
    {
        GameObject go = _freezes.Dequeue();
        
        Destroy(go);
    }
}
