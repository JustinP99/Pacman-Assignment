using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Vector2 curTarget; // Destination position PacStudent is moving to
    private bool isLerping = false; // If PacStudent is moving
    private Vector2 lastInput; // The last movement direction input by player (WASD)
    private Vector2 currentInput; // Direction PacStudent is moving

    public Tilemap[] tileMaps; // Array of tile maps for the level

    public Animator animator; // Reference to Animator component
    public AudioSource audioSource; // Reference to AudioSource component
    public AudioClip moveAudio; // Audio clip for moving
    public AudioClip pelletAudio; // Audio clip for eating pellets
    public ParticleSystem dustParticle; // Reference to the dust particle system

    // Update is called once per frame
    void Update()
    {
        PlayerMoveWASD(); // Runs every frame - handles player input movement

        // Check if PacStudent is currently moving
        if (isLerping)
        {
            // Smoothly move/lerp PacStudent from current position to target position (curTarget)
            transform.position = Vector2.Lerp(transform.position, curTarget, moveSpeed * Time.deltaTime);

            // If PacStudent is close/reached target (curTarget)
            if (Vector2.Distance(transform.position, curTarget) < 0.01f)
            {
                transform.position = curTarget;
                isLerping = false;

                // Stop movement audio when not moving
                audioSource.Stop();
                animator.SetBool("isMoving", false); // Set animation to idle
                dustParticle.Stop(); // Stop the dust particle effect
            }
            else
            {
                // Continue playing movement audio while moving
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = moveAudio; // Set the appropriate audio clip
                    audioSource.loop = true; // Loop the audio
                    audioSource.Play(); // Play the audio
                }
                animator.SetBool("isMoving", true); // Set animation to moving
                if (!dustParticle.isPlaying)
                {
                    dustParticle.Play(); // Play dust particles while moving
                }
            }
        }
        else
        {
            TryToMove(lastInput); // Move in the direction of lastInput
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
        Vector2 nextPosition = transform.position + (Vector3)dir;

        if (canWalk(nextPosition))
        {
            currentInput = dir;
            curTarget = nextPosition;
            isLerping = true;

            // Check if next position is a pellet
            if (CheckForPellet(nextPosition))
            {
                audioSource.clip = pelletAudio; // Set pellet eating audio
                audioSource.PlayOneShot(pelletAudio); // Play sound once
            }
        }
    }

    private bool canWalk(Vector2 posTarget)
    {
        Vector3Int cellPos = Vector3Int.FloorToInt(tileMaps[0].WorldToCell(posTarget)); // Convert world position to tilemap cell

        foreach (Tilemap tileMap in tileMaps)
        {
            if (tileMap.HasTile(cellPos))
            {
                return false; // Not walkable
            }
        }
        return true; // Walkable
    }

    private bool CheckForPellet(Vector2 posTarget)
    {
        Vector3Int cellPos = Vector3Int.FloorToInt(tileMaps[0].WorldToCell(posTarget)); // Convert world position to tilemap cell

        foreach (Tilemap tileMap in tileMaps)
        {
            if (tileMap.HasTile(cellPos)) // Check if there is a tile (pellet) at the position
            {
                return true; // Pellet is present
            }
        }
        return false; // No pellet
    }

}

