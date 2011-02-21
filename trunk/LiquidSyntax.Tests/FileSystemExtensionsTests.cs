using System.IO;
using NUnit.Framework;
using LiquidSyntax.ForTesting;

namespace LiquidSyntax.Tests {
    [TestFixture]
    public class FileSystemExtensionsTests {
        private DirectoryInfo tempFolder;
        private DirectoryInfo testFolder;

        [SetUp]
        public void SetUp() {
            tempFolder = new DirectoryInfo(Path.GetTempPath());
            testFolder = tempFolder.CreateSubdirectory("FileSystemExtensionsTests");
        }

        [TearDown]
        public void TearDown() {
            testFolder.Delete(true);
        }

        [Test]
        public void FindsSubdirectoryByName() {
            var actual = tempFolder.GetSubdirectory("FileSystemExtensionsTests");
            actual.FullName.Should(Be.EqualTo(testFolder.FullName));
        }

        [Test]
        public void ReturnsNullWhenSubdirectoryDoesNotExist() {
            var actual = tempFolder.GetSubdirectory("shouldntExist");
            actual.Should(Be.Null);
        }
    }
}