using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BaseObjectsSpawner : MonoBehaviour
{
    [SerializeField] private BaseEatObjects[] _prefabs;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    private ObjectPool<BaseEatObjects> _pool;
    private Coroutine _coroutine;

    private bool _isRunning = false;
    private int _spawnPointIndex = 0;

    private void Awake()
    {
        _pool = new ObjectPool<BaseEatObjects>(
            createFunc: () => Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], transform),
            actionOnGet: (myObj) => SpawnObject(myObj),
            actionOnRelease: (myObj) => myObj.Deactivate(),
            actionOnDestroy: (myObj) => Destroy(myObj.gameObject),
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

    private void OnReleaseObject(BaseEatObjects myObj)
    {
        _pool.Release(myObj);

        myObj.Eated -= OnReleaseObject;
    }

    private void SpawnObject(BaseEatObjects myObj)
    {
        if (_spawnPointIndex < _spawnPoints.Count)
        {
            Vector3 startPosition = _spawnPoints[_spawnPointIndex++].position;
            myObj.transform.position = startPosition;
            myObj.Activate();
        }
    }

    private IEnumerator Spawning()
    {
        while (_isRunning)
        {
            if (_pool.CountAll < _poolMaxSize || _pool.CountInactive > 0)
            {
                var fruit = _pool.Get();
                fruit.Eated += OnReleaseObject;
            }

            yield return null;
        }
    }
}
