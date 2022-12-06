using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botão : MonoBehaviour
{
    ScrInput inputScript;
    private bool ligada = false;


    private void OnTriggerStay2D (Collider2D collision)
    {
        //Debug.Log("Achou a alavanca");
        if(collision.gameObject.tag == "Player" && inputScript.botaoAlavancaPress && ligada)
        {
            ligada = false;
            scrVariaveis.chaves--;
            //Debug.Log("Alavanca desligou");
        }
        
        if(collision.gameObject.tag == "Player" && inputScript.botaoAlavancaPress && !ligada)
        {
            ligada = true;
            scrVariaveis.chaves++;
            //Debug.Log("Alavanca ligou");
        }
    }
}