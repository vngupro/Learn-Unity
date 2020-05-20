using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public  TextMeshProUGUI nameText;
    public  TextMeshProUGUI dialogueText;
    private Queue<string> sentences;

    SceneLoader sceneManage;
    public Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        StartDialogue(dialogue);
        sceneManage = FindObjectOfType<SceneLoader>();
    }

    private void Update() {
        // Talk();
    }

    public void StartDialogue(Dialogue dialogue){
        // Debug.Log("Start Conversation with " + dialogue.name);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string sentence in dialogue.sentence){
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if(sentences.Count == 0){
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        // Debug.Log(sentence + sentences.Count);
        // dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence (string sentence){
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray()){
            dialogueText.text += letter;
            yield return null;
        }
    }
    void EndDialogue(){
        Debug.Log("Ending Talk");
        sceneManage.LoadNextScene();
    }
}
