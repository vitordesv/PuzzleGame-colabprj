using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    private SpriteRenderer render;
    private Sprite presente, futuro;


    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        presente = Resources.Load<Sprite>("ItemBAzulPresente");
        futuro = Resources.Load<Sprite>("ItemFuture");
    }

    void Update()
    {
        if(!scrVariaveis.nopresente)
        {
            render.sprite = futuro;
        }
        else render.sprite = presente;
    }
}