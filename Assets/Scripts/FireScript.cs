using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Platform":
                collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                collision.gameObject.GetComponent<ObjectFireCheck>().onFire = true;
                Destroy(this.gameObject);
                break;
        }
    }
}
