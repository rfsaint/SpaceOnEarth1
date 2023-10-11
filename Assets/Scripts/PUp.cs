using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUp : MonoBehaviour
{
    [SerializeField]
    private float Velocidade = 3.5f;
    public int PUpID = 0;
    public int _vidaParaAdicionar = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Velocidade * Time.deltaTime);
        if (transform.position.x < -9.28f)
        {
            transform.position = new Vector3(9.3f, Random.Range(5.3f, -5.3f), 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Fui Pego no flagra"); 
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null){
                if (PUpID== 0)
                {
                     player.LigarPUDisparoTriplo();
                }
                else if (PUpID == 1) 
                {
                    player.VidasExtras(_vidaParaAdicionar);
                }
                else if (PUpID == 2)
                {
                    //Adicionar Campo de Força 
                }
               
            }
            Destroy(this.gameObject);
        }
    }
}
