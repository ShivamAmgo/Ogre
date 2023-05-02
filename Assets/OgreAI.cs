using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum EnemyState
{ 
    Idle,
    Attacking,
    Manuever,
    Defending,
    Chase
}
public class OgreAI : MonoBehaviour
{
    [SerializeField] Animator Enemyanim;
    [SerializeField] FxManagerOgre fxManagerOgre;
    [SerializeField] float attackRate = 1;
    [SerializeField] float ChaseDistance = 15;
    [SerializeField] float AttackDistance = 5;
    [SerializeField] float JumpStrikeRange = 3;
    [SerializeField] float CloseAttackDistanceRatio = 1.5f;
    [SerializeField] List<string> AnimationSwordAttackTriggers;
    [SerializeField] List<string> AnimationCLoseRangeTriggers;
    [SerializeField]
    EnemyState m_enemystate;
    Transform Target;
    float TargetDistance = 0;
    float StateTimer =0;
    bool Canattack = true;
    float PowerCount = 0;
    bool PowerAttacking = false;
    bool MagicAttacking = false;
    bool SpecialAttackActive = false;
    bool IsChasing = false;
    string PreviousAttack="";
    //AnimatorStateInfo PreviousAnimation;
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        TargetDistance = Vector3.Distance(transform.position, Target.position);
        fxManagerOgre.SetPlayer(Target);
        //Enemyanim.SetTrigger("Idle");
        m_enemystate = EnemyState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if(Target==null)return;
        TargetDistance = Vector3.Distance(transform.position, Target.position);
        //Debug.Log(m_enemystate + "  " + TargetDistance);
        if (m_enemystate == EnemyState.Idle)
        {
            Idle();
        }
        else if (m_enemystate == EnemyState.Chase)
        {
            Chase();
        }
        else if (m_enemystate == EnemyState.Attacking)
        {
            Attack();
        }
        else if (m_enemystate == EnemyState.Manuever)
        {
            Manuever();
        }
        else if (m_enemystate == EnemyState.Defending)
        {
            Defend();
        }
    }
    #region States
    private void LateUpdate()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
    void Idle()
    {
        if (IsChasing)
        {
            IsChasing = false;
        }
        if (TargetDistance > ChaseDistance)
        {
            m_enemystate = EnemyState.Chase;
            //Enemyanim.SetTrigger("WalkForward");
        }
    }
    void Chase()
    {
        RotateTowardsTarget();
        if (!IsChasing)
        {
            IsChasing = true;
            Enemyanim.SetTrigger("WalkForward");
        }
        if (TargetDistance<=AttackDistance)
        {
            StateTimer = 0;
            m_enemystate = EnemyState.Attacking;
            
        }
    }
    void Attack()
    {
        //Debug.Log("" + TargetDistance);
        if (IsChasing)
            IsChasing = false;
        StateTimer += Time.deltaTime;
        if (PowerAttacking && StateTimer > 1 / attackRate && Canattack && TargetDistance > AttackDistance )
        {
            StateTimer = 0;
            PowerAttacking = false;
            SpecialAttackActive = true;
            Debug.Log("power count " + PowerCount + " ");
            if (PowerCount / 10 >= 1)
            {
                PowerCount = 0;
                MagicAttack();
            }
            else
            {
                JumpPowerAttack();
            }
            
        }
        else if (TargetDistance > AttackDistance && !SpecialAttackActive && !PowerAttacking)
        {
            StateTimer = 0;
            Canattack = true;
            Enemyanim.SetTrigger("WalkForward");
            m_enemystate = EnemyState.Chase;
            
        }
        else if (StateTimer > 1 / attackRate && Canattack && !SpecialAttackActive)
        {
            
            StateTimer = 0;
            RandomAttack();
            Canattack = false;
        }
    }
    void Manuever()
    { 
        
    }
    void Defend()
    { 
        
    }
    #endregion
    public void RotateTowardsTarget()
    {
        Vector3 Dir = (Target.position - transform.position).normalized;

        
        transform.rotation = Quaternion.LookRotation(Dir);
    }
    public void SetCanAttack()
    {
        //Debug.Log("set");
        //PreviousAnimation = Enemyanim.GetCurrentAnimatorStateInfo(0);
        Canattack = true;
        SpecialAttackActive = false;
    }
    
    void RandomAttack()
    {
        PowerCount++;
        int random = Random.Range(1, 16);
        string Trigger = "";
        if (PowerCount % 5 == 0)
        {
            Debug.Log("powrup");
            PowerAttacking = true;
        }
        if (TargetDistance < AttackDistance / CloseAttackDistanceRatio)
        {
            Trigger = RandomAttackGenerate(AnimationCLoseRangeTriggers);
            Enemyanim.SetTrigger(Trigger);
            return;

        }
        else if ( TargetDistance >= AttackDistance / CloseAttackDistanceRatio)
        {
            Trigger= RandomAttackGenerate(AnimationSwordAttackTriggers);
            Enemyanim.SetTrigger(Trigger);
        }
        
    }
    void JumpPowerAttack()
    {
        Enemyanim.SetTrigger("JumpStrike");
    }
    void MagicAttack()
    {
        Enemyanim.SetTrigger("Roar");
        Enemyanim.SetTrigger("SwordMagic");
    }
    public void LightningStrikeGround()
    {
        fxManagerOgre.MagicAttack();
    }
    string RandomAttackGenerate(List<string> Triggers)
    {

        List<string> temp = new List<string>(Triggers);
        if (temp.Contains(PreviousAttack))
        {
            temp.Remove(PreviousAttack);
        }
        int random = Random.Range(0,temp.Count);
        PreviousAttack = temp[random];
        return temp[random];
    }
}
