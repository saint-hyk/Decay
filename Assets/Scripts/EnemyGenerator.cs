// -----------------------------------------------
// Filename: EnemyGenerator.cs
// Author:   Harold Absalom
// Licence:  GNU General Public License
// -----------------------------------------------

using System.Collections;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGenerator : MonoBehaviour {
   #region events

   public event System.EventHandler EnemyDestroyed;

   public event System.EventHandler CentreReached;

   #endregion events

   #region properties

   public Bounds SpawnBounds {
      get { return _spawnBounds; }
      set { _spawnBounds = value; }
   }

   public bool Paused {
      get {
         return _paused;
      }

      set {
         _paused = value;
         if (_paused) {
            PauseAllEnemies();
         }
      }
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
   private bool _paused = true;

   #endregion fields

   #region methods

   #region unity methods

   private void Start() {
      SetTimerInterval();
   }

   private void Update() {
      if (_paused)
         return;

      _elapsedSpawnTime += Time.deltaTime;

      if (_elapsedSpawnTime >= _currentSpawnTime) {
         SpawnEnemy();
         _elapsedSpawnTime = 0;
         SetTimerInterval();
      }
   }

   #endregion unity methods

   public void Reset() {
      GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
      foreach (GameObject go in enemies) {
         Destroy(go);
      }
      _elapsedSpawnTime = 0.0f;
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
         enemy.EnemyDestroyed += OnEnemyDestroyed;
         enemy.CentreReached += OnCentreReached;
      }
   }

   private void SetTimerInterval() {
      _currentSpawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
   }

   private void PauseAllEnemies() {
      GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
      foreach (GameObject go in enemies) {
         Enemy e = go.GetComponent<Enemy>();
         if (e != null) {
            e.Paused = true;
         }
      }
   }

   private void OnEnemyDestroyed(object sender, System.EventArgs e) {
      if (EnemyDestroyed != null) {
         EnemyDestroyed(sender, e);
      }
   }

   private void OnCentreReached(object sender, System.EventArgs e) {
      if (CentreReached != null) {
         CentreReached(sender, e);
      }
   }

   #endregion methods
}
