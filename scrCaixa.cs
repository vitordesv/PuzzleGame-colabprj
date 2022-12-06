using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCaixa : MonoBehaviour
{
    //Encaixa no receptor
    [SerializeField] private Rigidbody2D rbcaixa;
    private Transform encaixe;
    //sprites
    private SpriteRenderer render;
    private Sprite presente, futuro;
    
    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        presente = Resources.Load<Sprite>("BoxPresente");
        futuro = Resources.Load<Sprite>("BoxFuturo");
    }

    void Update()
    {
        if (!scrVariaveis.nopresente)
        {
            render.sprite=futuro;
        }
        else
        {
            render.sprite=presente;
        }
    }

    //Assim que a caixa entra no encaixa o player para de segurar e a caixa fica presa, o problema é caso você passe pela encaixa por acidente
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Encaixa")
        {
            //Debug.Log("Ativo");
            scrVariaveis.chaves++;
            encaixe= collider.gameObject.transform.GetChild(0);
            rbcaixa.transform.position = encaixe.transform.position;
            scrVariaveis.segurarcaixa = false;
            //Debug.Log(scrVariaveis.chaves);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Encaixa")
        {
            scrVariaveis.chaves--;
        }
    }
}