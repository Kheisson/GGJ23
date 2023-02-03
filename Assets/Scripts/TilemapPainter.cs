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
    private Vector3Int interactingCell;
    private Vector3 gridOffset;
    // Start is called before the first frame update
    private void Start()
    {
        gridOffset = new Vector3(grid.cellSize.x / 2, 0, -grid.cellSize.y / 2);
    }

    // Update is called once per frame
    void Update()
    {
        interactingCell = grid.WorldToCell(Player.transform.position + Player.transform.forward);
        if (playerInput.actions["Fire"].WasPerformedThisFrame())
        {
            Instantiate(marker, grid.CellToWorld(interactingCell) + gridOffset, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(grid.CellToWorld(interactingCell) + gridOffset, Vector3.one);
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(Player.transform.position + Player.transform.forward, Vector3.one);
    }
}
