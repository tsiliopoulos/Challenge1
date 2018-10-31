using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float speed;
	public float screenHeight;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.up * speed;
		_checkBounds();
	}

	private void _checkBounds() {
		if(transform.position.y > screenHeight) {
			Destroy(this.gameObject);
		}
	}
}
