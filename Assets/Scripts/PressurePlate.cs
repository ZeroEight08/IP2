using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject door;
    Vector2 doorPos;
    public LevelController lc;

    // Start is called before the first frame update
    void Start()
    {
        doorPos = new Vector2(door.transform.position.x, door.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Player":
                lc.platesPushed++;
                if(lc.platesPushed > 0)
                {
                    door.transform.position = new Vector2(1000, 1000);
                }
                break;
        }    
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Player":
                lc.platesPushed--;
                if(lc.platesPushed == 0)
                {
                    door.transform.position = doorPos;
                }
                break;
        }
    }

}
