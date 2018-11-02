using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameController : MonoBehaviour {

	public List<GameObject> tiles;
  public static List<GameObject> gameObjects = new List<GameObject>();

	public static bool GamePlaying;
    public static GameObject previousObject;
    public Text gameStatusLabel;
    public  static Button Undo;
    public static Button Redo;
	public static AudioSource enemyHitSound;

	public static int numCentipedeHeads;

	// Use this for initialization
	void Start () {
		GamePlaying = false;
        Undo = GameObject.Find("Undo").GetComponent<Button>();
        Redo = GameObject.Find("Redo").GetComponent<Button>();

        enemyHitSound = GetComponent<AudioSource>();
		numCentipedeHeads = 0;
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnGameStart() {
		GamePlaying = true;
        gameStatusLabel.text = "Game Status: PLAYING";
		PlayerController.canMove = true;
	}

	public void OnGameStop() {
		GamePlaying = false;
        gameStatusLabel.text = "Game Status: STOPPED";
		PlayerController.canMove = false;
	}
}

