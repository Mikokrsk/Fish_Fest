using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "myAssets/Game Event")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> gameEventListeners = new List<GameEventListener>();

    public void RegisterListener(GameEventListener listener)
    {
        gameEventListeners.Add(listener);
    }
    public void UnRegisterListener(GameEventListener listener)
    {
        gameEventListeners.Remove(listener);
    }
    public void Raise()
    {
        foreach (GameEventListener listener in gameEventListeners)
        {
            listener.OnEventRaised(this);
        }
    }
}