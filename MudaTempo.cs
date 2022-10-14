using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudaTempo : MonoBehaviour
{

    GameObject [] pres;
    GameObject [] fut;
    public bool ligado = true;

    void Start()
    {
        
        pres = GameObject.FindGameObjectsWithTag("Presente");
        fut = GameObject.FindGameObjectsWithTag("Futuro");

        foreach(GameObject futuro in fut){

            futuro.SetActive(false);

        }

    }

    void Update()
    {

        if(ligado){

            if(Input.GetKeyDown(KeyCode.E)){

                ligado = false;
                foreach(GameObject presente in pres){

                    presente.SetActive(false);

                }

                foreach(GameObject Futuro in fut){

                    Futuro.SetActive(true);

                }

                Debug.Log("FOI!");

            }

        }else{

                if(!ligado){

                    if(Input.GetKeyDown(KeyCode.E)){

                        ligado = true;
                        foreach(GameObject presente in pres){

                            presente.SetActive(true);

                        }

                        foreach(GameObject futuro in fut){

                            futuro.SetActive(false);

                        }

                        Debug.Log("FOI DE NOVO!");

                    }

                }

        }
        
    }
}
