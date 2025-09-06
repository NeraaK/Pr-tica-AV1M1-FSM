using UnityEngine;


public class AtaqueProjetil : BossBaseState
{
    private float timer;
    private const float cooldown = 1.5f;
    public override void EnterState(StateMachineBossManager boss)
    {
        Debug.Log("Projetil");
        timer = 0f;
    }
    public override void UpdateState(StateMachineBossManager boss)
    {
        boss.t_Boss.LookAt(boss.t_Player);
        timer += Time.deltaTime;

        if (timer >= cooldown)
        {
            GameObject novoProjetil = boss.Instanciar(boss.projetilPrefab, boss.spawnPointProjetil.position, boss.spawnPointProjetil.rotation);
            Projetil scripProjetil = novoProjetil.GetComponent<Projetil>();
            boss.anim.Play(StateMachineBossManager.ATAQUEDISTANCIA);

            if (scripProjetil != null)
            {
                scripProjetil.AlvoPlayer(boss.t_Alvo);
            }
            timer = 0f;
        }
        
        if(Vector3.Distance(boss.t_Boss.position, boss.t_Player.position) <= 3f)
        {
            boss.TrocarEstado(boss.estadoAtaqueNormal);
        }
         if (Vector3.Distance(boss.t_Boss.position, boss.t_Player.position) <= 3f && boss.vidaAtual <= 100f)
        {
            boss.TrocarEstado(boss.estadoAtaqueEspecial);
            
        }
         else if(boss.vidaAtual < 50f)
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
