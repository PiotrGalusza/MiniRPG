using UnityEngine;

// Parent Interactable class for Items, NPCs, enemies, interactable environment objects
public class Interactable : MonoBehaviour {

    public float radius = 3f;
    public Transform interactTransf; // Enables us to limit from which direction players and object can interact (ie. chests)

    bool isFocus = false;
    bool hasInteracted = false;
    Transform player;

    // Method enabling children interactable objects to trigger unique interactions with the player
    public virtual void Interact()
    {
        // Designed to be overriden
        Debug.Log("Currently interacting with" + transform.name);
    }

    private void Update()
    {
        if(isFocus && !hasInteracted)
        {
            float distFromPlayer = Vector3.Distance(player.position, interactTransf.position);

            if (distFromPlayer <= radius)
            {
                Debug.Log("INTERACT - DO SOMETHING YA FOOL");
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    void OnDrawGizmosSelected()
    {
        if(!interactTransf) // interactionTransform == null
        {
            interactTransf = transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactTransf.position, radius);
    }


}
