﻿namespace Marols.FsExecutorPackage.Modificators
{
    internal interface IDocumentModificator
    {
        void Modify(VsDocumentWrapper document, string text);
    }
}