using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float holdMouse0Threshold;
    private Rigidbody2D playerRb;
    private Vector2 playerInput;
    private float holdMouse0Timer;
    private bool isHoldingMouse0;
    BasicAttack basicAttack;
    Dash dash;
    ChargeAttack chargeAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        
        basicAttack = GetComponent<BasicAttack>();
        dash = GetComponent<Dash>();
        chargeAttack = GetComponent<ChargeAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInput = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

        if (playerInput != Vector2.zero)
            PlayerAnimManager.Instance.SetIsWalkinkg(true);
        else
            PlayerAnimManager.Instance.SetIsWalkinkg(false);

        basicAttack.Tick();
        dash.Tick();
        chargeAttack.Tick();

        if (Input.GetMouseButtonDown(0))
        {
            isHoldingMouse0 = true;
            holdMouse0Timer = 0f;
        }

        if (Input.GetMouseButton(0) && isHoldingMouse0)
            holdMouse0Timer += Time.deltaTime;
        
        if (Input.GetMouseButtonUp(0) && isHoldingMouse0)
        {
            isHoldingMouse0 = false;

            if (holdMouse0Timer >= holdMouse0Threshold)
                chargeAttack.TryActivate();
            else
                basicAttack.TryActivate();
        }
        
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
