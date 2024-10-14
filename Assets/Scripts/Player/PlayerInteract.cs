using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 3f;

    [SerializeField]
    private LayerMask mask;

    private PlayerUI playerUI;

    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<InputManager>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo; // variable to store our collision information

        //raycast to center of string
        if(Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            //if gameobject has an interactable component and if YES store in a variable interactable
            if(hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
                //TODO: fix this
                /*
                if(inputManager.OnFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
                */
                Debug.Log(hitInfo.collider.GetComponent<Interactable>().promptMessage);
            }
        }

    }
    
}
