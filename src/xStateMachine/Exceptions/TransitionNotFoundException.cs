using System;

namespace xStateMachine.Exceptions
{
    public class TransitionNotFoundException : Exception
    {
        public TransitionNotFoundException() : base("The current state has no transitions")
        {
            
        }
    }
}