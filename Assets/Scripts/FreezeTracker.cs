using System.Collections.Generic;
using UnityEngine;

public class FreezeTracker
{
    private Queue<GameObject> _freezes = new Queue<GameObject>();
    private int _maxFreezeNum;

    public int Count => _freezes.Count;

    public FreezeTracker(int maxFreezeNum)
    {
        _maxFreezeNum = maxFreezeNum;
    }

    public void AddFreeze(GameObject gameObject)
    {
        if (Count > _maxFreezeNum)
        {
            DequeueFreeze();
        }
        _freezes.Enqueue(gameObject);
    }

    public GameObject DequeueFreeze()
    {
        return _freezes.Dequeue();
    }
}
