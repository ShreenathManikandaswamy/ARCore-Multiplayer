using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Photon.MonoBehaviour
{
    public GameObject SaveFile;
    public InputField SaveWorkSpaceName;

    public GameObject LoadFile;
    public InputField LoadWorkSpaceName;

    //When using testing scene use this.
    //public const string playerPath = "TestPrefabGreen";
    //public const string playerPath2 = "TestPrefabYellow";

    //When using ARCore Scene Use this
    public const string playerPath = "GreenAndyPrefab";
    public const string playerPath2 = "YellowAndyPrefab";

    private static string savedatapath = string.Empty;
    private static string loaddatapath = string.Empty;


    private void Awake()
    {
        //datapath = System.IO.Path.Combine(Application.persistentDataPath, "Actors.json");
        //datapath = System.IO.Path.Combine(Application.persistentDataPath, SaveFile.name + ".json");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static Actor CreateActor (string path, Vector3 position, Quaternion rotation)
    {
            GameObject prefab = Resources.Load<GameObject>(path);

            GameObject go = PhotonNetwork.Instantiate(prefab.name, position, rotation, 0) as GameObject;

            Actor actor = go.GetComponent<Actor>() ?? go.AddComponent<Actor>();

            return actor;
    }

    public static Actor CreateActor(ActorData data, string path, Vector3 position, Quaternion rotation)
    {
        Actor actor = CreateActor(path, position, rotation);

        actor.data = data;

        return actor;
    }

    public void Save()
    {
        Debug.Log("Save Button Clicked");
        SaveFile.SetActive(true);
    }

    public void Load()
    {
        LoadFile.SetActive(true);
        //SaveData.Load(datapath);
    }

    public void SaveInto()
    {
        Debug.Log(SaveWorkSpaceName.text);   
        SaveFile.SetActive(false);
        savedatapath = System.IO.Path.Combine(Application.persistentDataPath, SaveWorkSpaceName.text.ToString() + ".json");
        SaveData.Save(savedatapath, SaveData.actorContainer);
        Debug.Log("File Saved");
    }

    public void LoadFrom()
    {
        LoadFile.SetActive(false);
        loaddatapath = System.IO.Path.Combine(Application.persistentDataPath, LoadWorkSpaceName.text.ToString() + ".json");
        SaveData.Load(loaddatapath);
        Debug.Log("File Loaded");
    }
}
