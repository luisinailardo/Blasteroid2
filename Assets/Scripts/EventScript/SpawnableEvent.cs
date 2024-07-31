using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Event", menuName = "Events/New Event", order = 0)]
public class SpawnableEvent : ScriptableObject
{
    public event Action OnActionInvoked;

    public void Invoke()
    {
        OnActionInvoked?.Invoke();
    }
}
