using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CentipedeController : MonoBehaviour
{

    public float speed;
    public GameObject canvas;

    private bool isFacingRight;
    private bool isResetting;

    // Use this for initialization
    void Start()
    {
		this.name = "Centipede_Head";
        isResetting = false;
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((GameController.GamePlaying) && (!isResetting) && (!UIManager.isVisible))
        {
            _move();

            _checkbounds();
        }
    }

    // PRIVATE METHODS
    private void _move()
    {
        if (isFacingRight)
        {
            transform.position += Vector3.right * speed;
            transform.localScale = new Vector3(0.33f, 0.33f, 0.33f);
        }
        else
        {
            transform.position += Vector3.left * speed;
            transform.localScale = new Vector3(-0.33f, 0.33f, 0.33f);
        }
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
        yield return new WaitForSeconds(3.0f);
        transform.localPosition = new Vector3(UnityEngine.Random.Range(-350.0f, 350.0f), 200.0f, 0.0f);
        isResetting = false;
        GetComponent<CanvasGroup>().alpha = 1.0f;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((GameController.GamePlaying) && (!isResetting) && (!UIManager.isVisible))
        {
            isFacingRight = (isFacingRight) ? false : true;

            ContactPoint2D[] contacts = new ContactPoint2D[4];
            if (GetComponent<Rigidbody2D>().GetContacts(contacts) < 2)
            {
                transform.position += Vector3.down * speed * 10;
            }
        }

    }
}