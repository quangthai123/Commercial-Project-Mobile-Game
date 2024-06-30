using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    
    [SerializeField] protected List<Transform> objPrefabs;
    [SerializeField] protected List<Transform> poolObjList;
    protected Transform holder;
    protected Transform obj;
    protected void Reset()
    {
        holder = transform.Find("Holder");
        Transform prefabs = transform.Find("Prefab");
        foreach (Transform prefab in prefabs)
        {
            objPrefabs.Add(prefab);
            prefab.gameObject.SetActive(false);
        }
    }
    protected virtual void Awake()
    {
        holder = transform.Find("Holder");
        Transform prefabs = transform.Find("Prefab");
        if (objPrefabs.Count > 0) return;
        foreach (Transform prefab in prefabs)
        {
            objPrefabs.Add(prefab);
            prefab.gameObject.SetActive(false);
        }
    }
    protected Transform FindAnObjInPoolWithName(string name)
    {
        foreach (Transform obj in poolObjList)
        {
            if (obj.name == name + "(Clone)")
            {
                return obj;
            }
        }
        return null;
    }

    public virtual Transform Spawn(string name, Vector2 pos, Quaternion rot)
    {
        obj = FindAnObjInPoolWithName(name);
        if(obj != null)
        {
            poolObjList.Remove(obj);
        } else
        {
            for(int i=0; i < objPrefabs.Count; i++) 
            {
                if (objPrefabs[i].name == name)
                {
                    obj = Instantiate(objPrefabs[i]);
                    Debug.Log("Instantiate New Obj!");
                    break;
                }
            }
            if (obj == null)
            {
                Debug.LogWarning("Can not found " + name + " to spawn!");
                return null;
            }
        }
        obj.parent = holder;
        obj.SetPositionAndRotation(pos, rot);
        return obj;
        //obj.gameObject.SetActive(true);
    }
    public void Despawn(Transform obj)
    {
        poolObjList.Add(obj);
        obj.gameObject.SetActive(false);
    }
}
