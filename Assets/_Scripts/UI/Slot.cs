using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{

  void start() {

  }

    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!item)
        {
            Draggable.itemBeingDragged.transform.SetParent(transform);
            Draggable.itemBeingDragged.transform.localScale = new Vector3(0.66f, 0.66f, 0.66f);
            Draggable.itemBeingDragged.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            GameController.gameObjects.Add(Draggable.itemBeingDragged);
            Destroy(Draggable.itemBeingDragged.gameObject.GetComponent("Draggable"));

            /* CPP goes here */
        }
    }

}
