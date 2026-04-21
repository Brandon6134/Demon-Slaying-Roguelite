using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputActionReference moveInput;
    [SerializeField] InputActionReference attackInput;
    [SerializeField] InputActionReference dashInput;
    [SerializeField] InputActionReference elementInput;
    private Vector2 playerMovement;
    public float playerSpeed;
    public float holdAttackTimeThreshhold;
    private Rigidbody2D playerRb;
    private PlayerProperties playerProperties;
    //private Vector2 playerInput;
    private float attackStartTime;
    private bool isHoldingAttack;
    private Vector2 lastInputDirection;
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

    void Update()
    {
        playerMovement = moveInput.action.ReadValue<Vector2>();

        // if (playerMovement != Vector2.zero)
        //     PlayerAnimManager.Instance.SetIsWalkinkg(true);
        // else
        //     PlayerAnimManager.Instance.SetIsWalkinkg(false);

        TickAbilities();
    }

    void FixedUpdate()
    {
        if (!dash.IsDashing() && !playerProperties.GetIsKnockedback() && !playerProperties.GetIsStunned())
            playerRb.linearVelocity = playerMovement*playerSpeed; 
        
        ChangeSpriteDirection(); 
    }
    
    public Vector2 GetPlayerInputDirection()
    {
        return lastInputDirection;
    }
    void ChangeSpriteDirection()
    {
        if (playerMovement != Vector2.zero) //save last player input before is zero, so animator remembers last direction facing (never reset to 0,0)
            lastInputDirection = playerMovement.normalized; //is normalized to set e.g. (0,0.002) to (0,1), important so all animations aren't used at once
        
        PlayerAnimManager.Instance.SetDirection(GetCardinalDirection(lastInputDirection));
    }

    private void OnEnable()
    {
        attackInput.action.started += AttackStart; //.started -> button was pressed down
        attackInput.action.canceled += AttackCancel; //.cancled -> button was released
        dashInput.action.started += Dash;
        elementInput.action.started += Element;
    }

    private void OnDisable()
    {
        attackInput.action.started -= AttackStart;
        attackInput.action.canceled -= AttackCancel;
        dashInput.action.started -= Dash;
        elementInput.action.started -= Element;
    }


    private void Dash(InputAction.CallbackContext obj)
    {
        dash.TryActivate();
    }

    private void Element(InputAction.CallbackContext obj)
    {
        shootFireballs.TryActivate();
    }

    private void AttackStart(InputAction.CallbackContext obj)
    {
        isHoldingAttack = true;
        attackStartTime = Time.time;
        playerProperties.ChangeColor(Color.yellow,0.5f);
    }

    private void AttackCancel(InputAction.CallbackContext obj)
    {
        if (!isHoldingAttack) return;
        
        isHoldingAttack = false;
        float heldTime = Time.time - attackStartTime;

        if (heldTime >= holdAttackTimeThreshhold)
            chargeAttack.TryActivate();
        else
            basicAttack.TryActivate();
        
        playerProperties.StopChangeColor();
        playerProperties.ResetColor();
    }

    private void TickAbilities()
    {
        basicAttack.Tick();
        dash.Tick();
        chargeAttack.Tick();
        shootFireballs.Tick();
    }

    //returns absolute input directions, e.g. (0.8,0.2) -> (1,0) moving right
    private Vector2 GetCardinalDirection(Vector2 dir)
    {
        // compare absolute values to find dominant axis
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            return new Vector2(Mathf.Sign(dir.x), 0); // Left or Right

        else //if x == y like (0.7,0,7) top right diagonal input, is defined as moving up
            return new Vector2(0, Mathf.Sign(dir.y)); // Up or Down
    }

}
