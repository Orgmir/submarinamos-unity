using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameObjectPool
{
    public GameObject[] prefabs;
    public GameObject container;
    private Dictionary<int, GameObject> prefabDict;
    private Dictionary<int, List<GameObject>> pool;

    public GameObjectPool()
    {
        prefabDict = new Dictionary<int, GameObject>();
        pool = new Dictionary<int, List<GameObject>>();
        for (int i = 0; i < prefabs.Length; i++) {
            GameObject prefab = prefabs[i];
            int id = prefab.GetInstanceID();
            pool.Add(id, new List<GameObject>());
            prefabDict.Add(id, prefab);
        }
    }

    public GameObject Dequeue(int prefabId)
    {
        List<GameObject> queue = pool[prefabId];

        GameObject obj = GetInactiveObj(queue);
        if (obj == null)
        {
            obj = GameObject.Instantiate(prefabDict[prefabId]) as GameObject;
            obj.transform.parent = container.transform;
            queue.Add(obj);
        }

        obj.SetActive(true);

        return obj;
    }

    private GameObject GetInactiveObj(List<GameObject> list) {
        foreach (GameObject obj in list) {
            if(!obj.activeInHierarchy) {
                return obj;
            }
        }
        return null;
    }
}
