using Equipment;
using HoldableItems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables {
    public class LandBlock : InteractableObject
    {
        public enum Status{ EMPTY, FERTILE, SEEDED, WET, RIPE, ROTTEN };

        private Status status;
        private GameObject producePrefab;
        private List<GameObject> children;
        private bool[] activateFertileLand = { false, true, false };
        private bool[] activateSeededLand = { false, true, true };
        private bool[] activateEmptyLand = { true, false, false };

        private void Awake()
        {
            InteractableType = EInteractableType.Land;
            status = Status.EMPTY;
            MeshRenderer = GetComponentInChildren<MeshRenderer>();
            OriginalMaterial = MeshRenderer.material;
            children = new List<GameObject>();
            foreach (Transform child in transform)
            {
                children.Add(child.gameObject);
            }
        }
        public override void Interact(WorkItem workItem, HoldableItem leftHandItem)
        {
            Debug.Log("interacting with landblock");
            if (workItem == null) { return; }
            switch (status)
            {
                case Status.EMPTY: 
                    if (workItem.Type == WorkItem.ItemType.SHOVEL ) { 
                        status = Status.FERTILE;
                    }
                    changeLandMesh(activateFertileLand);
                    break;
                case Status.FERTILE:
                    if (leftHandItem != null && leftHandItem.Type == HoldableItem.ItemType.SEED) { 
                        changeLandMesh(activateSeededLand);
                        plantSeed(leftHandItem);
                    } 
                    break;
                case Status.SEEDED: 
                    if(workItem.Type == WorkItem.ItemType.WATERCAN) {
                        status = Status.WET;
                    }
                    break;
                case Status.WET: break;
                case Status.RIPE: //TODO: Spawn produce prefab in the place.
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

        private void plantSeed(HoldableItem leftHandItem)
        {
            Debug.Log("Planeted" + leftHandItem.CurrentVeggy.veggeyName);
            status = Status.SEEDED;
            producePrefab = leftHandItem.CurrentVeggy.veggeyPrefab;
            Destroy(leftHandItem.gameObject);
        }

        private void changeLandMesh(bool[] childrenFlags)
        {
            this.unhighlightObject();
            for(int i = 0; i < children.Count; ++i)
            {
                children[i].SetActive(childrenFlags[i]);
            }
            MeshRenderer = GetComponentInChildren<MeshRenderer>();
            OriginalMaterial = MeshRenderer.material;
        }
    }
}
