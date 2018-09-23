using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public Interactable curFocus;

    public LayerMask movementMask;
    Camera cam;
    PlayerMotor motor;
    
	// Use this for initialization
	void Start () {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {

        // Checks if the mouse is currently hovering over the UI - prevents movement when selecting inventory slots
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

		if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100, movementMask))
            {
                // Debug.Log("We hit " + hit.collider.name + " " + hit.point);

                // Move our player to where we pressed
                motor.MoveToPoint(hit.point);

                // Stop focusing on an object
                RemoveFocus();
            }
        } else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                // Debug.Log("We hit " + hit.collider.name + " " + hit.point);

                Interactable interactable = hit.collider.GetComponent<Interactable>();
                // Check if we hit an interactable
                // If we did set it as our focus
                if (interactable) // interactable != null
                {
                    SetFocus(interactable);
                }
            }
        }

    }

    void SetFocus(Interactable newFocus)
    {
        if(curFocus != newFocus)
        {
            if(curFocus) // curFocus != null
            {
                curFocus.OnDefocused();
            }

            curFocus = newFocus;
            motor.FollowTarget(newFocus);
        }
        
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if(curFocus) // curFocus != null
        {
            curFocus.OnDefocused();
        }
        
        curFocus = null;
        motor.StopFollowingTarget();
    }
}
