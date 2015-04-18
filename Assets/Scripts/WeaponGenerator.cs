// -----------------------------------------------
// Filename: WeaponGenerator.cs
// Author:   Harold Absalom
// Licence:  GNU General Public License
// -----------------------------------------------

using UnityEngine;
using System.Collections;

public class WeaponGenerator : MonoBehaviour {
   #region fields

   [SerializeField]
   private GameObject _weaponPrefab;

   [SerializeField]
   private Vector3 _centrePoint;

   [SerializeField]
   private int _initialWeaponCount;

   [SerializeField]
   private float _minWeaponSpeed;

   [SerializeField]
   private float _maxWeaponSpeed;

   [SerializeField]
   private float _weaponDistance;

   #endregion fields

   #region methods

   #region unity methods

   private void Start() {
      for (int i = 0; i < _initialWeaponCount; ++i) {
         GameObject obj = GameObject.Instantiate(_weaponPrefab) as GameObject;

         Vector3 offset = Random.onUnitSphere;
         offset.z = 0;

         obj.transform.position = _centrePoint + offset * _weaponDistance;

         Weapon weapon = obj.GetComponent<Weapon>();
         if (weapon != null) {
            weapon.OrbitPoint = _centrePoint;
            weapon.Speed = Random.Range(_minWeaponSpeed, _maxWeaponSpeed);
         }
      }
   }

   #endregion unity methods

   #endregion methods
}
