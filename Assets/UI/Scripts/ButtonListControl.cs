using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListControl : Photon.MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;

    public Text txt;

    int i = 0;

    /*private void Start()
    {
        for (int i = 1; i <= 20; i++)
        {
            GameObject Button = Instantiate(buttonTemplate) as GameObject;
            Button.SetActive(true);

            Button.GetComponent<ButtonListButton>().SetText("Button # " + i);

            Button.transform.SetParent(buttonTemplate.transform.parent, false);
        }

        txt.text = i + " Rooms Active";

        foreach (RoomInfo game in PhotonNetwork.GetRoomList())
        {
            i++;
            txt.text = i + " Rooms Active";
            GameObject Button = Instantiate(buttonTemplate) as GameObject;
            Button.SetActive(true);

            Button.GetComponent<ButtonListButton>().SetText(game.Name);

            Button.transform.SetParent(buttonTemplate.transform.parent, false);
            if (GUILayout.Button(game.name + " " + game.playerCount + "/" + game.maxPlayers, GUILayout.Width(500), GUILayout.Height(100)))
            {
                PhotonNetwork.JoinOrCreateRoom(game.name, null, null);
            }
        }
    }*/

    public void CheckRooms()
    {
        txt.text = i + " Rooms Active";

        foreach (RoomInfo game in PhotonNetwork.GetRoomList())
        {
            i++;
            txt.text = i + " Rooms Active";
            GameObject Button = Instantiate(buttonTemplate) as GameObject;
            Button.SetActive(true);

            Button.GetComponent<ButtonListButton>().SetText(game.Name);

            Button.transform.SetParent(buttonTemplate.transform.parent, false);
            /*if (GUILayout.Button(game.name + " " + game.playerCount + "/" + game.maxPlayers, GUILayout.Width(500), GUILayout.Height(100)))
            {
                PhotonNetwork.JoinOrCreateRoom(game.name, null, null);
            }*/
        }
    }

    public void ButtonClicked(string roomName)
    {
        PhotonNetwork.JoinOrCreateRoom(roomName, null, null);
    }
    
}
