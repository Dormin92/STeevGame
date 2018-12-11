using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour {

    public void Awake()
    {
        if (PlayerPrefs.HasKey("Music Volume"))
            gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Music Volume");
    }

    public void UpdateVolumePrefs()
    {
        PlayerPrefs.SetFloat("Music Volume", gameObject.GetComponent<Slider>().value);
    }
}
