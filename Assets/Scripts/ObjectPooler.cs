using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    public GameObject PoolObject;
    public List<GameObject> PooledObjects;
    public int PoolAmount;
    
	void Start () {
        PooledObjects = new List<GameObject>();

        for (int i = 0; i < PoolAmount; i++)
        {
            PooledObjects.Add(InitializePoolObject());
        }
    }

    public GameObject GetPooledObject() {
        for (int i = 0; i < PooledObjects.Count; i++)
        {
            if (!PooledObjects[i].activeInHierarchy)
            {
                return PooledObjects[i];
            }
        }

        var poolObject = InitializePoolObject();

        PooledObjects.Add(poolObject);

        return poolObject;
    }

    private GameObject InitializePoolObject()
    {
        var poolObject = Instantiate(PoolObject);

        poolObject.SetActive(false);

        return poolObject;
    }
}
