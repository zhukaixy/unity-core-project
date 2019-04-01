﻿namespace Core.AI
{
    public class TerminateBlueprint : NodeBlueprint
    {      
        public override IEntityData GetInputValue()
        {
            return null;
        }
        
        public override IEntityData GetOutputValue()
        {
            return null;
        }

        public override Node CreateInstance(NodeBlueprint node)
        {
            throw new System.NotImplementedException();
        }
    }
}