namespace NooNe.FsExecutorPackage
{
    using NooNe.FsExecutorPackage.Modificators;

    internal class FsCodeExecutor
    {
        private readonly IDocumentModificator documentModifier;

        public FsCodeExecutor(IDocumentModificator documentModifier)
        {
            this.documentModifier = documentModifier;
        }

        public void Do(VsDocumentWrapper document)
        {
            if (!document.HasAnyContent)
            {
                return;
            }

            var result = Execute(document.TextContent);

            if (!result.Success)
            {
                // TODO: a little bit of logging would be good
                // TODO: what to do with err msgs
                return;
            }

            documentModifier.Modify(document, result.Output);
        }

        private Executor.Result Execute(string code)
        {
            return Executor.TryExecute(code);
        }
    }
}
