using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public Tile[] tileMatchingArray = new Tile[7];
    public Vector3[] slotPositions = new Vector3[7];
    public int numberOfTiles;
    
    void Awake()
    {
        InitPositions();
        numberOfTiles = FindObjectsOfType<Tile>().Length;
        Debug.Log("Current number of tiles: " + numberOfTiles);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPosition = new Vector2(worldPoint.x, worldPoint.y);
            Collider2D hit = Physics2D.OverlapPoint(touchPosition);
            if (hit != null)
            {
                tileMatchingArray[0] = hit.gameObject.GetComponent<Tile>();
                numberOfTiles--;
                Debug.Log("Current number of tiles: " + numberOfTiles);
                Debug.Log(tileMatchingArray[0].gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name);
                MoveTileToMatchingArray(hit.gameObject.transform, slotPositions[0]);
            }
        }
    }

    int ValidPosition (Tile tile)
    {
        return 0;
    }

    void InitPositions()
    {
        slotPositions[0] = new Vector3(-1.5f, -2.5f);
        slotPositions[1] = new Vector3(-1f, -2.5f);
        slotPositions[2] = new Vector3(-0.5f, -2.5f);
        slotPositions[3] = new Vector3(0f, -2.5f);
        slotPositions[4] = new Vector3(0.5f, -2.5f);
        slotPositions[5] = new Vector3(1f, -2.5f);
        slotPositions[6] = new Vector3(1.5f, -2.5f);
    }
    
    void MoveTileToMatchingArray (Transform transform, Vector3 dest)
    {
        transform.DOMove(dest, 1);
    }
}
