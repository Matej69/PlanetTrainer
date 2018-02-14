using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMouse : MonoBehaviour {

    private static GameMouse ref_gameMouse;
    static public GameMouse GetRefrence() { return ref_gameMouse; }

    [HideInInspector]
    public GameObject lastHoveredPlanet;
    private bool isMouseOnAnyPlanet;

    public enum MOUSE_STATE { NORMAL, INTERACTIVE, WAITING }
    [HideInInspector]
    public MOUSE_STATE mouseState;
    Dictionary<MOUSE_STATE, Sprite> mouseSpriteMap = new Dictionary<MOUSE_STATE, Sprite>();

    private void Awake()
    {
        ref_gameMouse = this;
    }

    // Use this for initialization
    void Start ()
    {
        LoadMouseImgs();
        SetMouseSprite(MOUSE_STATE.NORMAL);
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateMousePos();
        UpdateMouseOverPlanetState();
        if (IsMouseOnAnyPlanet() && mouseState != MOUSE_STATE.WAITING)
            SetMouseSprite(MOUSE_STATE.INTERACTIVE);
        else if (!IsMouseOnAnyPlanet() && mouseState != MOUSE_STATE.WAITING)
            SetMouseSprite(MOUSE_STATE.NORMAL);

    }



    void UpdateMousePos()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);
    }


    void LoadMouseImgs()
    {
        mouseSpriteMap.Add(MOUSE_STATE.NORMAL, ResourceManager.GetSprite("Graphics/mouse/normal"));
        mouseSpriteMap.Add(MOUSE_STATE.INTERACTIVE, ResourceManager.GetSprite("Graphics/mouse/interactive"));
        mouseSpriteMap.Add(MOUSE_STATE.WAITING, ResourceManager.GetSprite("Graphics/mouse/waiting"));
    }

    public void SetMouseSprite(MOUSE_STATE spriteId)
    {
        GetComponent<SpriteRenderer>().sprite = mouseSpriteMap[spriteId];
        mouseState = spriteId;
    }


        
    private void UpdateMouseOverPlanetState()
    {
        isMouseOnAnyPlanet = false;
        foreach (GameObject planet in MapGeneration.GetRefrence().visiblePlanets)
            if (Vector2.Distance(planet.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < PlanetTypes.GetPlanetRadius())
            {
                lastHoveredPlanet = planet;
                isMouseOnAnyPlanet = true;
            }       
    }

    public bool IsMouseOnAnyPlanet()
    {
        return isMouseOnAnyPlanet;
    }



}
                                                                                                                 