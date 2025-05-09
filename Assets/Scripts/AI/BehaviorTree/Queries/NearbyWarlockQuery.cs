using UnityEngine;
using static BehaviorTree;


public class NearbyWarlockQuery : BehaviorTree
{
    float distance;

    public override Result Run()
    {
        var nearby = GameManager.Instance.GetEnemiesInRange(agent.transform.position, distance);
        foreach(var enemy in nearby) 
        {
            if (enemy.GetComponent<EnemyController>().monster == "warlock") //This is very silly
            {
                return Result.SUCCESS;
            }
        }

        return Result.FAILURE;
    }

    public NearbyWarlockQuery(float distance) : base()
    {
        this.distance = distance;
    }

    public override BehaviorTree Copy()
    {
        return new NearbyWarlockQuery(distance);
    }
}
