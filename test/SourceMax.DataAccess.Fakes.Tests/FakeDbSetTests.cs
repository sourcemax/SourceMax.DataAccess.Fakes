using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

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

        [TestMethod]
        public async Task ToListAsync_Default_AsyncReturnsCorrectItem() {

            // Arrange
            var repository = new FakeDbSet<DummyModel>();
            repository.Add(new DummyModel { Id = 1, Value = "Dummy" });

            // Act
            var type = await repository.ToListAsync();

            // Assert
            Assert.AreEqual(1, type.Count);
            Assert.AreEqual(1, type[0].Id);
        }
    }
}
