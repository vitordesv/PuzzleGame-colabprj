using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarregaCena : MonoBehaviour{

    //N�mero da cena que ser� carregada
    public int nCenaACarregar;
    //Nome da cena que ser� carregada
    public string tCenaACarregar;

    //boolean para escolher se a troca ser� feita pelo texto ou n�mero da cena
    public bool usaIntParaCarregarCena = false;

        //fun��o que usa a colis�o para chamar a transi��o de cena
         void OnTriggerEnter2D(Collider2D collision){

            //condi��o que define se a transi��o entre cenas ser� acionada ou n�o
            if (collision.gameObject.tag == "Player")SceneManager.LoadScene(nCenaACarregar);
            
            //fun��o que detectar� a colis�o entre os GameObjects
            GameObject collisionGameObject = collision.gameObject;

            //testes com outras alternativas (n�o utilizados!)
            //if (collisionGameObject.name == "Player") {
            //if(botaoInter())LoadScene();
        //}
    }
   
    //testes com a fun��o load scene feita de forma separada (n�o utilizados!)
   /* void LoadScene (){

        if(usaIntParaCarregarCena)SceneManager.LoadScene(nCenaACarregar);
        else SceneManager.LoadScene(tCenaACarregar);

    }
   */

   //bot�o de intera��o
   /*
    public bool botaoInter(){

        return Input.GetKey(KeyCode.Space);

    }

    */
}
