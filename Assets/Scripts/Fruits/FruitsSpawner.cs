using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using System.Collections.Generic;

public class FruitsSpawner : MonoBehaviour
{
    [SerializeField] private Fruit[] _prefabs;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    private ObjectPool<Fruit> _pool;
    private Coroutine _coroutine;

    private bool _isRunning = false;
    private int _spawnPointIndex = 0;

    private void Awake()
    {
        _pool = new ObjectPool<Fruit>(
            createFunc: () => Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], transform),
            actionOnGet: (fruit) => SpawnObject(fruit),
            actionOnRelease: (fruit) => fruit.Deactivate(),
            actionOnDestroy: (fruit) => Destroy(fruit),
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

    private void OnReleaseFruit(Fruit fruit)
    {
        _pool.Release(fruit);

        fruit.Eated -= OnReleaseFruit;
    }

    private void SpawnObject(Fruit fruit)
    {
        if (_spawnPointIndex < _spawnPoints.Count)
        {
            Vector3 startPosition = _spawnPoints[_spawnPointIndex++].position;
            fruit.transform.position = startPosition;
            fruit.Activate();
        }
    }

    private IEnumerator Spawning()
    {
        while (_isRunning)
        {
            if (_pool.CountAll < _poolMaxSize || _pool.CountInactive > 0)
            {
                var fruit = _pool.Get();
                fruit.Eated += OnReleaseFruit;
            }

            yield return null;
        }
    }
}
