using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class MapGenerator : MonoBehaviour {

    public int width;
    public int height;

    public bool useRandomSeed;  //Random lvl
    public bool walls;          //Are the lvl surrounded by walls
    public bool floor;          //Does the lvl have a floor
    public string seed;         //Preset lvl


    [Range(0, 100)]              //limits a range on ints
    public int randomFillPercent;
    int[,] map;

    void Start() {
        GenerateMap();
    }

    void Update() {
        if (Input.GetKeyDown("r")) {
            GenerateMap();
        }
    }

    void GenerateMap() {
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < 5; i++) {
            SmoothMap();
        }

    }

    void RandomFillMap() {
        if (useRandomSeed) {
            seed = DateTime.Now.Ticks.ToString();    //get random seed from time
        }

        System.Random pseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;    //if the randomnumber < randomFillPercent = 1 else 0

                if (walls) {
                    if (x == 0 || x == width - 1 || y == 0 || y == height - 1) {    //if walls then fill edges
                        map[x, y] = 1;
                    }
                }
                if (floor) {
                    if (y == 0) {    //if walls then fill edges
                        map[x, y] = 1;
                    }
                }
            }
        }
    }

    void SmoothMap() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                int neighborWallTiles = GetSurroundingWallCount(x, y);

                if (neighborWallTiles > 4)
                    map[x, y] = 1;
                else if (neighborWallTiles < 4)
                    map[x, y] = 0;
            }
        }
    }

    int GetSurroundingWallCount(int gridX, int gridY) {
        int wallCount = 0;
        for (int neighborX = gridX - 1; neighborX <= gridX + 1; neighborX++) {      //for each surrounding x
            for (int neighborY = gridY - 1; neighborY <= gridY + 1; neighborY++) {   //for each surrounding y
                if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height) {  //if neighbor is inside the map
                    if (neighborX != gridX || neighborY != gridY) {
                        wallCount += map[neighborX, neighborY];         //if map[x,y] = 1 then wallCount ++
                    }
                } else {
                    if (walls) //if checked wall is an edge, increase wall count to encourage growth near edges
                        wallCount++;
                    else {
                        if (floor && gridY == 0) //if checked wall is an edge, increase wall count to encourage growth near floor
                            wallCount++;
                    }
                }
            }
        }

        return wallCount;
    }

    // https://docs.unity3d.com/ScriptReference/Gizmos.html
    private void OnDrawGizmos() {
        if (map != null) {
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    Gizmos.color = (map[x, y] == 1) ? Color.black : Color.clear;
                    Vector3 pos = new Vector3(-width / 2 + x + .5f, 0, -height / 2 + y + .5f);  //Center and align the cubes
                    Gizmos.DrawCube(pos, Vector3.one);
                }
            }
        }
    }

}
  
