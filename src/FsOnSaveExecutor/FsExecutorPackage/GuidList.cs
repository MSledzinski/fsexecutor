// Guids.cs
// MUST match guids.h
using System;

namespace NooNe.FsExecutorPackage
{
    static class GuidList
    {
        public const string guidFsExecutorPackagePkgString = "87684728-b0a1-4749-a947-58014b848499";
        public const string guidFsExecutorPackageCmdSetString = "f9a6b7fa-610a-4aee-af56-6c606d8226b5";

        public static readonly Guid guidFsExecutorPackageCmdSet = new Guid(guidFsExecutorPackageCmdSetString);
    };
}