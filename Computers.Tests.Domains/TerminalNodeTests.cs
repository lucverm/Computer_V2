using Computers.Domains;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Computers.Tests.Domains
{
    [TestFixture]
    public class TerminalNodeTests
    {
        private const string NameValue1 = "Some Name";
        private const string ImageValue1 = "Image URL";

        [Test]
        public void InitialisesWithKeyValuePairs()
        {
            var tn = new TerminalNode()
            {
                {NodeKeys.Name, NameValue1 },
                {NodeKeys.ImageUrl, ImageValue1 }
            };

            Assert.That(tn[NodeKeys.Name], Is.EqualTo(NameValue1));
            Assert.That(tn[NodeKeys.ImageUrl], Is.EqualTo(ImageValue1));
            Assert.That(() => tn[NodeKeys.Description], Throws.InstanceOf<KeyNotFoundException>());
        }

        [Test]
        public void ProvidesWithKeys()
        {
            var tn = new TerminalNode()
            {
                {NodeKeys.Name, NameValue1 },
                {NodeKeys.ImageUrl, ImageValue1 },
                {"A Dummy Key", "A Dummy Value" }
            };

            Assert.That(tn.Keys, Is.EquivalentTo(new HashSet<string>() {NodeKeys.Name, NodeKeys.ImageUrl, "A Dummy Key"}));
        }

        [Test]
        public void RejectsCallsOnAdd()
        {
            var tn = new TerminalNode()
            {
                { NodeKeys.Name, NameValue1 }
            };

            Assert.That(() => tn.Add(new Mock<ITreeNode>().Object), Throws.InstanceOf<InvalidOperationException>());
        }

        [Test]
        public void HasNoChild()
        {
            var tn = new TerminalNode()
            {
                { NodeKeys.Name, NameValue1 }
            };

            Assert.That(tn.HasChildren, Is.False);
        }

        [Test]
        public void EnumeratesOnEmptyChildrenCollection() 
        {
            var tn = new TerminalNode()
            {
                { NodeKeys.Name, NameValue1 }
            };
            

            Assert.That(tn.Count(), Is.EqualTo(0));
        }
    }
}
