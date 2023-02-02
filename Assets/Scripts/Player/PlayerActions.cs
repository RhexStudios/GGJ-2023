using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    /*O personagem irá:
        - Semear uma plantinha em áreas específicas
        - Utilizar um portal para viajar entre espaços
        - Atacar
    */

    //Semear 
    public GameObject[] seeds;
    public float seedTime = .3f;
    public bool isSeeding;
    public bool canSeed;

    void Start() {
        canSeed = false;
        isSeeding = false;
    }

    void Update()
    {

        Seeding();
    }


    public void OnTriggerStay2d(Collider2D col) 
    {
        if(col.gameObject.tag == "seedground")
            canSeed = true;
    }

    public void OnTriggerExit2D(Collider2D col) {
        if(col.gameObject.tag == "seedground")
            canSeed = false;
    }

    public void Seeding() 
    {
        int seed;
        if(Input.GetKeyDown("x") && canSeed)
        {
            isSeeding = true;
            seed = Random.Range( 0, seeds.Length );
            Instantiate(seeds[seed]);
        }
    }
}
