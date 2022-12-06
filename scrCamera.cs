using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCamera : MonoBehaviour
{
    [SerializeField] private Transform player_1;
    public Vector3 offset;
    [Range(1,10)] public float suavizacao;
    public Vector3 minimo, maximo;


    private void FixedUpdate()
    {
       Seguir();
    }

    void Seguir()
    {
        Vector3 playerposition = player_1.position + offset;
        Vector3 limiteCamera = new Vector3(Mathf.Clamp(playerposition.x, minimo.x, maximo.x),
            Mathf.Clamp(playerposition.y, minimo.y, maximo.y),
            Mathf.Clamp(playerposition.z, minimo.z, maximo.z));

        Vector3 followPlayer = Vector3.Lerp(transform.position, limiteCamera, suavizacao * Time.fixedDeltaTime);
        transform.position = followPlayer;
    }
}