using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
//using UnityStandardAssets.Characters.FirstPerson;

[System.Serializable]
public class UIManager : MonoBehaviour
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

    public static Dictionary<Vector2,System.IntPtr> pointers=new Dictionary<Vector2, System.IntPtr>();

    private int x = 0;
    private int y = 0;

    [Header("Panel Settings")]
    public GameObject panel;
    public GameObject panelSpawnPoint;
    public float panelSpeed;
    public Animator animator;
    public GameController gameController;
    public GameObject shipInPanel;

    public static bool isVisible;

    // Use this for initialization
    void Start()
    {
        Initalize();
        isVisible = false;
        foreach (var tile in gameController.tiles)
        {
           tile.GetComponent<GridPosition>().gridPointer = CreateCommand(x, y);
            tile.GetComponent<GridPosition>().x = x;
            tile.GetComponent<GridPosition>().y = y;
            x++;
            if (x==12)
            {
                y++;
                x = 0;
            }
        }
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (panel.activeInHierarchy)
            {
                StartCoroutine(this.ClosePanel());
            }
            else
            {
                panel.SetActive(true);
                isVisible = true;
                animator.SetInteger("AnimState", 1);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                PlayerController.canMove = false;
                shipInPanel.SetActive(true);
                


                foreach (var tile in gameController.tiles)
                {

                    tile.GetComponent<Image>().color = Color.grey;
                }
            }
        }
        /* 
        if (GameController.GamePlaying == true && panel.activeInHierarchy)
        {
            StartCoroutine(this.ClosePanel());
        }
        */
    }

    private IEnumerator ClosePanel()
    {

        yield return new WaitForSeconds(0.2f);
        animator.SetInteger("AnimState", 0);

        foreach (var tile in gameController.tiles)
        {
            tile.GetComponent<Image>().color = Color.black;
        }

        if(GameController.GamePlaying) {
            PlayerController.canMove = true;
        }

        if(GameController.gameObjects.Count > 0) {
          foreach (var gameObject in GameController.gameObjects)
          {
              gameObject.transform.SetParent(transform);
          }
          GameController.gameObjects.Clear();
        }


        yield return new WaitForSeconds(0.2f);
        isVisible = false;
        shipInPanel.SetActive(false);
        panel.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        CleanUp();
    }
}

