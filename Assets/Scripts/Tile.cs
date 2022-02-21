using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool locked = false;
    public SpriteRenderer maskRenderer;
    public new string name;

    private void Awake()
    {
        if (this.locked)
        {
            maskRenderer.GetComponent<SpriteRenderer>().enabled = false;
        }
        this.name = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
    }
}
