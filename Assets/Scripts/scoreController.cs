using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreController : MonoBehaviour
{
    int MyScore=0;
    [SerializeField]TMPro.TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTextObject();
    }

    // Update is called once per frame
    void Update()
    {
        //print("Score:"+MyScore);
    }
    public void WinScore(int score)
    {
        MyScore += score;
        UpdateTextObject();
    }
    public void LoseScore(int score)
    {
        MyScore -= score;
        UpdateTextObject();
    }
    public void UpdateTextObject()
    {
        scoreText.text = MyScore.ToString();
        if (MyScore<0)
        {   //oyun bitti
            //scoreText.text = "X";
            Time.timeScale = 0f;
        }
    }
}
