// -----------------------------------------------
// Filename: Weapon.cs
// Author:   Harold Absalom
// Licence:  GNU General Public License
// -----------------------------------------------

using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour {
   #region properties

   public Vector3 OrbitPoint {
      get { return _orbitPoint; }
      set { _orbitPoint = value; }
   }

   public float Speed {
      get { return _speed; }
      set { _speed = value; }
   }

   #endregion properties

   #region fields

   private Vector3 _orbitPoint;

   private float _speed;

   #endregion fields

   #region methods

   #region unity methods

   private void Start() {
   }

   private void Update() {
      transform.RotateAround(_orbitPoint, Vector3.forward, _speed);
   }

   #endregion unity methods

   #endregion methods
}
