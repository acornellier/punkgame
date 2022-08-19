using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject wrapper;
    [SerializeField] Image talkingHead;
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text contents;
    [SerializeField] PlayerActions playerActions;
    [SerializeField] float speed;
    [SerializeField] float talkingSpeed;

    Dialogue _currentDialogue;
    Queue<string> _lines;
    string _currentLine;
    Coroutine _coroutine;

    void Start()
    {
        playerActions.actions.Dialogue.Next.performed += OnNextInput;
    }

    void OnDisable()
    {
        playerActions.actions.Dialogue.Next.performed -= OnNextInput;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _currentDialogue = dialogue;

        playerActions.DisablePlayerControls();
        playerActions.actions.Dialogue.Enable();

        wrapper.SetActive(true);
        title.text = dialogue.name;
        _lines = new Queue<string>(dialogue.lines);
        TypeNextLine();
    }

    void StopDialogue()
    {
        playerActions.actions.Dialogue.Disable();
        playerActions.EnablePlayerControls();

        wrapper.SetActive(false);
    }

    void OnNextInput(InputAction.CallbackContext ctx)
    {
        if (contents.text == _currentLine)
        {
            TypeNextLine();
            return;
        }

        StopCoroutine(_coroutine);
        contents.text = _currentLine;
    }

    void TypeNextLine()
    {
        if (_lines.Count <= 0)
        {
            StopDialogue();
            return;
        }

        _coroutine = StartCoroutine(CO_TypeNextLine());
    }

    IEnumerator CO_TypeNextLine()
    {
        _currentLine = _lines.Dequeue();
        contents.text = string.Empty;

        var textTime = 0f;
        var charIndex = 0;
        var spriteTime = 0f;
        while (charIndex < _currentLine.Length)
        {
            textTime += Time.deltaTime * speed;
            charIndex = Mathf.Clamp(Mathf.CeilToInt(textTime), 0, _currentLine.Length);
            contents.text = _currentLine[..charIndex];

            spriteTime += Time.deltaTime * talkingSpeed;
            talkingHead.sprite = Mathf.Floor(spriteTime) % 2 == 0
                ? _currentDialogue.mouthClosedSprite
                : _currentDialogue.mouthOpenSprite;

            yield return null;
        }

        talkingHead.sprite = _currentDialogue.mouthClosedSprite;
    }
}