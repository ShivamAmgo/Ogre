using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float Damage = 5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("" + other.name);
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            
            other.transform.root.GetComponent<Damage>().TakeDamage(Damage);
        }
    }
}
