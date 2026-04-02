using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class CharacterEntity : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //each character entity should have health, die when health = 0
    public float definedHealth = 10f;
    protected float activeHealth;
    public GameObject popupDamageText;
    protected Rigidbody2D rb;

    void Awake()
    {
        activeHealth = definedHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage)
    {
        activeHealth -= damage;
        Vector3 damageTextPos = gameObject.transform.position + new Vector3(Random.Range(-1f,1f),Random.Range(1f,2f),0);
        GameObject newPopupDamageText = Instantiate(popupDamageText,damageTextPos,Quaternion.identity);
        newPopupDamageText.GetComponent<PopupDamageBehaviour>().SetText(damage);
    }

}
