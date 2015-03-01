namespace NooNe.FsExecutorPackage
{
    using EnvDTE;

    internal static class FsDocumentRecognizer
    {
        private const string FsLangaugeName = "F#";

        public static bool Check(VsDocumentWrapper document)
        {
            return CheckFileExtension(document.RawDocument.FullName) && CheckLanguage(document.RawDocument.Language);
        }

        private static bool CheckLanguage(string language)
        {
            return language == FsLangaugeName;
        }

        private static bool CheckFileExtension(string fullFileName)
        {
            if (!System.IO.Path.HasExtension(fullFileName))
            {
                return false;
            }

            var loweredExtension = System.IO.Path.GetExtension(fullFileName).ToLowerInvariant();

            return loweredExtension == ".fsx" || loweredExtension == ".fs";
        }
    }
}
