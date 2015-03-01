namespace Tests
{
    using NooNe.FsExecutorPackage.Modificators;

    using NUnit.Framework;

    [TestFixture]
    public class ResultBlockTests
    {
        [Test]
        public void ShouldFindPreviouslyCreatedResultBlock()
        {
            // given
            var preText = "SOME text before result block \r\n";
            var text = preText + ResultBlockTextHelper.PrepareResultBlockText("> let a = 4 \r\n a = 4 \r\n");

            // when
            var actual = ResultBlockTextHelper.RemoveAllExistingResultBlocks(text);

            // then
            Assert.That(actual, Is.EqualTo(preText));
        }
    }
}
