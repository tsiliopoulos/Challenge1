﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public enum Directions
{
    RIGHT,
    LEFT,
    UP,
    DOWN
}

[System.Serializable]
public class PlayerController : MonoBehaviour
{
    [DllImport("EntityComponentSystemChallenge")]
    public static extern IntPtr CreateEntity();  //create our entity

    [DllImport("EntityComponentSystemChallenge")]
    public static extern IntPtr CreateComponent();  //create our component (in this case, only score)

    [DllImport("EntityComponentSystemChallenge")]
    public static extern void AttachComponent(IntPtr entity, IntPtr component);

    [DllImport("EntityComponentSystemChallenge")]
    public static extern void RemoveComponent(IntPtr entity, IntPtr component);

    [DllImport("EntityComponentSystemChallenge")]
    public static extern void UpdateComponent(IntPtr component);

    [DllImport("EntityComponentSystemChallenge")]
    public static extern int GetScore(IntPtr component);

    [DllImport("EntityComponentSystemChallenge")]
    public static extern void DeleteEntity(IntPtr entity);
    [Header("Options")]
    public int playerSpeed;
	public static bool canMove;
	public GameObject bullet;

    private bool canMoveLeft;
    private bool canMoveRight;
    private bool canMoveUp;
    private bool canMoveDown;
    private Directions lastMove;

	private GameObject canvas;

    public System.IntPtr myEntity;
    public System.IntPtr myEntityComponent;

    public bool isInGame;


    // Use this for initialization
    void Start()
    {
		canvas = GameObject.FindWithTag("Canvas");
		canMove = false;
        _resetMove();
        myEntity = CreateEntity();
        myEntityComponent = CreateComponent();
        AttachComponent(myEntity, myEntityComponent);
        isInGame = false;
    }

    // Update is called once per frame
    void Update()
    {
		if(canMove && (!UIManager.isVisible)) {
			_getInput();
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
        if(!canMoveLeft) {
			canMoveLeft = true;
		}

		if(!canMoveRight) {
			canMoveRight = true;
		}

		if(!canMoveUp) {
			canMoveUp = true;
		}

		if(!canMoveDown) {
			canMoveDown = true;
		}
    }

	// PRIVATE METHODS

	private void _getInput()
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

		if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) {

			if(Time.frameCount % 10 == 0) {
				_bulletFire();
			}

		}
	}

    private void _bulletFire()
    {
        var newBullet = Instantiate(bullet, transform.position + new Vector3(0.0f, 65.0f, 0.0f), Quaternion.identity);
		//newBullet.transform.parent = canvas.transform;
        newBullet.transform.SetParent(canvas.transform);

		newBullet.GetComponent<RectTransform>().rect.Set(newBullet.transform.position.x,newBullet.transform.position.y, 64, 64);
		newBullet.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
	}

    private void _resetMove()
    {
        canMoveLeft = true;
        canMoveRight = true;
        canMoveUp = true;
        canMoveDown = true;
    }

    private void OnApplicationQuit()
    {
        DeleteEntity(myEntity);
    }
}

