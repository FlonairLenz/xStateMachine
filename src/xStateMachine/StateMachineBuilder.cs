using System.Collections.Generic;

namespace xStateMachine
{
    public static class StateMachineBuilder
    {
        public static StateMachine<T> Create<T>(T initialState) => new StateMachine<T>(initialState);
        public static StateMachine<T> Create<T>(Dictionary<T, HashSet<T>> transitions, T initialState) => new StateMachine<T>(transitions, initialState);
    }
}