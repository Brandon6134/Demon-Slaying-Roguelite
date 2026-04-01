using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    private Rigidbody2D playerRb;
    private Vector2 playerInput;
    BasicAttack basicAttack;
    Dash dash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        
        basicAttack = GetComponent<BasicAttack>();
        dash = GetComponent<Dash>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInput = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

        basicAttack.Tick();
        dash.Tick();

        if (Input.GetMouseButtonDown(0))
            basicAttack.TryActivate();
        if (Input.GetKeyDown(KeyCode.LeftShift))
            dash.TryActivate();
    }

    void FixedUpdate()
    {
        if (!dash.IsDashing())
            playerRb.linearVelocity = playerInput*playerSpeed; 

        FlipSprite(); 
    }
    
    public Vector2 GetPlayerInput()
    {
        return playerInput;
    }
    void FlipSprite()
    {
        if (playerInput.x > 0f)
            transform.localScale = new Vector3(1,1,1);
        else if (playerInput.x < 0f)
            transform.localScale = new Vector3(-1,1,1);
    }

}
