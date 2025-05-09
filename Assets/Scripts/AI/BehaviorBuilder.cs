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
                //TODO: Prioritize enemies to buff/heal

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

                //If player is in range, attack
                new Sequence(new BehaviorTree[] {
                    new NearbyPlayerQuery(18),
                    new MoveToPlayer(agent.GetAction("attack").range),
                    new Attack()
                }),

                //If there are 4 enemies in range, attack the player
                new Sequence(new BehaviorTree[] {
                    new NearbyEnemiesQuery(6, 15),
                    new MoveToPlayer(agent.GetAction("attack").range),
                    new Attack()
                }),

                //If warlock is nearby, go to that warlock
                //FIX: Cannot break out of sequence
                 new Sequence(new BehaviorTree[] {
                    //new NearbyWarlockQuery(30),
                    new MaxEnemiesQuery(2, 10),
                    new MoveToWarlock(30, 3),
                    //new MaxEnemiesQuery(2, 10)
                })
             });
        }
        else
        {
            result = new Selector(new BehaviorTree[] {
                //TODO: Seek out nearest zombie and follow them around
                 new Sequence(new BehaviorTree[] {
                    new NearbyPlayerQuery(20),
                    new MoveToPlayer(agent.GetAction("attack").range),
                    new Attack()
                 }),

                new Sequence(new BehaviorTree[] {
                    new MoveToZombie(20, 2),
                    new MaxEnemiesQuery(2, 10)
                })
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
