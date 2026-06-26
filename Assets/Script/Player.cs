using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    private Vector3 LastInteractDirection;
    private void Update()
    {
        Movement();
        HandleInteraction();
    }
    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDirection != Vector3.zero)
        {
            LastInteractDirection = moveDirection;
        }

        float InteractionDistance = 2f;

        if (Physics.Raycast(transform.position, LastInteractDirection, out RaycastHit raycastHit, InteractionDistance))
        {
            if(raycastHit.transform.TryGetComponent(out Counter counter))
            {
                //Has Counter
                counter.Interact();
            }
        }
        else
        {
            Debug.Log("-");
        }
    }
    private void Movement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveSpeed * Time.deltaTime);

        if (canMove)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        float rotateSpeed = 10f;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

    }

 }

