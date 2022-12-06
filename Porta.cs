using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public static bool aberta = false;
    public Animator animporta;
    public int tipoPorta = 0;


    // Update is called once per frame
    void Update()
    {
        if (scrVariaveis.chaves == tipoPorta)
        {
            aberta = true;
            //Debug.Log("ABRE PORTA");
        }
        else aberta = false;

        abrePorta();
    }

    void abrePorta()
    {
        if(aberta == true && gameObject.tag =="Porta")
        {
            animporta.SetBool("open",true);
            //gameObject.SetActive(false);
        }
    }
}