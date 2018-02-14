using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTravelerBehaviour : MonoBehaviour {

    private enum e_travelState { TRAVELING, NOT_TRAVELING }
    e_travelState trevelState;

    Vector2 pointToTravelTo;
    float maxTravelSpeed;
    float timeOfMovementStart;
    float timeToReachMaxSpeed;
    float stopMovementTargetRadius;
    float slownDownMovementRadius;

    // Use this for initialization 
    void Start () {
        trevelState = e_travelState.NOT_TRAVELING;        
        maxTravelSpeed = 5f;
        timeOfMovementStart = Time.time;
        timeToReachMaxSpeed = 1.3f;
        stopMovementTargetRadius = 0.25f;
        slownDownMovementRadius = 2.2f;

    }
	
	// Update is called once per frame
	void Update () {
        if (trevelState == e_travelState.TRAVELING)
        {
            if (IsInTravelTargetRadius())
            {
                trevelState = e_travelState.NOT_TRAVELING;
                //if(GameMouse.GetRefrence().curSpriteId == GameMouse.MOUSE_IMG.WAITING)  
                //this check will maybe be done since we can iteract with something else and mouse will change even if we are waiting for travel to be completed 
                GameMouse.GetRefrence().SetMouseSprite(GameMouse.MOUSE_STATE.NORMAL);
            }
            else if (IsInSlowDownRadius())
                transform.Translate(Vector2.up * GetCurrentTravelSpeed() * Time.deltaTime * GetSLownDownFactor());
            else
                transform.Translate(Vector2.up * GetCurrentTravelSpeed() * Time.deltaTime);

        }
        else
        {

        }

        if (Input.GetMouseButtonDown(0) && trevelState==e_travelState.NOT_TRAVELING && GameMouse.GetRefrence().IsMouseOnAnyPlanet())
        {
            pointToTravelTo = GameMouse.GetRefrence().lastHoveredPlanet.transform.position;
            RotateTowardsTarget();
            ResetMovementStartTime();
            trevelState = e_travelState.TRAVELING;
            GameMouse.GetRefrence().SetMouseSprite(GameMouse.MOUSE_STATE.WAITING);
        }
		
	}



    float GetCurrentTravelSpeed()
    {
        return (Time.time - timeOfMovementStart > timeToReachMaxSpeed) ? maxTravelSpeed : (Time.time - timeOfMovementStart) / timeToReachMaxSpeed * maxTravelSpeed;
    }

    bool IsInTravelTargetRadius()
    {
        return (Vector2.Distance(transform.position, pointToTravelTo) < stopMovementTargetRadius) ? true : false;
    }
    
    bool IsInSlowDownRadius()
    {
        return (Vector2.Distance(transform.position, pointToTravelTo) < slownDownMovementRadius) ? true : false;
    }
    void RotateTowardsTarget()
    {
        Vector2 unitVecToDestination = (pointToTravelTo - (Vector2)transform.position).normalized;
        float rotZ = Mathf.Atan2(unitVecToDestination.y, unitVecToDestination.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
    float GetSLownDownFactor()
    {
        return Vector2.Distance(transform.position, pointToTravelTo) / slownDownMovementRadius;
    }
    void ResetMovementStartTime()
    {
        timeOfMovementStart = Time.time;
    }





}





