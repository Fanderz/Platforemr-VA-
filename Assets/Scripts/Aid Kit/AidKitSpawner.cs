using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using System.Collections.Generic;

public class AidKitSpawner : MonoBehaviour
{
    [SerializeField] private AidKit _prefab;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    private ObjectPool<AidKit> _pool;
    private Coroutine _coroutine;

    private bool _isRunning = false;
    private int _spawnPointIndex = 0;

    private void Awake()
    {
        _pool = new ObjectPool<AidKit>(
            createFunc: () => Instantiate(_prefab, transform),
            actionOnGet: (kit) => SpawnObject(kit),
            actionOnRelease: (kit) => kit.Deactivate(),
            actionOnDestroy: (kit) => Destroy(kit),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void OnEnable()
    {
        _isRunning = true;
        _coroutine = StartCoroutine(Spawning());
    }

    private void OnDisable()
    {
        _isRunning = false;

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void OnReleaseKit(AidKit kit)
    {
        _pool.Release(kit);

        kit.Heal -= OnReleaseKit;
    }

    private void SpawnObject(AidKit kit)
    {
        if (_spawnPointIndex < _spawnPoints.Count)
        {
            Vector3 startPosition = _spawnPoints[_spawnPointIndex++].position;
            kit.transform.position = startPosition;
            kit.Activate();
        }
    }

    private IEnumerator Spawning()
    {
        while (_isRunning)
        {
            if (_pool.CountAll < _poolMaxSize || _pool.CountInactive > 0)
            {
                var kit = _pool.Get();
                kit.Heal += OnReleaseKit;
            }

            yield return null;
        }
    }
}
