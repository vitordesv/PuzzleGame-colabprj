using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSysCollision : MonoBehaviour
{
    [SerializeField] PlayerController playerContrlSCRIPT;


    void OnTriggerEnter2D(Collider2D collision)
    {
        //chão
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
        {
            playerContrlSCRIPT.isGrounded = true;
            playerContrlSCRIPT.animator.SetBool("jumping", false);
            //Debug.Log("Pisante maneiro");
        }
        //item
        if (collision.gameObject.tag == "Item" && scrVariaveis.nopresente)
        {
            //Debug.Log("É ISSO!");
            Destroy(collision.gameObject);
            scrVariaveis.item = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9|| collision.gameObject.layer == 10)
        {
            playerContrlSCRIPT.isGrounded = false;
            playerContrlSCRIPT.animator.SetBool("jumping", true);
        }
    }
}