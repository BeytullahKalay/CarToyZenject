using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolerManager : MonoBehaviour
{
    [SerializeField] private List<ObjectToPool> objectToPools = new List<ObjectToPool>();


    public Dictionary<PooledObjectType, ObjectPool<GameObject>> AllPooledGameObjectsDictionary =
        new Dictionary<PooledObjectType, ObjectPool<GameObject>>();

    private void Awake()
    {
        foreach (var toPool in objectToPools)
        {
            PoolingStuff(toPool, toPool.PoolAmount);
        }
    }

    private void PoolingStuff(ObjectToPool objectToPool, int maxPoolingAmount = 100)
    {
        var pool = new ObjectPool<GameObject>(
            () => { return Instantiate(objectToPool.PoolingGameObject); },
            pooledObj => { pooledObj.gameObject.SetActive(true); },
            pooledObj =>
            {
                if (Application.isPlaying)
                    pooledObj.gameObject.SetActive(false);
            },
            pooledObj => { Destroy(pooledObj.gameObject); },
            false, objectToPool.PoolAmount, maxPoolingAmount);

        if (!AllPooledGameObjectsDictionary.ContainsKey(objectToPool.PooledObjectType))
            AllPooledGameObjectsDictionary.Add(objectToPool.PooledObjectType, pool);
        else
            Debug.LogError("Same type object tried to pooling");
    }

    public ObjectPool<GameObject> GetPoolWithType(PooledObjectType pooType)
    {
        return AllPooledGameObjectsDictionary[pooType];
    }
}

[Serializable]
public class ObjectToPool
{
    public GameObject PoolingGameObject;
    public int PoolAmount = 30;
    public PooledObjectType PooledObjectType;
}

[Serializable]
public enum PooledObjectType
{
    ExplosionDustParticleFx
}