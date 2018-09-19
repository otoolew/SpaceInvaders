// ----------------------------------------------------------------------------
//  University of Pittsburgh  
//  GamesEdu Workshop #2
//  19 SEPT 2018
// ----------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Moves Player Tank via Input Axis
/// </summary>
public class TankMovement : MonoBehaviour {
    [SerializeField]
    private float _moveRate;
    public float MoveRate
    {
        get { return _moveRate; }
        set { _moveRate = value; }
    }
    [SerializeField]
    private Vector2 _axisInput;
    public Vector2 AxisInput
    {
        get
        {
            _axisInput.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            return _axisInput;
        }
        private set { _axisInput = value; }
    }

    private void Update()
    {
        transform.Translate(AxisInput * Time.deltaTime * MoveRate);
    }
}
