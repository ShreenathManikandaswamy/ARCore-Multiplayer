using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : Photon.MonoBehaviour
{

    private PhotonView PV;
    private CharacterController myCC;
    public float movementspeed;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        myCC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PV.isMine)
        {
            BasicMovement();
        }
    }

    void BasicMovement()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            myCC.Move(transform.forward * Time.deltaTime * movementspeed);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            myCC.Move(-transform.right * Time.deltaTime * movementspeed);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            myCC.Move(-transform.forward * Time.deltaTime * movementspeed);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            myCC.Move(transform.right * Time.deltaTime * movementspeed);
        }
    }

}
