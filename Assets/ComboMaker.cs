using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ComboMaker : MonoBehaviour
{
    [SerializeField] Animator PlayerAnim;
    bool canattack = true;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        if (!canattack) return;
        if (Input.GetMouseButtonDown(0))
        {
            Attack("FrontSlash");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Attack("FrontAxeCombo");
        }
    }
    public void SetcanAttackTrue()
    {
        canattack = true;
    }
   
    public void FrontAxeCombo()
    {
        DOVirtual.DelayedCall(0, () =>
        {
        }).OnUpdate(() => 
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlayerAnim.SetTrigger("FrontAxeCombo");
            }
        });
    }
    void Attack(string ComboTrigger)
    {
        canattack = false;
        PlayerAnim.SetTrigger("" + ComboTrigger);
    }
}
