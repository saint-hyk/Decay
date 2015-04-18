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

   public float Speed {
      get { return _speed; }
      set { _speed = value; }
   }

   #endregion properties

   #region fields

   private Vector3 _destination;
   private float _speed;

   #endregion fields

   #region methods

   #region unity methods

   private void Update() {
      Vector3 direction = (_destination - transform.position).normalized;
      transform.Translate(direction * _speed);
   }

   private void OnTriggerEnter2D(Collider2D other) {
      Weapon weapon = other.gameObject.GetComponent<Weapon>();
      if (weapon != null) {
         Destroy(gameObject);
      }
   }

   #endregion unity methods

   #endregion methods
}
