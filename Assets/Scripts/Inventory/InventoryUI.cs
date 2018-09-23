using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject inventoryUI;

    // Caches Inventory Singleton for better performance
    Inventory inventory;
    InventorySlot[] slots;

    // Use this for initialization
    void Start() {
        inventory = Inventory.instance;
        // Subscribes to Callback system - update Inventory UI whenever a change occurs
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    // Contains all the code for updating our inventory
    void UpdateUI()
    {
        Debug.Log("Updating Inventory UI.");

        for(int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            } else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
