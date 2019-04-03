using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Testing : Photon.MonoBehaviour
{

    public GameObject GreenAndyAndroidPrefab;

    public GameObject YelloyAndyAndroidPrefab;

    public string verNum = "0.1";

    public string roomName = "room01";

    public string playerName = "user 420";

    public Transform spawnPoint;

    public GameObject playerPref;

    public bool isConnected = false;

    public void Start()
    {
        roomName = "Room " + Random.Range(0, 999);
        playerName = "User " + Random.Range(0, 999);
        PhotonNetwork.ConnectUsingSettings(verNum);
        Debug.Log("Starting Connection!");

    }

    public void OnJoinedLobby()
    {
        //PhotonNetwork.JoinOrCreateRoom(roomName, null, null);
        isConnected = true;
        Debug.Log("Starting Server!");
    }

    public void OnJoinedRoom()
    {
        PhotonNetwork.playerName = playerName;
        isConnected = true;
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        string userName = PlayerPrefs.GetString("UserName");
        GameObject pl = PhotonNetwork.Instantiate(playerPref.name, spawnPoint.position, spawnPoint.rotation, 0) as GameObject;
        pl.name = userName;
    }

    public void SpawnGreenAndy()
    {
        GameObject GAnd = PhotonNetwork.Instantiate(GreenAndyAndroidPrefab.name, spawnPoint.position, spawnPoint.rotation, 0) as GameObject;
        GAnd.name = "GreenAndy";
    }

    public void SpawnYellowAndy()
    {
        GameObject YAnd = PhotonNetwork.Instantiate(YelloyAndyAndroidPrefab.name, spawnPoint.position, spawnPoint.rotation, 0) as GameObject;
        YAnd.name = "YellowAndy";
    }

    public void QuitScene()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.Disconnect();
        isConnected = false;
        Application.LoadLevel(0);
    }

    void OnGUI()
    {

        if (isConnected)
        {
            GUI.skin.textField.fontSize = 40;
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 250, 500, 500));
            playerName = GUILayout.TextField(playerName);
            roomName = GUILayout.TextField(roomName);

            if (GUILayout.Button("Create", GUILayout.Width(500), GUILayout.Height(100)))
            {
                PhotonNetwork.JoinOrCreateRoom(roomName, null, null);
            }

            foreach (RoomInfo game in PhotonNetwork.GetRoomList())
            {
                if (GUILayout.Button(game.name + " " + game.playerCount + "/" + game.maxPlayers, GUILayout.Width(500), GUILayout.Height(100)))
                {
                    PhotonNetwork.JoinOrCreateRoom(game.name, null, null);
                }
            }
            GUILayout.EndArea();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("P pressed");
            SpawnGreenAndy();
        } 

        if( Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Y pressed");
            SpawnYellowAndy();
        }
    }
}
