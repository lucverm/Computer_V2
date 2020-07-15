using Computers.Domains;
using Moq;
using NUnit.Framework;
using System;

namespace Computers.Tests.Domains
{
    [TestFixture]
    public class NodesRunTests
    {
        [Test]
        public void InitialisesTheCurrentNodeWithRoot() 
        {
            var root = new Mock<ITreeNode>();
            var run = new StackedNodesRun(root.Object);

            Assert.That(run.Current, Is.SameAs(root.Object));
        }

        [Test]
        public void UpdatesNodeOnGoTo() 
        {
            var root = new Mock<ITreeNode>();
            var next = new Mock<ITreeNode>();
            var run = new StackedNodesRun(root.Object);    

            run.GoTo(next.Object);

            Assert.That(run.Current, Is.SameAs(next.Object));
        }

        [Test]
        public void NotifiesOnGoTo() 
        {
            var root = new Mock<ITreeNode>();
            var next = new Mock<ITreeNode>();
            var observerMock = new Mock<Action<object, ITreeNode>>();
            var run = new StackedNodesRun(root.Object);    
            run.NodeChanged += observerMock.Object;

            run.GoTo(next.Object);

            observerMock.Verify(observer => observer(run, It.IsAny<ITreeNode>()), Times.Once);
        }

        [Test]
        public void NotifiesOnGoBack() 
        {
            var root = new Mock<ITreeNode>();
            var next = new Mock<ITreeNode>();
            var observerMock = new Mock<Action<object, ITreeNode>>();
            var run = new StackedNodesRun(root.Object);    
            run.NodeChanged += observerMock.Object;

            run.GoTo(next.Object);
            run.GoBack();

            observerMock.Verify(observer => observer(run, It.IsAny<ITreeNode>()), Times.Exactly(2));
        }

        [Test]
        public void IgnoresNotificationsOnGoBackOnRoot() 
        {
            var root = new Mock<ITreeNode>();
            var next = new Mock<ITreeNode>();
            var observerMock = new Mock<Action<object, ITreeNode>>();
            var run = new StackedNodesRun(root.Object);    

            run.GoTo(next.Object);
            run.GoBack();
              
            run.NodeChanged += observerMock.Object;
            run.GoBack();

            observerMock.Verify(observer => observer(run, It.IsAny<ITreeNode>()), Times.Never);
        }

        [Test]
        public void IgnoresGoBackOnRoot() 
        {
            var root = new Mock<ITreeNode>();
            var next = new Mock<ITreeNode>();
            var run = new StackedNodesRun(root.Object);    

            run.GoTo(next.Object);
            run.GoBack();
            run.GoBack();

            Assert.That(run.Current, Is.SameAs(root.Object));
        }
    }
}
