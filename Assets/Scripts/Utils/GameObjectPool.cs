using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameObjectPool
{
    private GameObject prefab;
    private Queue<GameObject> queue;

    public GameObjectPool(GameObject prefab)
    {
        this.prefab = prefab;
        queue = new Queue<GameObject>();
    }

    public GameObject Dequeue(Vector3 pos)
    {
        GameObject obj = queue.Dequeue();

        if (obj == null)
        {
            obj = GameObject.Instantiate(prefab, pos, Quaternion.identity) as GameObject;
        }

        obj.SetActive(true);

        return obj;
    }

    public void recycle(GameObject obj)
    {
        obj.SetActive(false);
        queue.Enqueue(obj);
    }
}
