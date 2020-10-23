using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rulePlayerController : playerController {
    //class for player control in the rule stage, where the player has direct control
    private bool haveInput;
    public ruleStageManager manager;//script that manages this stage
    int refund = 0;

    new void Start()
    {
        base.Start();
        haveInput = false;
    }

	new void OnGUI()
    {
        if (Event.current.isKey && !haveInput)
        {
            //this fires on any keypress, uses parent function but handles movement as well
            base.OnGUI();

            movePlayer(direction);
            haveInput = true;
        }

    }
    new void FixedUpdate()
    {

    }
    //OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
    new void OnTriggerEnter2D(Collider2D other)
    {
        
        base.OnTriggerEnter2D(other);
        //Check the colliding tag
        if (other.gameObject.GetComponent<SpriteRenderer>().color == GameData.goodColor)
        {
             refund = -1;
        }
        else
        {
            refund = 10;
        }
        rigidBody.velocity = Vector2.zero;//stop player
        manager.FixedUpdate();//update GUI

        StartCoroutine(waitSeconds(3));
        
    }

    IEnumerator waitSeconds(int seconds)
    {
        //function to wait a few seconds then load the level
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("mainLevel");
        GameData.score += refund;
    }
}
