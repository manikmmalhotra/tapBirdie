using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class highscore : MonoBehaviour
{
    Text Highscore;

    void OnEnable()
    {
        Highscore = GetComponent<Text>();
        Highscore.text = "High score:" + PlayerPrefs.GetInt("HighScore").ToString();
    }
}
