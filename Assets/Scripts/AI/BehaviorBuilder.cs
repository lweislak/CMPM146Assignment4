using Unity.VisualScripting;
using UnityEngine;

public class BehaviorBuilder
{
    public static BehaviorTree MakeTree(EnemyController agent)
    {
        BehaviorTree result = null;
        if (agent.monster == "warlock")
        {
            result = new Selector(new BehaviorTree[] {

                //Buff enemy
                new Sequence(new BehaviorTree[] {
                    new NearbyEnemiesQuery(1, 5), //Max range of 5
                    new Buff()
                }),

                //Perm buff enemy
                 new Sequence(new BehaviorTree[] {
                    new NearbyEnemiesQuery(1, 5), //Max range of 5
                    new PermaBuff()
                }),

                //Heal enemy
                //Note: Will damage the warlock
                /*
                new Sequence(new BehaviorTree[] {
                    new NearbyEnemiesQuery(1, 5), //Max range of 5
                    //TODO: Check for health
                    new Heal()
                })
                */
            });
        }
        else if (agent.monster == "zombie")
        {
            result = new Selector(new BehaviorTree[] {
                //TODO: If GetClosestOtherEnemy is a warlock, go to that warlock
                //TODO: Use GetEnemiesInRange

                //Player in range
                new Sequence(new BehaviorTree[] {
                    new NearbyEnemiesQuery(10, 100),
                    new MoveToPlayer(agent.GetAction("attack").range),
                    new Attack()
                }),
               
                new Sequence(new BehaviorTree[] {
                    new NearbyPlayerQuery(20),
                    new MoveToPlayer(agent.GetAction("attack").range),
                    new Attack()
                }),
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
