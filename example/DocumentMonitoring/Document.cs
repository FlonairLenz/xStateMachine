using System;
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
            if (this._stateMachine.IsTransactionAllowedTo(DocumentState.Changed))
            {
                this.Text = newText;
                this._stateMachine.ChangeState(DocumentState.Changed);
            }
        }

        public void Remove()
        {
            var removeDocumentState = IsImportant() ? DocumentState.Archived : DocumentState.Deleted;
            this._stateMachine.ChangeState(removeDocumentState);
        }

        public void Archive()
        {
            if (this._stateMachine.IsTransactionAllowedTo(DocumentState.Archived))
            {
                // Do something
                this._stateMachine.ChangeState(DocumentState.Archived);
            }
        }

        public void Delete()
        {
            // Do something
            this._stateMachine.ChangeState(DocumentState.Deleted);
        }
        
        public void Publish()
        {
            if (this._stateMachine.IsTransactionAllowedTo(DocumentState.Published))
            {
                // Do something to publish
                this._stateMachine.ChangeState(DocumentState.Published);
            }
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