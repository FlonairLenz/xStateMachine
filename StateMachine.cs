using System.Collections.Generic;
using TStateMachine.Exceptions;

namespace TStateMachine
{
    public class StateMachine<T>
    {
        #region properties
        
        public T CurrentState { get; private set; }

        private HashSet<T> CurrentTrasactions
        {
            get => this._transactions[this.CurrentState];
        }

        private readonly Dictionary<T, HashSet<T>> _transactions;
        
        #endregion

        #region ctor

        public StateMachine(T initialState)
        {
            this.CurrentState = initialState;
            this._transactions = new Dictionary<T, HashSet<T>>();
        }

        public StateMachine(Dictionary<T, HashSet<T>> transactions, T initialState)
        {
            this.CurrentState = initialState;
            this._transactions = transactions;
        }

        #endregion
        
        #region methods (public)

        public void AddTransaction(T from, T to)
        {
            if (!this._transactions.ContainsKey(from))
            {
                this._transactions.Add(from, new HashSet<T>() { to });
            }
            else
            {
                this._transactions[from].Add(to);
            }
        }
        
        public void AddTransaction(T from, HashSet<T> to)
        {
            if (!this._transactions.ContainsKey(from))
            {
                this._transactions.Add(from, to);
            }
            else
            {
                this._transactions[from].UnionWith(to);
            }
        }

        public T ChangeState(T to)
        {
            if (!IsTransactionAllowedTo(to))
            {
                throw new InvalidTransactionException(this.CurrentState.ToString(), to.ToString());
            }

            this.CurrentState = to;
            return this.CurrentState;
        }

        public bool IsTransactionAllowedTo(T to)
        {
            return this.CurrentTrasactions.Contains(to);
        }
        
        #endregion
    }
}