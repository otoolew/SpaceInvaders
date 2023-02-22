// ----------------------------------------------------------------------------
//  Chatham University  
//  
//  JAN 2023
// ----------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Moves Player Tank via Input Axis
/// </summary>
public class TankMovement : MonoBehaviour 
{
    [SerializeField,Header("Move Speed")]
    private float moveRate;
    
    [SerializeField,Header("Move Speed")]
    private float minBounds;
    [SerializeField]
    private float maxBounds;
    private void Update()
    {
        //Every update we will look for the input.GetAxis("Horizontal")
        transform.Translate(Vector2.right * Input.GetAxis("Horizontal")  * Time.deltaTime * moveRate);
        if (transform.position.x < minBounds)
        {
            transform.position = new Vector2(minBounds, 0);
        }
        else if (transform.position.x > maxBounds)
        {
            transform.position = new Vector2(maxBounds, 0);
        }
        
    }
}
