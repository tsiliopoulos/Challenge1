using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpiderController : MonoBehaviour
{

    public float speed;
    public GameObject mushroom;
    public GameObject canvas;

    private bool isResetting;

    // Use this for initialization
    void Start()
    {
        isResetting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((GameController.GamePlaying) && (!isResetting) && (!UIManager.isVisible))
        {
            _move();

            if ((Time.frameCount % 40 == 0) && (transform.localPosition.y > -500.0f) && (transform.localPosition.y < 250.0f))
            {
                _plantMushroom();
            }

            _checkbounds();
        }
    }

    // PRIVATE METHODS

    private void _move()
    {
        transform.position += Vector3.down * speed;
    }

    private void _plantMushroom()
    {
        var newMushroom = Instantiate(mushroom, transform.position + new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        newMushroom.transform.SetParent(canvas.transform);
        newMushroom.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        newMushroom.GetComponent<BoxCollider2D>().enabled = true;
        newMushroom.name = "Spawn";
    }

    private void _checkbounds()
    {
        if (transform.localPosition.y <= -520.0f)
        {
            GetComponent<CanvasGroup>().alpha = 0.0f;
            isResetting = true;
            StartCoroutine(_reset());
        }
    }

    private IEnumerator _reset()
    {
        yield return new WaitForSeconds(6.0f);
        transform.localPosition = new Vector3(UnityEngine.Random.Range(-350.0f, 350.0f), 200.0f, 0.0f);
        isResetting = false;
        GetComponent<CanvasGroup>().alpha = 1.0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((GameController.GamePlaying) && (!isResetting) && (!UIManager.isVisible))
        {
            if ((other.tag == "Enemy") && (other.name != "Spawn") && (other.tag != "Centipede"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
