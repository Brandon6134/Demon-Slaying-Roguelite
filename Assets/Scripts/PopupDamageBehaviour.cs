using System.Collections;
using TMPro;
using UnityEngine;

public class PopupDamageBehaviour : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    private Pool dmgTextPool;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dmgTextPool = transform.parent.GetComponent<Pool>(); //gain access to the dmg text pool when initailzed
    }

    // Update is called once per frame
    void Update()
    {
        damageText.alpha -= Time.deltaTime;
        if (damageText.alpha <= 0f)
            Die();
    }

    public void SetText(float damage)
    {
        damageText.text = damage.ToString();
    }

    void Die()
    {
        dmgTextPool.Despawn(gameObject);
        damageText.alpha = 1f;
    }
}
