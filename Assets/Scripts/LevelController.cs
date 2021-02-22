using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    Player1Code pc;
    Slider slider;


    void Start()
    {
        slider = Slider.FindObjectOfType<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void volumeChange()
    {
        pc.volume = slider.value;
    }
}
