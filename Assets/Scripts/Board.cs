using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;
    private BackgroundTile[,] allTiles;
    public GameObject[] dots;
    public GameObject[,] allDots;
    void Start()
    {
        allTiles = new BackgroundTile[width, height];
        allDots = new GameObject[width, height];
        SetUp();
    }

    private void SetUp() {
    
    for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 tempPosition = new Vector2(i, j);
                GameObject backGroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                backGroundTile.transform.parent = this.transform;
                backGroundTile.name = "(" + i +" , " + j + ")";
                int dotToUse = Random.Range(0, dots.Length);
                
                int maxIterations = 0;
                while(MatchesAt(i, j, dots[dotToUse]) && maxIterations < 100){
                    dotToUse = Random.Range(0, dots.Length);
                    maxIterations++;
                }
                maxIterations = 0;

                GameObject dot = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                dot.transform.parent = this.transform;
                dot.name = "(" + i + " , " + j + ")";
                allDots[i,j] = dot;

            }
        }
    
    }

    private bool MatchesAt(int column,int row, GameObject piece)
    {
        if (column > 1 && row  > 1)
        {
            if (allDots[column - 1, row].tag == piece.tag && allDots[column -2 , row].tag == piece.tag)
            {
                return true;
            }
            if (allDots[column , row - 1].tag == piece.tag && allDots[column , row - 2].tag == piece.tag)
            {
                return true;
            }
        }else if (column <= 1 || row <= 1)
        {
            if (row > 1)
            {
                if (allDots[column, row - 1].tag == piece.tag && allDots[column, row - 2].tag == piece.tag)
                {
                    return true;
                }
            }
            if (column > 1)
            {
                if (allDots[column - 1, row ].tag == piece.tag && allDots[column - 2, row ].tag == piece.tag)
                {
                    return true;
                }
            }

            
        }



        return false;
    }

    private void DestroyMatchesAt(int column, int row)
    {
        if (allDots[column ,row].GetComponent<Dot>().isMatched)
        {
            Destroy(allDots[column, row]);
            allDots[column,row]=null;
        }
    }

    public void DestroyMatches()
    {
        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if (allDots[i, j] != null)
                {
                    DestroyMatchesAt(i, j);
                }
            }
        }
    }
}
