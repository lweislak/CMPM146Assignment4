using System.Collections.Generic;

public class Selector : InteriorNode
{
    public override Result Run()
    {
        Result res = children[current_child].Run();
        if (res == Result.SUCCESS)
        {
            return Result.SUCCESS;
        }
        if (res == Result.FAILURE)
        {
            current_child++;
        }
        if (current_child >= children.Count)
        {
            current_child = 0;
            return Result.FAILURE;
        }
        return Result.IN_PROGRESS;
    }

    public Selector(IEnumerable<BehaviorTree> children) : base(children)
    {

    }

    public override BehaviorTree Copy()
    {
        return new Selector(CopyChildren());
    }

}
