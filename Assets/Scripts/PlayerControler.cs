using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{

    Vector2 playerInput;
    Vector2 moveDirection;
    Rigidbody2D rb;

    public float moveSpeed = 6f;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(!Mathf.Approximately(playerInput.x, 0) || !Mathf.Approximately(playerInput.y, 0)) {
            moveDirection.Set(playerInput.x, playerInput.y);
            moveDirection.Normalize();
        }
        Debug.DrawRay(rb.position, moveDirection * 2f, Color.red);
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + playerInput * moveSpeed * Time.deltaTime);
        
    }

    void OnMove(InputValue inputValue) {
        playerInput = inputValue.Get<Vector2>();
    }

    void OnInteract() {
        RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, moveDirection * 2f, LayerMask.GetMask("NPC"));
        if(hit.collider != null) {
            DialogueTrigger dialogueTrigger = hit.collider.gameObject.GetComponent<DialogueTrigger>();
            if(dialogueTrigger == null){
                Debug.Log("DialogueTrigger script not found on hit gameobject");
                return;
            }
            dialogueTrigger.TriggerDialogue();
        }
    }

}
