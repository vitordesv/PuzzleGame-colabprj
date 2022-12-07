using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class scrHUD : MonoBehaviour
{

    [SerializeField] private Image img1,img2,img3;
    [SerializeField] private List<Image> cadeados;
    private GameObject[] fechaduras;

    private int _chavesPegas;
    // private Image[] Imagem;
    
    void Start() 
    {
        _chavesPegas=scrVariaveis.chaves;
        
   
        fechaduras = GameObject.FindGameObjectsWithTag("Cadeado");
        foreach(GameObject fechadura in fechaduras)
        {

            fechadura.GetComponent<Image>().enabled=false;
        }
         for (int i = 0; i < cadeados.Count; i++)
            {
            cadeados[i].enabled=true;                
            }
    }

  

    // Update is called once per frame
    void Update()
    {

        switch (scrVariaveis.item)
        {
            case 1:
            img1.enabled=true;
            img2.enabled=false;
            img3.enabled=false;
            Debug.Log("pegou" + scrVariaveis.item);
            break;
            case 2:
            img1.enabled=true;
            img2.enabled=true;
            img3.enabled=false;
            Debug.Log("pegou2" + scrVariaveis.item);
            break;
            case 3:
            img1.enabled=true;
            img2.enabled=true;
            img3.enabled=true; 
            Debug.Log("pegou3" + scrVariaveis.item);
            break;
            default:
            img1.enabled=false;
            img2.enabled=false;
            img3.enabled=false;
            Debug.Log("pegou4" + scrVariaveis.item);

            break;

        }

// if(Porta.aberta)
// {
//      for (int i = 0; i < cadeados.Count; i++)
//             {
//             cadeados[i].enabled=false;                
//             }
// }else 
if(scrVariaveis.chaves>_chavesPegas)
{

for(int i = 0; i < scrVariaveis.chaves; i++)
{
    cadeados[cadeados.Count-scrVariaveis.chaves].enabled=false;
     
}
_chavesPegas=scrVariaveis.chaves;
} else if(!Porta.aberta && scrVariaveis.chaves!=_chavesPegas)
{
    //tá facendo aparecer tudo tem que ser só um
    // for (int i = 0; i < cadeados.Count; i++)
    //         {
    //         cadeados[i].enabled=true;                
    //         }
    for(int i = 0; i < _chavesPegas; i++)
{
    cadeados[cadeados.Count-_chavesPegas].enabled=true;
     
}

    _chavesPegas=scrVariaveis.chaves;
}
        

    }
}
