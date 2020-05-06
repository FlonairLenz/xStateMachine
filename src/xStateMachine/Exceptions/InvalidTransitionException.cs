using System;

namespace xStateMachine.Exceptions
{
    public class InvalidTransitionException : Exception
    {
        public InvalidTransitionException(string fromState, string toState) : base($"The transition from state { fromState } to state { toState } is not valid.")
        {
        }
    }
}
