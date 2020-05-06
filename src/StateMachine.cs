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

        /// <summary>
        /// Add a transaction to the state machine
        /// </summary>
        /// <param name="from">State where the transaction starts</param>
        /// <param name="to">State where the transaction ends</param>
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
        
        /// <summary>
        /// Add a set of transactions to the state machine
        /// </summary>
        /// <param name="from">State where the transaction starts</param>
        /// <param name="to">State where the transaction ends</param>
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

        /// <summary>
        /// Change the current state to a new one
        /// </summary>
        /// <param name="to">State where the transaction ends</param>
        /// <returns>The current state after the transaction ends</returns>
        public T ChangeState(T to)
        {
            if (!IsTransactionAllowedTo(to))
            {
                throw new InvalidTransactionException(this.CurrentState.ToString(), to.ToString());
            }

            this.CurrentState = to;
            return this.CurrentState;
        }

        /// <summary>
        /// Check if the transaction from the current state to a new state is valid
        /// </summary>
        /// <param name="to">State where the transaction ends</param>
        /// <returns>Whether the transaction is valid</returns>
        public bool IsTransactionAllowedTo(T to)
        {
            return this.CurrentTrasactions.Contains(to);
        }
        
        #endregion
    }
}