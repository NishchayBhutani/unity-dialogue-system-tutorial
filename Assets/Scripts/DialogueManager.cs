using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour
{

    private Queue<string> sentences;
    Label npcName;
    Label npcDialogue;
    NPCDialogueScript dialogueScript;

    public UIDocument dialogueBox;

    void Start()
    {
        dialogueScript = dialogueBox.GetComponent<NPCDialogueScript>();
        sentences = new Queue<string>();
        VisualElement rootElement = dialogueBox.rootVisualElement;
        npcName = rootElement.Q<Label>("NPCName");
        npcDialogue = rootElement.Q<Label>("NPCDialogue");
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        npcName.text = dialogue.name;
        dialogueScript.isVisible = true;

        foreach(string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0) {
            EndConversation();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence) {
        npcDialogue.text = "";
        foreach(char letter in sentence.ToCharArray()) {
            npcDialogue.text += letter;
            yield return null;
        }
    }

    private void EndConversation()
    {
        Debug.Log("End of conversation");
        dialogueScript.isVisible = false;
    }
}
