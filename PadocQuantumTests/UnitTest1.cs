using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PadocEF.Models.Context;

namespace MyProject.Tests {
    [TestClass]
    public class MyTests {
        [TestMethod]
        public void MyTest() {
            // Arrange
            var options = new DbContextOptionsBuilder<PatdocQuantumContext>()
                .UseInMemoryDatabase(databaseName: "MyInMemoryDatabase")
                .Options;

            using (var context = new PatdocQuantumContext(options)) {
                // Perform operations on the context
            }
        }
    }
}
