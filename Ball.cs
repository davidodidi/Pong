using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Needed to manipulate the UI
using UnityEngine.UI;

public class Ball : MonoBehaviour {

    // Ball default speed
    public float speed = 30;

    // Reference to the balls Rigidbody component
    private Rigidbody2D rigidBody;

    // Reference the AudioSource for the Ball
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {

        rigidBody = GetComponent<Rigidbody2D>(); //reference to my rigid body component
        rigidBody.velocity = Vector2.right * speed; // this will make the ball to move to the AI paddle
	}

    private void OnCollisionEnter2D(Collision2D col) //called anytime our ball collides with something
    {
        //Check if we collided with LeftPaddle or RightPaddle
        if((col.gameObject.name == "LeftPaddle") ||
          (col.gameObject.name == "RightPaddle"))
        {
            HandlePandleHit(col);
        }

        //WallBottom or WallTop

        if ((col.gameObject.name == "WallBottom") ||
          (col.gameObject.name == "WallTop"))
        {

            SoundManager.Instance.PlayOneShot
            (SoundManager.Instance.wallBloop);

        }

        //LeftGoal or RightGoal
        if ((col.gameObject.name == "LeftGoal") ||
          (col.gameObject.name == "RightGoal"))
        {
            //play sound effect
            SoundManager.Instance.PlayOneShot
            (SoundManager.Instance.goalBloop);

            if(col.gameObject.name == "LeftGoal")
            {
                IncreaseTextUIScore("RightScoreUI");
            }

             else if (col.gameObject.name == "RightGoal")
            {
                IncreaseTextUIScore("LeftScoreUI");
            }


            //move the ball to the middle
            transform.position = new Vector2(0, 0); //this changes the ball position to the middle whenever a goal is scored
        }
    }

    void HandlePandleHit(Collision2D col) //this will handle how the ball would go when hit, col is what it collided with
    {

        float y = BallHitPaddleWhere(transform.position, //ball's position, y is where the ball would move if the ball  its the top, middle or bottom of the paddle
            col.transform.position, //paddle's position
            col.collider.bounds.size.y); //height of the paddle

        // Calculate direction of ball
        // A vector is a line pointing from an origin
        // to a point x, y
        // Magnitude is the length of the line
        Vector2 dir = new Vector2();

        // If(0, 1) is straight up and down is (0, -1), 
		// normalized would change our vector into a value 
		// between 0 and 1
        if (col.gameObject.name == "LeftPaddle")
        {
            dir = new Vector2(1, y).normalized;

        }

        if (col.gameObject.name == "RightPaddle")
        {
            dir = new Vector2(-1, y).normalized;

        }

        // Change the velocity / direction of the ball
        // You assign a vector to velocity here
        rigidBody.velocity = dir * speed;

        // Call for SoundManager to play paddle sound
        SoundManager.Instance.PlayOneShot
            (SoundManager.Instance.hitPaddleBloop);

    }

    // Calculate where the ball hits the paddle by dividing
    // the ball's y coordinate by the paddles height
    // If the ball hits above the midpoint ricochet up
    // and vice versa
    float BallHitPaddleWhere(Vector2 ball, Vector2 paddle, float paddleHeight)
    {
        return (ball.y - paddle.y) / paddleHeight;
    }

    void IncreaseTextUIScore(string textUIName) //thus will update the score whenever a goal is scored
    {
        // Find the matching text UI component
        var textUIComp = GameObject.Find(textUIName).GetComponent<Text>();
        int score = int.Parse(textUIComp.text); //this saves what the current score is

        score++; //this updates the score

        textUIComp.text = score.ToString(); //this converts the score to a String and updates the UI
    }

}
