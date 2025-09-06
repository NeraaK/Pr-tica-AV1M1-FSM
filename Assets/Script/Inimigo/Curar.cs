using UnityEngine;

public class Curar : BossBaseState
{
    public override void EnterState(StateMachineBossManager boss)
    {
        
        boss.anim.Play(StateMachineBossManager.IDLE);
    }
    public override void UpdateState(StateMachineBossManager boss)
    {
        if (Vector3.Distance(boss.t_Boss.position, boss.t_Recover.position) < 0.5)
        {
            boss.tempoRegeneracao += Time.deltaTime;
            if(boss.tempoRegeneracao >= boss.intervaloRegeneracao)
            {
                Debug.Log("Curando");
                boss.vidaAtual += boss.taxaDeRegeneracao;
                if (boss.vidaAtual > boss.vidaMaxima)
                {
                    boss.vidaAtual = boss.vidaMaxima;
                }
                boss.tempoRegeneracao = 0f;
            }
        }

        if(boss.vidaAtual == boss.vidaMaxima)
        {
            boss.TrocarEstado(boss.estadoPerseguir);
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
