using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ComboMaker : MonoBehaviour
{
    [SerializeField] Animator PlayerAnim;
    [SerializeField] PlayerMovement m_PlayerMovement;
    [SerializeField] Damage DMG;
    
    [SerializeField] GameObject HitFx;
    [SerializeField] float UltimatePowerPointsNeeded = 7;
    //[SerializeField] float MagicAttackPointsRequired = 8;
    bool canattack = true;
    bool InComboState = false;
    Tween ActiveTween;
    bool CanPowerAttack = false;
    float PowerCount = 0;

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
        else if (Input.GetMouseButton(1) && CanPowerAttack && canattack)
        {
            CanPowerAttack = false;
            Attack("AxeMagicAttack");
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
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerAnim.SetTrigger("Jump");
        }
        else if (Input.GetKeyDown(KeyCode.X) && PowerCount % UltimatePowerPointsNeeded == 0 && PowerCount>0)
        {
            Attack("BattleCry");
            PowerCount = 0;
            CanPowerAttack = true;
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
        //if(!CanPowerAttack)
        DMG.SetCanTakeDamage();
        PowerCount++;
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
   /*
    public void UltimateFX(int num)
    {
        if (num == 0)
        {
            PlayFx(MagicBuff, false);
            PlayFx(BlueAuraFx, false);
        }
        else
        {
            PlayFx(MagicBuff, true);
            PlayFx(BlueAuraFx, true);
        }
        
    }
    void PlayFx(GameObject fx, bool status)
    {
        fx.SetActive(false);
        fx.SetActive(status);
    }*/
   /*
    private void OnTriggerEnter(Collider collision)
    
    {
        //Debug.Log(collision.name);
        return;
        Vector3 contact = collision.ClosestPoint(transform.position);
        
            if (collision.transform.tag == "Damagable")
            {
                HitFx.transform.position = contact;
            HitFx.SetActive(false);
            HitFx.SetActive(true);
            }
            //print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
            // Visualize the contact point
            //Debug.DrawRay(contact., contact.normal, Color.white);
        
    }
   */
}
