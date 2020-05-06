using System;

namespace xStateMachine.Exceptions
{
    public class TransactionNotFoundException : Exception
    {
        public TransactionNotFoundException() : base("The current state has no transactions")
        {
            
        }
    }
}