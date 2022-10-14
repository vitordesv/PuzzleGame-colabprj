using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrRefletor : MonoBehaviour
{
    
    //[SerializeField] private LineRenderer m_lineRenderer;
    //private BoxCollider2D boxColliderMirror;
    //ligar e desligar laser
    [SerializeField] private float defDistanceRay = 1000f;
    public Transform saidaLaser;
    public LineRenderer lineRendererM;
    Transform positionMirror;
    public bool LaserLigado;

 private void Awake()
    {

       positionMirror= GetComponent<Transform>();
    scrVariaveis.refletir=false;

    }
    
    
    void Update()
    {

        if(scrVariaveis.refletir)
        {
            LaserLigado=true;
        }
        if(!scrVariaveis.refletir)
        {
            LaserLigado=false;
        }
        
        if(LaserLigado)
        {
        lineRendererM.enabled= true;
        ShootRefrator();
        }
        else
        {
            lineRendererM.enabled= false;
        }
        //-
        // if (hit.boxColliderMirror != null)
        // {
        //     RaycastHit2D _hit= Physics2D.Raycast(laserf);
        //     soltar raio 
        // } 

//op1 achar o final do line renderer e verificar se condiz com o box collider
//op2 se tem um hit.collider, mas ele verifica um raycast desse codigo
//criar um bool publico no laser pra ver se ele tocou no espelho ou nao
        // if(espelho esta sendo atingido por raio)
        // {
        //     Debug.Log("pode dale");
        // }
        //-
    }

//só ativar se estiver sendo atingido pelo raio
    void ShootRefrator()
    {
        if (Physics2D.Raycast(positionMirror.position, transform.right))
        {
            //if saidalaser=up transoform.up
            RaycastHit2D _hit= Physics2D.Raycast(saidaLaser.position,transform.up,defDistanceRay);
            Draw2DRay(saidaLaser.position, _hit.point);
        }
        else
        {
               Draw2DRay(saidaLaser.position, saidaLaser.transform.right*1000f);
       
        }
    }

    
    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRendererM.SetPosition(0,startPos);
        lineRendererM.SetPosition(1,endPos);
    }
}
