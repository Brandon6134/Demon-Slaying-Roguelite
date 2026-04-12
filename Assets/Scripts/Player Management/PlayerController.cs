using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float holdMouse0Threshold;
    private Rigidbody2D playerRb;
    private PlayerProperties playerProperties;
    private Vector2 playerInput;
    private float holdMouse0Timer;
    private bool isHoldingMouse0;
    private int playerDirection;
    BasicAttack basicAttack;
    Dash dash;
    ChargeAttack chargeAttack;
    ShootFireballs shootFireballs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerProperties = GetComponent<PlayerProperties>();
        
        basicAttack = GetComponent<BasicAttack>();
        dash = GetComponent<Dash>();
        chargeAttack = GetComponent<ChargeAttack>();
        shootFireballs = GetComponent<ShootFireballs>();
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
        shootFireballs.Tick();

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

        if(Input.GetMouseButtonDown(1))
            shootFireballs.TryActivate();

        
        if (Input.GetKeyDown(KeyCode.LeftShift))
            dash.TryActivate();
    }

    void FixedUpdate()
    {
        if (!dash.IsDashing() && !playerProperties.GetIsKnockedback() && !playerProperties.GetIsStunned())
            playerRb.linearVelocity = playerInput*playerSpeed; 
        
        ChangeSpriteDirection(); 
    }
    
    public Vector2 GetPlayerInput()
    {
        return playerInput;
    }
    void ChangeSpriteDirection() //let playerDirection = 1 for right, 2 for left, 3 for up, 4 for down
    {
        // if (playerInput.x > 0f)
        //     transform.localScale = new Vector3(1,1,1);
        // else if (playerInput.x < 0f)
        //     transform.localScale = new Vector3(-1,1,1);

        if (playerInput.x > 0f && playerInput.x > Math.Abs(playerInput.y))
            playerDirection = 1;
        else if (playerInput.x < 0f && Math.Abs(playerInput.x) > Math.Abs(playerInput.y))
            playerDirection = 2;
        else if (playerInput.y > 0f && playerInput.y > Math.Abs(playerInput.x))
            playerDirection = 3;
        else if (playerInput.y < 0f && Math.Abs(playerInput.y) > Math.Abs(playerInput.x))
            playerDirection = 4;
        
        PlayerAnimManager.Instance.SetDirection(playerDirection);
    }

}
