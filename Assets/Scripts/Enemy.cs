// -----------------------------------------------
// Filename: Enemy.cs
// Author:   Harold Absalom
// Licence:  GNU General Public License
// -----------------------------------------------

using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {
   #region properties

   public Vector3 Destination {
      get { return _destination; }
      set { _destination = value; }
   }

   #endregion properties

   #region fields

   private Vector3 _destination;
   private float _speed = 0.001f;

   #endregion fields

   #region methods

   #region unity methods

   private void Start() {
   }

   private void Update() {
      Vector3 direction = (_destination - transform.position).normalized;
      transform.Translate(direction * _speed);
   }

   #endregion unity methods

   #endregion methods
}
