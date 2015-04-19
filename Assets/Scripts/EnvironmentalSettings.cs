// -----------------------------------------------
// Filename: EnvironmentalSettings.cs
// Author:   Harold Absalom
// Licence:  GNU General Public License
// -----------------------------------------------

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentalSettings : MonoBehaviour {
   #region fields

   [SerializeField]
   private Bounds _spawnBounds;

   [SerializeField]
   private Text _scoreDisplay;

   [SerializeField]
   private Text _highScoreDisplay;

   private EnemyGenerator _enemyGenerator;
   private WeaponGenerator _weaponGenerator;

   private int _score = 0;
   private int _highScore = 0;

   #endregion fields

   #region methods

   #region unity methods

   private void Awake() {
      _enemyGenerator = GetComponent<EnemyGenerator>();
      if (_enemyGenerator == null) {
         throw new NullReferenceException("No EnemyGenerator on EnvironmentalSettings.");
      }

      _weaponGenerator = GetComponent<WeaponGenerator>();
      if (_weaponGenerator == null) {
         throw new NullReferenceException("No WeaponGenerator on EnvironmentalSettings.");
      }

      _enemyGenerator.SpawnBounds = _spawnBounds;

      _enemyGenerator.EnemyDestroyed += OnEnemyDestroyed;
      _enemyGenerator.CentreReached += OnCentreReached;

      _weaponGenerator.SpawnBounds = _spawnBounds;
   }

   #endregion unity methods

   public void Restart() {
      UpdateHighScore();
      _score = 0;
      _scoreDisplay.text = _score.ToString();

      _enemyGenerator.Reset();
      if (_weaponGenerator.Paused == true) {
         _weaponGenerator.Reset();
      }
      _enemyGenerator.Paused = false;
      _weaponGenerator.Paused = false;
   }

   private void OnEnemyDestroyed(object sender, EventArgs e) {
      ++_score;
      _scoreDisplay.text = _score.ToString();
   }

   private void OnCentreReached(object sender, EventArgs e) {
      _enemyGenerator.Paused = true;
      _weaponGenerator.Paused = true;
      UpdateHighScore();
   }

   private void UpdateHighScore() {
      if (_score > _highScore) {
         _highScore = _score;
         _highScoreDisplay.text = _highScore.ToString();
      }
   }

   #endregion methods
}
