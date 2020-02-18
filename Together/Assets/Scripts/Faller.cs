using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faller : MonoBehaviour
{
    Rigidbody2D rb;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void DoSpecialEffect(Heart h)
    {
        return;
    }

    public virtual void Break()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet b = collision.gameObject.GetComponent<Bullet>();
        Heart h = collision.gameObject.GetComponent<Heart>();
        Faller f = collision.gameObject.GetComponent<Faller>();
        if (b != null)
        {
            //meets bullet, both break
            b.Break();
            Break(); 
        }
        if (h != null)
        {
            //meets heart, do effect
            DoSpecialEffect(h);
        }
        if (f != null)
        {
            //meets other faller, break
            Break();
        }
    }

    void OnBecameInvisible()
    {
        Break();
    }

}
