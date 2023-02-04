using Equipment;
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
        public override void Interact(WorkItem workItem, bool isLeftHandEmpty, string seedHeldName)
        {
            // if (status < Status.ROTTEN) { status += 1; }
            if (workItem == null) { return; }
            switch (status)
            {
                case Status.EMPTY: if (workItem.Id == 0) { status = Status.FERTILE; } break;
                case Status.FERTILE: if (seedHeldName != null) { plantSeed(seedHeldName); } break;
                case Status.SEEDED: if(workItem.Id == 1) { status = Status.WET; } break;
                case Status.WET: break;
                case Status.RIPE:
                case Status.ROTTEN: if(workItem.Id == 2) { status = Status.FERTILE; } break;
            }
            Debug.Log(status);
            return;
            
        }

        public override bool IsInteractable()
        {
            return true;
        }

        public Status getStatus() { return status; }

        private void plantSeed(string seedName)
        {
            Debug.Log("Planeted" + seedName);
            status = Status.SEEDED;
        }
    }
}
