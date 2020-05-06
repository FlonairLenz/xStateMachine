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

`Current document state: Created`  
`Current document state: Published`  
`Current document state: Changed`  
`Current document state: Published`  
`Current document state: Archived`  
`The transaction from state Archived to state Deleted is not valid.`


## Acknowledgements
xStateMachine is built using the following open source and free projects:

- [XUnit](https://xunit.github.io/)
