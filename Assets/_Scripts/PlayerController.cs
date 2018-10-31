using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Directions
{
    RIGHT,
    LEFT,
    UP,
    DOWN
}

public class PlayerController : MonoBehaviour
{

    public int playerSpeed;

    private bool canMoveLeft;
    private bool canMoveRight;
    private bool canMoveUp;
    private bool canMoveDown;

    private Directions lastMove;

    void ResetMove()
    {
        canMoveLeft = true;
        canMoveRight = true;
        canMoveUp = true;
        canMoveDown = true;
    }

    // Use this for initialization
    void Start()
    {
        ResetMove();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (canMoveUp)
            {
                transform.position += Vector3.up * playerSpeed;
                lastMove = Directions.UP;
            }

        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (canMoveLeft)
            {
                transform.position += Vector3.left * playerSpeed;
                lastMove = Directions.LEFT;
            }

        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (canMoveDown)
            {
                transform.position += Vector3.down * playerSpeed;
                lastMove = Directions.DOWN;
            }

        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (canMoveRight)
            {
                transform.position += Vector3.right * playerSpeed;
                lastMove = Directions.RIGHT;
            }

        }


    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        switch (lastMove)
        {
            case Directions.RIGHT:
                canMoveRight = false;
                break;
            case Directions.LEFT:
                canMoveLeft = false;
                break;
            case Directions.DOWN:
                canMoveDown = false;
                break;
            case Directions.UP:
                canMoveUp = false;
                break;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        ResetMove();
    }
}
