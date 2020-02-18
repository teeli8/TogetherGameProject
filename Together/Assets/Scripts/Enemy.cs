using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    //rigidbody
    Rigidbody2D rb;

    //health
    public int maxHealth;
    int currentHealth;

    //movement
    public float speed;
    public float range;

    //dropping
    public Faller dangerFaller;
    public Faller[] fallers;
    public Transform[] dropPositions;
    public Transform specialPosition;
    public float dropDuration;
    public float chanceOfDanger;




    // Start is called before the first frame update
    void Start()
    {       
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(speed, 0, 0);
        currentHealth = maxHealth;
        StartCoroutine("GenerateFaller");
    }

    // Update is called once per frame
    void Update()
    {
        //health check
        if (currentHealth <= 0)
        {
            
            return;
        }
        
            //sin movement
        float xPosition = transform.position.x;
        if(xPosition <= -range)
        {
            rb.velocity = new Vector3(speed, 0, 0);
        }
        if (xPosition >= range)
        {
            rb.velocity = new Vector3(-speed, 0, 0);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet b = collision.gameObject.GetComponent<Bullet>();
        if (b != null)
        {
            if (currentHealth > 0)
            {
                currentHealth -= b.damage;
                GameController.instance.ChangeEnemyHealthBar((float)currentHealth/maxHealth);
                if (currentHealth <= 0)
                {
                    //die
                    GameController.instance.EnemyIsDead();
                    rb.velocity = Vector3.zero;
                    StopCoroutine("GenerateFaller");
                }
                b.Break();
            }
            
        }
    }

    IEnumerator GenerateFaller()
    {
        int length = fallers.Length;
        int randI = Random.Range(0, length);
        bool isNextDanger = Random.Range(0f, 1f) < chanceOfDanger;
        yield return new WaitForSeconds(dropDuration);
        if (isNextDanger)
        {
            foreach(Transform t in dropPositions)
            {
                Instantiate(dangerFaller, t.position,Quaternion.identity);
            }   
        }
        else
        {
            Instantiate(fallers[randI], specialPosition.position, Quaternion.identity);
        }
            
        StartCoroutine("GenerateFaller");
    }


}
