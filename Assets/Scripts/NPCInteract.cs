using TMPro;
using UnityEngine;
using System.Collections;

public class NPCInteraction : MonoBehaviour
{
    private bool isPlayerNearby = false;
    public GameObject dialogueBox;
    public GameObject interactBox;
    public TextMeshPro dialogueText;
    public TextMeshPro interactText;
    private string interactWarning = "E'ye bas!";

    private string[] dialogues = {
        "Merhaba, Turpa!",
        "Zeyina'yý kötü güçlerin kaçýrdýðýný gördüm.",
        "Baþýna bir þey gelmeden Zeyinayý kurtar!",
        "Elini çabuk tut!",
        "Chat GPT seninle olsun!"
    };
    private int dialogueIndex = 0;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ShowNextDialogue();
        }
    }

    private void ShowNextDialogue()
    {
        if (!dialogueBox.activeSelf)
        {
            dialogueBox.SetActive(true);
            dialogueIndex = 0;
        }

        if (dialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[dialogueIndex];
            GetComponent<DialogueTyper>().StartTyping(dialogueText.text);
            dialogueIndex++;
        }
        else
        {
            dialogueBox.SetActive(false);
            interactBox.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactBox.SetActive(true);
            interactText.text = interactWarning;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            dialogueBox.SetActive(false);
            interactBox.SetActive(false);
        }
    }
}
