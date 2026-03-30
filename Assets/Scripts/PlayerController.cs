using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D playerRb;
    private SpriteRenderer spriteRd;
    private Vector2 input;
    
    private float timeBtwAttack; //attack timer
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;
    private AudioSource audioSource;
    public AudioClip hitSwordSlashSFX;
    public AudioClip missSwordSlashSFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        spriteRd = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        playerRb.linearVelocity = input*speed;
        attack();
        flipSprite();
    }

    void attack()
    {
        if (timeBtwAttack <= 0) //if can attack
        {
            if (Input.GetKey(KeyCode.Space))
            {
                
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position,attackRange,whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++) //make all enemies in circle hitbox take damage
                {
                    enemiesToDamage[i].GetComponent<DemonBehaviour>().TakeDamage(damage);
                }

                //plays correct sword slash sfx if enemy hit or missed
                if (enemiesToDamage.Length==0)
                    audioSource.PlayOneShot(missSwordSlashSFX);
                else
                    audioSource.PlayOneShot(hitSwordSlashSFX);
                
                timeBtwAttack = startTimeBtwAttack; //reset time attack timer 
            }        
            
        }
        else
            timeBtwAttack -= Time.deltaTime; //countdown attack timer

    }

    void OnDrawGizmosSelected() //make circle hitbox visible in editor
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position,attackRange);
    }

    void flipSprite()
    {
        if (input.x > 0f)
            transform.localScale = new Vector3(1,1,1);
        else if (input.x < 0f)
            transform.localScale = new Vector3(-1,1,1);
    }
}
