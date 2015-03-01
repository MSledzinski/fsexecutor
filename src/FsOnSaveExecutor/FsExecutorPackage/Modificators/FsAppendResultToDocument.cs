namespace NooNe.FsExecutorPackage.Modificators
{
    using System.Text;

    internal class FsAppendResultToDocument : IDocumentModificator
    {
        public void Modify(VsDocumentWrapper document, string text)
        {
            var documentText = ResultBlockTextHelper.RemoveAllExistingResultBlocks(document.TextContent);
            var contentToAppend = ResultBlockTextHelper.PrepareResultBlockText(text);

            var builder = new StringBuilder();
            builder.AppendLine(documentText);
            builder.AppendLine(contentToAppend);

            document.StartEditPoint.ReplaceText(document.EndEditPoint, builder.ToString(), 0);
        }
    }
}
