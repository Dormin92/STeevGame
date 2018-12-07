using UnityEngine;
using System.Collections;

public class QuitOnClick : MonoBehaviour
{

    public void Quit()
    {
        if (UnityEditor.EditorApplication.isPlaying == false)
        {
            Application.Quit();
        }
    }
}