using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentSeed : MonoBehaviour
{
    [SerializeField] private Sprite carrot;
    [SerializeField] private Sprite pumpkin;
    [SerializeField] private Sprite raddish;
    [SerializeField] private Sprite watermelon;

    private Equipment.EquipmentManager equipManager;
    private Image image;
    private Color emptyColor;
    private void Awake()
    {
        equipManager = GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment.EquipmentManager>();
        equipManager.itemPickUp += ChangeSeedImage;
        equipManager.seedDestroyed += ClearSeedImage;
        image = this.GetComponent<Image>();
        emptyColor = image.color;
    }

    private void ChangeSeedImage(HoldableItems.HoldableItem pickedItem)
    {
        if(pickedItem.Type == HoldableItems.HoldableItem.ItemType.SEED)
        {
            image.sprite = equipManager.ItemInLeftHand.CurrentVeggy.veggeySprite;
            image.color = Color.white;
        }
    }

    private void ClearSeedImage()
    {
        image.sprite = null;
        image.color = emptyColor;
    }


}
