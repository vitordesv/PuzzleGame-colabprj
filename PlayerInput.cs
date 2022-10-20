using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerController playerContrlSCRIPT;

    void Update()
    {
        playerContrlSCRIPT.moveinput = Input.GetAxisRaw("Horizontal");

        //agachar
        if (Input.GetButtonDown("Crouch"))
        {
            playerContrlSCRIPT.crouchPressed = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            playerContrlSCRIPT.crouchPressed = false;
        }

        //pular
        if (Input.GetButtonDown("Jump"))
        {
            playerContrlSCRIPT.jumpBuffer = true;
        }

        //segurar
        if (Input.GetKeyDown(KeyCode.F))
            playerContrlSCRIPT.segurarPressed = true;
    }
}
