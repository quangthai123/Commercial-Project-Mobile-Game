using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    [SerializeField] private List<Transform> objPrefabs;
    [SerializeField] private List<Transform> poolObjList;
    private Transform holder;
    private void Reset()
    {
        holder = transform.Find("Holder");
        Transform prefabs = transform.Find("Prefab");
        foreach (Transform prefab in prefabs)
        {
            objPrefabs.Add(prefab);
            prefab.gameObject.SetActive(false);
        }
    }
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else 
            instance = this;
        holder = transform.Find("Holder");
        Transform prefabs = transform.Find("Prefab");
        if (objPrefabs.Count > 0) return;
        foreach (Transform prefab in prefabs)
        {
            objPrefabs.Add(prefab);
            prefab.gameObject.SetActive(false);
        }
    }
    //private Transform FindAnObjInPoolWithName(string name)
    //{
    //    foreach (Transform obj in poobObjList)
    //    {
    //        if(obj.name == name+"(Clone)")
    //        {
    //            return obj;
    //        }
    //    }
    //    if(objnull;
    //}

    public void Spawn(Vector2 pos, Quaternion rot)
    {
        Transform obj;
        if (poolObjList.Count > 0)
        {
            obj = poolObjList[0];
            poolObjList.RemoveAt(0);
            Debug.Log("Reused");
        } else
        {
            obj = Instantiate(objPrefabs[0]);
        }
        obj.parent = holder;
        obj.SetPositionAndRotation(pos, rot);
        obj.gameObject.SetActive(true);
    }
    public void Despawn(Transform obj)
    {
        poolObjList.Add(obj);
        obj.gameObject.SetActive(false);
    }
}
