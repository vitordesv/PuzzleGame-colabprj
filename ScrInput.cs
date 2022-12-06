using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrInput : MonoBehaviour
{
    public bool botaoAlavancaPress = false;
    public bool mudaTempoPress = false;
    public bool leverPress = false;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (!MenuPausa.estaPausado)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                botaoAlavancaPress = true;
            if (Input.GetKeyUp(KeyCode.Q))
                botaoAlavancaPress = false;

            if (Input.GetKeyDown(KeyCode.Z))
                mudaTempoPress = true;
            if (Input.GetKeyUp(KeyCode.Z))
                mudaTempoPress = false;

            if (Input.GetKeyDown(KeyCode.X))
                leverPress = true;
            if (Input.GetKeyUp(KeyCode.X))
                leverPress = false;
        }
    }
}