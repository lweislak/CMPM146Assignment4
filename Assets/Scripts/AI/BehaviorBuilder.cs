using UnityEngine;

public class BehaviorBuilder
{
    public static BehaviorTree MakeTree(EnemyController agent)
    {
        BehaviorTree result = null;
        if (agent.monster == "warlock")
        {
            result = new Sequence(new BehaviorTree[] {
                                        new MoveToPlayer(agent.GetAction("attack").range),
                                        new Attack(),
                                        new PermaBuff(),
                                        new Heal(),
                                        new Buff()
                                     });
        }
        else if (agent.monster == "zombie")
        {
            result = new Selector(new BehaviorTree[] {
                //Move towards other zombie
                new Sequence(new BehaviorTree[]
                {
                    new NearbyEnemiesQuery(2, 10),
                    //new GoTowards(GameManager.Instance.GetClosestEnemy(agent.transform.position).transform, 6, 5)
                    new GoTo(GameManager.Instance.GetClosestEnemy(agent.transform.position).transform, 5)
                }),

                //Player in range
                new Sequence(new BehaviorTree[] {
                    new NearbyPlayerQuery(30),
                    new MoveToPlayer(agent.GetAction("attack").range),
                    new Attack()
                })
             });
        }
        else
        {
            result = new Sequence(new BehaviorTree[] {
                                       new MoveToPlayer(agent.GetAction("attack").range),
                                       new Attack()
                                     });
        }

        // do not change/remove: each node should be given a reference to the agent
        foreach (var n in result.AllNodes())
        {
            n.SetAgent(agent);
        }
        return result;
    }
}
