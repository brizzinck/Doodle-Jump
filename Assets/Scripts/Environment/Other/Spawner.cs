using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected int[] _chanceOfEnviroments;
    [SerializeField] protected Environments[] _environments;
    [SerializeField] protected int[] _chanceOfBoosts;
    [SerializeField] protected Boost[] _boosts;
    [SerializeField] protected int _chanceOfSpawnBoost = 2;
    [SerializeField] protected Transform _topBound;
    [SerializeField] protected Environments _lastPlatform;
    [SerializeField] protected Score _score;
    [SerializeField] protected int[] _scoresToSpawnPlatform;
    [SerializeField] protected int _scoreForEnemySpawn;
    [SerializeField][Range(20, 40)] protected int[] _minMaxPlatformForSpawnEnemy;
    [SerializeField] private Enemies[] _enemies;

    protected bool[] _canPlatformSpawn;

    protected int _currentPlatformForEnemySpawn;
    private void Awake()
    {
        _canPlatformSpawn = new bool[_environments.Length];
        _currentPlatformForEnemySpawn = Random.Range(_minMaxPlatformForSpawnEnemy[0], _minMaxPlatformForSpawnEnemy[1]);
    }
    private void FixedUpdate()
    {
        if (_lastPlatform.transform.position.y < _topBound.position.y)
        {
            SpawnPlatform();
            if (_currentPlatformForEnemySpawn <= 0)
            {
                SpawnEnemies();
                _currentPlatformForEnemySpawn = Random.Range(_minMaxPlatformForSpawnEnemy[0], _minMaxPlatformForSpawnEnemy[1]);
            }
            else SpawnBoost();
        }
        for(int i = 0; i < _environments.Length; i++)
        {
            if (_scoresToSpawnPlatform[i] < _score.score)
                _canPlatformSpawn[i] = true;
        }
    }

    protected void SpawnPlatform()

    {
        Vector3 spawnPostion = new Vector3();
        spawnPostion.y = Random.Range(0.5f, 1.3f);
        do
        {
            spawnPostion.x = Random.Range(-2, 2);
        }
        while (Mathf.Abs(spawnPostion.x - _lastPlatform.transform.position.x) < .2f);
        if (_lastPlatform != null)
            spawnPostion.y += _lastPlatform.transform.position.y;
        else
        {
            spawnPostion.y = -3.5f;
            spawnPostion.x = 0;
        }
        _lastPlatform = Instantiate(RandomSpawnPlatform(), spawnPostion, Quaternion.identity);      
        if (_score.score > _scoreForEnemySpawn) _currentPlatformForEnemySpawn--;
    }

    protected void SpawnBoost()
    {
        if(Random.Range(0, _chanceOfSpawnBoost) == 1)
        {
            Vector3 offset = new Vector3();
            Boost boost = RandomSpawnBoost();
            if(boost.CompareTag("Spring"))
                offset = new Vector3(0, .2f);
            else if(boost.CompareTag("Coin"))
                offset = new Vector3(0, .5f);
            else if (boost.CompareTag("Jetpack"))
                offset = new Vector3(0, .5f);
            else if (boost.CompareTag("Hat"))
                offset = new Vector3(0, .4f);
            if (_lastPlatform.transform.GetComponent<SpriteRenderer>().sprite.name == "Gametiles_0")
            {
                Instantiate(boost, _lastPlatform.transform.position + offset + new Vector3(Random.Range(-0.25f, 0.25f), 0, 0), Quaternion.identity);
            }
        }
    }

    protected Environments RandomSpawnPlatform()
    {
        int maxValue = 0;
        int currentPlatform = 0;
        int[] randomArr = new int[_environments.Length];
        for (int i = 0; i < _environments.Length; i++)
        {
            randomArr[i] = Random.Range(0, _chanceOfEnviroments[i] + 1);
            if (maxValue < randomArr[i] && _canPlatformSpawn[i])
            {
                maxValue = randomArr[i];
                currentPlatform = i;
            }
        }
        return _environments[currentPlatform];
    }

    protected Boost RandomSpawnBoost()
    {
        int maxValue = 0;
        int currentBoost = 0;
        int[] randomArr = new int[_boosts.Length];
        for (int i = 0; i < _boosts.Length; i++)
        {
            randomArr[i] = Random.Range(0, _chanceOfBoosts[i] + 1);
            if (maxValue < randomArr[i])
            {
                maxValue = randomArr[i];
                currentBoost = i;
            }
        }
        return _boosts[currentBoost];
    }
    protected void SpawnEnemies()
    {
         Instantiate(_enemies[Random.Range(0, _enemies.Length)], _lastPlatform.transform.position + Vector3.back + Vector3.up * .55f, Quaternion.identity);
    }

}
