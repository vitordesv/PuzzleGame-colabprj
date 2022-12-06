using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esse script contém as váriaveis de movimento do jogador assim como seus inputs, tem funções agachar e pular
public class PlayerController : MonoBehaviour
{
    #region SUB-SCRIPTS
    [SerializeField] internal PlayerInput playerInputSCRIPT;
    [SerializeField] internal PlayerMovement playerMoveSCRIPT;
    [SerializeField] internal PlayerSysCollision playerCollideSCRIPT;
    #endregion
    
    #region Components & Classes
    internal Rigidbody2D rb;
    [SerializeField] internal LayerMask groundLayer;
    //agachar
    [SerializeField] internal Collider2D standingCollider;
    [SerializeField] internal Transform overheadCollider;
    //Sprites/animações
    public Animator animator;
    #endregion

    #region Serialized & Normal vars
    //Serialized
    [SerializeField] internal float speed;
    [SerializeField] internal float jumpForce;
    [SerializeField] internal float gravityDown = 6;
    [SerializeField] internal float gravityUp = 4;
    //Variáveis normais
    internal bool isGrounded;
    internal bool facingR = true; //Flip
    internal int japegou = 0; //verifica se ja pegou caixa
    //input
    internal float moveinput;
    internal bool crouchPressed;
    internal bool jumpBuffer;
    internal bool segurarPressed;
    //agachar
    [SerializeField] internal float crouchSpeedModifier = 0.5f;
    internal float overheadRadius = 0.2f;
    //pegar caixa
    [SerializeField] internal Transform holdingPoint;
    [SerializeField] internal Transform rayPoint;
    private GameObject objetoSegurado;
    [SerializeField] private LayerMask layerIndex;
    private float rayDistance=1f;   
    
    //tentativa de dar collider para caixa segurada
    [SerializeField] private Collider2D caixaCollider;
    #endregion


    //ativado apenas uma vez antes do start
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        scrVariaveis.item = false;
        scrVariaveis.ativado = false;
        scrVariaveis.pegavel = false;
        scrVariaveis.segurarcaixa = false;
        scrVariaveis.renovar = false;
        scrVariaveis.chaves = 0;
    }

    // Start is called before the first frame update, ou quando objeto é ativo, só é chamado se estiver ativo
    void Start()
    {
        isGrounded = false;
        caixaCollider.enabled = false;     
    }

    // Update is called once per frame
    void Update()
    {
        Segurar();
        //muda sprite
        if(!standingCollider.enabled)
        {
            animator.SetBool("crouched", true);
            // render.sprite=agachado;
        } 
        else animator.SetBool("crouched",false);
    }

    void Segurar()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right * -moveinput, rayDistance, layerIndex);
        Debug.DrawRay(rayPoint.position, transform.right * -moveinput * rayDistance, Color.red);
        if(hitInfo.collider != null && !Porta.aberta) 
        {
            //Debug.Log("tocavel");
            scrVariaveis.pegavel = true;
            //sem precisar da colisão no script da caixa

            if (segurarPressed && objetoSegurado == null)
            {
                //Debug.Log("Segurou");
                scrVariaveis.segurarcaixa = true;
                objetoSegurado = hitInfo.collider.gameObject;
                objetoSegurado.GetComponent<Rigidbody2D>().isKinematic = true;
                objetoSegurado.transform.position=holdingPoint.position;
                objetoSegurado.transform.SetParent(transform);
                caixaCollider.enabled = true;
            }
        } 
        else if (segurarPressed || !scrVariaveis.segurarcaixa && objetoSegurado != null)
        {
            //Debug.Log("LARGOU");
            scrVariaveis.segurarcaixa = false;
            objetoSegurado.GetComponent<Rigidbody2D>().isKinematic = false;
            objetoSegurado.transform.SetParent(null);
            objetoSegurado = null;
            caixaCollider.enabled = false;
        }
    }
}