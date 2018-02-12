using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {

	// Use this for initialization
	void Start () {

        RadialPlanetSpawn(new Vector2(0,0), 40, 5, PlanetTypes.e_type.ICE, "IcePlanetGroup");
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void RadialPlanetSpawn(Vector2 centerPos, float maxRadiusSpawn, float density, PlanetTypes.e_type planetType, string planetGroupName)
    {
        const float minDistanceFromCenter = 4f; //used since density close to center is huge(planets would be to close to each other)
        float layerDistance = 1f; //distance between 2 neighbourhood layers on which planets might be spawn
        GameObject planets = new GameObject(planetGroupName);   //Parent GameObject of all instantiated objects

        for(float distanceFromCenter = minDistanceFromCenter; distanceFromCenter < maxRadiusSpawn; distanceFromCenter += layerDistance)    //loop through every layer
        {
            for(int i = 0; i < density; ++i)    //Try to spawn this many planets on current layer
            {
                float distancePercentFactor = 1 - distanceFromCenter / maxRadiusSpawn;  //it gets lower, the chance that planet will be spawn are also lower
                bool shouldPlanetBeSpawn = (distancePercentFactor > Random.Range(0f, 1f)) ? true : false;
                if (shouldPlanetBeSpawn)
                {
                    float randomAngle = Random.Range(0, 360) * Mathf.Deg2Rad;   //sin and cos function work with radians
                    Vector2 randomPosOnLayer = new Vector2(distanceFromCenter * Mathf.Cos(randomAngle) + centerPos.x, distanceFromCenter * Mathf.Sin(randomAngle) + centerPos.y);   //calculate position of coordinate with given angle
                    Instantiate(ResourceManager.GetPlanetObject(), randomPosOnLayer, Quaternion.identity, planets.transform);
                }
            }
            
        }
    }




}
