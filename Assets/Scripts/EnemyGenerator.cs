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
      set { _spawnBounds = value; }
   }

   #endregion properties

   #region fields

   [SerializeField]
   private GameObject _enemyPrefab;

   [SerializeField]
   private float _enemySpeed;

   [SerializeField]
   private float _minSpawnTime;

   [SerializeField]
   private float _maxSpawnTime;

   private Bounds _spawnBounds;

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
      GameObject obj = GameObject.Instantiate(_enemyPrefab, spawnPos, Quaternion.identity) as GameObject;
      Enemy enemy = obj.GetComponent<Enemy>();

      if (enemy != null) {
         enemy.Destination = _spawnBounds.center;
         enemy.Speed = _enemySpeed;
      }
   }

   private void SetTimerInterval() {
      _currentSpawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
   }

   #endregion unity methods

   #endregion methods
}
