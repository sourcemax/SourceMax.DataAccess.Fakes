using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SourceMax.DataAccess.Fakes.Tests {

    [TestClass]
    public class FakeDbSetTests {

        [TestMethod]
        public void GetType_Default_ImplementsIDbSet() {

            // Arrange
            var repository = new FakeDbSet<DummyModel>();

            // Act
            var type = repository.GetType();

            // Assert
            Assert.IsTrue(type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IDbSet<>)));

            //repository.Add(new DummyModel { Id = 1, Value = "dummy item #1" });
        }
    }
}
