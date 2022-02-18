using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool onTopLayer = false;
    public Sprite tileSprite;
    public new string name;

    private void Awake()
    {
        this.name = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
    }
}
