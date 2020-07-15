using Computers.Domains;
using Moq;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Computers.Tests.Domains
{
    [TestFixture]
    public class NonTerminalNodeTests
    {
        private const string NameValue1 = "Some Name";
        private const string ImageValue1 = "Image URL";

        [Test]
        public void InitialisesWithKeyValuePairs()
        {
            //TODO : duplication potentielle d'un cas (v. TerminalNodeTests).
            var nonTerminalNode = new NonTerminalNode()
            {
                {NodeKeys.Name, NameValue1 },
                {NodeKeys.ImageUrl, ImageValue1 }
            };

            Assert.That(nonTerminalNode[NodeKeys.Name], Is.EqualTo(NameValue1));
            Assert.That(nonTerminalNode[NodeKeys.ImageUrl], Is.EqualTo(ImageValue1));
            Assert.That(() => nonTerminalNode[NodeKeys.Description], Throws.InstanceOf<KeyNotFoundException>());
        }

        [Test]
        public void ProvidesWithKeys()
        { 
            //TODO : duplication potentielle d'un cas (v. TerminalNodeTests).
            var nonTerminalNode = new NonTerminalNode()
            {
                {NodeKeys.Name, NameValue1 },
                {NodeKeys.ImageUrl, ImageValue1 },
                {"A Dummy Key", "A Dummy Value" }
            };

            Assert.That(nonTerminalNode.Keys, Is.EquivalentTo(new HashSet<string>() {NodeKeys.Name, NodeKeys.ImageUrl, "A Dummy Key"}));
        }

        [Test]
        public void AcceptsChildren()
        {
            var nonTerminalNode = new NonTerminalNode()
            {
                {NodeKeys.Name, NameValue1 },
                {NodeKeys.ImageUrl, ImageValue1 },
                //Children initialisation part
                new Mock<ITreeNode>().Object,
                new Mock<ITreeNode>().Object,
                new Mock<ITreeNode>().Object
            };

            Assert.That(nonTerminalNode.HasChildren, Is.True);
        }

        [Test]
        public void EnumeratesChildren()
        {
            NodeMockBuilder builder = new NodeMockBuilder();
            List<ITreeNode> children = new List<ITreeNode>
            {
                builder.New("c1").Result,
                builder.New("c2").Result,
                builder.New("c3").Result,
                builder.New("c4").Result
            };
            ITreeNode nonTerminalNode = new NonTerminalNode()
            {
                {NodeKeys.Name, NameValue1 },
                {NodeKeys.ImageUrl, ImageValue1 },
                //Children initialisation part
                children[0],
                children[1],
                children[2],
                children[3]
            };
            //L'assertion se base sur les noms des noeuds enfants.
            //Si nous utilisons directement les IEnumerable, NUnit tente de comparer les noeuds enfants 
            //sur base de leurs propres enfants. Avec les Mocks, cela conduit à une NullReferenceException.
            Assert.That(
                nonTerminalNode.Select(n => n[NodeKeys.Name]), 
                Is.EquivalentTo(children.Select(n => n[NodeKeys.Name])));
        }

    }

    /// <summary>
    /// Construit des doublures de noeuds à base de mocks.
    /// </summary>
    class NodeMockBuilder
    {
        private Mock<ITreeNode> _product;

        internal NodeMockBuilder New(string name)
        {
            _product = new Mock<ITreeNode>();
            return WithProp(NodeKeys.Name, name);
        }

        internal NodeMockBuilder WithProp(string key, string value)
        {
            _product.Setup(n => n[key]).Returns(value);
            return this;
        }

        internal ITreeNode Result => _product.Object;
    }
}
