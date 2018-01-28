using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
    public enum GameState { Briefing, Playing, Won, Lost}
    public GameState state = GameState.Briefing;
    public GameObject level;
    public GameObject call;

    private void Start()
    {
        GetComponent<TextBehaviourScript>().ShowBriefingText();
    }

    public void StartLevel()
    {
        state = GameState.Playing;
        call.SetActive(false);
        level.SetActive(true);
    }

    public void Win()
    {
        state = GameState.Won;
        call.SetActive(true);
        level.SetActive(false);
        GetComponent<TextBehaviourScript>().Reset();
         GetComponent<TextBehaviourScript>().ShowWinningText();

    }

    public void Lose()
    {
        state = GameState.Lost;
        call.SetActive(true);
        level.SetActive(false);
        GetComponent<TextBehaviourScript>().Reset();
        GetComponent<TextBehaviourScript>().ShowLosingText();
    }
    public void Replay()
    {
    }
}
