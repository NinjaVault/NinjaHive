using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;

namespace NinjaHive.WebApp.Tests
{
    [TestClass]
    public class WebAppBootstrapperTests
    {
        [TestMethod]
        public void GetInitializedContainer_Always_ReturnsAContainerThatCanBeVerified()
        {
            //// Arrange
            //var container = Bootstrapper.GetInitializedContainer(new FakeOwinApp());

            //// Act
            //container.Verify();
        }
    }

    internal class FakeOwinApp : IAppBuilder
    {
        public object Build(Type returnType)
        {
            throw new NotImplementedException();
        }

        public IAppBuilder New()
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.IDictionary<string, object> Properties
        {
            get { throw new NotImplementedException(); }
        }

        public IAppBuilder Use(object middleware, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
