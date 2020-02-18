using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{

    //reference to characters
    public Character charOne;
    public Character charTwo;

    //shield
    public GameObject shield;
    public bool isShieldOpen;
    public float shieldDuration;


    // Start is called before the first frame update
    void Start()
    {
        isShieldOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer != LayerMask.NameToLayer("Characters"))
        {
            //fail
            Faller collisionFaller = collision.gameObject.GetComponent<Faller>();
            
            if (collisionFaller == null)  //collision is not faller
            {
                Bullet b = collision.gameObject.GetComponent<Bullet>();
                if (b != null)  //collide with bullet
                {
                    b.Break();
                }
                //collisionFaller.DoSpecialEffect(this);
                Break();
            }
            
        }
    }

    public void Break()
    {
        GameController.instance.HeartIsDead(gameObject);
    }

    public void OpenShield()
    {
        if (isShieldOpen)
        {
            //restart if already open
            StopCoroutine("ShieldCountdown");
        }
        StartCoroutine("ShieldCountdown");
    }

    IEnumerator ShieldCountdown()
    {
        isShieldOpen = true;
        shield.SetActive(true);
        SpriteRenderer sr = shield.GetComponent<SpriteRenderer>();
        sr.enabled = true;
        yield return new WaitForSeconds(shieldDuration); 
        sr.enabled = false;
        yield return new WaitForSeconds(0.5f);
        sr.enabled = true;
        yield return new WaitForSeconds(0.5f);
        sr.enabled = false;
        yield return new WaitForSeconds(0.5f);
        sr.enabled = true;
        yield return new WaitForSeconds(0.5f);
        isShieldOpen = false;
        shield.SetActive(false);
    }

    //add bullets
    public void AddBullets()
    {
        charOne.AddBullets();
        charTwo.AddBullets();
    }

    public void ClearDanger()
    {
        DangerFaller[] allDF = FindObjectsOfType<DangerFaller>();
        foreach (DangerFaller df in allDF)
        {
            df.Break();
        }
    }
}
