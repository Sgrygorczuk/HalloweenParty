using System;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    private Door _doorScript;           //The script that controls the doors' actions 
    private Person _personScript;       //The script that controls the persons' actions 
    private bool _doorClosedFlag;       //Used to tell if the player has passed the door 
    
    private enum GameState              //Different states that the game could exits in 
    {
        Start, Choice, Finish
    }
    GameState _currentState = GameState.Start;      //Keeps track of what state we're currently in
    
    // Start is called before the first frame update
    private void Start()
    {
        //Searches the Scene for GameObjects Door and Person and grabs their script components 
        _doorScript = GameObject.Find("Door").GetComponent<Door>();
        _personScript = GameObject.Find("Person").GetComponent<Person>();
    }

    // Update is called once per frame
    private void Update()
    {
        switch (_currentState)
        {
            case GameState.Start:
            {
                //Opens the door, once the door is fully open allows the player to act 
                _doorScript.DoorOpen();
                if (_doorScript.GetIsDoorOpen())
                {
                    _currentState = GameState.Choice;
                }
                break;
            }
            case GameState.Choice:
            {
                //Player clicks the accept or reject button (A or B) to prompt the next action 
                if (Input.GetButtonDown($"Accept"))
                {
                    _personScript.StartWalking();
                }
                else if (Input.GetButtonDown($"Reject"))
                {
                    _doorScript.CloseDoor();
                    _personScript.RandomizePerson();
                }
                
                //If player is walking we check if they walked far enough to close the door and we do it
                if (_personScript.IsHalfWay() && !_doorClosedFlag)
                {
                    _doorScript.CloseDoor();
                    _doorClosedFlag = true;
                }

                //If the door is closed we go back to the start state 
                if (!_doorScript.GetIsDoorOpen())
                {
                    _currentState = GameState.Start;
                    _doorClosedFlag = false;
                }
                break;
            }
            case GameState.Finish:
            {
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
