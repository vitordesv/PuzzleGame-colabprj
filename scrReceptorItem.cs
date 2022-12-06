using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptorItem : MonoBehaviour
{
    private bool on = false;
    private SpriteRenderer render;
    private Sprite onpresente, onfuturo, offpresente, offfuturo;


    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        onpresente = Resources.Load<Sprite>("EncaixeOn");
        onfuturo = Resources.Load<Sprite>("EncaixeOnF");
        offpresente = Resources.Load<Sprite>("EncaixeOff");
        offfuturo = Resources.Load<Sprite>("EncaixeOffF");
    }

    void Start()
    {
        render.sprite = offpresente;        
    }

    // Update is called once per frame
    void Update()
    {
        if(scrVariaveis.nopresente)
        {
            if(on)
            {
                render.sprite = onpresente;
            }
            else
            {
                render.sprite = offpresente;
            }
        }
        else
        {
            if (on)
            {
                render.sprite = onfuturo;
            }
            else
            {
                render.sprite = offfuturo;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Receptor" && scrVariaveis.nopresente)
        {
            if(scrVariaveis.item)
            {
                scrVariaveis.chaves++;
                scrVariaveis.item = false;
                on = true;
            }
        }
    }
}