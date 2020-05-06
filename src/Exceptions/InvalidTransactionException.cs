using System;

namespace TStateMachine.Exceptions
{
    public class InvalidTransactionException : Exception
    {
        public InvalidTransactionException(string fromState, string toState) : base($"The transaction from state { fromState } to state { toState } is not valid.")
        {
        }
    }
}
