using Equipment;
using HoldableItems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timers;
using System;
using TMPro;

namespace Interactables {
    public class LandBlock : InteractableObject
    {
        [SerializeField] private Material rottenCropMaterial;
        [SerializeField] private Material wetFertiledLand;
        [SerializeField] private Material FertiledLand;
        [SerializeField] private Transform LeftHandPos;
        public enum Status{ EMPTY, FERTILE, SEEDED, WET, RIPE, ROTTEN };

        public float baseGrowTimeout = 10f;
        public float baseRotTimeout = 5f;
        public float randomGrowTimeRange = 10f;

        private EquipmentManager equipManager;
        private TextMeshProUGUI growTimerVisual;
        private Status status;
        private GameObject producePrefab;
        private GameObject cropObject;
        private List<GameObject> children;
        private Timer growTimer;
        private Timer rotTimer;
        private bool[] activateFertileLand = { false, true, false, true};
        private bool[] activateSeededLand = { false, true, true, true };
        private bool[] activateEmptyLand = { true, false, false, true };
        private VeggySo currentVeggyOnLand;

        public VeggySo CurrentVeggyOnLand => currentVeggyOnLand;

        public GameObject CropObject
        {
            get
            {
                if (cropObject != null)
                {
                    InteractableType = EInteractableType.Land;
                    status = Status.EMPTY;
                    changeLandMesh(activateEmptyLand);
                    return cropObject;
                }

                return null;
            }
        }

        private void Awake()
        {
            equipManager = GameObject.FindGameObjectWithTag("Player").GetComponent<EquipmentManager>();
            InteractableType = EInteractableType.Land;
            status = Status.EMPTY;
            growTimerVisual = GetComponentInChildren<TextMeshProUGUI>();
            MeshRenderer = GetComponentInChildren<MeshRenderer>();
            OriginalMaterial = MeshRenderer.material;
            children = new List<GameObject>();
            growTimer = new Timer(baseGrowTimeout);
            rotTimer = new Timer(baseRotTimeout);
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
                growTimerVisual.text = ((int)growTimer.GetTime()).ToString();
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
                        growTimerVisual.enabled = true;
                        growTimer.ChangeTime(baseGrowTimeout + UnityEngine.Random.Range(0f, randomGrowTimeRange));
                        growTimer.ResetTimer();
                        growTimer.StartTimer();
                    }
                    break;
                case Status.WET: break;
                case Status.RIPE:
                    rotTimer.StopTimer();
                    rotTimer.ResetTimer();
                    break;
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
            currentVeggyOnLand = leftHandItem.CurrentVeggy;
            producePrefab = currentVeggyOnLand.veggeyPrefab;
            equipManager.DestroyItemInLeftHand();
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

            growTimerVisual.enabled = false;
            growTimer.StopTimer();
            growTimer.ResetTimer();

            status = Status.RIPE;
            InteractableType = EInteractableType.Vegetable;

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
