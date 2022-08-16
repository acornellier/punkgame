using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject wrapper;
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text contents;
    [SerializeField] PlayerActions playerActions;
    [SerializeField] float speed;

    Queue<string> _lines;
    string _currentLine;
    Coroutine _coroutine;

    public void StartDialogue(Dialogue dialogue)
    {
        playerActions.DisablePlayerControls();
        playerActions.actions.Dialogue.Enable();
        playerActions.actions.Dialogue.Next.performed += OnNextInput;

        wrapper.SetActive(true);
        title.text = dialogue.name;
        _lines = new Queue<string>(dialogue.lines);
        TypeNextLine();
    }

    public void StopDialogue()
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

        var t = 0f;
        var charIndex = 0;
        while (charIndex < _currentLine.Length)
        {
            t += Time.deltaTime * speed;
            charIndex = Mathf.CeilToInt(t);
            contents.text = _currentLine[..charIndex];
            yield return null;
        }
    }
}