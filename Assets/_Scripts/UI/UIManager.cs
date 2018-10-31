using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityStandardAssets.Characters.FirstPerson;

[System.Serializable]
public class UIManager : MonoBehaviour
{

    [Header("Panel Settings")]
    public GameObject panel;
    public GameObject panelSpawnPoint;
    public float panelSpeed;
    public Animator animator;
    public GameController gameController;

    // Use this for initialization
    void Start()
    {
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
                animator.SetInteger("AnimState", 1);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                
                foreach (var tile in gameController.tiles)
                {
                    tile.GetComponent<Image>().color = Color.grey;
                }
            }
        }
    }

    private IEnumerator ClosePanel()
    {
        
        yield return new WaitForSeconds(0.2f);
        animator.SetInteger("AnimState", 0);

        foreach (var tile in gameController.tiles)
        {
            tile.GetComponent<Image>().color = Color.black;
        }


        yield return new WaitForSeconds(0.2f);
        panel.SetActive(false);
    }

}

