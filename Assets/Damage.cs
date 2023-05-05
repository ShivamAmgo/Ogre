using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float MaxHealth = 100;
    [SerializeField] List<GameObject> BloodFxs;
    [SerializeField] List<string> DamageHitTriggers;
    [SerializeField] Animator m_animator;
    [SerializeField] ComboMaker m_comboMaker;
    bool CanTakeDamage = true;
    float CurrentHealth;
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Die()
    { 
        
    }
    public void TakeDamage(float damage)
    {
        if (!CanTakeDamage) return;
        CanTakeDamage = false;
        BloodFx(true);
        PlayHitAnimation();
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
        Debug.Log("Health " + CurrentHealth);
    }
    public void SetCanTakeDamage()
    {
        CanTakeDamage = true;

    }
    void BloodFx(bool ActiveStatus)
    {
        int random = Random.Range(0, BloodFxs.Count);

        BloodFxs[random].SetActive(false);
        BloodFxs[random].SetActive(ActiveStatus);
        
    }
    void PlayHitAnimation()
    {
        int random = Random.Range(0, DamageHitTriggers.Count);
        m_animator.SetTrigger(DamageHitTriggers[random]+"");
        m_comboMaker.SetcanAttackTrue();
    }
}
