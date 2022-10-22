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

    public GameObject[] players; //GameObject array para corrigir bugs da transição de cena 
    //private Vector3 playerSpawn; //Spawnpoint do player

    //agachar
    [SerializeField] internal Collider2D standingCollider;
    [SerializeField] internal Transform overheadCollider;
    #endregion

    #region Serialized & Normal vars
    //Serialized
    [SerializeField] internal float speed;
    [SerializeField] internal float jumpForce;
    [SerializeField] internal float moveinput;
    [SerializeField] internal float gravityDown = 6;
    [SerializeField] internal float gravityUp = 4;
    //Variáveis normais
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
        rb = GetComponent<Rigidbody2D>();

        //VariaveisGlobais.itemAtivo = false;
        //VariaveisGlobais.ativado = false;
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

        #region Coments
        //item
        //item.SetActive(VariaveisGlobais.itemAtivo);

        // if (transform.position.x > 9.5f || transform.position.x < -9.5f || transform.position.y < -5.5f)
        // {
        //     transform.position = playerSpawn;
        // }


        // if (Input.GetKeyDown(KeyCode.F) && scrVariaveis.pegavel || Input.GetKeyDown(KeyCode.F) && scrVariaveis.segurarcaixa)
        // {
        //     scrVariaveis.segurarcaixa = true;
        //     japegou++;
        //     Debug.Log("ja pegou" + japegou);

        // }
        // if (Input.GetKeyDown(KeyCode.F) && japegou==2 && scrVariaveis.segurarcaixa)
        // {
        //     scrVariaveis.segurarcaixa=false;
        //     japegou=0;
        //     Debug.Log("Solto");
        // }
        #endregion
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

        void encontraPosini(){

        //"spawn" do player em cada cena após a transi��o
        transform.position = GameObject.FindWithTag("Posini").transform.position;

    }
}
