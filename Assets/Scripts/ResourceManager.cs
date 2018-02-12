using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    static public GameObject GetPlanetObject()
    {
        return Resources.Load<GameObject>("GameObjects/PlanetTravelScreen");
    }
}