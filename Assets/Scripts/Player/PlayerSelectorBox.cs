using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;

public class PlayerSelectorBox : MonoBehaviour
{
    private bool collisionHappening = false;
    private GameObject collidingObject = null;
    private void OnTriggerEnter(Collider other)
    {
        collisionHappening = true;
        collidingObject = other.gameObject;
        collidingObject.GetComponent<InteractableObject>().highlightObject();
    }

    private void OnTriggerExit(Collider other)
    {
        collidingObject.GetComponent<InteractableObject>().unhighlightObject();
        collisionHappening = false;
        collidingObject = null;
    }

    public bool isColliding()
    {
        return collisionHappening;
    }

    public GameObject getCollidingObject()
    {
        return collidingObject;
    }
}
