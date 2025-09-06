using UnityEngine;

public class AtaqueEspecial : BossBaseState
{
    public override void EnterState(StateMachineBossManager boss)
    {
        Debug.Log("AtaqueForte");
    }
    public override void UpdateState(StateMachineBossManager boss)
    {
        boss.anim.Play(StateMachineBossManager.ESPECIAL);
        if(Vector3.Distance(boss.t_Boss.position, boss.t_Player.position) > 3f)
        {
            boss.TrocarEstado(boss.estadoAtaqueProjetil);
            
        }
        else if (boss.vidaAtual <= 50f)
        {
                boss.TrocarEstado(boss.estadoFugir);
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
