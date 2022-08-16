using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] bool triggerOnStart;
    [SerializeField] Dialogue dialogue;

    DialogueManager _dialogueManager;

    void Awake()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        if (!_dialogueManager)
            throw new Exception("Could not find DialogueManager");
    }

    void Start()
    {
        if (triggerOnStart) Trigger();
    }

    void Update()
    {
        if (Keyboard.current.fKey.IsPressed())
            Trigger();
    }

    void Trigger()
    {
        _dialogueManager.StartDialogue(dialogue);
        gameObject.SetActive(false);
    }
}