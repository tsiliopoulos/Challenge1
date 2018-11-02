using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;

public class Slot : MonoBehaviour, IDropHandler
{

    const string DLL_NAME = "DragDropPlugin";
    [DllImport(DLL_NAME)]
    private static extern void Initalize();
    [DllImport(DLL_NAME)]
    private static extern System.IntPtr CreateCommand(int x, int y);
    [DllImport(DLL_NAME)]
    private static extern void ExecuteCommand(System.IntPtr commands);
    [DllImport(DLL_NAME)]
    private static extern void Undo();
    [DllImport(DLL_NAME)]
    private static extern void Redo();
    [DllImport(DLL_NAME)]
    private static extern bool CheckGridAtPosition(int x, int y);
    [DllImport(DLL_NAME)]
    private static extern void CleanUp();

    public delegate void DoSomething();
    public static List<DoSomething> myDelegates = new List<DoSomething>();
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
        GridPosition myGrid = GetComponent<GridPosition>();
        if (!CheckGridAtPosition(myGrid.x, myGrid.y))
        {
            Draggable.itemBeingDragged.transform.SetParent(transform);
            Draggable.itemBeingDragged.transform.localScale = new Vector3(0.66f, 0.66f, 0.66f);
            Draggable.itemBeingDragged.gameObject.GetComponent<BoxCollider2D>().enabled = true;
           
            GameController.gameObjects.Add(Draggable.itemBeingDragged);

            if (GameController.Undo.onClick.GetPersistentEventCount()>=0)
            {
  
                GameController.Undo.onClick.RemoveAllListeners();
            }
            myDelegates.Add(delegate { UndoCommand(); });
            GameController.Undo.onClick.AddListener(
                     delegate { UndoItem(); }
                     );

            if (GameController.Redo.onClick.GetPersistentEventCount() >= 0)
            {
                GameController.Redo.onClick.RemoveAllListeners();
            }
            
            GameController.Redo.onClick.AddListener(
                     delegate { RedoCommand(Draggable.itemBeingDragged); }
                     );

            if (GameController.previousObject!=null &&!GameController.previousObject.activeSelf)
            {
                Destroy(GameController.previousObject);
                GameController.previousObject = null;
            }
            GameController.previousObject = Draggable.itemBeingDragged;
            Destroy(Draggable.itemBeingDragged.gameObject.GetComponent("Draggable"));
            ExecuteCommand(myGrid.gridPointer);
            /* CPP goes here */
        }
    }

    public void UndoCommand()
    {
        Undo();
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void UndoItem()
    {
        DoSomething something = myDelegates[myDelegates.Count-1];
        something.Invoke();
        myDelegates.RemoveAt(myDelegates.Count-1);
    }
    public void RedoCommand(GameObject item )
    {
        Redo();
        item.SetActive(true);

    }
}
