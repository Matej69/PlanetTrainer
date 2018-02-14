using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMouse : MonoBehaviour {

    private static GameMouse ref_gameMouse;
    static public GameMouse GetRefrence() { return ref_gameMouse; }

    public enum MOUSE_IMG { NORMAL, INTERACTIVE, WAITING }
    [HideInInspector]
    public MOUSE_IMG curSpriteId;
    Dictionary<MOUSE_IMG, Sprite> mouseSpriteMap = new Dictionary<MOUSE_IMG, Sprite>();

    private void Awake()
    {
        ref_gameMouse = this;
    }

    // Use this for initialization
    void Start ()
    {
        LoadMouseImgs();
        SetMouseSprite(MOUSE_IMG.NORMAL);
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateMousePos();
    }



    void UpdateMousePos()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);
    }


    void LoadMouseImgs()
    {
        mouseSpriteMap.Add(MOUSE_IMG.NORMAL, ResourceManager.GetSprite("Graphics/mouse/normal"));
        mouseSpriteMap.Add(MOUSE_IMG.INTERACTIVE, ResourceManager.GetSprite("Graphics/mouse/interactive"));
        mouseSpriteMap.Add(MOUSE_IMG.WAITING, ResourceManager.GetSprite("Graphics/mouse/waiting"));
    }

    public void SetMouseSprite(MOUSE_IMG spriteId)
    {
        GetComponent<SpriteRenderer>().sprite = mouseSpriteMap[spriteId];
        curSpriteId = spriteId;
    }




}
                                                                                                                 