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
    public float seedDuration = 1f;
    public bool isSeeding;
    public bool canSeed;
    public SeedGround sg;

    public Rigidbody2D rb;

    void Start() {    
        isSeeding = false;
        canSeed = false;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        Seeding();
        RefreshSeed();
    }

    #region - Seed Action - 

    public void Seeding() 
    {
        if(Input.GetKeyDown("x") && canSeed && ( sg.isEmpty == true ))
        {
            isSeeding = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D col) 
    {
        if(col.gameObject.tag == "seedground") 
        {
            canSeed = true;
            sg = col.gameObject.GetComponent<SeedGround>();
        }
    }

    public void OnTriggerExit2D(Collider2D col) 
    {
        if(col.gameObject.tag == "seedground")
            canSeed = false;
    }

    public void RefreshSeed() 
    {
        if (isSeeding) 
        {
            Vector3 stay = new Vector3(0f, 0f, 0f);
            rb.velocity = stay;
            seedDuration -= Time.deltaTime;
            if (seedDuration <= 0) 
            {
                seedDuration = 1f;
                isSeeding = false;
            }
        }
    }

    #endregion


}
