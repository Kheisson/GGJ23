using Equipment;
using HoldableItems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables {
    public class LandBlock : InteractableObject
    {
        public enum Status{ EMPTY, FERTILE, SEEDED, WET, RIPE, ROTTEN };
        [SerializeField] private Mesh fertileLand;

        private Status status;

        private void Awake()
        {
            InteractableType = EInteractableType.Land;
            status = Status.EMPTY;
            MeshRenderer = GetComponentInChildren<MeshRenderer>();
            OriginalMaterial = MeshRenderer.material;
        }
        public override void Interact(WorkItem workItem, HoldableItem leftHandItem, string seedHeldName)
        {
            // if (status < Status.ROTTEN) { status += 1; }
            Debug.Log("interacting with landblock");
            if (workItem == null) { return; }
            switch (status)
            {
                case Status.EMPTY: if (workItem.Type == WorkItem.ItemType.SHOVEL ) { status = Status.FERTILE; } break;
                case Status.FERTILE: if (leftHandItem != null && leftHandItem.Type == HoldableItem.ItemType.SEED) { plantSeed(leftHandItem, seedHeldName); } break;
                case Status.SEEDED: if(workItem.Type == WorkItem.ItemType.WATERCAN) { status = Status.WET; } break;
                case Status.WET: break;
                case Status.RIPE:
                case Status.ROTTEN: if(workItem.Type == WorkItem.ItemType.HANDS) { status = Status.FERTILE; } break;
            }
            Debug.Log(status);
            return;
            
        }

        public override bool IsInteractable()
        {
            return true;
        }

        public Status getStatus() { return status; }

        private void plantSeed(HoldableItem leftHandItem, string seedName)
        {
            Debug.Log("Planeted" + seedName);
            status = Status.SEEDED;
            Destroy(leftHandItem.gameObject);
        }
    }
}
