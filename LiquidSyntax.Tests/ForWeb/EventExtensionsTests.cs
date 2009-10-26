using System.Web.UI.WebControls;
using NUnit.Framework;
using LiquidSyntax.ForWeb;

namespace LiquidSyntax.Tests.ForWeb {
    [TestFixture]
    public class EventExtensionsTests {
        [Test]
        public void GridViewCommandEventArgs() {
            var row = new GridViewRow(5, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            var eventArgs = new GridViewCommandEventArgs(row, "commandSource", new CommandEventArgs("commandName", null));

            Assert.AreSame(row, eventArgs.Row());
            Assert.AreEqual(5, eventArgs.RowIndex());
        }
    }
}