using Computers.ViewModels.Impl;
using Moq;
using NUnit.Framework;
using System;

namespace Computers.Tests.ViewModels
{
    [TestFixture]
    public class BasicRelayCommandTests
    {
        [Test]
        public void ExecutesAlways() 
        {
            Mock<Action> action = new Mock<Action>();

            var command = new BasicRelayCommand(action.Object);

            Assert.True(command.CanExecute(null));
        }

        [Test]
        public void RejectsNullReceiver() 
        {
            Assert.Throws<ArgumentNullException>(() => new BasicRelayCommand(null));
        }

        [Test]
        public void DelegatesToReceiverOnExecute() 
        {
            Mock<Action> action = new Mock<Action>();
            var command = new BasicRelayCommand(action.Object);

            command.Execute(null);

            action.Verify(receiver => receiver(), Times.Once);
        }
    }
}
