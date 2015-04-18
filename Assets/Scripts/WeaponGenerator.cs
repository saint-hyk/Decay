// -----------------------------------------------
// Filename: WeaponGenerator.cs
// Author:   Harold Absalom
// Licence:  GNU General Public License
// -----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponGenerator : MonoBehaviour {
   #region fields

   [SerializeField]
   private GameObject _weaponPrefab;

   [SerializeField]
   private Vector3 _centrePoint;

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

   private List<Weapon> _weapons;

   #endregion fields

   #region methods

   #region unity methods

   private void Start() {
      _weapons = new List<Weapon>();

      MaximizeWeapons();
   }

   private void Update() {
      MaximizeWeapons();
   }

   private void FixedUpdate() {
      if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)) {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         RaycastHit2D[] hitObjects = Physics2D.RaycastAll(ray.origin, ray.direction);
         if (hitObjects.Length > 0) {
            for (int i = 0; i < hitObjects.Length; ++i) {
               Weapon hitWeapon = _weapons.Find(weapon => weapon.transform == hitObjects[i].transform);
               if (hitWeapon != null && hitWeapon.OnWeaponClicked()) {
                  _weapons.Remove(hitWeapon);
               }
            }
         }
      }
   }

   #endregion unity methods

   private void MaximizeWeapons() {
      while (_weapons.Count < _maximumWeaponCount) {
         GenerateWeapon();
      }
   }

   private void GenerateWeapon() {
      GameObject obj = GameObject.Instantiate(_weaponPrefab) as GameObject;

      float rotationAngle = Random.Range(-Mathf.PI, Mathf.PI);
      Vector3 offset = new Vector3(Mathf.Cos(rotationAngle), Mathf.Sin(rotationAngle), 0);

      obj.transform.position = _centrePoint + offset * _weaponDistance;
      obj.transform.rotation = Quaternion.Euler(0, 0, rotationAngle * Mathf.Rad2Deg);

      Weapon weapon = obj.GetComponent<Weapon>();
      if (weapon != null) {
         weapon.OrbitPoint = _centrePoint;
         weapon.Speed = Random.Range(_minWeaponSpeed, _maxWeaponSpeed);
         weapon.GrowthSpeed = _weaponGrowthSpeed;
         weapon.FinalColor = _finalWeaponColor;
         _weapons.Add(weapon);
      }
   }

   #endregion methods
}
