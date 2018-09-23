using UnityEngine;

public class ItemPickup : Interactable {

    public Item item;

    public override void Interact()
    {
        base.Interact(); // Will print out Log message

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);

        // If successfully added item to inventory, remove from scene
        bool wasPickedUp = Inventory.instance.Add(item);
        if(wasPickedUp) {
            Destroy(gameObject);
        }
    }

}
