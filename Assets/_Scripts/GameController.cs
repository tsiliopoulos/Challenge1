using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameController : MonoBehaviour {

	public List<GameObject> tiles;

	public static bool GamePlaying;


	// Use this for initialization
	void Start () {
		GamePlaying = false;
	}
	
	// Update is called once per frame
	void Update () {
		var count = 0;
		/* 
		foreach (var tile in tiles)
		{
			if(tile.transform.childCount > 0) {
				var child = tile.transform.GetChild(0).gameObject;

				if(count % 6 == 0) {
					child.transform.position += Vector3.left * 5;
					
				}
				else {
					child.transform.position += Vector3.right * 5;
				}
				
			}
			count++;
		}
		*/
	}

	public void OnGameStart() {
		GamePlaying = true;
		PlayerController.canMove = true;
	}
}
