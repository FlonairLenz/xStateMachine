using xStateMachine;

namespace DocumentMonitoring
{
    public class Document
    {
        private readonly StateMachine<DocumentState> _stateMachine;
        private readonly bool _isImportant;
        
        public string Text { get; private set; }

        public DocumentState State
        {
            get => this._stateMachine.CurrentState;
        }
        
        public Document(bool isImportant)
        {
            this._stateMachine = StateMachineBuilder.Create(DocumentState.Created);
            this._stateMachine.AddTransaction(DocumentState.Created, DocumentState.Published);
            this._stateMachine.AddTransaction(DocumentState.Created, DocumentState.Deleted);
            this._stateMachine.AddTransaction(DocumentState.Created, DocumentState.Archived);
            this._stateMachine.AddTransaction(DocumentState.Changed, DocumentState.Published);
            this._stateMachine.AddTransaction(DocumentState.Changed, DocumentState.Deleted);
            this._stateMachine.AddTransaction(DocumentState.Changed, DocumentState.Archived);
            this._stateMachine.AddTransaction(DocumentState.Published, DocumentState.Changed);
            this._stateMachine.AddTransaction(DocumentState.Published, DocumentState.Deleted);
            this._stateMachine.AddTransaction(DocumentState.Published, DocumentState.Archived);
            this._stateMachine.AddTransaction(DocumentState.Archived, DocumentState.Changed);
            this._stateMachine.AddTransaction(DocumentState.Archived, DocumentState.Published);
            this._isImportant = isImportant;
        }

        public void Change(string newText)
        {
            this.Text = newText;
            this._stateMachine.ChangeState(DocumentState.Changed);
        }

        public void Archive()
        {
            this._stateMachine.ChangeState(DocumentState.Archived);
        }

        public void Delete()
        {
            this._stateMachine.ChangeState(DocumentState.Deleted);
        }
        
        public void Publish()
        {
            this._stateMachine.ChangeState(DocumentState.Published);
        }

        public bool IsImportant() => _isImportant;
    }

    public enum DocumentState
    {
        Created,
        Changed,
        Published,
        Deleted,
        Archived
    } 
}