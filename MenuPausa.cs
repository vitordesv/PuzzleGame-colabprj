using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] GameObject menuPausa;
    public static bool estaPausado;

    void Start()
    {
        menuPausa.SetActive(false);
        estaPausado = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pausa"))
        {
            if (estaPausado)
            {
                ResumirJogo();
            }
            else
            {
                PausarJogo();
            }
        }
    }

    public void PausarJogo()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
        estaPausado = true;
    }

    public void ResumirJogo()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        estaPausado = false;
    }

    public void VoltarAoMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void SairdoJogo()
    {
        Application.Quit();
        Debug.Log("O Jogador saiu do jogo");
    }
}
