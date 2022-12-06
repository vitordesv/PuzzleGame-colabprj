using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scrMenu : MonoBehaviour
{
    public Animator transicao;
    public float tempotransicao;
    
    public void PlayGame()
    {
        StartCoroutine(LoadGame(SceneManager.GetActiveScene().buildIndex + 1));
    }
    
    IEnumerator LoadGame(int levelIndex)
    {
        transicao.SetTrigger("Intro");
        yield return new WaitForSeconds(tempotransicao);
        SceneManager.LoadScene(levelIndex);
    }
}