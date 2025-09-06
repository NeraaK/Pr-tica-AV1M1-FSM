using UnityEngine;

public class Fugir : BossBaseState
{
    public override void EnterState(StateMachineBossManager boss)
    {
        Debug.Log("Fugindo");
    }
    public override void UpdateState(StateMachineBossManager boss)
    {
        boss.t_Boss.position = Vector3.MoveTowards(boss.t_Boss.position, boss.t_Recover.position, boss.velocidade * 2 * Time.deltaTime);
        boss.t_Boss.LookAt(boss.t_Recover);
        boss.anim.Play(StateMachineBossManager.WALK);
        if(Vector3.Distance(boss.t_Boss.position, boss.t_Recover.position) < 0.5)
        {
            boss.TrocarEstado(boss.estadocurar);
        }
        else if (boss.vidaAtual <= 0)
        {
            boss.TrocarEstado(boss.estadoMorrer);
        }
    }
    public override void OnCollisionEnter(StateMachineBossManager boss, Collision collision)
    {

    }
    public override void OnExit(StateMachineBossManager boss)
    {

    }
}
