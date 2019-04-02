﻿using System.Collections.Generic;

namespace Core.AI
{
    public class RepeatBlueprint : BlockBlueprint
    {
        public override Node CreateNodeInstance(NodeBlueprint node)
        {
            return new Repeat();
        }

        public override Branch CreateBranchInstance(List<BranchBlueprint> nodes)
        {
            throw new System.NotImplementedException();
        }
    }
    
    public class Repeat : Block
    {
        public int count = 1;
        int currentCount = 0;

        // public Repeat(int count)
        // {
        //     this.count = count;
        // }

        public override BehaviorTreeState Tick()
        {
            if (count > 0 && currentCount < count)
            {
                var result = base.Tick();
                switch (result)
                {
                    case BehaviorTreeState.Continue:
                        return BehaviorTreeState.Continue;
                    default:
                        currentCount++;
                        if (currentCount == count)
                        {
                            currentCount = 0;
                            return BehaviorTreeState.Success;
                        }

                        return BehaviorTreeState.Continue;
                }
            }

            return BehaviorTreeState.Success;
        }
    }
}