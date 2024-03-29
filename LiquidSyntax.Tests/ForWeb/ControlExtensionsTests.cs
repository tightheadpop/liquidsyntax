using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LiquidSyntax.ForTesting;
using LiquidSyntax.ForWeb;
using NUnit.Framework;

namespace LiquidSyntax.Tests.ForWeb {
    [TestFixture]
    public class ControlExtensionsTests {
        private Panel rootPanel;
        private Panel firstChildPanel;
        private PanelNamingContainer secondChildPanelNamingContainer;
        private Label grandChildLabel;
        private Panel grandChildPanel;

        [SetUp]
        public void SetUp() {
            rootPanel = new Panel {ID = "parent"};

            firstChildPanel = new Panel {ID = "first"};
            rootPanel.Controls.Add(firstChildPanel);

            secondChildPanelNamingContainer = new PanelNamingContainer {ID = "second"};
            rootPanel.Controls.Add(secondChildPanelNamingContainer);

            grandChildLabel = new Label {ID = "parent"};
            secondChildPanelNamingContainer.Controls.Add(grandChildLabel);

            grandChildPanel = new Panel {ID = "grandpanel"};
            secondChildPanelNamingContainer.Controls.Add(grandChildPanel);
        }

        [Test]
        public void FindAncestorShouldNavigateObjectGraphRecursivelyToFindTheFirstAncestorOfAGivenType() {
            var anchor = new HtmlAnchor();
            grandChildPanel.Controls.Add(anchor);
            anchor.FindAncestor<PanelNamingContainer>().Should(Be.SameAs(secondChildPanelNamingContainer));
        }

        [Test]
        public void FindAncestorShouldReturnNullIfNoSuchParentExists() {
            grandChildPanel.FindAncestor<GridView>().Should(Be.Null);
        }

        [Test]
        public void FindAllShouldSearchRecursively() {
            var labels = rootPanel.FindAll<Label>();
            labels.Should(Be.EqualTo(new[] {grandChildLabel}));
        }

        [Test]
        public void FindShouldReturnEmptyEnumerableIfNoControlIsFound() {
            rootPanel.FindAll<TextBox>().Should(Be.Empty);
        }

        [Test]
        public void FindFirstShouldFindFirstInstanceOfTypeInControlTree() {
            rootPanel.FindFirst<Panel>().Should(Be.EqualTo(firstChildPanel));
        }

        [Test]
        public void FindFirstShouldReturnNullLookingForNonExistentTypeInControlTree() {
            rootPanel.FindFirst<Literal>().Should(Be.Null);
        }

        [Test]
        public void FindShouldHonorInterfaces() {
            var namingContainers = rootPanel.FindAll<INamingContainer>();
            namingContainers.Should(Be.EqualTo(new[] {secondChildPanelNamingContainer}));
        }

        [Test]
        public void ShouldFindAllDescendantsOfASpecificTypeInControlTree() {
            var panels = rootPanel.FindAll<Panel>();
            panels.Should(Be.EquivalentTo(new[] {firstChildPanel, secondChildPanelNamingContainer, grandChildPanel}));
        }

        [Test]
        public void ShouldIncludeControlsByPredicate() {
            var panels = rootPanel.FindAll<Panel>(c => !(c is INamingContainer));
            panels.Should(Be.EquivalentTo(new[] {firstChildPanel, grandChildPanel}));
        }

        [Test]
        public void FindFirstWhereShouldIgnoreParentControl() {
            var result = rootPanel.FindFirst<Control>(c => c.ID == "parent");
            result.Should(Be.SameAs(grandChildLabel));
        }

        [Test]
        public void FindFirstWhereShouldReturnNullIfNoDescendantExistsThatSatisfiesCondition() {
            rootPanel.FindFirst<Control>(c => c.ID == "not gonna find it").Should(Be.Null);
        }

        [Test]
        public void FindByTypeStopsRecursing() {
            var result = rootPanel.FindAllUntil<Panel>(c => c is INamingContainer);
            result.Should(Be.EqualTo(new[] {firstChildPanel}));
        }

        [Test]
        public void GetHtmlFromControl() {
            var thing = new Label {Text = "thing"};
            var result = thing.GetHtml();
            Assert.AreEqual("<span>thing</span>", result, "Html output does not match");
        }

        private class PanelNamingContainer : Panel, INamingContainer {}
    }
}