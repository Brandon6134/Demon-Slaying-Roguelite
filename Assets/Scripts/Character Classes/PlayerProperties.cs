using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using System;

public class PlayerProperties: CharacterEntity
{
    private Coroutine colorCoroutine;

    void Update()
    {
        Die();
    }

    void Die()
    {
        if (activeHealth <= 0f)
        {
            Destroy(gameObject);
            print("You died!");
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        HUDManager.Instance.UpdateHealth();
    }

    public void ChangeColor(Color targetColor, float duration)
    {
        colorCoroutine = StartCoroutine(LerpColour(targetColor,duration));
    }

    public void StopChangeColor()
    {
        StopCoroutine(colorCoroutine);
    }

    private IEnumerator LerpColour(Color targetColor, float duration)
    {
        Color startColor = spriteRenderer.color;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            spriteRenderer.color = Color.Lerp(startColor,targetColor,t);
            yield return null;
        }

        spriteRenderer.color = targetColor;
    }
}