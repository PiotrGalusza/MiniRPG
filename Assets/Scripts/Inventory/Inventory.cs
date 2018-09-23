using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Singleton
    // Creating singleton to allow access to this specific inv (smaller CPU burden)
    public static Inventory instance;

    private void Awake()
    {
        if(instance) // instance != null, guarantees only 1 inv will exist in this game
        {
            Debug.LogWarning("More than 1 instance of inventory found! How did you manage this?");
            return;
        }
        instance = this;
    }
    #endregion

    // Can subscribe methods to this event/delegate. Whenever the delegate is called, all subscribed methods are called as well
    // Will be used for updating the Inventory UI
    public delegate void OnItemChange();
    public OnItemChange onItemChangedCallback;

    public int space = 20;

    public List<Item> items = new List<Item>();

    public bool Add(Item newItem)
    {
        if(!newItem.isDefaultItem)
        {
            if(items.Count >= space)
            {
                Debug.Log("Not enough room, sowwy: " + items.Count + " " + space);
                return false;
            } else
            {
                items.Add(newItem);

                if(onItemChangedCallback != null)
                {
                    onItemChangedCallback.Invoke();
                }
            }
        }

        return true;
    }

    public void Remove(Item unwantedItem)
    {
        items.Remove(unwantedItem);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

}
