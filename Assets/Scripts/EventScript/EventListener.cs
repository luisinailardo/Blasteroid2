using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    [SerializeField] private SpawnableEvent eventInvoked;
    public UnityEvent onEvent;

    private void OnEventInvoked()
    {
        onEvent.Invoke();
    }

    private void Awake()
    {
        eventInvoked.OnActionInvoked += OnEventInvoked;
    }

    private void OnDestroy()
    {
        eventInvoked.OnActionInvoked -= OnEventInvoked;
    }
}
