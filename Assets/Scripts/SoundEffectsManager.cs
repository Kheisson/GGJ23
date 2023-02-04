using Equipment;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    [SerializeField] private AudioClip shoveling;
    [SerializeField] private AudioClip watering;
    [SerializeField] private AudioClip placingItemInTruck;
    [SerializeField] private AudioClip pickup;
    [SerializeField] private PlayerContainer playerContainer;
    [SerializeField] private DeliveryTruck deliveryTruck;
    [SerializeField] private EquipmentManager equipManager;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
        playerContainer.AddUsedShovelListener(playShoveling);
        playerContainer.AddUsedWaterCanListener(playWatering);
        deliveryTruck.barrelPlaced += playBarrelPlaced;
        equipManager.itemPickUp += playItemPickUp;
    }

    private void playShoveling()
    {
        audioSource.clip = shoveling;
        audioSource.Play();
    }

    private void playWatering()
    {
        audioSource.clip = watering;
        audioSource.Play();
    }

    private void playBarrelPlaced()
    {
        audioSource.clip = placingItemInTruck;
        audioSource.Play();
    }

    private void playItemPickUp()
    {
        audioSource.clip = pickup;
        audioSource.Play();
    }

}
