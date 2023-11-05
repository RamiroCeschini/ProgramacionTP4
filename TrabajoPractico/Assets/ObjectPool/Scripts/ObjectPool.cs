using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [HideInInspector]
    public static ObjectPool SharedInstance;

    [Header("Multiple ObjectPool")]
    [SerializeField] private List<ScriptablePool> objectPools;


    private void Awake()
    {
        SingletonSet();
    }

    private void Start()
    {
        for (int i = 0; i < objectPools.Count; i++)
        {
            FillPool(objectPools[i]);
        }
    }

    private void SingletonSet()
    {
        if (SharedInstance != null && SharedInstance != this)
        {
            Destroy(this);
        }
        else
        {
            SharedInstance = this;
        }
    }

    private void FillPool(ScriptablePool pool)
    {
        pool.s_pooledObjects.Clear();
        GameObject tmp;

        for (int i = 0; i < pool.S_amount; i++)
        {
            tmp = Instantiate(pool.S_bulletPrefab);
            tmp.SetActive(false);
            pool.s_pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject(string name)
    {
        ScriptablePool selected = GetPoolByName(name);

        for (int i = 0; i < selected.S_amount; i++)
        {
            if (!selected.S_pooledObjects[i].activeInHierarchy)
            {
                return selected.S_pooledObjects[i];
            }
        }

        return null;
    }

    private ScriptablePool GetPoolByName(string name)
    {
        for (int i = 0; i < objectPools.Count; i++)
        {
            if (name == objectPools[i].S_name)
            {
                return objectPools[i];
            }
        }
        return null;
    }
}
