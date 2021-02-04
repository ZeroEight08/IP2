using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Code : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 a;
    SpriteRenderer sr;
    public Sprite sp;
    Vector3 hookSpawnPos;
    public Quaternion q;
    bool objectInRange;
    GameObject objectToPickUp;
    bool holdingAnObject;
    GameObject objectHolder;
    public bool onRope;
    GameObject rope;

    // Start is called before the first frame update
    void Start()
    {
        q = new Quaternion(0, 0, 0, 0);
        rb = GetComponent<Rigidbody2D>();
        objectInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("d") && !onRope)
        {
            a.x = 5;
            a.y = rb.velocity.y;
            rb.velocity = (a);
            this.gameObject.transform.GetChild(0).localPosition = new Vector3(1, 0, 0);

        }
        if (Input.GetKeyUp("d"))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (Input.GetKey("a") && !onRope)
        {
            a.x = -5;
            a.y = rb.velocity.y;
            rb.velocity = (a);
            this.gameObject.transform.GetChild(0).localPosition = new Vector3(-1, 0, 0);
        }
        if (Input.GetKeyUp("a"))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (Input.GetKeyDown("w"))
        {
            a.x = rb.velocity.x;
            a.y = 5;
            rb.velocity = (a);
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
            sr.sprite = sp;
            hook.transform.position = transform.position;
            hook.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            hook.GetComponent<Rigidbody2D>().gravityScale = 0;
            Vector2 hookDirection = new Vector2((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).x, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).y);
            hookDirection.Normalize();

            //this singular line is to fire the object towards the mouse
            //and i hate it so much
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
            }
            if (holdingAnObject && pickingUp == false)
            {
                Vector2 throwingDirection = new Vector2((Camera.main.ScreenToWorldPoint(Input.mousePosition) - objectToPickUp.transform.position).x, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - objectToPickUp.transform.position).y);
                throwingDirection.Normalize();
                holdingAnObject = false;
                objectToPickUp.GetComponent<Rigidbody2D>().gravityScale = 1;
                objectToPickUp.GetComponent<Rigidbody2D>().velocity = (throwingDirection) * 10;
            }
        }
        if (holdingAnObject)
        {
            objectToPickUp.transform.position = this.gameObject.transform.GetChild(0).position;
        }
        if (onRope)
        {
            this.gameObject.transform.localPosition = rope.transform.localPosition;
            this.gameObject.transform.localPosition = new Vector2(this.gameObject.transform.localPosition.x, this.gameObject.transform.localPosition.y - 1.5f);
            if (Input.GetKeyDown("e"))
            {
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(50, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && onRope)
        {
            onRope = false;
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
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
                collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                break;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Rope":
                onRope = false;
                break;
        }
    }

}
