using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script que contém todos os atributos do jogador
public class PlayerScript : MonoBehaviour
{
    #region Components & Classes
    Rigidbody2D rb;
    [SerializeField] LayerMask groundLayer;
    SpriteRenderer spriteRenderer;
    Animator animator;

    //agachar
    [SerializeField] Collider2D standingCollider;
    [SerializeField] Transform overheadCheck;
    #endregion

    #region Serialized & Normal vars
    //Serialized
    [SerializeField] float speed = 10;
    [SerializeField] float jumpForce = 15;
    [SerializeField] float gravityDown = 6;
    [SerializeField] float gravityUp = 4;
    //Variáveis normais
    float inputX;
    bool isGrounded;
    bool facingR = true; //Flip
    int japegou = 0; //verifica se ja pegou caixa
    //input
    bool crouchPressed;
    bool jumpBuffer;
    bool segurarPressed;
    //agachar
    float crouchSpeedModifier = 0.5f;
    float overheadRadius = 0.2f;
    #endregion

    //ativado apenas uma vez antes do start
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        scrVariaveis.item=false;
        scrVariaveis.ativado=false;
        scrVariaveis.pegavel=false;
        scrVariaveis.segurarcaixa=false;
        scrVariaveis.renovar=false;
    }

    void Start()
    {
        isGrounded = false; //Set do valor inicial de isGrounded
        //DontDestroyOnLoad(gameObject); //função do unity que não destroy player na transição de cena
        //playerSpawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        #region Movimento
        Segurar();

        //Set velocidade em X (horizontal)
        if (standingCollider.enabled || !scrVariaveis.segurarcaixa)
            rb.velocity = new Vector3(inputX * speed, rb.velocity.y, 0);
        else
            rb.velocity = new Vector3(inputX * speed * crouchSpeedModifier, rb.velocity.y, 0);
        /* if (playerContrlSCRIPT.rb.velocity.y < 0) playerContrlSCRIPT.rb.gravityScale = playerContrlSCRIPT.gravityDown;
         else playerContrlSCRIPT.rb.gravityScale = playerContrlSCRIPT.gravityUp;*/

        Flip();
        Jump();
        #endregion

        #region Input Saving
        inputX = Input.GetAxisRaw("Horizontal");

        //agachar
        if (Input.GetButtonDown("Crouch"))
            crouchPressed = true;
        else if (Input.GetButtonUp("Crouch"))
            crouchPressed = false;

        //pular
        if (Input.GetButtonDown("Jump"))
            jumpBuffer = true;

        //segurar
        if (Input.GetKeyDown(KeyCode.F))
            segurarPressed = true;
        #endregion
    }

    #region Movimento
    private void FixedUpdate()
    {
        Agachar(crouchPressed);
    }

    //Pular
    internal void Jump()
    {
        if (jumpBuffer)
        {
            if (standingCollider.enabled && isGrounded && !scrVariaveis.segurarcaixa)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
            }
            jumpBuffer = false;
        }
    }

    //agachar
    internal void Agachar(bool crouchFlag)
    {
        if (!crouchFlag)
        {
            if (Physics2D.OverlapCircle(overheadCheck.position, overheadRadius, groundLayer))
            {
                crouchFlag = true;
            }
        }

        if (isGrounded)
        {
            standingCollider.enabled = !crouchFlag;

            if (crouchFlag)
                animator.SetBool("Crouch", true);
            else
                animator.SetBool("Crouch", false);
        }
    }

    //Virar Sprite do player
    internal void Flip()
    {
        if (inputX < 0) facingR = false;
        else if (inputX > 0) facingR = true;

        spriteRenderer.flipX = !facingR;    //transform.localRotation = new Quaternion(0, 0, 0, 0);   //Normal
                                            //transform.localRotation = new Quaternion(0, 180, 0, 0); //Virado
    }

    void Segurar()
    {
        if (segurarPressed && scrVariaveis.pegavel || segurarPressed && scrVariaveis.segurarcaixa)
        {
            scrVariaveis.segurarcaixa = true;
            japegou++;
            Debug.Log("ja pegou" + japegou);
        }
        if (segurarPressed && japegou==2 && scrVariaveis.segurarcaixa)
        {
            scrVariaveis.segurarcaixa=false;
            japegou=0;
            Debug.Log("Soltou");
        }
    }
    #endregion

    #region Colisões
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
            isGrounded = true;

        if (collision.gameObject.tag == "Item")
        {
            //Debug.Log("É ISSO!");
            Destroy(collision.gameObject);
            scrVariaveis.item = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
            isGrounded = false;
    }
    #endregion

}
