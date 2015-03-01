namespace NooNe.FsExecutorPackage
{
    using System.Linq;
    using System.Runtime.InteropServices;

    using EnvDTE;

    using EnvDTE80;

    using Microsoft.VisualStudio;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;

    internal class ExecuteFsCodeOnSaveHook : IVsRunningDocTableEvents3
    {
        private readonly RunningDocumentTable runningDocumentTable;

        private readonly DTE2 dte;

        private readonly FsCodeExecutor codeExecutor;

        public ExecuteFsCodeOnSaveHook(RunningDocumentTable runningDocumentTable, DTE2 dte, FsCodeExecutor codeExecutor)
        {
            this.runningDocumentTable = runningDocumentTable;
            this.dte = dte;
            this.codeExecutor = codeExecutor;
        }

        public int OnBeforeSave([ComAliasName("Microsoft.VisualStudio.Shell.Interop.VSCOOKIE")] uint docCookie)
        {
            var documentBeingSaved = FindDocumentBeingSaved(docCookie);

            if (documentBeingSaved == null ||
                !FsDocumentRecognizer.Check(documentBeingSaved))
            {
                // not interesed in other languages (at least not today :) )
                return VSConstants.S_OK;
            }

            codeExecutor.Do(documentBeingSaved);

            return VSConstants.S_OK;
        }

        #region Interface implementation noise

        public int OnAfterAttributeChange(
            [ComAliasName("Microsoft.VisualStudio.Shell.Interop.VSCOOKIE")] uint docCookie,
            [ComAliasName("Microsoft.VisualStudio.Shell.Interop.VSRDTATTRIB")] uint grfAttribs)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterAttributeChangeEx(
            [ComAliasName("Microsoft.VisualStudio.Shell.Interop.VSCOOKIE")] uint docCookie,
            [ComAliasName("Microsoft.VisualStudio.Shell.Interop.VSRDTATTRIB")] uint grfAttribs,
            IVsHierarchy pHierOld,
            [ComAliasName("Microsoft.VisualStudio.Shell.Interop.VSITEMID")] uint itemidOld,
            [ComAliasName("Microsoft.VisualStudio.OLE.Interop.LPCOLESTR")] string pszMkDocumentOld,
            IVsHierarchy pHierNew,
            [ComAliasName("Microsoft.VisualStudio.Shell.Interop.VSITEMID")] uint itemidNew,
            [ComAliasName("Microsoft.VisualStudio.OLE.Interop.LPCOLESTR")] string pszMkDocumentNew)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterDocumentWindowHide(
            [ComAliasName("Microsoft.VisualStudio.Shell.Interop.VSCOOKIE")] uint docCookie,
            IVsWindowFrame pFrame)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterFirstDocumentLock(
            [ComAliasName("Microsoft.VisualStudio.Shell.Interop.VSCOOKIE")] uint docCookie,
            [ComAliasName("Microsoft.VisualStudio.Shell.Interop.VSRDTFLAGS")] uint dwRDTLockType,
            [ComAliasName("Microsoft.VisualStudio.OLE.Interop.DWORD")] uint dwReadLocksRemaining,
            [ComAliasName("Microsoft.VisualStudio.OLE.Interop.DWORD")] uint dwEditLocksRemaining)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterSave([ComAliasName("Microsoft.VisualStudio.Shell.Interop.VSCOOKIE")] uint docCookie)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeDocumentWindowShow(
            [ComAliasName("Microsoft.VisualStudio.Shell.Interop.VSCOOKIE")] uint docCookie,
            [ComAliasName("Microsoft.VisualStudio.OLE.Interop.BOOL")] int fFirstShow,
            IVsWindowFrame pFrame)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeLastDocumentUnlock(
            [ComAliasName("Microsoft.VisualStudio.Shell.Interop.VSCOOKIE")] uint docCookie,
            [ComAliasName("Microsoft.VisualStudio.Shell.Interop.VSRDTFLAGS")] uint dwRDTLockType,
            [ComAliasName("Microsoft.VisualStudio.OLE.Interop.DWORD")] uint dwReadLocksRemaining,
            [ComAliasName("Microsoft.VisualStudio.OLE.Interop.DWORD")] uint dwEditLocksRemaining)
        {
            return VSConstants.S_OK;
        }

        #endregion

        private VsDocumentWrapper FindDocumentBeingSaved(uint docCookie)
        {
            var documentInfo = runningDocumentTable.GetDocumentInfo(docCookie);
            var documentFileFullName = documentInfo.Moniker;

            var document = dte.Documents.Cast<Document>().FirstOrDefault(d => d.FullName == documentFileFullName);

            return document == null ? VsDocumentWrapper.CreateEmpty() : VsDocumentWrapper.Create(document);
        }
    }
}
