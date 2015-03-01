namespace NooNe.FsExecutorPackage
{
    using System;

    using EnvDTE;

    internal class VsDocumentWrapper
    {
        private const string DocumentObjectType = "TextDocument";

        private readonly Document rawDocument;

        private readonly TextDocument textDocument;

        private readonly Lazy<EditPoint> startEditPoint;

        private readonly Lazy<EditPoint> endEditPoint;

        private readonly Lazy<string> textContent;

        internal VsDocumentWrapper(Document document)
        {
            if (document == null)
            {
                // TODO: better handling - factory method?
                return;
            }

            this.rawDocument = document;
            this.textDocument = document.Object(DocumentObjectType) as TextDocument;

            this.startEditPoint = new Lazy<EditPoint>(() => textDocument.StartPoint.CreateEditPoint());
            this.endEditPoint = new Lazy<EditPoint>(() => textDocument.EndPoint.CreateEditPoint());
            this.textContent = new Lazy<string>(() => StartEditPoint.GetText(EndEditPoint));
        }

        public static VsDocumentWrapper Create(Document document)
        {
            return new VsDocumentWrapper(document);
        }

        public static VsDocumentWrapper CreateEmpty()
        {
            return new VsDocumentWrapper(null);
        }

        public bool IsEmpty
        {
            get
            {
                return rawDocument == null;
            }
        }

        public Document RawDocument
        {
            get
            {
                return rawDocument;
            }
        }

        public string TextContent
        {
            get
            {
                return textContent.Value;
            }
        }

        public bool HasAnyContent
        {
            get
            {
                return !string.IsNullOrWhiteSpace(textContent.Value);
            }
        }

        public EditPoint StartEditPoint
        {
            get
            {
                return startEditPoint.Value;
            }
        }

        public EditPoint EndEditPoint
        {
            get
            {
                return endEditPoint.Value;
            }
        }
    }
}
