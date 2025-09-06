using UnityEngine;

public class Perseguir : BossBaseState
{
    
   public override void EnterState(StateMachineBossManager boss)
    {
        Debug.Log("Perseguindo");
    }
    public override void UpdateState(StateMachineBossManager boss)
    {
        boss.t_Boss.transform.position = Vector3.MoveTowards(boss.t_Boss.position, boss.t_Player.position, boss.velocidade  * Time.deltaTime);
        boss.t_Boss.LookAt(boss.t_Player);
        boss.anim.Play(StateMachineBossManager.WALK);

        if (Vector3.Distance(boss.t_Boss.position, boss.t_Player.position) <= 3f)
        {
            boss.TrocarEstado(boss.estadoAtaqueNormal);

            if ((Vector3.Distance(boss.t_Boss.position, boss.t_Player.position) >= 10f))
            {
                boss.TrocarEstado(boss.estadoAtaqueProjetil);
            }
            else if(boss.vidaAtual <= 0)
            {
                boss.TrocarEstado(boss.estadoMorrer);
            }
           
        }
       
    }
    public override void OnCollisionEnter(StateMachineBossManager boss, Collision collison)
    {

    }
    public override void OnExit(StateMachineBossManager boss)
    {
        
    }

}
