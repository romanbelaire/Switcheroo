using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour {
    protected float speed;
    protected Rigidbody2D rigidBody;
    protected int direction; //direction int representing 4 cardinal directions from top clockwise
    public float delay;//delay between movement in seconds
    private float timer;
    private float timeIntervals;//number of times FixedUpdate gets called in a delay cycle
    private int goodCount;//track number of good points reached
    

	protected void Start () {
        //initialize all variables
        goodCount = 0;
        rigidBody = GetComponent<Rigidbody2D>();
        direction = 0;
        timer = 0;
        speed = 100;

        //variables used in translation calculations later
        timeIntervals = 50;// delay / (Time.deltaTime * 2);


        //adjust level difficulty
        if (GameData.level == 1)
        {
            delay = 1.5f;
        }
        else if(GameData.level == 2)
        {
            delay = 1;
        }
        else
        {
            delay = 0.5f;
            speed = 125;
        }

    }
	
    protected void OnGUI()
    {
        //This function fires on any interface event
        switch(Event.current.keyCode)
        {
            //detect input and change direction variable correspondingly
            case KeyCode.W:
                direction = 0;
                break;
            case KeyCode.D:
                direction = 1;
                break;
            case KeyCode.S:
                direction = 2;
                break;
            case KeyCode.A:
                direction = 3;
                break;
            default://no input, keep direction the same
                break;
        }

        //adjust for inverted controls
        if(GameData.Invert)
        {
            direction += 2;
            direction %= 4;
        }
    }


	// Update is called at a fixed interval
	protected void FixedUpdate () {
        timer += Time.deltaTime;
        if (timer >= delay)
        {
            rigidBody.velocity = Vector2.zero;
            timer = 0;
            movePlayer(direction);
        }

        deaccelerate();
    }

    protected void movePlayer(int inputDirection)
    {
        //moves the player
        int x = 0;
        int y = 0;
        
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);//round the position to be centered
        switch (inputDirection)
        {
            case 0://up
                y = 1;
                break;
            case 1://right
                x = 1;
                break;
            case 2://down
                y = -1;
                break;
            case 3:
                x = -1;
                break;
            default:
                break;//there shouldn't ever be a default case here.
        }
        //create movement vector using our directional values
        Vector2 movement = new Vector2(x, y);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rigidBody.AddForce(movement * speed);
    }

    protected void deaccelerate()
    {
        //deaccelleration to make sure the player is resting by the time it moves again   
        float x = 0;
        float y = 0;
        if(rigidBody.velocity.x > 0)
        {
            x = 0 - (speed / timeIntervals);
        }
        else if(rigidBody.velocity.x < 0)
        {
            x = speed / timeIntervals;
        }

        if (rigidBody.velocity.y > 0)
        {
            y = 0 - (speed / timeIntervals);
        }
        else if (rigidBody.velocity.y < 0)
        {
            y = speed / timeIntervals;
        }

        Vector2 deacceleration = new Vector2(x, y);
        rigidBody.AddForce(deacceleration);
    }

    //OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
    protected void OnTriggerEnter2D(Collider2D other)
    {
        //Check the colliding tag
        if (other.gameObject.CompareTag("point"))
        {
            other.gameObject.SetActive(false);
            if(other.gameObject.GetComponent<SpriteRenderer>().color == GameData.goodColor)
            {
                GameData.score += 1;
                goodCount += 1;
            }
            else
            {
                GameData.score -= 10;
            }
        }
        if(goodCount == 10)
        {
            if (GameData.level < 3)
            {
                SceneManager.LoadScene("ruleLevel");
                GameData.level++;
            }
            else
            {
                SceneManager.LoadScene("ruleLevel");
                GameData.level = 1;
                GameData.score = 0;
            }
        }
    }

    
}
