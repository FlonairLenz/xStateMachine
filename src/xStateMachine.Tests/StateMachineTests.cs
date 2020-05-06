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
        public void IsTransactionAllowedTo_ValidTransaction_ReturnTrue()
        {
            // Arrange
            var stateMachine = StateMachineBuilder.Create(State.Start);
            stateMachine.AddTransaction(State.Start, State.Working);
            
            // Act
            var transactionAllowed = stateMachine.IsTransactionAllowedTo(State.Working);

            // Assert
            Assert.True(transactionAllowed);
        }
        
        [Fact]
        public void IsTransactionAllowedTo_InvalidTransaction_ReturnFalse()
        {
            // Arrange
            var stateMachine = StateMachineBuilder.Create(State.Start);
            
            // Act
            var transactionAllowed = stateMachine.IsTransactionAllowedTo(State.Working);

            // Assert
            Assert.False(transactionAllowed);
        }
        
        [Fact]
        public void ChangeState_ValidTransaction_ReturnNewState()
        {
            // Arrange
            var stateMachine = StateMachineBuilder.Create(State.Start);
            stateMachine.AddTransaction(State.Start, State.Working);
            
            // Act
            var newState = stateMachine.ChangeState(State.Working);

            // Assert
            Assert.Equal(State.Working, newState);
        }
        
        [Fact]
        public void ChangeState_InvalidTransaction_ThrowInvalidTransactionException()
        {
            // Arrange
            var stateMachine = StateMachineBuilder.Create(State.Start);
            stateMachine.AddTransaction(State.Start, State.Working);
            stateMachine.AddTransaction(State.Working, State.Deleted);
            
            
            // Act, Assert
            Assert.Throws<InvalidTransactionException>(() => stateMachine.ChangeState(State.Deleted));
        }
        
        [Fact]
        public void ChangeState_CurrentStateEqualsNewState_ReturnCurrentState()
        {
            // Arrange
            var stateMachine = StateMachineBuilder.Create(State.Start);
            stateMachine.AddTransaction(State.Start, State.Working);

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