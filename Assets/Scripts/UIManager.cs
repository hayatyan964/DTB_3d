using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainGameManager;
    private MainGame _mainGame;

    [SerializeField]
    private Canvas _playingCanvas;
    [SerializeField]
    private Canvas _PauseCanvas;

    [SerializeField]
    private GameObject PlayingUI;
    [SerializeField]
    private GameObject ResultPanel;
    [SerializeField]
    private Text _animalCount;
    [SerializeField]
    private Text _controlMode;

    [SerializeField]
    private Image ProgressRing;

    [SerializeField]
    private Text Scoretext;

    [SerializeField]
    private GameObject RestartPos;
    [SerializeField]
    private GameObject EndPos;
    [SerializeField]
    private GameObject cursor;
   

    void Start()
    {
        _mainGame = _mainGameManager.GetComponent<MainGame>();

        _playingCanvas.enabled = true;
        _PauseCanvas.enabled = false;

        PlayingUI.SetActive(true);
        ResultPanel.SetActive(false);

        _animalCount.text = "�ς񂾓����̐�:0";
    }


    public void AnimalCountText(int count)
    {
        _animalCount.text = "�ς񂾓����̐�:" + count.ToString() ;
    }

    public IEnumerator ProgressTimer(float timer)
    {
        float elapsedTime = 0f;

        while(elapsedTime < timer)
        {
            elapsedTime += Time.deltaTime;
            float filler = 1 - (elapsedTime / timer);
            ProgressRing.fillAmount = filler;
            yield return null;
        }
            
        ProgressRing.fillAmount = 0f;
        yield return new WaitForSeconds(0.1f);
        ProgressRing.fillAmount = 1;
    }

    public void ModeText()
    {
        switch (_mainGame._state)
        {
            case MainGame.State.Move:
                _controlMode.text = "���݂̃��[�h\n�ړ�";
                break;
            case MainGame.State.Rotation:
                _controlMode.text = "���݂̃��[�h\n��]";
                break;
            case MainGame.State.Drop:
                _controlMode.text = "���݂̃��[�h\n�ҋ@";
                break;
            default:
                break;
        }
    }

    public void ShowPause()
    {
        _playingCanvas.enabled = false;
        _PauseCanvas.enabled = true;
    }

    public void StopPause()
    {
        _playingCanvas.enabled = true;
        _PauseCanvas.enabled = false;
    }

    public void ShowResult(int count)
    {
        ResultPanel.SetActive(true);
        PlayingUI.SetActive(false);
        Scoretext.text = count.ToString() + "�C�̓�����ςݏグ�܂����I";
    }

    public void SelectResult(int _select)
    {
        if (_select == 0) 
        {
            cursor.transform.position = RestartPos.transform.position;
        }
        if (_select == 1)
        {
            cursor.transform.position = EndPos.transform.position;
        }
    }
}
