using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text score;

    [Range(1,1000000)]
    public int sc;

    private void Start()
    {
        sc = 0;
        actualizaScore();
    }

    public void Reset()
    {
        sc = 0;
        actualizaScore();
    }

    public void addScore()
    {
        sc++;
        actualizaScore();
    }

    void actualizaScore()
    {
        score.text = sc.ToString();
    }
}
