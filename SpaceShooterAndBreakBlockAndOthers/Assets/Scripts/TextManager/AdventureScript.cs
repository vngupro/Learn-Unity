using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdventureScript : MonoBehaviour
{   
    //Initialisation 
    [SerializeField] TextMeshProUGUI textComponent;
    [SerializeField] State startingState;
    private Queue<State> sentences;

    // State : compose of textarea and next State queue
    State state;
    // Start is called before the first frame update
    void Start()
    {
        state = startingState;
        textComponent.text = state.GetStateStory(); // GetStateStory : return Text
        sentences = new Queue<State>();
        ManageState();
        
    }

    // Update is called once per frame
    void Update()
    {  
    }

    // ManageState : get and display next state in canvas
    private void ManageState(){
        var nextStates = state.GetNextStates();
        foreach (State sentence in nextStates){
            sentences.Enqueue(sentence);
        }
    }

    public void DisplayNextSentence(){
        if(sentences.Count == 0){
            EndDialogue();
            return;
        }
        State sentence = sentences.Dequeue();
        textComponent.text = sentence.GetStateStory();
    }

    void EndDialogue(){
        Debug.Log("end dialogue");
    }
}
