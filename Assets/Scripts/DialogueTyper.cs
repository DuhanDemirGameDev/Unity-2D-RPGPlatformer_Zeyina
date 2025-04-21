using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueTyper : MonoBehaviour
{
    public TextMeshPro dialogueText;
    public float typingSpeed = 0.05f;

    private Coroutine typingCoroutine;

    public void StartTyping(string fullText)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(fullText));
    }

    private IEnumerator TypeText(string fullText)
    {
        dialogueText.text = "";
        foreach (char letter in fullText)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
