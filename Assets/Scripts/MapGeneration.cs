using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
        Instantiate(ResourceManager.GetPlanetObject(), new Vector2(4,4), Quaternion.identity, GameObject.Find("TravelScreen").transform);
        RadialPlanetSpawn(200, 5, PlanetTypes.e_type.ICE);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void RadialPlanetSpawn(float maxRadiusSpawn, float density, PlanetTypes.e_type planetType)
    {
        float layerDistance = 0.1f;
        for(float distanceFromCenter = 1f; distanceFromCenter < maxRadiusSpawn; distanceFromCenter += layerDistance)
        {
            float distancePercentFactor = 1 - distanceFromCenter / maxRadiusSpawn;
            if(distancePercentFactor > Random.Range(0f, 1f))
                Instantiate(ResourceManager.GetPlanetObject(), new Vector2(distanceFromCenter, 0), Quaternion.identity, GameObject.Find("TravelScreen").transform);
        }
    }




}
