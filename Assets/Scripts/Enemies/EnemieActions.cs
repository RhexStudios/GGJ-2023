using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieActions : MonoBehaviour
{
	public Animator anim;
	public SpriteRenderer sprite;

	private int life = 1;
	public float moveSpeed = 1f;

	public int startingPoint;
	public Transform[] pointsToMove;

	public BoxCollider2D colliderAtk;
	public BoxCollider2D colliderCheckAtk;

	void Start()
	{
		anim = GetComponent<Animator>();
		transform.position = pointsToMove[startingPoint].transform.position;
	}

	void Update()
	{
		if(startingPoint == 0)
		{
			sprite.flipX = true;
			colliderAtk.offset = new Vector2(-0.41f, 0.4f);
			colliderCheckAtk.offset = new Vector2(0.41f, 0.4f);
		}
		else
		{
            sprite.flipX = false;
            colliderAtk.offset = new Vector2(0.41f, 0.4f);
            colliderCheckAtk.offset = new Vector2(0.41f, 0.4f);
            
        }

		if(life == 0)
		{
			EnemyDead();
		}
	}

    private void FixedUpdate()
	{
		Move();
	}

	void Move()
	{
		transform.position = Vector2.MoveTowards(transform.position, pointsToMove[startingPoint].transform.position, moveSpeed * Time.deltaTime);

		if ( MeeleeCheckAttack.checkAttack == true)
		{
			StartCoroutine("Attack");
		}

		if(transform.position == pointsToMove[startingPoint].transform.position)
		{
			startingPoint += 1;
		}

		if(startingPoint == pointsToMove.Length)
		{
			startingPoint = 0;
		}
	}

	IEnumerator Attack()
	{
		anim.SetBool("Attack", true);
		moveSpeed = 0;

		yield return new WaitForSeconds(0.85f);
		anim.SetBool("Attack", false);
		moveSpeed = 1;

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Attack")
		{
			life--;

			if(life < 1)
			{
				StopCoroutine("Attack");
				EnemyDead();
			}
		}
	}

	private void EnemyDead()
	{
		life = 0;
		moveSpeed = 0;
		Destroy(transform.gameObject.GetComponent<BoxCollider2D>());
		Destroy(transform.gameObject.GetComponent<Rigidbody2D>());
		Destroy(colliderAtk);
		Destroy(colliderCheckAtk);
		Destroy(this);
	}
}
