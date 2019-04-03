using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerAPIHandler : MonoBehaviour {

    public static ServerAPIHandler Instance;
    public webRequestHandler webRequestHandler;
    public ServerStateManager serverStateManager;
    public UserLocalDB userLocalDB;

     private void Awake()
    {
        if (Instance == null)
            Instance = this;

        Init();
    }

    void Init()
    {
        webRequestHandler = gameObject.AddComponent<webRequestHandler>();
        serverStateManager = gameObject.AddComponent<ServerStateManager>();
        userLocalDB = UserLocalDB.LoadFromFile();

    }
}
