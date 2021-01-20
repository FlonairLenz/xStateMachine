## About xStateMachine
xStateMachine is a free, open source state machine for .NET.

## Example

There is a document that can have the following states:

- Created
- Published
- Changed
- Deleted
- Archived

The following figure illustrates the state transactions:
![alt text](https://raw.githubusercontent.com/FlonairLenz/xStateMachine/master/example/ExampleStateMachine.png "Sample state machine")

We can see that a document cannot change from the state archived to the state deleted.

`Current document state: Created`  
`Current document state: Published`  
`Current document state: Changed`  
`Current document state: Published`  
`Current document state: Archived`  
`The transaction from state Archived to state Deleted is not valid.`

## Create state machine

``` csharp
this._stateMachine = StateMachineBuilder.Create(DocumentState.Created);
```

## Add transaction

``` csharp
this._stateMachine.AddTransition(DocumentState.Created, DocumentState.Published);
```

## Change state

``` csharp
public void Publish()
{
    if (this._stateMachine.IsTransitionAllowedTo(DocumentState.Published))
    {
        // Do something to publish
        this._stateMachine.ChangeState(DocumentState.Published);
    }
}
```

## Acknowledgements
xStateMachine is built using the following open source and free projects:

- [XUnit](https://xunit.github.io/)
