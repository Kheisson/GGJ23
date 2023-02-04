using Equipment;
using HoldableItems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timers;
using System;

namespace Interactables {
    public class LandBlock : InteractableObject
    {
        [SerializeField] private Material rottenCropMaterial;
        [SerializeField] private Material wetFertiledLand;
        [SerializeField] private Material FertiledLand;
        [SerializeField] private Transform LeftHandPos;
        public enum Status{ EMPTY, FERTILE, SEEDED, WET, RIPE, ROTTEN };

        public float growTimeout = 10f;
        public float rotTimeout = 5f;

        private EquipmentManager equipManager;
        private Status status;
        private GameObject producePrefab;
        private GameObject cropObject;
        private List<GameObject> children;
        private Timer growTimer;
        private Timer rotTimer;
        private bool[] activateFertileLand = { false, true, false, true};
        private bool[] activateSeededLand = { false, true, true, true };
        private bool[] activateEmptyLand = { true, false, false, true };

        private void Awake()
        {
            InteractableType = EInteractableType.Land;
            status = Status.EMPTY;
            MeshRenderer = GetComponentInChildren<MeshRenderer>();
            OriginalMaterial = MeshRenderer.material;
            children = new List<GameObject>();
            growTimer = new Timer(growTimeout);
            rotTimer = new Timer(rotTimeout);
            growTimer.OnTimerEnd += changeToCropReady;
            rotTimer.OnTimerEnd += changeToCropRotten;
            foreach (Transform child in transform)
            {
                children.Add(child.gameObject);
            }
        }

        private void Update()
        {
            if (growTimer != null)
            {
                growTimer.UpdateTimer(); 
            }
            
            if (rotTimer != null)
            {
                rotTimer.UpdateTimer();
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
                        changeLandMesh(activateFertileLand);
                    }
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
                        children[1].GetComponent<MeshRenderer>().material = wetFertiledLand;
                        MeshRenderer = GetComponentInChildren<MeshRenderer>();
                        OriginalMaterial = MeshRenderer.material;
                        growTimer.StartTimer();
                    }
                    break;
                case Status.WET: break;
                case Status.RIPE: 
                case Status.ROTTEN: 
                    if(workItem.Type == WorkItem.ItemType.HANDS)
                    {
                        status = Status.FERTILE;
                        // InteractableType = EInteractableType.Land;
                        cropObject.transform.parent = LeftHandPos;
                        rotTimer.StopTimer();
                        rotTimer.ResetTimer();
                        changeLandMesh(activateFertileLand);
                    }
                    break;
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
            for(int i = 0; i < childrenFlags.Length; ++i)
            {
                children[i].SetActive(childrenFlags[i]);
            }
            MeshRenderer = GetComponentInChildren<MeshRenderer>();
            OriginalMaterial = MeshRenderer.material;
        }

        private void changeToCropReady()
        {
            Debug.Log("Crop Ready!");

            growTimer.StopTimer();
            growTimer.ResetTimer();

            status = Status.RIPE;
            //InteractableType = EInteractableType.LandVegitable;

            children[1].GetComponent<MeshRenderer>().material = FertiledLand;
            MeshRenderer = GetComponentInChildren<MeshRenderer>();
            OriginalMaterial = MeshRenderer.material;
            changeLandMesh(activateFertileLand);

            cropObject = Instantiate(producePrefab, children[3].transform);

            rotTimer.StartTimer();
        }

        private void changeToCropRotten()
        {
            rotTimer.StopTimer();
            rotTimer.ResetTimer();

            Material[] allMats = cropObject.GetComponent<MeshRenderer>().materials;
            for (int i = 0; i < allMats.Length; ++i)
            {
                allMats[i] = rottenCropMaterial;
            }
            cropObject.GetComponent<MeshRenderer>().materials = allMats;
        }
    }
}
