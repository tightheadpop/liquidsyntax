using System.Web.UI;
using System.Web.UI.WebControls;
using LiquidSyntax.ForTesting;
using LiquidSyntax.ForWeb;
using NUnit.Framework;

namespace LiquidSyntax.Tests.ForWeb {
    [TestFixture]
    public class ControlExtensionsTests {
        private Panel firstChildPanel;
        private Label grandChildLabel;
        private Panel rootPanel;
        private PanelNamingContainer secondChildPanelNamingContainer;

        [SetUp]
        public void SetUp() {
            rootPanel = new Panel {ID = "parent"};

            firstChildPanel = new Panel {ID = "first"};
            rootPanel.Controls.Add(firstChildPanel);

            secondChildPanelNamingContainer = new PanelNamingContainer {ID = "second"};
            rootPanel.Controls.Add(secondChildPanelNamingContainer);

            grandChildLabel = new Label {ID = "grandchild"};
            secondChildPanelNamingContainer.Controls.Add(grandChildLabel);
        }

        [Test]
        public void FindShouldSearchRecursively() {
            var labels = rootPanel.FindAll<Label>();
            labels.Should(Be.EqualTo(new[] {grandChildLabel}));
        }

        [Test]
        public void FindShouldReturnNullIfNoControlIsFound() {
            rootPanel.FindAll<TextBox>().Should(Be.Empty);
        }

        [Test]
        public void FindShouldHonorInterfaces() {
            var namingContainers = rootPanel.FindAll<INamingContainer>();
            namingContainers.Should(Be.EqualTo(new[] {secondChildPanelNamingContainer}));
        }

        [Test]
        public void ShouldFindByType() {
            var panels = rootPanel.FindAll<Panel>();
            panels.Should(Be.EquivalentTo(new[] {rootPanel, firstChildPanel, secondChildPanelNamingContainer}));
        }

        [Test]
        public void ShouldIncludeControlsByPredicate() {
            var panels = rootPanel.FindAll<Panel>(c => !(c is INamingContainer));
            panels.Should(Be.EquivalentTo(new[] {rootPanel, firstChildPanel}));
        }

        private class PanelNamingContainer : Panel, INamingContainer {}
    }
}