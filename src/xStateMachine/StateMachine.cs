using System.Collections.Generic;
using xStateMachine.Exceptions;

namespace xStateMachine
{
    public class StateMachine<T>
    {
        #region properties

        public T CurrentState { get; private set; }

        private HashSet<T> CurrentTrasactions
        {
            get => this._transitions[this.CurrentState] ?? throw new TransitionNotFoundException();
        }

        private readonly Dictionary<T, HashSet<T>> _transitions;
        
        #endregion

        #region ctor
        
        public StateMachine(T initialState)
        {
            this._transitions = new Dictionary<T, HashSet<T>>();
            this._transitions.Add(initialState, new HashSet<T>());
            this.CurrentState = initialState;
        }

        public StateMachine(Dictionary<T, HashSet<T>> transitions, T initialState)
        {
            this._transitions = transitions;
            this.CurrentState = initialState;
        }

        #endregion
        
        #region methods (public)

        /// <summary>
        /// Add a transition to the state machine
        /// </summary>
        /// <param name="from">State where the transition starts</param>
        /// <param name="to">State where the transition ends</param>
        public void AddTransition(T from, T to)
        {
            if (!this._transitions.ContainsKey(from))
            {
                this._transitions.Add(from, new HashSet<T>() { to });
            }
            else
            {
                this._transitions[from].Add(to);
            }
        }
        
        /// <summary>
        /// Add a set of transitions to the state machine
        /// </summary>
        /// <param name="from">State where the transition starts</param>
        /// <param name="to">State where the transition ends</param>
        public void AddTransition(T from, HashSet<T> to)
        {
            if (!this._transitions.ContainsKey(from))
            {
                this._transitions.Add(from, to);
            }
            else
            {
                this._transitions[from].UnionWith(to);
            }
        }

        /// <summary>
        /// Change the current state to a new one
        /// </summary>
        /// <param name="to">State where the transition ends</param>
        /// <returns>The current state after the transition ends</returns>
        public T ChangeState(T to)
        {
            if (this.CurrentState.Equals(to)) 
            {
                if (!IsTransitionAllowedTo(to))
                {
                    throw new InvalidTransitionException(this.CurrentState.ToString(), to.ToString());
                }

                this.CurrentState = to;
            }
            return this.CurrentState;
        }

        /// <summary>
        /// Check if the transition from the current state to a new state is valid
        /// </summary>
        /// <param name="to">State where the transition ends</param>
        /// <returns>Whether the transition is valid</returns>
        public bool IsTransitionAllowedTo(T to)
        {
            return this.CurrentTrasactions.Contains(to);
        }

        /// <summary>
        /// Check if the current state has any transitions
        /// </summary>
        /// <returns>Whether the current state has transitions</returns>
        public bool HasTransitions()
        {
            return this._transitions.ContainsKey(this.CurrentState);
        }
        
        #endregion
    }
}
