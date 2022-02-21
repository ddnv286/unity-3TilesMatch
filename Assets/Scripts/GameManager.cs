using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public List<Tile> tileMatchingList;
    public Vector3[] slotPositions = new Vector3[7];
    public int numberOfTiles;
    public ParticleSystem matchedParticle;
    
    private void Awake()
    {
        InitPositions();
        numberOfTiles = FindObjectsOfType<Tile>().Length;
    }

    // Update is called once per frame
    private void Update()
    {
        if (MatchArrayIsFull())
        {
            Debug.Log("Level lost.");
        } 
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPosition = new Vector2(worldPoint.x, worldPoint.y);
            Collider2D hit = Physics2D.OverlapPoint(touchPosition);
            if (hit != null)
            {
                int validPos = ValidPosition(hit.gameObject.GetComponent<Tile>());
                if (validPos != -1)
                {
                    if (validPos < tileMatchingList.Count)
                    {
                        RearrangeMatchingArrayFrom(validPos);
                    }
                    MoveTileToMatchingArea(hit.gameObject.GetComponent<Tile>(), slotPositions[validPos]);
                    numberOfTiles--;
                    hit.gameObject.GetComponent<Collider2D>().enabled = false;
                    tileMatchingList.Insert(validPos, hit.gameObject.GetComponent<Tile>());
                    CheckMatch(hit.gameObject.GetComponent<Tile>());
                }
            }
        }
        CheckWinCondition();
    }

    private int ValidPosition (Tile tile)
    {
        int count = 0, tilePos = 0;
        for (int i = 0; i < tileMatchingList.Count; i++)
        {
            if (tile.name.Equals(tileMatchingList[i].name))
            {
                count++;
                tilePos = i;
            }
        }
        if (count != 0)
        {
            return tilePos + 1;
        }
        else
            return tileMatchingList.Count;
    }

    private bool MatchArrayIsFull ()
    {
        if (tileMatchingList.Count == 7)
        {
            return true;
        }
        else
            return false;
    }

    private void CheckTileOverlapped(Tile tile) {
        // nothing for now

    }

    private void CheckMatch(Tile tile)
    {
        if (tileMatchingList.Count >= 3)
        {
            string tileName = tile.name;
            int count = 0;
            for (int i = 0; i < tileMatchingList.Count; i++)
            {
                if (tileMatchingList[i].name.Equals(tileName))
                {
                    count++;
                    if (count == 3)
                    {
                        //int index = ValidPosition(tileMatchingList[i + 1]);
                        // delay for a second then start processing lines inside lambda function
                        // using OnComplete with a lambda function or another callback function to call whatever we need to process after the animation finished.
                        /*
                        DOVirtual.DelayedCall(1, () => {
                            tileMatchingList[index - 2].gameObject.transform.DOScale(Vector3.zero, 0.5f);
                            tileMatchingList[index - 1].gameObject.transform.DOScale(Vector3.zero, 0.5f);
                            tileMatchingList[index].gameObject.transform.DOScale(Vector3.zero, 0.5f);
                        }).OnComplete(()=> {
                            // do nothing for now
                        });
                        */
                        Vector3 particlePosition = tileMatchingList[i - 1].gameObject.transform.position;
                        tileMatchingList[i - 2].gameObject.transform.DOScale(Vector3.zero, 0.15f).SetDelay(1f);
                        tileMatchingList[i - 1].gameObject.transform.DOScale(Vector3.zero, 0.15f).SetDelay(1f);
                        tileMatchingList[i].gameObject.transform.DOScale(Vector3.zero, 0.15f).SetDelay(1f);
                        DOVirtual.DelayedCall(1.15f, () =>
                        {
                            matchedParticle.transform.position = particlePosition;
                            matchedParticle.Play();
                        });
                        // destroy game object immediately after finish shrinking and playing particle
                        DestroyImmediate(tileMatchingList[i]);
                        DestroyImmediate(tileMatchingList[i - 1]);
                        DestroyImmediate(tileMatchingList[i - 2]);
                        tileMatchingList.RemoveAll(t => t.name == tile.name);
                        DOVirtual.DelayedCall(1.5f, FillTile);
                    }
                }
            }
        }
    }

    private void FillTile ()
    {
        for (int i = 0; i < tileMatchingList.Count; i++)
        {
            tileMatchingList[i].transform.DOMove(slotPositions[i], 0.25f);
        }
    }

    private void InitPositions()
    {
        slotPositions[0] = new Vector3(-1.5f, -2.5f);
        slotPositions[1] = new Vector3(-1f, -2.5f);
        slotPositions[2] = new Vector3(-0.5f, -2.5f);
        slotPositions[3] = new Vector3(0f, -2.5f);
        slotPositions[4] = new Vector3(0.5f, -2.5f);
        slotPositions[5] = new Vector3(1f, -2.5f);
        slotPositions[6] = new Vector3(1.5f, -2.5f);
    }
    
    private void CheckWinCondition ()
    {
        if (numberOfTiles == 0 && tileMatchingList.Count == 0)
        {
            Debug.Log("Level Completed.");
        }
    }

    private void MoveTileToMatchingArea (Tile tile, Vector3 dest)
    {
        tile.gameObject.transform.DOMove(dest, 1);
    }

    private void RearrangeMatchingArrayFrom(int index)
    {
        if (index == 0)
            return;
        for (int i = tileMatchingList.Count; i > index; i--)
        {
            MoveTileToMatchingArea(tileMatchingList[i - 1], slotPositions[i]);
        }
    }
}
