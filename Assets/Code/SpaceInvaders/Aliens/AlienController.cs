// ----------------------------------------------------------------------------
//  University of Pittsburgh  
//  GamesEdu Workshop #2
//  19 SEPT 2018
// ----------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Controls all Alien Behaviours
/// </summary>
public class AlienController : MonoBehaviour
{
    public AlienManager alienManager;    
	// Use this for initialization
	void Start ()
    {
        alienManager = FindObjectOfType<AlienManager>();
    }
    private void OnDisable()
    {
        alienManager.alienCount--;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.collider.gameObject);
        if (col.collider.tag == "Player")
        {
            GameManager.Instance.GameOver();
        }
    }
}
