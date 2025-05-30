using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private GameObject MainGameManager;
    private MainGame _mainGame;

    void Start()
    {
        _mainGame = MainGameManager.GetComponent<MainGame>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Animal")
        {
            Debug.Log("Gameover");
            _mainGame.GameEnd();
        }
    }
}
