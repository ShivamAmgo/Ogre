using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxManagerGow : MonoBehaviour
{
    [SerializeField] ComboMaker m_combomaker;
    [SerializeField] Animator PlayerAnim;
    [SerializeField] GameObject MagicAttackFx;
    [SerializeField] GameObject AxeChargeFX;
    
    [SerializeField] GameObject MagicBuff;
    [SerializeField] GameObject BlueAuraFx;
    Transform Enemy;
    [SerializeField] GameObject AxeStrikeFX;
    [SerializeField] float AxeLightningStrikeTravelDuration = 1;
    [SerializeField] float AxeLightningFXFrequency = 0.25f;
    [SerializeField] GameObject[] MagicAttackFxs;
    //[SerializeField] GameObject AxeMagicFX;
    
    bool IsAxeLightningFxActive = false;
    Vector3 AxeLightningFXPOs;


    private void OnEnable()
    {
        OgreAI.OnDeliverEnemy += RecieveEnemy;
    }
    private void OnDisable()
    {
        OgreAI.OnDeliverEnemy -= RecieveEnemy;
    }
    private void Start()
    {
        AxeLightningFXPOs = AxeStrikeFX.transform.localPosition;
    }
    public void MagicAXeAttack()
    {
        IsAxeLightningFxActive = true;
        PlayAxeLightningMagicFX(false);
        AxeChargeFX.SetActive(false);
        AxeStrikeFX.transform.localPosition = AxeLightningFXPOs;
        StartCoroutine("AxeLightningFXtravelAnimation");
        AxeStrikeFX.transform.DOMove(Enemy.position+new Vector3(0,1,0), AxeLightningStrikeTravelDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            IsAxeLightningFxActive = false;
            AxeStrikeFX.SetActive(false);
            AxeStrikeFX.SetActive(true);
        });
    }
    IEnumerator AxeLightningFXtravelAnimation()
    {

        if (!IsAxeLightningFxActive)
        {
            yield break;

        }
        AxeStrikeFX.SetActive(false);
        AxeStrikeFX.SetActive(true);
        yield return new WaitForSeconds(AxeLightningFXFrequency);
        StartCoroutine("AxeLightningFXtravelAnimation");

    }
    void PlayAxeLightningMagicFX(bool status)
    {
        foreach (GameObject obj in MagicAttackFxs)
        {
            obj.SetActive(false);
            obj.SetActive(status);
        }
    }
    public void playLightningFx()//From Animator Bidu
    {
        PlayAxeLightningMagicFX(true);
    }
    
    private void RecieveEnemy(Transform enemy)
    {
        Enemy = enemy;
    }
    public void ChargeAxeFxPlay()
    {
        
        PlayFx(AxeChargeFX, true);
    }
    public void MagicAttackFxAnimationPlay()
    {
        PlayFx(AxeChargeFX, false);
        UltimateFX(0);
        m_combomaker.SetcanAttackTrue();
        PlayMagicAttackAnimation();

    }
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
    void PlayFx(GameObject fx,bool Activestatus)
    {
        fx.SetActive(false);
        fx.SetActive(Activestatus);
    }
    public void PlayMagicAttackAnimation()
    {
        MagicAXeAttack();
    }
}
