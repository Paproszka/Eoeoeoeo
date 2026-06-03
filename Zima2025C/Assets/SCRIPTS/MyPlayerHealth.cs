using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyPlayerHealth : MonoBehaviour
{
    public float maxHP = 100;
    public float HP = 100;
    public bool dead = false;
    public UnityAction onDeath;

    public GameObject deathPrefab;
    public MyPlayerMovement playerMovement;
    //public MyPlayerShooting playerShooting;
    public CharacterController cc;

    //public Slider healthBar;
    //public Image healthBarFillImage;
    //public Gradient colorGradient;
    //public TMP_Text hpText;

    public void Start()
    {
        HP = maxHP;
        //healthBar.maxValue = maxHP;
        //UpdateHealthSlider();
    }

    public void TakeDamage(float damage)
    {
        if (dead)
            return;

        HP -= damage;
        //UpdateHealthSlider();

        if (HP <= 0)
        {
            dead = true;
            Camera.main.GetComponent<MyCameraMovement>().DetachCamera();
            Instantiate(deathPrefab, transform.position, transform.rotation);
            onDeath?.Invoke();

            playerMovement.enabled = false;
            //playerShooting.enabled = false;
            cc.enabled = false;
        }
    }

    /*private void UpdateHealthSlider()
    {
        healthBar.value = HP;
        healthBarFillImage.color = colorGradient.Evaluate(HP / maxHP);
        hpText.text = ((HP / maxHP) * 100f).ToString("F0") + "%";
    }*/
}