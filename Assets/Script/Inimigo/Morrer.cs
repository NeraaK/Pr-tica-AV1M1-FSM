using UnityEngine;

public class Morrer : BossBaseState
{
    public override void EnterState(StateMachineBossManager boss)
    {
        Debug.Log("Estou Morto");
        boss.anim.Play(StateMachineBossManager.MORRER);
    }
    public override void UpdateState(StateMachineBossManager boss)
    {

    }
    public override void OnCollisionEnter(StateMachineBossManager boss, Collision collision)
    {

    }
    public override void OnExit(StateMachineBossManager boss)
    {

    }
}
