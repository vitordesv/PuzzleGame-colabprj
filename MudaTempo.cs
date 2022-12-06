using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudaTempo : MonoBehaviour
{
    ScrInput inputScript;
    GameObject [] pres;
    GameObject [] fut;

    void Awake()
    {
        scrVariaveis.nopresente = true;
        
        //inicializar
        pres = GameObject.FindGameObjectsWithTag("Presente");
        fut = GameObject.FindGameObjectsWithTag("Futuro");
        //Debug.Log("pegou base");
        //apagar anterior
        if(pres[0] != null)
        {
            LimparHistorico();
            //Debug.Log("tem coisa");
            pres = GameObject.FindGameObjectsWithTag("Presente");
            fut = GameObject.FindGameObjectsWithTag("Futuro");
        }

        //definir como passado sempre
        foreach(GameObject futuro in fut)
        {
            futuro.SetActive(false);
        }
    }

    void Update()
    {  
        if(scrVariaveis.nopresente)
        {
            if(inputScript.mudaTempoPress)
            {
                scrVariaveis.nopresente = false;
                foreach(GameObject presente in pres)
                {
                    presente.SetActive(false);
                }

                foreach(GameObject Futuro in fut)
                {
                    Futuro.SetActive(true);
                }

                //Debug.Log("FOI!");
            }
        }
        else
        {
            if(!scrVariaveis.nopresente)
            {
                if(inputScript.mudaTempoPress)
                {
                    scrVariaveis.nopresente = true;
                    foreach(GameObject presente in pres)
                    {
                        presente.SetActive(true);
                    }

                    foreach(GameObject futuro in fut)
                    {
                        futuro.SetActive(false);
                    }
                    
                    //Debug.Log("FOI DE NOVO!");
                }
            }
        }
    }

    void LimparHistorico()
    {
        int p = 0;
        int f = 0;
        //funciona somente se tiver o mesmo nmr de objetos no presente e no futuro
        foreach(GameObject obj in pres)
        {
            //Debug.Log("VazioP" + p);
            pres[p] = null;
            p++;
        }
        
        foreach(GameObject obj in fut)
        {
            //Debug.Log("VazioF" + f);
            fut[f] = null;
            f++;
        }
        
        for (int i = 0; i < pres.Length; i++)
        {
            pres[i] = null;
        }
        
        for (int i = 0; i < fut.Length; i++)
        {
            fut[i] = null;
        }
    }

    void ColetarObjetos()
    {
        pres = GameObject.FindGameObjectsWithTag("Presente");
        fut = GameObject.FindGameObjectsWithTag("Futuro");
    }
}