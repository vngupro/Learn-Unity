using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "ProjectMullover/State", order = 0)]
public class State : ScriptableObject
{
    //  [SerializeField] private string MainTitle;
    [TextArea(10,14)] [SerializeField] string storyText;
    // Queue work FIFO
    [SerializeField] State[] nextStates;
    public string GetStateStory(){
        return storyText;
    }
    public State[] GetNextStates(){
        return nextStates;
    }
}
