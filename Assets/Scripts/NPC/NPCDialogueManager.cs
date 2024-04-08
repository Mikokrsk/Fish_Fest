using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;


public class NPCDialogueManager : MonoBehaviour
{
    // [Header("Dialogue")]
    [SerializeField] private DialogueAsset _currentDialogueTree;
    [SerializeField] private float _printSpeed;
    [SerializeField] private float _afterPrintDelay;
    private VisualElement _dialogueBox;
    private Label _dialogueLabel;
    private Label _headerLabel;

    //  [Header("Answers")]
    [SerializeField] private StyleSheet _answersStyleSheet;
    private VisualElement _answersBox;
    private ScrollView _answersScrollView;

    public static NPCDialogueManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        _dialogueBox = UIHandler.Instance._uiDocument.rootVisualElement.Q<VisualElement>("DialogueBackground");
        _answersBox = UIHandler.Instance._uiDocument.rootVisualElement.Q<VisualElement>("AnswersBox");
        _answersScrollView = UIHandler.Instance._uiDocument.rootVisualElement.Q<ScrollView>("AnswersScrollView");
        _dialogueLabel = UIHandler.Instance._uiDocument.rootVisualElement.Q<Label>("DialogText");
        _headerLabel = UIHandler.Instance._uiDocument.rootVisualElement.Q<Label>("HeaderText");
        _dialogueBox.style.display = DisplayStyle.None;
        _answersBox.style.display = DisplayStyle.None;
    }

    public void StartDialogue(DialogueAsset dialogueTree, string name, int startSection)
    {
        ResetDialogueBox();
        _currentDialogueTree = dialogueTree;
        _headerLabel.text = name;
        _dialogueBox.style.display = DisplayStyle.Flex;
        StartCoroutine(RunDialogue(dialogueTree, startSection));
    }

    public void EndDialogue()
    {
        ResetDialogueBox();
        _dialogueBox.style.display = DisplayStyle.None;
    }

    public void ResetDialogueBox()
    {
        StopAllCoroutines();
        _answersBox.style.display = DisplayStyle.None;
        _headerLabel.text = null;
        _currentDialogueTree = null;
        _answersScrollView.Clear();
    }

    IEnumerator RunDialogue(DialogueAsset dialogueTree, int section)
    {
        for (int i = 0; i < dialogueTree.dialogueSections[section].dialogue.Length; i++)
        {
            StartCoroutine(PrintPhrase(dialogueTree.dialogueSections[section].dialogue[i]));
            yield return new WaitForSeconds(Int32.Parse(dialogueTree.dialogueSections[section].dialogue[i].Length.ToString()) * _printSpeed + _afterPrintDelay);
        }
        if (dialogueTree.dialogueSections[section].endAfterDialogue)
        {
            _dialogueBox.style.display = DisplayStyle.None;
            EndDialogue();
            yield break;
        }
        else
        {
            StartCoroutine(PrintPhrase(dialogueTree.dialogueSections[section].branchPoint.question));
            yield return new WaitForSeconds(Int32.Parse(dialogueTree.dialogueSections[section].branchPoint.question.Length.ToString()) * _printSpeed + _afterPrintDelay);
            ShowAnswers(dialogueTree.dialogueSections[section].branchPoint.answers);
        }
    }

    IEnumerator PrintPhrase(string phrase)
    {
        _dialogueLabel.text = null;
        for (int i = 0; i < phrase.Length; i++)
        {
            _dialogueLabel.text += phrase[i];
            yield return new WaitForSeconds(_printSpeed);
        }
    }

    public void ShowAnswers(DialogueAsset.Answer[] answers)
    {
        _answersBox.style.display = DisplayStyle.Flex;

        for (int i = 0; i < answers.Length; i++)
        {
            var answerButton = CreateAnswerButton(_currentDialogueTree, answers[i]);
            _answersScrollView.Insert(i, answerButton);
        }
    }

    public Button CreateAnswerButton(DialogueAsset dialogueTree, DialogueAsset.Answer answer)
    {
        var answerButton = new Button();
        answerButton.AddToClassList("AnswerText");
        answerButton.text = answer.answerLabel;
        if (answer.nextDialogueSection >= 0)
        {
            answerButton.clicked += () =>
            {
                foreach (var gameEvent in answer.gameEvents)
                {
                    gameEvent.Raise();
                }
                StartDialogue(dialogueTree, _headerLabel.text, answer.nextDialogueSection);
            };
        }
        else
        {
            answerButton.clicked += () =>
            {
                foreach (var gameEvent in answer.gameEvents)
                {
                    gameEvent.Raise();
                }
                EndDialogue();
            };
        }

        return answerButton;
    }
}