using UnityEngine;

public class Idle : BossBaseState
{
    
    public override void EnterState(StateMachineBossManager boss)
    {
       
        Debug.Log("Parado");
    }
    public override void UpdateState(StateMachineBossManager boss)
    {
        boss.anim.Play(StateMachineBossManager.IDLE);

        if (Vector3.Distance(boss.transform.position, boss.t_Player.transform.position) < 15f)
        {
            boss.TrocarEstado(boss.estadoPerseguir);
        }
        else
        {
            
        }
    }
    public override void OnCollisionEnter(StateMachineBossManager boss, Collision collision)
    {

    }
    public override void OnExit(StateMachineBossManager boss)
    {

    }
}
