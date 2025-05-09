using UnityEngine;

public class MoveToWarlock : BehaviorTree
{
    float distance;
    float arrived_distance;

    public override Result Run()
    {
        GameObject warlock = null;
        var nearby = GameManager.Instance.GetEnemiesInRange(agent.transform.position, distance);
        foreach (GameObject enemy in nearby)
        {
            if(enemy.GetComponent<EnemyController>().monster == "warlock") //This is very silly
            {
                warlock = enemy;
                break;
            }
        }

        if (!warlock) { return Result.FAILURE; }

        Vector3 direction = warlock.transform.position - agent.transform.position;
        if (direction.magnitude < arrived_distance)
        {
            agent.GetComponent<Unit>().movement = new Vector2(0, 0);
            return Result.SUCCESS;
        }
        else
        {
            agent.GetComponent<Unit>().movement = direction.normalized;
            return Result.IN_PROGRESS;
        }
    }

    public MoveToWarlock(float distance, float arrived_distance) : base()
    {
        this.distance = distance;
        this.arrived_distance = arrived_distance;
    }

    public override BehaviorTree Copy()
    {
        return new MoveToWarlock(distance, arrived_distance);
    }
}
