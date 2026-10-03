using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Text _gameOvertxt;
    [SerializeField]
    private Text _restartTxt;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 00;
        _gameOvertxt.gameObject.SetActive(false);
        _restartTxt.gameObject.SetActive(false);
    }

    public void updateScore(int newScore)
    {
        _scoreText.text = "Score: " + newScore;
    }

    public void updateLives(int currentLives)
    {
        _livesImg.sprite = _livesSprites[currentLives];

        if(currentLives == 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        _gameOvertxt.gameObject.SetActive(true);
        _restartTxt.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOvertxt.text = "GAME OVER !!!";
            yield return new WaitForSeconds(0.5f);
            _gameOvertxt.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
