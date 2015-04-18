// -----------------------------------------------
// Filename: EnvironmentalSettings.cs
// Author:   Harold Absalom
// Licence:  GNU General Public License
// -----------------------------------------------

using UnityEngine;
using System.Collections;

public class EnvironmentalSettings : MonoBehaviour {
   #region fields

   [SerializeField]
   private Bounds _spawnBounds;

   #endregion fields

   #region methods

   #region unity methods

   private void Start() {
      EnemyGenerator enemyGenerator = GetComponent<EnemyGenerator>();
      if (enemyGenerator != null) {
         enemyGenerator.SpawnBounds = _spawnBounds;
      }

      WeaponGenerator weaponGenerator = GetComponent<WeaponGenerator>();
      if (weaponGenerator != null) {
         weaponGenerator.SpawnBounds = _spawnBounds;
      }
   }

   #endregion unity methods

   #endregion methods
}
