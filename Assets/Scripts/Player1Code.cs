using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player1Code : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 playerSpeed;
    SpriteRenderer sr;
    public Sprite[] sp;
    Vector3 hookSpawnPos;
    public Quaternion q;
    bool objectInRange;
    GameObject objectToPickUp;
    bool holdingAnObject;
    GameObject objectHolder;
    public bool onRope;
    GameObject rope;
    Transform[] bottomOfRope;
    bool onGround;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
        q = new Quaternion(0, 0, 0, 0);
        rb = GetComponent<Rigidbody2D>();
        objectInRange = false;
        sp = Resources.LoadAll<Sprite>("");
        rb.drag = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey("d") && !onRope && rb.velocity.x < 8)
        {
            playerSpeed.x = 0.25f;
            playerSpeed.y = 0;
            rb.velocity += playerSpeed;
            this.gameObject.transform.GetChild(0).localPosition = new Vector3(3, 0, 0);
        }
        if (Input.GetKeyDown("d"))
        {
            anim.SetTrigger("startWalkingRight");
        }
        if (Input.GetKeyUp("d"))
        {
            anim.SetTrigger("stopWalkingRight");
        }
        if (Input.GetKeyDown("a"))
        {
            anim.SetTrigger("startWalkingLeft");
        }
        if (Input.GetKeyUp("a"))
        {
            anim.SetTrigger("stopWalkingLeft");
        }
        if (Input.GetKey("a") && !onRope && rb.velocity.x > -8)
        {
            playerSpeed.x = -0.25f;
            playerSpeed.y = 0;
            rb.velocity += playerSpeed;
            this.gameObject.transform.GetChild(0).localPosition = new Vector3(-3, 0, 0);
        }
        if (onGround)
        {
            rb.drag = 5;
            rb.sharedMaterial.friction = 0.4f;
        }
        if (!onGround)
        {
            rb.drag = 3;
            rb.sharedMaterial.friction = 0;
        }
        if (Input.GetKeyDown("w"))
        {
            playerSpeed.x = rb.velocity.x;
            playerSpeed.y = 20;
            rb.velocity = playerSpeed;
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("IdleRight") || anim.GetCurrentAnimatorStateInfo(0).IsName("BearWalkRight"))
            {
                anim.SetTrigger("JumpRight");
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject hook = new GameObject("Hook");
            hook.AddComponent<Rigidbody2D>();
            hook.AddComponent<SpriteRenderer>();
            hook.AddComponent<HookScript>();
            hook.AddComponent<BoxCollider2D>().size = new Vector2(1, 1);
            hook.GetComponent<BoxCollider2D>().isTrigger = true;
            sr = hook.GetComponent<SpriteRenderer>();
            sr.sprite = sp[0];
            hook.transform.position = transform.position;
            hook.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            hook.GetComponent<Rigidbody2D>().gravityScale = 0;
            Vector2 hookDirection = new Vector2((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).x, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).y);
            hookDirection.Normalize();

            
            hook.GetComponent<Rigidbody2D>().velocity = (hookDirection) * 25;
        }

        if (Input.GetKeyDown("e"))
        {
            bool pickingUp = false;

            if (objectInRange && !holdingAnObject)
            {
                objectToPickUp.GetComponent<Rigidbody2D>().gravityScale = 0;
                objectToPickUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                objectToPickUp.transform.position = this.gameObject.transform.GetChild(0).position;
                holdingAnObject = true;
                pickingUp = true;
                objectToPickUp.layer = 10;
            }
            if (holdingAnObject && pickingUp == false)
            {
                Vector2 throwingDirection = new Vector2((Camera.main.ScreenToWorldPoint(Input.mousePosition) - objectToPickUp.transform.position).x, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - objectToPickUp.transform.position).y);
                throwingDirection.Normalize();
                holdingAnObject = false;
                objectToPickUp.GetComponent<Rigidbody2D>().gravityScale = 1;
                objectToPickUp.GetComponent<Rigidbody2D>().velocity = (throwingDirection) * 10;
                objectInRange = false;
                objectToPickUp.layer = 9;
                objectToPickUp = null;
            }
        }
        if (holdingAnObject)
        {
            objectToPickUp.transform.position = this.gameObject.transform.GetChild(0).position;
        }
        if (onRope)
        {
            this.gameObject.transform.position = bottomOfRope[1].position;
        }

        if (Input.GetKeyDown(KeyCode.Space) && onRope)
        {
            onRope = false;
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = rope.GetComponent<Rigidbody2D>().velocity;
            StartCoroutine("gettingOffRope");
            rope = null;
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Box":
                objectInRange = true;
                objectToPickUp = collision.gameObject;
                break;

            case "Level Trigger":
                SceneManager.LoadScene("Test Area 2");
                break;
        }

        switch (collision.gameObject.layer)
        {
            case 9:
                objectInRange = true;
                objectToPickUp = collision.gameObject;
                break;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Box":
                objectInRange = false;
                objectToPickUp = null;
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Rope":
                onRope = true;
                rope = collision.gameObject;
                this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                bottomOfRope = collision.gameObject.GetComponentsInChildren<Transform>();
                Physics2D.IgnoreLayerCollision(7, 6, true);
                break;

            case "Platform":
                if (collision.gameObject.GetComponent<ObjectFireCheck>().onFire)
                {
                    Destroy(this.gameObject);
                }
                break;
        }    
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Square":
                onGround = false;
                break;
        }
        switch (collision.gameObject.layer)
        {
            case 8:
                onGround = false;
                break;
        }
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Platform":
                if (collision.gameObject.GetComponent<ObjectFireCheck>().onFire)
                {
                    Destroy(this.gameObject);
                }
                break;

            case "Rope":
                print("test");
                break;

            case "Square":
                onGround = true;
                break;
        }

        switch (collision.gameObject.layer)
        {
            case 8:
                onGround = true;
                break;
        }
    }


    IEnumerator gettingOffRope()
    {
        yield return new WaitForSeconds(2);
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
}
