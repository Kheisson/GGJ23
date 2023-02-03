using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables {
    public class LandBlock : InteractableObject
    {
        public enum Status{ EMPTY, FERTILE, SEEDED, WET, RIPE, ROTTEN };

        private Status status;

        private void Awake()
        {
            status = Status.EMPTY;
        }
        public override void Interact()
        {
            if (status < Status.ROTTEN) { status += 1; }
            Debug.Log(status);
            return;
            
        }

        public override bool IsInteractable()
        {
            return true;
        }

        public Status getStatus() { return status; }
    }
}
