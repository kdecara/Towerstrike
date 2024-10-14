using System.Collections;
using System.Collections.Generic;

//using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private PlayerMotor motor;
    //private PlayerLook look;
    public Camera cam;
    private float xRotation = 0f;
    //private float yRotation = 0f;
    public float xSensitivity = 30f;
    public float ySensitivity = 15f;
    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        //look = GetComponent<PlayerLook>();
        onFoot.Jump.performed += ctx => motor.Jump();

        onFoot.Crouch.performed += ContextMenu => motor.Crouch();
        onFoot.Sprint.performed += ContextMenu => motor.Sprint();
        //onFoot.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        Vector2 position = onFoot.Look.ReadValue<Vector2>();
        ProcessLook(position); //error
    }


     public void ProcessLook(Vector2 input)
    {
        float sensitivity = 50f;
        //Debug.Log(cam.transform.position.x);
        float mouseX = input.x * Time.deltaTime * xSensitivity;
        float mouseY = input.y * Time.deltaTime * ySensitivity;
        //calculate camera rotation for looking up and down
        xRotation -= mouseY; //Time.deltaTime);
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        Quaternion xRotationq = Quaternion.Euler(xRotation, transform.eulerAngles.y, 0);
        //apply this to our camera transform
        //cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        cam.transform.localRotation = Quaternion.Slerp(cam.transform.localRotation, xRotationq, Time.deltaTime * sensitivity);
        //rotate the player to look left and right
        transform.Rotate(Vector3.up * (mouseX));
    }
    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }

}
