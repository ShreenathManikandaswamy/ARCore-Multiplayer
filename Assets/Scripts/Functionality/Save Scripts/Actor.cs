using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Actor : MonoBehaviour
{
    //public GameObject Andy;
    //static int i = 0;

    public ActorData data = new ActorData();

    //private string name = "Prefab";

    public void StoreData()
    {
        string username = PlayerPrefs.GetString("UserName");
        data.UserName = username;
        data.ObjectName = this.name.ToString();
        //data.pos = Andy.transform.position;
        data.pos = transform.position;
        data.rotation = transform.rotation;
        data.Scale = transform.localScale;
    }

    public void ApplyData()
    {
        SaveData.AddActorData(data);
    }

    public void LoadData()
    {
        //Andy.transform.position = data.pos;
        transform.position = data.pos;
        transform.rotation = data.rotation;
        transform.localScale = data.Scale;
        this.name = data.ObjectName;

    }

    private void OnEnable()
    {
        SaveData.OnLoaded += LoadData;
        SaveData.OnBeforeSave += StoreData;
        SaveData.OnBeforeSave += ApplyData;
    }

    void OnDisable()
    {
        SaveData.OnLoaded -= LoadData;
        SaveData.OnBeforeSave -= StoreData;
        SaveData.OnBeforeSave += ApplyData;
    }

}

[Serializable]
public class ActorData
{
    public string UserName;
    public string ObjectName;
    public Vector3 pos;
    public Quaternion rotation;
    public Vector3 Scale;
}


