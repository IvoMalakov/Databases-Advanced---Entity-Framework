namespace PhotoShare.Client.Core.Commands
{
    using Data.Interfaces;
    using Interfaces;
    using Attributes;

    public abstract class Command : IExecutable
    {
        [Inject]
        protected IUnitOfWork unit;

        private string[] data;

        protected Command(string[] data)
        {
            this.Data = data;
        }

        protected string[] Data
        {
            get { return this.data; }
            private set { this.data = value; }
        }

        public void CommitChanges()
        {
            this.unit.Commit();
        }

        public abstract string Execute();
             
    }
}
