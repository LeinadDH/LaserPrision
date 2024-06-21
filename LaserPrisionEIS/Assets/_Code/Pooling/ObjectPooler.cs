using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static Dictionary<string, Component> PoolLookup = new Dictionary<string, Component>();
    public static Dictionary<string, Queue<Component>> PoolDictionary = new Dictionary<string, Queue<Component>>();
    private static Dictionary<string, GameObject> _poolParents = new Dictionary<string, GameObject>();

    public static void EnqueueObject<T>(T item, string name) where T : Component
    {
        if (!item.gameObject.activeSelf)
        {
            return;
        }

        item.transform.position = Vector2.zero;
        PoolDictionary[name].Enqueue(item);
        item.gameObject.SetActive(false);
    }

    public static T DequeueObject<T>(string key) where T : Component
    {
        foreach (var item in PoolDictionary[key])
        {
            if (!item.gameObject.activeSelf)
            {
                item.gameObject.SetActive(true);
                return (T)item;
            }
        }

        return (T)EnqueNewInstance(PoolLookup[key], key);
    }

    public static T EnqueNewInstance<T>(T item, string key) where T : Component
    {
        T newInstance = Object.Instantiate(item);
        newInstance.gameObject.SetActive(false);
        newInstance.transform.SetParent(_poolParents[key].transform);
        newInstance.transform.position = Vector2.zero;
        PoolDictionary[key].Enqueue(newInstance);
        return newInstance;
    }

    public static void SetupPool<T>(T pooledItemPrefab, int poolSize, string dictionaryEntry) where T : Component
    {
        if (PoolDictionary.ContainsKey(dictionaryEntry))
        {
            PoolDictionary[dictionaryEntry].Clear();
            PoolLookup.Remove(dictionaryEntry);
            Destroy(_poolParents[dictionaryEntry]);
            _poolParents.Remove(dictionaryEntry);
        }

        PoolDictionary[dictionaryEntry] = new Queue<Component>();
        PoolLookup[dictionaryEntry] = pooledItemPrefab;

        GameObject poolParent = new GameObject(dictionaryEntry + "Pool");
        _poolParents[dictionaryEntry] = poolParent;

        for (int i = 0; i < poolSize; i++)
        {
            T pooledInstance = Object.Instantiate(pooledItemPrefab, poolParent.transform);
            pooledInstance.gameObject.SetActive(false);
            PoolDictionary[dictionaryEntry].Enqueue((T)pooledInstance);
        }
    }
}