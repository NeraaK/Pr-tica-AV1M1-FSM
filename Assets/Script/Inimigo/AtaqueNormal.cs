using UnityEngine;

public class AtaqueNormal : BossBaseState
{
    public override void EnterState(StateMachineBossManager boss)
    {
        Debug.Log("AtaqueBase");
    }
    public override void UpdateState(StateMachineBossManager boss)
    {

        boss.anim.Play(StateMachineBossManager.ATAQUENORMAL);
        boss.t_Boss.LookAt(boss.t_Player);
        if (Vector3.Distance(boss.t_Boss.position, boss.t_Player.position) >= 3f && (Vector3.Distance(boss.t_Boss.position, boss.t_Player.position) <=10))
        {
            boss.TrocarEstado(boss.estadoAtaqueProjetil);
        }
        else if (boss.vidaAtual < 100f)
        {
            boss.TrocarEstado(boss.estadoAtaqueEspecial);
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
