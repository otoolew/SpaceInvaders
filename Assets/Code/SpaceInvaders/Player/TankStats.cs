using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankStats : MonoBehaviour {
    public string tankName;
    public int hp;
    public TankShoot tankShootBehaviour;
	// Use this for initialization
	void Start () {
        tankShootBehaviour = GetComponent<TankShoot>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(tankShootBehaviour.fireBulletKey))
        {
            Debug.Log(tankName + " has fired.");
        }
           

    }
}
