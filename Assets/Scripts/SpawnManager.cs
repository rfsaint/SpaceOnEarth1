using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _inimigoPrefab;
    [SerializeField]
    private GameObject [] _powerUps;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RotinaGeracaoInimigo());
        StartCoroutine(RotinaGeracaoPU());

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator RotinaGeracaoInimigo(){

       while (true) 

        {

            Instantiate(_inimigoPrefab, new Vector3(-9.28f, Random.Range(5.3f, -5.3f), 0), Quaternion.identity);
            yield return new WaitForSeconds(6.0f);

        }
    }

    public IEnumerator RotinaGeracaoPU()
    {
        while (true)
        {
            int PowerUpsAleatorio  = Random.Range(0, 2);

            Instantiate((_powerUps[PowerUpsAleatorio]), new Vector3(-9.28f, Random.Range(5.3f, -5.3f), 0), Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(7, 2));
        }
    }
}
