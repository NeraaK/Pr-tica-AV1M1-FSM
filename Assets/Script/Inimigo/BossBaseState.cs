using UnityEngine;

public abstract class BossBaseState
{
    public abstract void EnterState(StateMachineBossManager boss);
    public abstract void UpdateState(StateMachineBossManager boss);
    public  abstract void OnCollisionEnter(StateMachineBossManager boss, Collision collision);
    public abstract void OnExit(StateMachineBossManager boss);
 
}
