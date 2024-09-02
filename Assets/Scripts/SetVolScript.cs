using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVolScript : MonoBehaviour
{
    public AudioSource aSource;

    private void Start() 
    {
        float val = PlayerPrefs.GetFloat("volume"); // On dit qu'il faut se souvenir de val en tant que "volume"
        /*if(val == 0)
        {
            val = 0.2f;
        }*/
        GetComponent<Slider>().value = val;
        aSource.volume = val;
    }

    /*private void Update()
    {
        float val = PlayerPrefs.GetFloat("volume");
        GetComponent<Slider>().value = val;
        aSource.volume = val;
    }*/

    public void SetMusiqueVolume()
    {
        // On récup le composant de la valeur de la slide bar
        float val = GetComponent<Slider>().value;
        // On save la value
        PlayerPrefs.SetFloat("volume", val);
        aSource.volume = val;

    }
}
