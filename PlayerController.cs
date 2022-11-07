using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script que contém todos os atributos do jogador
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
    internal SpriteRenderer spriteRenderer;
    internal Animator animator;

    //agachar
    [SerializeField] internal Collider2D standingCollider;
    [SerializeField] internal Transform overheadCheck;
    #endregion

    #region Serialized & Normal vars
    //Serialized
    [SerializeField] internal float speed = 10;
    [SerializeField] internal float jumpForce = 15;
    [SerializeField] internal float gravityDown = 6;
    [SerializeField] internal float gravityUp = 4;
    //Variáveis normais
    internal float moveinput;
    internal bool isGrounded;
    internal bool facingR = true; //Flip
    internal int japegou = 0; //verifica se ja pegou caixa
    //input
    internal bool crouchPressed;
    internal bool jumpBuffer;
    internal bool segurarPressed;
    //agachar
    internal float crouchSpeedModifier = 0.5f;
    internal float overheadRadius = 0.2f;
    #endregion


    //ativado apenas uma vez antes do start
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        scrVariaveis.item=false;
        scrVariaveis.ativado=false;
        scrVariaveis.pegavel=false;
        scrVariaveis.segurarcaixa=false;
        scrVariaveis.renovar=false;
    }
    
    void Start()
    {
        isGrounded = false; //Seta o valor inicial de isGrounded
    }
}
