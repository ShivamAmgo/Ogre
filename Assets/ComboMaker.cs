using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ComboMaker : MonoBehaviour
{
    [SerializeField] Animator PlayerAnim;
    [SerializeField] PlayerMovement m_PlayerMovement;
    bool canattack = true;
    bool InComboState = false;
    Tween ActiveTween;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        if (!canattack || InComboState) return;
        if (Input.GetMouseButtonDown(0) && !(Input.GetKey(KeyCode.LeftShift)))
        {
            Attack("FrontSlash");
        }
        else if (Input.GetMouseButtonDown(1) && !(Input.GetKey(KeyCode.LeftShift)))
        {
            Attack("FrontAxeCombo");
        }
        else if (Input.GetMouseButtonDown(0) && (Input.GetKey(KeyCode.LeftShift)))
        {
            Attack("DownSlash");
        }
        else if (Input.GetMouseButtonDown(1) && (Input.GetKey(KeyCode.LeftShift)))
        {
            Attack("HorizontalSlash");
        }
        else if (Input.GetKeyDown(KeyCode.Space) )
        {
            PlayerAnim.SetTrigger("Jump");
        }
    }
    public void SetcanAttackTrue()
    {
        canattack = true;
        InComboState = false;
        m_PlayerMovement.ResetSpeed();
        
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
    public void Attack(string ComboTrigger)
    {
        m_PlayerMovement.SetPlayerSpeed(0);
        canattack = false;
        PlayerAnim.SetTrigger("" + ComboTrigger);
    }
    public void Combo(string ComboName)
    {
       // Debug.Log("ComboCAlled");
         ActiveTween= DOVirtual.DelayedCall(0, () =>
        {
        }).OnUpdate(() =>
        {
            //Debug.Log("InTween");
            if (Input.GetMouseButton(0))
            {
                canattack = false;
                InComboState = true;
                PlayerAnim.SetTrigger("" + ComboName);
                ActiveTween.Kill();
                return;
            }
        });
    }
}
