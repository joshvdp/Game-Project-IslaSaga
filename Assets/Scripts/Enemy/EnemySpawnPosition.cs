using System.Collections;
using Enemy;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnPosition : MonoBehaviour
{
    [SerializeField] private float flagRange = 0.1f;
    [SerializeField] private bool atSpawnPoint;
    [SerializeField, HideIf(nameof(atSpawnPoint))] private Vector3 idlePoint;
    [SerializeField] private EnemyZone zone;
    [SerializeField] private UnityEvent onPlayerExitZone;

    public UnityEvent spawnRange;

    private bool HasZone => zone != null;
    private Vector3 SpawnPoint { get; set; }
    
    private bool _hasStarted;
    private bool _isListening;

    private IEnumerator Start()
    {
        _hasStarted = true;

        yield return new WaitForSeconds(0.2f);
        SpawnPoint = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, GetPoint()) <= flagRange)
            spawnRange?.Invoke();
    }

    public Vector3 GetPoint() => atSpawnPoint ? SpawnPoint : idlePoint;


    public void StartWaitingForReturn(bool isListening)
    {
        if (isListening)
        {
            if (HasZone && !_isListening)
                zone.onPlayerExit.AddListener(onPlayerExitZone.Invoke);
            _isListening = true;
        }
        else
        {
            if (HasZone && _isListening)
                zone.onPlayerExit.RemoveListener(onPlayerExitZone.Invoke);
            _isListening = false;
        }
        
    }
}

