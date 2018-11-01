using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float speed;
    public float screenHeight;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((GameController.GamePlaying) && (!UIManager.isVisible))
        {
            transform.position += Vector3.up * speed;
        }


    }

    private void _checkBounds()
    {
        if (transform.position.y > screenHeight)
        {
            Destroy(this.gameObject);
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if ((GameController.GamePlaying) && (!UIManager.isVisible))
        {
            if (other.tag == "Enemy")
            {
                Destroy(other.gameObject);
                StartCoroutine(_destroyBullet());

                /* CPP Code goes here */
            }

            if (other.tag == "Grid")
            {
                 Destroy(this.gameObject);
            }

            if (other.tag == "Centipede")
            {
                Destroy(other.gameObject);
                StartCoroutine(_destroyBullet());
            }
        }
    }


    private IEnumerator _destroyBullet() 
    {
        GameController.enemyHitSound.Play();
        yield return new WaitForSeconds(0.05f);
        Destroy(this.gameObject);
    }
}
