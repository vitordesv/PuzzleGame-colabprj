using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public static bool aberta = false;
    public Animator animporta;
    [SerializeField]public int tipoPorta = 0;


    void Start() {
        
        aberta=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (scrVariaveis.chaves == tipoPorta){

            
                aberta = true;
                Debug.Log("ABRE PORTA");

        }
        // else aberta = false;

        abrePorta();
    }

    void abrePorta(){

        if(aberta == true)
        {
            animporta.SetBool("open",true);
            //    gameObject.SetActive(false);
        }
     

    }
}
