using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isOverlapped = false;
    public SpriteRenderer maskRenderer;
    public new string name;
    public ContactFilter2D contactFilter;

    private void Awake()
    {
        contactFilter.useTriggers = false;
        if (this.isOverlapped)
        {
            maskRenderer.GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
        this.name = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
    }
}
