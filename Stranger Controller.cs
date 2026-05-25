using UnityEngine;
using UnityEngine.Rendering;

public class StrangerController : MonoBehaviour
{
   [SerializeField] GameObject[] dialogueBox;
   [SerializeField] GameObject dialogueUI;
   [SerializeField] GameObject player;
   bool hasSpoken = false;

    void Awake()
    {
        dialogueUI.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player && !hasSpoken)
        {
            dialogueUI.SetActive(true);
            dialogueBox[0].SetActive(true);
            Time.timeScale = 0f;
            hasSpoken = true;
        }
    }

    public void NextDialogueButton()
    {
        for (int i = 0; i < dialogueBox.Length; i++)
        {
            if (dialogueBox[i].activeSelf)
            {
                dialogueBox[i].SetActive(false);
                if (i + 1 < dialogueBox.Length)
                {
                    dialogueBox[i + 1].SetActive(true);
                }

                else if (i == dialogueBox.Length - 1)
                {
                    dialogueUI.SetActive(false);
                    Time.timeScale = 1f;
                }
                break;
            }
        }
    }

}
