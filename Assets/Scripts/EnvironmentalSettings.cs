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

   [SerializeField]
   private AudioClip _loseSound;

   [SerializeField]
   private AudioClip _winSound;

   private AudioSource _soundSource;

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

   private void Start() {
      _soundSource = gameObject.AddComponent<AudioSource>();
   }

   #endregion unity methods

   public void Restart() {
      _score = 0;
      _scoreDisplay.text = _score.ToString();

      _enemyGenerator.Reset();
      if (_weaponGenerator.Paused == true) {
         _weaponGenerator.Reset();
      }
      _enemyGenerator.Paused = false;
      _weaponGenerator.Paused = false;
   }

   public void SetAudioMute(bool mute) {
      AudioSource[] sources = GameObject.FindObjectsOfType<AudioSource>();
      foreach (AudioSource s in sources) {
         s.mute = mute;
      }
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
         _soundSource.clip = _winSound;
      } else {
         _soundSource.clip = _loseSound;
      }
      _soundSource.Play();
   }

   #endregion methods
}
