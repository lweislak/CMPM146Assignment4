using UnityEngine;

public class PermaBuff : BehaviorTree
{
    public override Result Run()
    {
        var target = GameManager.Instance.GetClosestOtherEnemy(agent.gameObject);
        if (target.GetComponent<EnemyController>().GetEffect("strength") > 4 && GameManager.Instance.GetClosestEnemy(target.transform.position))
        {
            target = GameManager.Instance.GetClosestEnemy(target.transform.position);
        }
        EnemyAction act = agent.GetAction("permabuff");
        if (act == null) return Result.FAILURE;

        bool success = act.Do(target.transform);
        return (success ? Result.SUCCESS : Result.FAILURE);
    }

    public PermaBuff() : base()
    {

    }

    public override BehaviorTree Copy()
    {
        return new PermaBuff();
    }
}
