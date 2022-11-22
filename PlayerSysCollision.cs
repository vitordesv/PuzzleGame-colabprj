using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSysCollision : MonoBehaviour
{
    [SerializeField] PlayerController playerContrlSCRIPT;

// void OnCollisionStay2D(Collision2D collider)
//     {
//          if(collider.gameObject.tag == "Caixa")
//         {   
//             //quando esncostar na caixa apertar um botão de interagir,fazendo com que o x aumente se o do player aumentar e diminua se diminuir
//             //diminuir velocidade se precisar talvez a massa ja torne dificil
//             //linkar com animações
//             scrVariaveis.pegavel=true;
//             //rbcaixa.constraints = ~RigidbodyConstraints2D.FreezePositionX;
//         }        
//     }
//     void OnCollisionExit2D(Collision2D colli)
//     {
//         if(colli.gameObject.tag=="Player")
//         {
//             scrVariaveis.pegavel=false;
//         }
//     }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //chão
        if (collision.gameObject.layer== 9)
        {
            playerContrlSCRIPT.isGrounded = true;
            playerContrlSCRIPT.animator.SetBool("jumping",false);
            Debug.Log("Pisante maneiro");
        }
        //item
        if (collision.gameObject.tag == "Item" && scrVariaveis.nopresente)
        {if (playerContrlSCRIPT.standingCollider.IsTouching(collision))
        {
            Destroy(collision.gameObject);
            scrVariaveis.item=scrVariaveis.item+1;
            Debug.Log("ITEM:" + scrVariaveis.item);   
        }
        } 
        //receptor item
        // if (collision.gameObject.tag == "Receptor" && scrVariaveis.nopresente)
        // {
        // if(scrVariaveis.item)
        // {
        //     scrVariaveis.chaves++;
        //     scrVariaveis.item = false;
        // }
        // }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer== 9)
        {
            playerContrlSCRIPT.isGrounded = false;
            playerContrlSCRIPT.animator.SetBool("jumping",true);

        }
    }
}