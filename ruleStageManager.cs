using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ruleStageManager : MonoBehaviour {
    //this class simply randomizes the rules of the game every time it is loaded.
    private Color[] validColors = { Color.blue, Color.cyan, Color.green, Color.magenta, Color.red, Color.yellow };
    public GameObject nPoint, sPoint, ePoint, wPoint;
    public GameObject scoreline;
    public GameObject levelline;
    private Text scoretxt;
    private Text leveltxt;

    void Start ()
    {
        randomizeColors();
        randomizePoints();
        randomizeControls();


        //setup UI link
        scoretxt = scoreline.GetComponent<Text>();
        leveltxt = levelline.GetComponent<Text>();
    }
	
	// Update is called once per frame
	public void FixedUpdate () {
        //updates UI
        scoretxt.text = GameData.score.ToString();
        leveltxt.text = GameData.level.ToString();
    }

    private void randomizeColors()
    {
        //pick random unique colors for good and bad points
        GameData.goodColor = validColors[Random.Range(0, validColors.Length)];
        do
        {
            GameData.badColor = validColors[Random.Range(0, validColors.Length)];
        }
        while (GameData.goodColor == GameData.badColor);
    }

    private void randomizePoints()
    {
        //determine which points are good and bad for this level
        if (Random.Range(0, 2) == 1)
        {
            //at random, choose the north and south points to be good
            nPoint.GetComponent<SpriteRenderer>().color = GameData.goodColor;
            sPoint.GetComponent<SpriteRenderer>().color = GameData.goodColor;

            wPoint.GetComponent<SpriteRenderer>().color = GameData.badColor;
            ePoint.GetComponent<SpriteRenderer>().color = GameData.badColor;
        }
        else
        {
            //inverse decision
            nPoint.GetComponent<SpriteRenderer>().color = GameData.badColor;
            sPoint.GetComponent<SpriteRenderer>().color = GameData.badColor;

            wPoint.GetComponent<SpriteRenderer>().color = GameData.goodColor;
            ePoint.GetComponent<SpriteRenderer>().color = GameData.goodColor;
        }
    }

    private void randomizeControls()
    {
        //determine if controls are inverted or not
        if (Random.Range(0, 2) == 1)
        {
            GameData.Invert = true;
        }
    }
}
