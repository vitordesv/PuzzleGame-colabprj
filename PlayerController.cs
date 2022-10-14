using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveinput;
    [SerializeField] private float gravityDown = 6;
    [SerializeField] private float gravityUp = 4;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;

    public GameObject [] players; //GameObject array para corrigir bugs da transição de cena 

    //public GameObject item;
    //private bool itemAtivo;

    public bool facingR = true;
    private Vector3 playerSpawn;

    private int japegou = 0;

    private Rigidbody2D rb;

    //agachar
    [SerializeField] Collider2D standingCollider;
    [SerializeField] private Transform overheadCollider;
    bool crouchPressed;
    const float overheadRadius = 0.2f;
    bool abaixado;

    // Start is called before the first frame update
    void Start()
    {
        //função do unity que não destroy player na transição de cena
        DontDestroyOnLoad(gameObject);
        playerSpawn = transform.position;
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Awake(){

        VariaveisGlobais.itemAtivo = false;
        VariaveisGlobais.ativado = false;
        scrVariaveis.item=false;
        scrVariaveis.ativado=false;
        scrVariaveis.pegavel=false;
        scrVariaveis.segurarcaixa=false;

    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < 0) rb.gravityScale = gravityDown;
        else rb.gravityScale = gravityUp;
        Jump();
        //item.SetActive(VariaveisGlobais.itemAtivo);
        // if (transform.position.x > 9.5f || transform.position.x < -9.5f || transform.position.y < -5.5f)
        // {
        //     transform.position = playerSpawn;
        // }
        
        if (Input.GetKeyDown(KeyCode.F) && scrVariaveis.pegavel || Input.GetKeyDown(KeyCode.F) && scrVariaveis.segurarcaixa)
        {
            scrVariaveis.segurarcaixa = true;
            japegou++;
            Debug.Log("ja pegou" + japegou);
            
        }
        if (Input.GetKeyDown(KeyCode.F) && japegou==2 && scrVariaveis.segurarcaixa)
        {
            scrVariaveis.segurarcaixa=false;
            japegou=0;
            Debug.Log("Solto");
        }


//agachar
        if (Input.GetButtonDown("Crouch"))
        {
            Debug.Log("AGACHADO REAL");
            crouchPressed = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            Debug.Log("EM PÉ");
            if(!abaixado)crouchPressed = false;
        }

      
     if(!crouchPressed)
     {
         standingCollider.enabled = true;
     }
      
    }

    private void FixedUpdate()
    {
           Agachar(crouchPressed);
        moveinput = Input.GetAxisRaw("Horizontal");
        if(moveinput < 0)
        {
            facingR = false;
        }
        if(moveinput > 0)
        {
            facingR = true;
        }
        Flip();
       
        rb.velocity = new Vector3(moveinput * speed, rb.velocity.y, 0);

    }
//agachar
    void Agachar(bool crouchFlag)
    {

       

        if (!crouchFlag)
        {
            
            if (Physics2D.OverlapCircle(overheadCollider.position,overheadRadius, groundLayer))
            {
            Debug.Log("ABAIXO");
            crouchFlag = true;
            
            abaixado=true;
            }
        }
         if (Grounded())  
        {
            
            standingCollider.enabled = !crouchFlag;
            // if(crouchPressed)
            // {
            //     standingCollider.enabled=false;
            // }
            //  if(!crouchPressed) standingCollider.enabled=true;
        }
    }

    void Flip()
    {
        
        Vector3 scale = transform.localScale;
        if(!scrVariaveis.segurarcaixa){
            if(facingR == true && transform.localScale.x < 0 )
            {
            scale.x *= -1;
            transform.localScale = scale; 
            }
            if(facingR == false && transform.localScale.x > 0)
            {
            scale.x *= -1;
            transform.localScale = scale;
            }
        }
    }
    void Jump()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && Grounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
        }
    }
    bool Grounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size,0,Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }


    private void OnLevelWasLoaded(int level) {

        encontraPosini();//encontra o spawn do player na cena

        //liga o GameObject array ao elemento no unity com a tag "Player"
        players = GameObject.FindGameObjectsWithTag("Player");

        //destroy as c�pias do player geradas pela transi��o de cena
        if (players.Length > 1) Destroy(players[1]);
    
    }

        void encontraPosini(){

        //"spawn" do player em cada cena após a transi��o
        transform.position = GameObject.FindWithTag("Posini").transform.position;

    }

    void OnTriggerEnter2D(Collider2D collision){

        
        if(collision.gameObject.tag == "Item"){
            
            Debug.Log("É ISSO!");
            Destroy(collision.gameObject);
            VariaveisGlobais.itemAtivo = true;

        }

        if(collision.gameObject.tag == "Receptor" && VariaveisGlobais.itemAtivo){

                Debug.Log("ATIVADO GARAI");
                VariaveisGlobais.itemAtivo = false;
                VariaveisGlobais.ativado = true;

        }



    }
    void OnCollisionEnter2D(Collision2D collide){

        if(collide.gameObject.tag == "Porta" && VariaveisGlobais.ativado){

                Destroy(collide.gameObject);

            }

         //abrir porta
         if(collide.gameObject.tag == "Porta" && scrVariaveis.ativado)
        {   
            //fazer animação
            //ficar trigger pra entrar na parede
            //collision.gameObject.setActive(false);
            Destroy(collide.gameObject);
        }

    }
 //void Raycast()

    /*private void OnTriggerEnter2D(Collider2D collision){

        if(collision.gameObject.CompareTag("Item")){

            Debug.Log("FUNFOU!");

        }

    }*/
}