using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;

    public Vector2 move;
    public Vector2 look;
    public bool next;
    public bool prev;

    private bool nextPressed;
    private bool prevPressed;
    private bool pausePressed;
    private bool resumePressed;

    public bool resume;
    public bool pause;
    public bool cursorInputForLook = true;
    public bool cursorLocked = true;

    public Vector2 Selector;
    public bool confirm;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log(move);
        move  = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (cursorInputForLook)
        {
            look = context.ReadValue<Vector2>();
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        SetCursorState(cursorLocked);
    }
    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            nextPressed = true;
        }
    }

    public void OnBack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            prevPressed = true;
        }
    }

    private void LateUpdate()
    {
        next = nextPressed;
        prev = prevPressed;
        pause = pausePressed;
        resume = resumePressed;

        // 一度使ったらリセット
        nextPressed = false;
        prevPressed = false;
        pausePressed = false;
        resumePressed = false;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            pausePressed = true;
            //playerInput.SwitchCurrentActionMap("UI");
        }
    }
    public void OnResume(InputAction.CallbackContext context)
    {
        Debug.Log(resume);
        if (context.started)
        {
            resumePressed = true;
            //playerInput.SwitchCurrentActionMap("Player");
        }
    }

    public void OnSelecter(InputAction.CallbackContext context)
    {
        Selector = context.ReadValue<Vector2>();
    }

    public void OnConfirm(InputAction.CallbackContext context)
    {
        confirm = context.performed;
    }
}
