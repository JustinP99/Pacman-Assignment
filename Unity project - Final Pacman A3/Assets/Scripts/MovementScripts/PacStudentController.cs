using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PacStudentController : MonoBehaviour
{
    public float moveSpeed = 4.0f;
    private Vector2 curTarget;//pos pacStudent moving to
    private bool isLerpingMove = false;//if pacStudent is moving
    private Vector2 lastInput;//last movement direction input by player(WASD)
    private Vector2 currentInput;//dir pacStudent is moving

    public Tilemap[] pacTiles;

    void Update()
    {
        //player input
        if (Input.GetKeyDown(KeyCode.W)) lastInput = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.A)) lastInput = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.S)) lastInput = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.D)) lastInput = Vector2.right;

        //if pacStudent IS moving
        if (isLerpingMove){
            //move pacstudent from current position to target position
            float pacSpeed = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, curTarget, pacSpeed);

            //if pacStudent close/reached target(curTarget)
            if (Vector2.Distance(transform.position, curTarget) < 0.03f)
            {
                isLerpingMove = false; transform.position = curTarget;
            }
        }
        //if pacStudent NOT moving
        else if (!isLerpingMove){
            TryToMove(lastInput);
        }

    }

    private void TryToMove(Vector2 dir)
    {
        Vector3 targetPos = transform.position + (Vector3)dir;
        bool canWalk = true;

        foreach (Tilemap tileMap in pacTiles){
            //conver world pos to cell pos in grid
            Vector3Int cellPos = tileMap.WorldToCell(targetPos);
            //if tile at position - not walkable
            if (tileMap.HasTile(cellPos)){
                canWalk = false; break;
            }
        }
        if (canWalk) {isLerpingMove = true; currentInput = dir; curTarget = targetPos;}
    }

}
