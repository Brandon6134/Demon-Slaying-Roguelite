using UnityEngine;

public class DemonBehaviour : MonoBehaviour
{
    private float health = 10f;
    public GameObject popupDamageText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health<=0f)
            Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Vector3 damageTextPos = gameObject.transform.position + new Vector3(Random.Range(-1f,1f),Random.Range(1f,2f),0);
        GameObject newPopupDamageText = Instantiate(popupDamageText,damageTextPos,Quaternion.identity);
        newPopupDamageText.GetComponent<PopupDamageBehaviour>().SetText(damage);
        Debug.Log("damage taken! health is now: " + health);
    }
}
