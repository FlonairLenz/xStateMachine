using xStateMachine.Exceptions;
using Xunit;

namespace xStateMachine.Tests
{
    public class StateMachineTests
    {
        [Fact]
        public void Ctor_SuccessfullyCreateStateMachine_CurrentStateIsEqualToInitalState()
        {
            // Arrange
            var stateMachine = StateMachineBuilder.Create(State.Start);

            // Act    
            var currentState = stateMachine.CurrentState;

            // Assert
            Assert.Equal(State.Start, currentState);
        }
        
        [Fact]
        public void IsTransitionAllowedTo_ValidTransition_ReturnTrue()
        {
            // Arrange
            var stateMachine = StateMachineBuilder.Create(State.Start);
            stateMachine.AddTransition(State.Start, State.Working);
            
            // Act
            var transitionAllowed = stateMachine.IsTransitionAllowedTo(State.Working);

            // Assert
            Assert.True(transitionAllowed);
        }
        
        [Fact]
        public void IsTransitionAllowedTo_InvalidTransition_ReturnFalse()
        {
            // Arrange
            var stateMachine = StateMachineBuilder.Create(State.Start);
            
            // Act
            var transitionAllowed = stateMachine.IsTransitionAllowedTo(State.Working);

            // Assert
            Assert.False(transitionAllowed);
        }
        
        [Fact]
        public void ChangeState_ValidTransition_ReturnNewState()
        {
            // Arrange
            var stateMachine = StateMachineBuilder.Create(State.Start);
            stateMachine.AddTransition(State.Start, State.Working);
            
            // Act
            var newState = stateMachine.ChangeState(State.Working);

            // Assert
            Assert.Equal(State.Working, newState);
        }
        
        [Fact]
        public void ChangeState_InvalidTransition_ThrowInvalidTransitionException()
        {
            // Arrange
            var stateMachine = StateMachineBuilder.Create(State.Start);
            stateMachine.AddTransition(State.Start, State.Working);
            stateMachine.AddTransition(State.Working, State.Deleted);
            
            
            // Act, Assert
            Assert.Throws<InvalidTransitionException>(() => stateMachine.ChangeState(State.Deleted));
        }
        
        [Fact]
        public void ChangeState_CurrentStateEqualsNewState_ReturnCurrentState()
        {
            // Arrange
            var stateMachine = StateMachineBuilder.Create(State.Start);
            stateMachine.AddTransition(State.Start, State.Working);

            // Act
            var newState = stateMachine.ChangeState(State.Start);

            // Assert
            Assert.Equal(State.Start, newState);
        }
    }

    enum State
    {
        Start,
        Working,
        Deleted,
    }
}