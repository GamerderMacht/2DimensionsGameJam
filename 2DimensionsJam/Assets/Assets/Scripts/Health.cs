using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
     public int currentHealth;
    [SerializeField] public int TotalHealth = 100;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Transform explosionLocation;

    public Slider hpSlider;

    public Animator loseScreenAnimator;

    private void Start() {
        currentHealth = TotalHealth;
        SetMaxHealth(TotalHealth);
    }
    public void TakeDamage(int amount){
        currentHealth -= amount;
        SetHealth(currentHealth);
        if(currentHealth <= 0){
            Die();
        }
    }

    public void SetMaxHealth(int health)
    {
        hpSlider.maxValue = health;
        hpSlider.value = health;
    }

    public void SetHealth(int health)
    {
        hpSlider.value = health;
        currentHealth = health;
    }

    
    private void Update() {
        if(Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(5);
        }
        
    }


    //only have implemented server so far
    private void Die(){
        Instantiate(explosionPrefab, explosionLocation.position, Quaternion.identity);
        Destroy(gameObject);
        if (this.gameObject.layer == 7) loseScreenAnimator.SetTrigger("PlayerLost");
    }
}
