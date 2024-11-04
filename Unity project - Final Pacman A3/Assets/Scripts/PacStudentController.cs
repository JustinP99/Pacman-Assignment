using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PacStudentController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Vector2 curTarget;//destination pos pacStudent moving to
    private bool isLerping = false;//if pacStudent is moving
    private Vector2 lastInput;//the last movement direction input by player(WASD)
    private Vector2 currentInput;//dir pacStudent is moving

    public Tilemap[] tileMaps;

    void Update()
    {
        PlayerMoveWASD();//runs every frame - handles player input movement

        //PacStudent must constantly be moving so
        if (!isLerping){//if PacStudent NOT moving 
            TryToMove(lastInput);//move in direction of lastInput
        }

        if (isLerping)//if pacStudent IS moving
        {//smoothly move/lerp pacstudent from current position to target position(curTarget)
            transform.position = Vector2.Lerp(transform.position, curTarget, moveSpeed*Time.deltaTime);

            //if pacStudent close/reached target(curTarget)
            if(Vector2.Distance(transform.position, curTarget) < 0.01f)
            {
                transform.position = curTarget;
                isLerping=false;
            }
        }
    }

    private void PlayerMoveWASD()
    {
        if (Input.GetKeyDown(KeyCode.W)) lastInput = Vector2.up;
        else if (Input.GetKey(KeyCode.A)) lastInput = Vector2.left;
        else if (Input.GetKey(KeyCode.S)) lastInput = Vector2.down;
        else if (Input.GetKey(KeyCode.D)) lastInput = Vector2.right;
    }

    private void TryToMove(Vector2 dir)
    {
        if (canWalk(transform.position + (Vector3)dir))
        {
            currentInput = dir;
            curTarget = (Vector2)transform.position + dir;
            isLerping = true;
        }
    }

    private bool canWalk(Vector2 posTarget)
    {
        //Vector3Int cellPos = Tilemap.WorldToCell(posTarget);//convert world position to tilemap cell
        Vector3Int cellPos = Vector3Int.FloorToInt(tileMaps[0].WorldToCell(posTarget)); // Convert world position to tilemap cell using the first tile map

        foreach (Tilemap tileMap in tileMaps)
        {
            if (tileMap.HasTile(cellPos))
            {
                return false;//not walkable
            }
        }
        return true;//walkable
    }


}
