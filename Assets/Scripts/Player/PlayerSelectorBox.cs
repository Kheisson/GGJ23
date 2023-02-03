using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectorBox : MonoBehaviour
{
    private bool collisionHappening = false;
    private GameObject collidingObject = null;
    private void OnTriggerEnter(Collider other)
    {
        collisionHappening = true;
        collidingObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
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
