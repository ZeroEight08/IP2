using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{
    GameObject player;
    Sprite[] sp;
    GameObject platform;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine("timedDestruction");
        sp = Resources.LoadAll<Sprite>("Sprites");
        platform = GameObject.Find("Platform");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "HookPoint":
                Vector2 pullDirection = new Vector2(collision.gameObject.transform.position.x - player.transform.position.x, collision.gameObject.transform.position.y - player.transform.position.y);
                pullDirection.Normalize();
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(pullDirection.x, pullDirection.y) * 20;
                break;

            case "FireCreate":
                GameObject fire = new GameObject();
                fire.AddComponent<Rigidbody2D>();
                fire.AddComponent<BoxCollider2D>().isTrigger = true;
                fire.AddComponent<SpriteRenderer>().sprite = sp[0];
                fire.GetComponent<SpriteRenderer>().color = Color.red;
                fire.AddComponent<FireScript>();
                fire.transform.position = new Vector2(platform.transform.position.x, platform.transform.position.y + 10);
                break;
        }
    }


    IEnumerator timedDestruction()
    { 
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
