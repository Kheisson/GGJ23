using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TilemapPainter : MonoBehaviour
{

    [SerializeField] private GameObject Player;
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject marker;
    [SerializeField] PlayerInput playerInput;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        Vector3Int currentCell = grid.WorldToCell(Player.transform.position);
        Debug.Log(currentCell);
        if (playerInput.actions["Fire"].WasPerformedThisFrame())
        {
            Instantiate(marker, grid.CellToWorld(currentCell), Quaternion.identity);
        }
    }
}
