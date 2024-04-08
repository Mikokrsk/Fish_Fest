using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static GameEventListener;

public class GameEventListener : MonoBehaviour
{
    public List<GameEvents> gameEventsList;
    [System.Serializable]
    public struct GameEvents
    {
        public GameEvent gameEvent;

        public UnityEvent response;
    }

    private void OnEnable()
    {
        foreach (var gameEvent in gameEventsList)
        {
            gameEvent.gameEvent.RegisterListener(this);
        }
    }
    private void OnDisable()
    {
        foreach (var gameEvent in gameEventsList)
        {
            gameEvent.gameEvent.UnRegisterListener(this);
        }
    }

    public void OnEventRaised(GameEvent _gameEvent)
    {
        gameEventsList.Find(x => x.gameEvent == _gameEvent).response?.Invoke();
    }
}