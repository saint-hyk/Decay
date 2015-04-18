// -----------------------------------------------
// Filename: EnemyGenerator.cs
// Author:   Harold Absalom
// Licence:  GNU General Public License
// -----------------------------------------------

using System.Collections;
using System.Timers;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {
   #region properties

   public Bounds SpawnBounds {
      get { return _spawnBounds; }
   }

   #endregion properties

   #region fields

   [SerializeField]
   private GameObject _enemyPrefab;

   [SerializeField]
   private Bounds _spawnBounds;

   [SerializeField]
   private float _minSpawnTime;

   [SerializeField]
   private float _maxSpawnTime;

   private float _currentSpawnTime;
   private float _elapsedSpawnTime = 0;

   #endregion fields

   #region methods

   #region unity methods

   private void Start() {
      SetTimerInterval();
   }

   private void Update() {
      _elapsedSpawnTime += Time.deltaTime;

      if (_elapsedSpawnTime >= _currentSpawnTime) {
         SpawnEnemy();
         _elapsedSpawnTime = 0;
         SetTimerInterval();
      }
   }

   private void SpawnEnemy() {
      bool major = Random.value > 0.5f;
      bool minor = Random.value > 0.5f;

      float xPos;
      float yPos;

      if (major) {
         xPos = Random.Range(_spawnBounds.min.x, _spawnBounds.max.x);
         yPos = (minor ? _spawnBounds.min.y : _spawnBounds.max.y);
      } else {
         xPos = (minor ? _spawnBounds.min.x : _spawnBounds.max.x);
         yPos = Random.Range(_spawnBounds.min.y, _spawnBounds.max.y);
      }

      Vector3 spawnPos = new Vector3(xPos, yPos, 0);
      GameObject enemy = GameObject.Instantiate(_enemyPrefab, spawnPos, Quaternion.identity) as GameObject;
      enemy.GetComponent<Enemy>().Destination = _spawnBounds.center;
   }

   private void SetTimerInterval() {
      _currentSpawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
   }

   #endregion unity methods

   #endregion methods
}
