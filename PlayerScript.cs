using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script que contém todos os atributos do jogador
public class PlayerScript : MonoBehaviour
{
    #region Components & Classes
    internal Rigidbody2D rb;
    [SerializeField] internal LayerMask groundLayer;
    SpriteRenderer spriteRenderer;

    public GameObject[] players; //GameObject array para corrigir bugs da transição de cena 
    //private Vector3 playerSpawn; //Spawnpoint do player

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


    // Start is called before the first frame update, ou quando objeto é ativo, só é chamado se estiver ativo
    void Start()
    {
        isGrounded = false;
        //função do unity que não destroy player na transição de cena
        DontDestroyOnLoad(gameObject);
        //playerSpawn = transform.position;
    }

    //ativado apenas uma vez antes do start
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        scrVariaveis.item=false;
        scrVariaveis.ativado=false;
        scrVariaveis.pegavel=false;
        scrVariaveis.segurarcaixa=false;
        scrVariaveis.renovar=false;
    }

    // Update is called once per frame
    void Update()
    {
        Segurar();

        #region Movimento
        //Set velocidade em X (horizontal)
        if (standingCollider.enabled || !scrVariaveis.segurarcaixa)
            rb.velocity = new Vector3(moveinput * speed, rb.velocity.y, 0);
        else
            rb.velocity = new Vector3(moveinput * speed * crouchSpeedModifier, rb.velocity.y, 0);
        /* if (playerContrlSCRIPT.rb.velocity.y < 0) playerContrlSCRIPT.rb.gravityScale = playerContrlSCRIPT.gravityDown;
         else playerContrlSCRIPT.rb.gravityScale = playerContrlSCRIPT.gravityUp;*/

        Flip();
        Jump();
        #endregion

        #region Input Saving
        moveinput = Input.GetAxisRaw("Horizontal");

        //agachar
        if (Input.GetButtonDown("Crouch"))
        {
            crouchPressed = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouchPressed = false;
        }

        //pular
        if (Input.GetButtonDown("Jump"))
        {
            jumpBuffer = true;
        }

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
        if (jumpBuffer && isGrounded && !scrVariaveis.segurarcaixa)
        {
            if (standingCollider.enabled)
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
            if (Physics2D.OverlapCircle(overheadCollider.position, overheadRadius, groundLayer))
            {
                crouchFlag = true;
            }
        }

        if (isGrounded)
            standingCollider.enabled = !crouchFlag;
    }

    //Virar Sprite do player
    internal void Flip()
    {
        if (moveinput < 0) facingR = false;
        if (moveinput > 0) facingR = true;

        if (facingR) spriteRenderer.flipX = false; //transform.localRotation = new Quaternion(0, 0, 0, 0);
        else spriteRenderer.flipX = true; //transform.localRotation = new Quaternion(0, 180, 0, 0); 
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
            Debug.Log("Solto");
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
            Debug.Log("É ISSO!");
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


    /*bool Grounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }*/

    private void OnLevelWasLoaded(int level) 
    {
        encontraPosini();//encontra o spawn do player na cena

        //liga o GameObject array ao elemento no unity com a tag "Player"
        players = GameObject.FindGameObjectsWithTag("Player");

        //destroy as c�pias do player geradas pela transi��o de cena
        if (players.Length > 1) Destroy(players[1]);
    }

    void encontraPosini()
    {
        //"spawn" do player em cada cena após a transi��o
        transform.position = GameObject.FindWithTag("Posini").transform.position;
    }
}
