using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float veloc;
    public float dashSpeed = 10f;
    public float dashDuration = 0.5f;
    public float entradaHorizontal;
    public float entradaVertical;
    public GameObject pfMissel;
    public GameObject DisparoTriplo;
    public bool possoDarDisparoTriplo = false;
    public float tempoDeDisparo = 0.3f;
    public float podeDisparar = 0.0f;
    private bool methodActivated = false;
    public int _vidasLimite = 5;// Limite máximo de vidas do jogador
    public float rotationSpeed = 5000000000000.0f;
    private Quaternion initialRotation;
    private GerenciadorIU _iuGerenciador;
    private bool isDashing = false;
    private Rigidbody2D rb;
    public GameObject _dashingPlayer;
    public bool possoUsarDash = false;
    public bool possoUsarCampoDeForca = false;
    private int currentLives = 1;// Inicializa com uma vida
    public GameObject _explosaoPlayer;
    public void DanoAoPlayer()
    {
        _vidasLimite--;
        _iuGerenciador.AtualizaVidas(_vidasLimite);

        if (_vidasLimite < 1)
        {
            Instantiate(_explosaoPlayer, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
      
       
    }

    public void VidasExtras(int amount) 
    {
        _vidasLimite++;
         if (_vidasLimite < 5)
        {
            currentLives += amount;
            currentLives = Mathf.Clamp(currentLives, 0, _vidasLimite);
           _iuGerenciador.AtualizaVidas(_vidasLimite);
        }
        else if ( _vidasLimite != 5 )
        {
            _iuGerenciador.AtualizaPlacar();
            DanoAoPlayer();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Método Start de" + this.name);
        veloc = 3.75f;
        transform.position = new Vector3(-7.6f,-0.36f,0);
        initialRotation = transform.rotation;
        _iuGerenciador = GameObject.Find("Canvas").GetComponent<GerenciadorIU>();
        if (_iuGerenciador != null ) 
        {
            _iuGerenciador.AtualizaVidas(_vidasLimite);
        }
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (isDashing == true)
        {
            // Ative o GameObject do sprite do Dash
            _dashingPlayer.SetActive(false);
        }
        else if (isDashing == false) 
        {

            
            // Desative o GameObject do sprite do Dash
            _dashingPlayer.SetActive(true);
        }


        if (Input.GetKeyDown(KeyCode.L) && !isDashing)
        {
            
            StartCoroutine(Dash1());
        }

        if (Input.GetKeyDown(KeyCode.J) && !isDashing)
        {
            StartCoroutine(Dash2());
        }

        if (Input.GetKeyDown(KeyCode.K) && !isDashing)
        {
            StartCoroutine(Dash3());
        }

        if (Input.GetKeyDown(KeyCode.I) && !isDashing)
        {
            StartCoroutine(Dash4());
        }

        this.Movimento(); 
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
         Disparo();
            // Lógica para o jogador enquanto o jogo não estiver pausado
            // Por exemplo, movimento, ataque, etc.

 if (methodActivated)
        {
            // Coloque aqui o código que deseja executar quando o método estiver ativado.
            Debug.Log("Método ativado!");
        }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                // Rotaciona o jogador para cima
                transform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime);
                
            }
            // Verifica se o jogador pressionou a tecla "S" para girar para baixo
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                // Rotaciona o jogador para baixo
                transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
            }
            else
            {
                // Se nenhuma tecla estiver sendo pressionada, retorna à rotação inicial
                transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, Time.deltaTime * 2.0f);
            }

        }
 }

   
    private void Disparo()
    {
      if ( Input.GetKeyDown(KeyCode.Space)){

           if( Time.time > podeDisparar )
           {
              if(possoDarDisparoTriplo == true)
            {
               Instantiate(DisparoTriplo,transform.position + new Vector3(-0.03f,0,0),Quaternion.identity);
            }
            else
            {
                Instantiate(pfMissel,transform.position + new Vector3 (-0.03f,0,0),Quaternion.identity);
            }
               podeDisparar = Time.time + tempoDeDisparo ;
           }
       }
    }
    private void Movimento(){
         float entradaHorizontal = Input.GetAxis("Horizontal");
         transform.Translate(Vector3.right * entradaHorizontal * Time.deltaTime*veloc);
         if (transform.position.y > 5.45f){
            transform.position = new Vector3(transform.position.x,5.45f,0);
         } else if(transform.position.x < -8.60f){
            transform.position = new Vector3(-8.60f,transform.position.y,0);
         }     
        float entradaVertical = Input.GetAxis("Vertical");
         transform.Translate(Vector3.up * entradaVertical * Time.deltaTime*veloc);
          if (transform.position.x > 10.35f){
            transform.position = new Vector3(10.35f,transform.position.y,0);
         } else if(transform.position.y < -5.5f){
            transform.position = new Vector3(transform.position.x,-5.5f,0);
         }

        float rotationAmount = entradaVertical * rotationSpeed * Time.deltaTime;
        transform.Rotate( 0, 0, rotationAmount);
    }

    public void LigarPUDisparoTriplo(){
        possoDarDisparoTriplo = true;
        StartCoroutine(DisparoTriploRotina());
    }

    public IEnumerator DisparoTriploRotina (){ 
        yield return new WaitForSeconds(7.0f);
        possoDarDisparoTriplo = false;
    }

    IEnumerator Dash1()
    {
        isDashing = true;
        
        Vector2 dashDirection = Vector3.right * Mathf.Sign(rb.velocity.x); // Dash na direção em que o jogador está se movendo
        rb.velocity = dashDirection * dashSpeed;


        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector3.zero; // Parar o jogador após o dash
        isDashing = false;
    }

    IEnumerator Dash2()
    {
        isDashing = true;

        Vector2 dashDirection = Vector3.left * Mathf.Sign(rb.velocity.x); // Dash na direção em que o jogador está se movendo
        rb.velocity = dashDirection * dashSpeed;


        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector3.zero; // Parar o jogador após o dash
        isDashing = false;
    }

    IEnumerator Dash3()
    {
        isDashing = true;

        Vector2 dashDirection = Vector3.down * Mathf.Sign(rb.velocity.y); // Dash na direção em que o jogador está se movendo
        rb.velocity = dashDirection * dashSpeed;


        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector3.zero; // Parar o jogador após o dash
        isDashing = false;
    }

    IEnumerator Dash4()
    {
        isDashing = true;

        Vector2 dashDirection = Vector3.up * Mathf.Sign(rb.velocity.y); // Dash na direção em que o jogador está se movendo
        rb.velocity = dashDirection * dashSpeed;


        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector3.zero; // Parar o jogador após o dash
        isDashing = false;
    }

   
} 
