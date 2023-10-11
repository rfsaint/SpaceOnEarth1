using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAInimigo : MonoBehaviour
{
    [SerializeField]
    private float _velocInimigo = 10.0f;
    [SerializeField]
    private GameObject _explosaoDoInimigo;
    private GerenciadorIU _gerenciadorIU;
    // Start is called before the first frame update
    void Start()
    {
        _gerenciadorIU = GameObject.Find("Canvas").GetComponent<GerenciadorIU>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * _velocInimigo * Time.deltaTime);

        if (transform.position.x < -9.28f)
        {
            transform.position = new Vector3(9.3f,Random.Range( 5.3f, -5.3f), 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("O objeto " + name + " colidiu com o objeto " + other.name);
        if (other.tag == "Tiro")
        {
           Destroy(other.gameObject);
            Instantiate(_explosaoDoInimigo, transform.position, Quaternion.identity);
            _gerenciadorIU.AtualizaPlacar();
        }
       if ( other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null) 
            {
              
                player.DanoAoPlayer();
                Instantiate(_explosaoDoInimigo, transform.position, Quaternion.identity);
            }
        }
        Destroy(this.gameObject);
    }
}
