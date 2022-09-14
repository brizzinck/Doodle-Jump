using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : Spawner
{
    [SerializeField] private int _scorePerLevel = 1000;
    [SerializeField] GameObject _finish;
    [SerializeField] FinishController _finishController;
    [SerializeField] GameLevelsUIController _UIController;
    private int _currentLevel;
    private bool _finished = false;
    private void FixedUpdate()
    {
        if (_lastPlatform.transform.position.y < _topBound.position.y && _score.score < _currentLevel * _scorePerLevel)
        {
            SpawnPlatform();
            if (_currentPlatformForEnemySpawn <= 0)
            {
                SpawnEnemies();
                _currentPlatformForEnemySpawn = Random.Range(_minMaxPlatformForSpawnEnemy[0], _minMaxPlatformForSpawnEnemy[1]);
            }
            else SpawnBoost();
        }
        else if(_score.score >= _currentLevel * _scorePerLevel && !_finished)
        {
            var finish = Instantiate(_finish, new Vector3(0, _lastPlatform.transform.position.y + 1, 0), Quaternion.identity);
            finish.GetComponent<Finish>().finish = _finishController;
            finish.GetComponent<Finish>().UIController = _UIController;
            _finished = true;
        }
        for (int i = 0; i < _environments.Length; i++)
        {
            if (_scoresToSpawnPlatform[i] < _score.score)
                _canPlatformSpawn[i] = true;
        }
    }

    private void Start()
    {
        _currentLevel = Saver.GetIntPrefs("CurrentLevel");
        Random.InitState(_currentLevel);
    }
}
