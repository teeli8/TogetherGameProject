using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public bool isCharOne;
    // Start is called before the first frame update

    //KeyCode
    KeyCode leftKeycode;
    KeyCode rightKeycode;
    KeyCode throwKeycode;
    KeyCode shootKeycode;
    
    //rigidbody
    Rigidbody2D rb;

    //speed
    public float movementSpeed;
    Vector2 rightVelocity;
    Vector2 leftVelocity;

    //holding
    public Transform holdingPoint;
    bool isHoldingHeart;

    //reference to heart
    public Heart heart;

    //throwing
    public Vector2 throwingForce;

    //shooting
    public Bullet bullet;
    public float shootingDuration;
    bool canShoot;
    public int maxBullet;
    int remainingBullet;

    //animation
    Animator anim;

    void Start()
    {

        //dynamicly decide input keys
        if (isCharOne)
        {
            leftKeycode = KeyCode.A;
            rightKeycode = KeyCode.D;
            throwKeycode = KeyCode.W;
            shootKeycode = KeyCode.Space;
        }
        else
        {
            leftKeycode = KeyCode.LeftArrow;
            rightKeycode = KeyCode.RightArrow;
            throwKeycode = KeyCode.UpArrow;
            shootKeycode = KeyCode.Return;
        }
        rb = GetComponent<Rigidbody2D>();
        isHoldingHeart = false;
        canShoot = true;
        remainingBullet = 0;
        anim = GetComponent<Animator>();
        rightVelocity = new Vector2(movementSpeed * Time.deltaTime, 0);
        leftVelocity = new Vector2(-movementSpeed * Time.deltaTime, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (GameController.instance.isFrozen)
        {
            return;
        }

        //behaviors
        if (Input.GetKey(leftKeycode))
        {
            //rb.AddForce(new Vector2(-movementSpeed, 0));
            rb.velocity = leftVelocity;
        }
        if (Input.GetKey(rightKeycode))
        {
            //rb.AddForce(new Vector2(movementSpeed, 0));
            rb.velocity = rightVelocity;
        }
        if (Input.GetKeyDown(throwKeycode) && isHoldingHeart)
        {
            ThrowIt();
        }

        //hold heart
        if (heart != null && isHoldingHeart)
        {
            heart.transform.position = holdingPoint.position;
        }

        //shooting
        if (!isHoldingHeart && remainingBullet>0 && canShoot)
        {
            if (Input.GetKeyDown(shootKeycode))
            {
                Shoot();
            }
        }

    }

    void ThrowIt()
    {
        //throwing
        isHoldingHeart = false;       
        if (heart != null)
        {
            Rigidbody2D hrb = heart.gameObject.GetComponent<Rigidbody2D>();
            hrb.velocity = throwingForce;
        }    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       // print("touch");
        if (collision.gameObject.GetComponent<Heart>() != null)
        {
            isHoldingHeart = true;
            remainingBullet = maxBullet;
        }
    }

    void Shoot()
    {
        Instantiate(bullet, holdingPoint);
        --remainingBullet;
        StartCoroutine("ShootCountdown");
    }

    IEnumerator ShootCountdown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootingDuration);
        canShoot = true;
    }


    public void AddBullets()
    {
        remainingBullet = ++maxBullet;
    }


    public void ToSad()
    {
        anim.SetTrigger("toSad");
    }

    public void ToGlad()
    {
        anim.SetTrigger("toGlad");
    }



}
