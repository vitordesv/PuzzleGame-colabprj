using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrLever : MonoBehaviour
{
    bool interagivel;
    bool olhaesquerda;
    bool puxado;
    //public GameObject alavancaF;
    private SpriteRenderer render;
    private Sprite desligada,ligada,futuro;
    [SerializeField] PlayerController playerContrlSCRIPT;
    ScrInput inputScript;


    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        desligada = Resources.Load<Sprite>("AlavancaOff");
        ligada = Resources.Load<Sprite>("AlavancaOn");
        futuro = Resources.Load<Sprite>("AlavancaFuturo");
    }

    void Start()
    {
        render.sprite = desligada;
        olhaesquerda = true;
        //puxado = false;
    }

    // Update is called once per frame
    void Update()
    {
        FlipLever();
        //botar segurarPressed ao inves de input E colocar metodo fliplever num local que seja mais preciso
        
        //faz a ação puxar alavanca
        if(interagivel && inputScript.leverPress && olhaesquerda && scrVariaveis.nopresente)
        {
            //Debug.Log("puxou");
            puxado = true;
            scrVariaveis.chaves++;
        }

        if(interagivel && inputScript.leverPress && !olhaesquerda && scrVariaveis.nopresente)
        {
            //Debug.Log("despuxou");
            puxado = false;
            scrVariaveis.chaves--;
        }
        //faz sprite mudar 
        if(scrVariaveis.nopresente)
        {
            render.flipX = false;

            if(puxado) 
                render.sprite = ligada;
            else 
                render.sprite = desligada;
        }
        //faz sprite mudar no futuro e virar dependendo de estar puxado ou não
        if(!scrVariaveis.nopresente)
        {
            if(puxado)
            {
                render.sprite = futuro;
                render.flipX = true;
            }        
            else
            {
                render.sprite = futuro;
                render.flipX = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("OLHAELEAE");

        if (GameObject.FindWithTag("Player"))
        {
             interagivel = true;
        }
    }

    void OnTriggerExit2D(Collider2D colisor)
    {
         if (GameObject.FindWithTag("Player"))
         {
            interagivel = false;
         }
    }

    void FlipLever()
    {
        if(olhaesquerda && puxado || !olhaesquerda && !puxado)
        {
            olhaesquerda = !olhaesquerda;
            playerContrlSCRIPT.animator.SetTrigger("interact");
            //Debug.Log("PUXADO" + puxado);
        }
    }
}