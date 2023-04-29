using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FxManagerOgre : MonoBehaviour
{
    [SerializeField] GameObject StrikeFX;
    [SerializeField] float LightningStrikeTravelDuration = 1;
    [SerializeField] float LightningFXFrequency = 0.25f;
    [SerializeField] GameObject[] MagicAttackFxs;
    [SerializeField] GameObject SwordMagicFX;
    Transform Target;
    bool IsLightningFxActive = false;
    Vector3 LightningFXPOs;
    private void Start()
    {
        LightningFXPOs = StrikeFX.transform.localPosition;
    }
    public void SetPlayer(Transform Player)
    {
        Target = Player;
    }
    public void MagicAttack()
    {
        IsLightningFxActive = true;
        PlayLightningMagicFX(false);
        SwordMagicFX.SetActive(false);
        StrikeFX.transform.localPosition = LightningFXPOs;
        StartCoroutine("LightningFXtravelAnimation");
        StrikeFX.transform.DOMove(Target.position, LightningStrikeTravelDuration).SetEase(Ease.Linear).OnComplete(() => 
        {
            IsLightningFxActive = false;
            StrikeFX.SetActive(false);
            StrikeFX.SetActive(true);
        });
    }
    IEnumerator  LightningFXtravelAnimation()
    {

        if (!IsLightningFxActive)
        {
            yield break;
            
        }
        StrikeFX.SetActive(false);
        StrikeFX.SetActive(true);
        yield return new WaitForSeconds(LightningFXFrequency);
        StartCoroutine("LightningFXtravelAnimation");

    }
    void PlayLightningMagicFX(bool status)
    {
        foreach (GameObject obj in MagicAttackFxs)
        {
            obj.SetActive(false);
            obj.SetActive(status);
        }
    }
    public void playLightningFx()//From Animator Bidu
    {
        PlayLightningMagicFX(true);
    }
    public void PlaySwordMagic()
    {
        SwordMagicFX.SetActive(false);
        SwordMagicFX.SetActive(true);
    }
    
}
