using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeBodyController : MonoBehaviour
{

    public GameObject centipedeHead;
    public GameObject canvas;
    // Use this for initialization
    void Start()
    {
        this.name = "Centipede_Body";
    }

    // Update is called once per frame
    void Update()
    {
        if ((GameController.GamePlaying) && (!UIManager.isVisible))
        {
            if (GameController.numCentipedeHeads > 0)
            {
                Debug.Log(GameController.numCentipedeHeads);
            }
            else
            {
                var newCentipedeHead = Instantiate(centipedeHead, transform.position, Quaternion.identity);
                newCentipedeHead.transform.SetParent(canvas.transform);
				newCentipedeHead.GetComponent<RectTransform>().rect.Set(transform.position.x, transform.position.y, 190.0f, 190.0f);
                newCentipedeHead.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                newCentipedeHead.GetComponent<BoxCollider2D>().enabled = true;
                Destroy(this.gameObject);
            }
        }
    }
}
