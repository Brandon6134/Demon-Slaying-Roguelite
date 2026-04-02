using UnityEngine;

public class Dash : Ability
{
    private Vector2 dashDirection;
    public float dashSpeed;
    public float dashDuration;
    private float dashTimeLeft;
    public AudioClip dashSFX;
    public override void TryActivate()
    {
        if (!Ready()) return;

        dashDirection = player.GetPlayerInput().normalized;

        if (dashDirection == Vector2.zero) //if player isn't moving, set dash direction to player's forward 
            dashDirection = new Vector2(player.transform.localScale.x,0f); 
        
        dashTimeLeft = dashDuration;

        AudioManager.Instance.PlaySFX(dashSFX);

        StartCooldown();
    }

    void FixedUpdate()
    {
        if (dashTimeLeft > 0f)
        {
            rb.linearVelocity = dashDirection * dashSpeed;
            dashTimeLeft -= Time.deltaTime;
        }
    }

    public bool IsDashing()
    {
        if (dashTimeLeft > 0f)
            return true;
        else
            return false;
    }
}
