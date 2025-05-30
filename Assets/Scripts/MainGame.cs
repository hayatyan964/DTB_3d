using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainGame : MonoBehaviour
{
    public int playerTurn = 0; // 0: プレイヤー1, 1: プレイヤー2
    

    [SerializeField]
    private GameObject AnimalGenerator;
    AnimalGenerator _animGen;

    [SerializeField]
    private GameObject _inputContoller;
    private InputManager _input;
    private PlayerInput _playerInput;

    [SerializeField]
    private GameObject _UIManager;
    private UIManager _uimanager;

    GameObject GameOverLine;
    GameOver gameover;

    public int AnimalCount = 0;
    private bool isEnd = false;
    private bool isWaiting = false;
    [SerializeField]
    private float waitTimer = 3f;

    int _select = 0;

    public enum State
    {
        Start,
        Prepar,
        Move,
        Rotation,
        Drop,
        Wait,
        Pause,
        End
    }
    public State _state;
    private State prevState;
    void Start()
    {
        _select = 0;

        _animGen = AnimalGenerator.GetComponent<AnimalGenerator>();
        _input = _inputContoller.GetComponent<InputManager>();
        _playerInput = _inputContoller.GetComponent<PlayerInput>();

        _uimanager = _UIManager.GetComponent<UIManager>();

        GameOverLine = GameObject.Find("GameOverLine");
        gameover = GameOverLine.GetComponent<GameOver>();

    }

    private void Update()
    {
        // Debug.Log(_playerInput.currentActionMap);
        if (isWaiting) return;
        if(_input.pause == true) GamePause();
        if (isEnd) _state = State.End;

        switch (_state)
        {
            case State.Start:
                StateChange();
                break;
            case State.Prepar:
                _animGen.PreparAnimal();
                StateChange();
                break;
            case State.Move:
                _animGen.MoveAnimal();
                StateChange();
                break;
            case State.Rotation:
                _animGen.RotateAnimal();
                StateChange();
                break;
            case State.Drop:
                _animGen.DropAnimal();
                StateChange();
                break;
            case State.Wait:
                StartCoroutine(WaitCoroutine());
                isWaiting = true;
                break;
            case State.Pause:
                if(_input.resume == true) ResumeGame();
                break;
            case State.End:
                ResultSelector();
                ResultConfirm();
                break;
        }
    }

    private void StateChange()
    {

        switch (_state)
        {
            case State.Start:
                _state = State.Prepar;
                break;
            case State.Prepar:
                _state = State.Move;
                break;
            case State.Move:
                _uimanager.ModeText();
                if (_input.next) _state = State.Rotation;
                break;
            case State.Rotation:
                _uimanager.ModeText();
                if (_input.next) _state = State.Drop;
                if (_input.prev) _state = State.Move;
                break;
            case State.Drop:
                _uimanager.ModeText();
                _state = State.Wait;
                break;
            case State.Wait:
                _state = State.Prepar;
                break;
            case State.Pause:
                break;
            case State.End:
                break;
        }
    }

    private void TurnManage()
    {
        AnimalCount++;
        _uimanager.AnimalCountText(AnimalCount);
    }

    private IEnumerator WaitCoroutine()
    {
        yield return StartCoroutine(_uimanager.ProgressTimer(waitTimer));
        TurnManage();
        _state = State.Prepar;
        isWaiting = false;
    }

    private void GamePause()
    {
        prevState = _state;
        _state = State.Pause;
        //Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        _playerInput.SwitchCurrentActionMap("UI");
        _uimanager.ShowPause();
    }

    public void ResumeGame()
    {
        _uimanager.StopPause();

        //Time.timeScale = 1;
        _state = State.Move;
        Debug.Log("Resume:" + prevState);
        Cursor.lockState = CursorLockMode.Locked;
        _playerInput.SwitchCurrentActionMap("Player");
    }

    public void GameEnd()
    {
        //Time.timeScale = 1;
        _state = State.End;
        isEnd = true;
        Cursor.lockState = CursorLockMode.None;
        _playerInput.SwitchCurrentActionMap("Result");
        _uimanager.ShowResult(AnimalCount);
    }

    public void RestartGame()
    {
        //Time.timeScale = 1;
        isEnd = false;
        Cursor.lockState = CursorLockMode.Locked;
        _playerInput.SwitchCurrentActionMap("Player");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DestroyGame()
    {
        Application.Quit();
    }

    private void ResultSelector()
    {
        if(_input.Selector.y > 0.1f)
        {
            _select = 0;
            _uimanager.SelectResult(_select);
        }
        if(_input.Selector.y < -0.1f)
        {
            _select = 1;
            _uimanager.SelectResult(_select);
        }
    }

    private void ResultConfirm()
    {
        if (_input.confirm)
        {
            if (_select == 0) { RestartGame(); Debug.Log("Restart"); }
            if (_select == 1) { DestroyGame(); Debug.Log("End"); }
        }
    }
}