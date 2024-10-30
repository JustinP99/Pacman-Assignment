using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManLevLayout : MonoBehaviour
{
    public GameObject OutsideCorner;
    public GameObject OutsideWall;
    public GameObject InsideCorner;
    public GameObject InsideWall;
    public GameObject StandardPellet;
    public GameObject PowerPellet;
    public GameObject tJunction;

    private void Start()
    {
        CreateLayout();
    }

    void CreateLayout()
    {
        int[,] levelMap =
        {
            {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
            {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
            {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
            {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
            {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
            {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
            {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
            {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
            {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
            {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
            {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
        };

        //for (int row = 0; row < levelMap.Length; row++);
        //{
        //    //int posX = tile width * row;
        //    int rowLength = levelMap[row].length;

        //    for (int column = 0; column < rowLength; column++)
        //    {
        //        //int posY = tile height * column;
        //        //this will iterate over each position in the array aka tile
        //        int tile = levelMap[row][column];
        //    }


        //}



        int sizeOfTile = 1; //1 is size of each tile - placeholder of sprites
        // Use GetLength to determine the number of rows and columns
        int numberOfRows = levelMap.GetLength(0);
        int numberOfColumns = levelMap.GetLength(1);

        for (int row = 0; row < numberOfRows; row++)
        {
            for(int col = 0; col < numberOfColumns; col++)
            {
                int tile = levelMap[row,col];
                GameObject TilePrefabtoCreate = null;

                switch (tile) 
                {
                    case 1: TilePrefabtoCreate = OutsideCorner; break;
                    case 2: TilePrefabtoCreate = OutsideWall; break;
                    case 3: TilePrefabtoCreate = InsideCorner; break;
                    case 4: TilePrefabtoCreate = InsideWall; break;
                    case 5: TilePrefabtoCreate = StandardPellet; break;
                    case 6: TilePrefabtoCreate = PowerPellet; break;
                    case 7: TilePrefabtoCreate = tJunction; break;
                    case 0: continue;//empty tile
                }

                //instanitate prefab/tile at position
                Vector3 position = new Vector3(col * sizeOfTile, 0, row * sizeOfTile);
                Instantiate(TilePrefabtoCreate, position, Quaternion.identity, transform);
            }
        }



    }

}
