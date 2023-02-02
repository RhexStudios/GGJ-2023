using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGround : MonoBehaviour
{
    //Sementes, Verificação de se o solo pode ser plantado, tempo da planta nascer
    public GameObject[] seeds;
    public bool isEmpty;
    public float seedTime = .6f;

    //Verificando Jogador
    public PlayerActions actions;
    public Rigidbody2D rb;

    void Awake()
    {
        isEmpty = true;
        actions = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerActions>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Germinate();
    }

    public void Germinate() 
    {
        int seed;
        if (isEmpty && ( actions.isSeeding == true )) 
        {
            seedTime -= Time.deltaTime;            
            if (seedTime <= 0) 
            {
                seed = Random.Range( 0, seeds.Length );
                Instantiate(seeds[seed]);
                isEmpty = false;
            }
        }
    }
}
