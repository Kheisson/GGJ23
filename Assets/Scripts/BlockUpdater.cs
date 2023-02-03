using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockUpdater : MonoBehaviour
{
    [SerializeField] Player.Player player;
    private void Start()
    {
        player._playerInteractions.OnInteractEvent += updateCell;
        
    }

    private void updateCell(Interactables.IInteractable interactable) {
        
        Debug.Log("Block interacted");
    }
}
