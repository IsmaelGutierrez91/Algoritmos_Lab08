using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float PlayerSpeed;
    float xDirection;
    float zDirection;
    Rigidbody RigidbodyComponent;
    public static event Action OnNodeInteraction;
    void Start()
    {
        RigidbodyComponent = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        RigidbodyComponent.velocity = new Vector3(xDirection * PlayerSpeed, RigidbodyComponent.velocity.y, zDirection * PlayerSpeed);
    }
    public void OnMovementX(InputAction.CallbackContext context)
    {
        xDirection = context.ReadValue<float>();
    }
    public void OnMovementZ(InputAction.CallbackContext context)
    {
        zDirection = context.ReadValue<float>();
    }
    public void OnInteractionWhitNode(InputAction.CallbackContext context)
    {
        OnNodeInteraction?.Invoke();
    }
}
