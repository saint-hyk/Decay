// -----------------------------------------------
// Filename: Weapon.cs
// Author:   Harold Absalom
// Licence:  GNU General Public License
// -----------------------------------------------

using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour {
   #region enums

   public enum State {
      Growing,
      Spinning,
      Flying
   }

   #endregion enums

   #region properties

   public State CurrentState {
      get { return _currentState; }
   }

   public Color FinalColor {
      get { return _finalColor; }
      set { _finalColor = value; }
   }

   public Vector3 OrbitPoint {
      get { return _orbitPoint; }
      set { _orbitPoint = value; }
   }

   public float Speed {
      get { return _speed; }
      set { _speed = value; }
   }

   public float GrowthSpeed {
      get { return _growthSpeed; }
      set { _growthSpeed = value; }
   }

   public float MaxSqDistance {
      get { return _maxSqDistance; }
      set { _maxSqDistance = value; }
   }

   public bool Paused {
      get { return _paused; }
      set { _paused = value; }
   }

   #endregion properties

   #region fields

   private State _currentState;
   private Color _finalColor;
   private Vector3 _orbitPoint;
   private float _speed;
   private float _growthSpeed;
   private float _maxSqDistance;
   private Vector3 _offcastDirection;
   private bool _paused;

   #endregion fields

   #region methods

   #region unity methods

   private void Start() {
      _currentState = State.Growing;
      transform.localScale = new Vector3(0, 0, 0);
   }

   private void Update() {
      if (_paused)
         return;

      switch (_currentState) {
         case State.Growing:
            GrowWeapon();
            break;

         case State.Spinning:
            transform.RotateAround(_orbitPoint, Vector3.forward, _speed * Time.deltaTime);
            break;

         case State.Flying:
            transform.Translate(_offcastDirection * _speed / 4.0f * Time.deltaTime);

            if ((transform.position - _orbitPoint).sqrMagnitude > _maxSqDistance) {
               Destroy(gameObject);
            }

            break;
      }
   }

   #endregion unity methods

   public bool OnWeaponClicked() {
      if (_currentState != State.Spinning) {
         return false;
      }

      Vector3 toCentre = (_orbitPoint - transform.position).normalized;

      _offcastDirection = Quaternion.Inverse(transform.rotation) * new Vector3(toCentre.y, -toCentre.x, 0);

      _currentState = State.Flying;

      return true;
   }

   private void GrowWeapon() {
      if (transform.localScale.x < 1) {
         float growth = _growthSpeed * Time.deltaTime;
         Vector3 newScale = new Vector3(transform.localScale.x + growth,
                                        transform.localScale.y + growth,
                                        transform.localScale.z + growth);
         transform.localScale = newScale;
      } else {
         transform.localScale = Vector3.one;
         GetComponent<SpriteRenderer>().color = _finalColor;
         _currentState = State.Spinning;
      }
   }

   #endregion methods
}
