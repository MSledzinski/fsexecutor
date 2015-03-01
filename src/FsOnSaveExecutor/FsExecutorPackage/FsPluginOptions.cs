namespace NooNe.FsExecutorPackage
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    using Microsoft.VisualStudio.Shell;

    [ClassInterface(ClassInterfaceType.AutoDual)]
    [CLSCompliant(false)]
    [ComVisible(true)]
    public class FsPluginOptions : DialogPage
    {
        [Category("FSCE Plugin")]
        [DisplayName(@"Enabled")]
        [Description("When enabled, FS code will be executed on file save.")]
        public bool Enabled
        {
            get;
            set;
        }

        public override void LoadSettingsFromStorage()
        {
            Enabled = true;
            base.LoadSettingsFromStorage();
        }
    }
}