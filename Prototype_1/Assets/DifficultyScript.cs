using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyScript : MonoBehaviour {

    public void Awake()
    {
        if (PlayerPrefs.HasKey("Difficulty"))
            gameObject.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("Difficulty");
    }

    public void UpdateDifficultyPrefs()
    {
        PlayerPrefs.SetInt("Difficulty", gameObject.GetComponent<Dropdown>().value);
    }
}
