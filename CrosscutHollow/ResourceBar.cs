using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class ResourceBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    //Player health
    public PlayerHealth pHealth;
    //Player mana
    public PlayerMana pMana;


    public void SetMaxValue(int resource)
    {

        slider.maxValue = resource;
        slider.value = resource;
        fill.color = gradient.Evaluate(1f);

    }

    public void SetValue(int health)
    {

        Debug.Log("setting hp to " + health);
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);

    }

    //fully heals the player
    public void heal()
    {

        pHealth.currentHealth = pHealth.maxHealth;
        SetValue(pHealth.currentHealth);

    }

    //restores ammunition
    public void rejuvinate()
    {

        pMana.currentMana = pMana.maxMana;
        SetValue(pMana.currentMana);

    }

    //Player Health relative
    public void TakeDamage(int damage)
    {

        pHealth.currentHealth -= damage;
        SetValue(pHealth.currentHealth);

    }
    public void Heal(int healing)
    {

        pHealth.currentHealth += healing;
        SetValue(pHealth.currentHealth);

    }

    //Player Mana relative

    public void UseMana(int drain)
    {

        pMana.currentMana -= drain;
        SetValue(pMana.currentMana);

    }

    public void GainMana(int mBoost)
    {

        pMana.currentMana += mBoost;
        SetValue(pMana.currentMana);

    }

    void Update()
    {
        //slider.value = pHealth.currentHealth;
    }

}
