using System.Numerics;
using UnityEngine;

public class NearbyPlayerQuery : BehaviorTree
{
    float distance;

    public override Result Run()
    {
        if (UnityEngine.Vector3.Distance(agent.transform.position, GameManager.Instance.player.transform.position) < distance)
        {
            return Result.SUCCESS;
        }
        return Result.FAILURE;
    }

    public NearbyPlayerQuery(float distance) : base()
    {
        this.distance = distance;
    }

    public override BehaviorTree Copy()
    {
        return new NearbyPlayerQuery(distance);
    }
}
