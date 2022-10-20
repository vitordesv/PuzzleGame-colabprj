using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSysCollision : MonoBehaviour
{
    [SerializeField] PlayerController playerContrlSCRIPT;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
            playerContrlSCRIPT.isGrounded = true;

        if (collision.gameObject.tag == "Item")
        {
            Debug.Log("É ISSO!");
            Destroy(collision.gameObject);
            scrVariaveis.item = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
            playerContrlSCRIPT.isGrounded = false;
    }

}