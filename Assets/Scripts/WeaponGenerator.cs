// -----------------------------------------------
// Filename: WeaponGenerator.cs
// Author:   Harold Absalom
// Licence:  GNU General Public License
// -----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponGenerator : MonoBehaviour {
   #region properties

   public Bounds SpawnBounds {
      get { return _spawnBounds; }
      set { _spawnBounds = value; }
   }

   public bool Paused {
      get { return _paused; }
      set {
         _paused = value;
         if (_paused) {
            PauseWeapons();
         }
      }
   }

   #endregion properties

   #region fields

   [SerializeField]
   private GameObject _weaponPrefab;

   private Bounds _spawnBounds;

   [SerializeField]
   private int _maximumWeaponCount;

   [SerializeField]
   private float _weaponGrowthSpeed;

   [SerializeField]
   private float _minWeaponSpeed;

   [SerializeField]
   private float _maxWeaponSpeed;

   [SerializeField]
   private float _weaponDistance;

   [SerializeField]
   private Color _finalWeaponColor;

   [SerializeField]
   private AudioClip _weaponSound;

   private List<Weapon> _weapons;
   private bool _paused = false;

   private AudioSource _weaponSoundSource;

   #endregion fields

   #region methods

   #region unity methods

   private void Start() {
      _weapons = new List<Weapon>();

      MaximizeWeapons();

      _weaponSoundSource = gameObject.AddComponent<AudioSource>();
      _weaponSoundSource.clip = _weaponSound;
   }

   private void Update() {
      if (_paused)
         return;

      MaximizeWeapons();

      if (Input.GetMouseButtonDown(0)) {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         RaycastHit2D[] hitObjects = Physics2D.RaycastAll(ray.origin, ray.direction);
         if (hitObjects.Length > 0) {
            RemoveWeapons(hitObjects);
         }
      }
   }

   #endregion unity methods

   public void Reset() {
      foreach (Weapon w in _weapons) {
         Destroy(w.gameObject);
      }
      _weapons.Clear();

      MaximizeWeapons();
   }

   private void RemoveWeapons(RaycastHit2D[] hitObjects) {
      for (int i = 0; i < hitObjects.Length; ++i) {
         List<Weapon> hitWeapons = _weapons.FindAll(weapon => weapon.transform == hitObjects[i].transform);
         if (hitWeapons.Count > 0) {
            foreach (Weapon w in hitWeapons) {
               if (w.OnWeaponClicked()) {
                  _weaponSoundSource.Play();
                  _weapons.Remove(w);
               }
            }
         }
      }
   }

   private void MaximizeWeapons() {
      while (_weapons.Count < _maximumWeaponCount) {
         GenerateWeapon();
      }
   }

   private void GenerateWeapon() {
      GameObject obj = GameObject.Instantiate(_weaponPrefab) as GameObject;

      float rotationAngle = Random.Range(-Mathf.PI, Mathf.PI);
      Vector3 offset = new Vector3(Mathf.Cos(rotationAngle), Mathf.Sin(rotationAngle), 0);

      obj.transform.position = _spawnBounds.center + offset * _weaponDistance;
      obj.transform.rotation = Quaternion.Euler(0, 0, rotationAngle * Mathf.Rad2Deg);

      Weapon weapon = obj.GetComponent<Weapon>();
      if (weapon != null) {
         weapon.OrbitPoint = _spawnBounds.center;
         weapon.Speed = Random.Range(_minWeaponSpeed, _maxWeaponSpeed);
         weapon.GrowthSpeed = _weaponGrowthSpeed;
         weapon.FinalColor = _finalWeaponColor;
         weapon.MaxSqDistance = _spawnBounds.size.x * _spawnBounds.size.x +
                                _spawnBounds.size.y * _spawnBounds.size.y;
         _weapons.Add(weapon);
      }
   }

   private void PauseWeapons() {
      foreach (Weapon w in _weapons) {
         w.Paused = true;
      }
   }

   #endregion methods
}
