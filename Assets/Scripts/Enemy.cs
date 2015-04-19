// -----------------------------------------------
// Filename: Enemy.cs
// Author:   Harold Absalom
// Licence:  GNU General Public License
// -----------------------------------------------

using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {
   #region events

   public event EventHandler EnemyDestroyed;

   public event EventHandler CentreReached;

   #endregion events

   #region properties

   public Vector3 Destination {
      get { return _destination; }
      set { _destination = value; }
   }

   public float Speed {
      get { return _speed; }
      set { _speed = value; }
   }

   public bool Paused {
      get { return _paused; }
      set { _paused = value; }
   }

   #endregion properties

   #region fields

   private Vector3 _destination;
   private float _speed;
   private bool _paused = false;

   #endregion fields

   #region methods

   #region unity methods

   private void Update() {
      if (_paused)
         return;

      Vector3 direction = (_destination - transform.position).normalized;
      transform.Translate(direction * _speed * Time.deltaTime);
   }

   private void OnTriggerEnter2D(Collider2D other) {
      Weapon weapon = other.gameObject.GetComponent<Weapon>();
      if (weapon != null) {
         if (EnemyDestroyed != null) {
            EnemyDestroyed(this, null);
         }

         Destroy(gameObject);
      }

      if (other.tag == "Base") {
         if (CentreReached != null) {
            CentreReached(this, null);
         }
      }
   }

   #endregion unity methods

   #endregion methods
}
