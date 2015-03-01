namespace NooNe.FsExecutorPackage.Modificators
{
    using System;
    using System.Text.RegularExpressions;

    // TODO: change to 'internal' when ready
    public static class ResultBlockTextHelper
    {
        private const string ResultBlockText = @"(** RESULT: {0} {1} {2} **)";

        // TODO: shorter regex please :)
        private static readonly Lazy<Regex> resultMatcher = 
                new Lazy<Regex>(() => new Regex(@"\(\*\* RESULT: ([\w\s\r\n\b<>=\(\)\:])*\*\*\)", RegexOptions.Compiled), true);

        public static string RemoveAllExistingResultBlocks(string textContent)
        {
            return resultMatcher.Value.Replace(textContent, string.Empty);
        }

        public static string PrepareResultBlockText(string text)
        {
            return string.Format(ResultBlockText, Environment.NewLine, text, Environment.NewLine);
        }
    }
}
