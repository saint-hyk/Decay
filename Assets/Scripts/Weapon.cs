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

   private bool _spinning = true;

   private Vector3 _offcastDirection;

   #endregion fields

   #region methods

   #region unity methods

   private void Start() {
   }

   private void Update() {
      if (_spinning) {
         transform.RotateAround(_orbitPoint, Vector3.forward, _speed);
      } else {
         transform.Translate(_offcastDirection * _speed / 2.0f);
      }
   }

   private void OnMouseDown() {
      Vector3 toCentre = (_orbitPoint - transform.position).normalized;

      //transform.rotation = Quaternion.identity;
      _offcastDirection = Quaternion.Inverse(transform.rotation) * new Vector3(toCentre.y, -toCentre.x, 0);

      _spinning = false;
   }

   #endregion unity methods

   #endregion methods
}
