namespace NooNe.FsExecutorPackage
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    using EnvDTE;

    using EnvDTE80;

    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;

    using NooNe.FsExecutorPackage.Modificators;

    using IServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [Guid(GuidList.guidFsExecutorPackagePkgString)]
    [ProvideAutoLoad(UIContextGuids80.NoSolution)]
    public sealed class FsExecutorPackagePackage : Package
    {
        private const string PluginFriendlyName = "FS code executor";

        private readonly Lazy<DTE2> currentDte;

        private readonly Lazy<RunningDocumentTable> runningDocumentsTable;

        private readonly Lazy<FsPluginOptions> pluginOptions;

        private readonly Lazy<IServiceProvider> serviceProvider;

        private readonly Lazy<ExecuteFsCodeOnSaveHook> executeCodeHook;

        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public FsExecutorPackagePackage()
        {
            Debug.WriteLine("Entering constructor for: {0}", this.ToString());

            currentDte = new Lazy<DTE2>(() => ServiceProvider.GlobalProvider.GetService(typeof(DTE)) as DTE2); ;
            serviceProvider = new Lazy<IServiceProvider>(
                                            () => GetGlobalService(
                                                    typeof(IServiceProvider)) 
                                                        as IServiceProvider);

            runningDocumentsTable = new Lazy<RunningDocumentTable>(() => new RunningDocumentTable(new ServiceProvider(serviceProvider.Value)));

            pluginOptions = new Lazy<FsPluginOptions>(() => GetDialogPage(typeof(FsPluginOptions)) as FsPluginOptions, true);

            executeCodeHook = new Lazy<ExecuteFsCodeOnSaveHook>(CreateHookObject);
        }

        protected override void Initialize()
        {
            Debug.WriteLine ("Entering Initialize() of: {0}", this.ToString());

            if (pluginOptions.Value.Enabled)
            {
                runningDocumentsTable.Value.Advise(executeCodeHook.Value);
            }

            base.Initialize();
        }

        public override string ToString()
        {
            return PluginFriendlyName;
        }

        private ExecuteFsCodeOnSaveHook CreateHookObject()
        {
            // TODO: inject, inject, inject
            var documentModifier = new FsAppendResultToDocument();
            var codeExecutor = new FsCodeExecutor(documentModifier);

            return new ExecuteFsCodeOnSaveHook(runningDocumentsTable.Value, currentDte.Value, codeExecutor);
        }
    }
}
