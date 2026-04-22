using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
public enum GameState
{ 
    Paused,
    Running
 }

public class GameStateManager : MonoBehaviour
{
    public GameState CurrentState = GameState.Running;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (CurrentState == GameState.Paused)
            {
                SetGameState(GameState.Running);
            }
            else if (CurrentState == GameState.Running)
            {
                SetGameState(GameState.Paused);
            }
        }

    }

    public void SetGameState(GameState newState)
    {
        if (newState == GameState.Running)
        {
            Time.timeScale = 1.0f;
        }
        else if (newState == GameState.Paused)
        {
            Time.timeScale = 0.0f;
        }
        CurrentState = newState;

    }
}  
