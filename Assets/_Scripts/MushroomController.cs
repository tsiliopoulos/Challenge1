using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(_renameMushroom());
	}

	// Update is called once per frame
	void Update () {

	}

  private IEnumerator _renameMushroom() {

    yield return new WaitForSeconds(0.5f);
    this.name = "Mushroom";
  }
}
