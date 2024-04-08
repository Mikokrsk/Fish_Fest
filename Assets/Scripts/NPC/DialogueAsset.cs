using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "myAssets/Dialogue Asset")]
public class DialogueAsset : ScriptableObject
{
    public DialogueSection[] dialogueSections;

    [System.Serializable]
    public struct DialogueSection
    {
        [TextArea]
        public string[] dialogue;
        public bool endAfterDialogue;
        public BranchPoint branchPoint;
    }
    [System.Serializable]
    public struct BranchPoint
    {
        [TextArea]
        public string question;
        public Answer[] answers;
    }
    [System.Serializable]
    public struct Answer
    {
        public string answerLabel;
        public GameEvent[] gameEvents;
        public int nextDialogueSection;
    }
}