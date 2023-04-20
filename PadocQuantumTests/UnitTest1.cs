using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PadocEF.Models.Context;

namespace MyProject.Tests {
    [TestClass]
    public class MyTests {

        [TestInitialize]
        public void CleanInMemoryDatabase() {
            var options = new DbContextOptionsBuilder<PatdocQuantumContextInMemory>()
                .Options;

            using (var context = new PatdocQuantumContextInMemory()) {
                if (context.GetType().Name.Contains("InMemory")) {
                    var dbSetProperties = context.GetType().GetProperties()
                        .Where(p => 
                            p.PropertyType.IsGenericType &&
                            p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)
                        );

                    foreach (var property in dbSetProperties) {
                        var dbSet = (dynamic)property.GetValue(context);
                        dbSet.RemoveRange(dbSet);
                    }

                    context.SaveChanges();
                }
            }
        }

        [TestMethod]
        [TestCategory("Policy")]
        public void MyTest1() {
            using (var context = new PatdocQuantumContextInMemory()) {
                var count = context.Policy.Count();
                Assert.AreEqual(0, count);
            }
        }


        [TestMethod]
        [TestCategory("Policy")]
        public void MyTest2() {

            using (var context = new PatdocQuantumContextInMemory()) {


                context.Policy.Add(new PadocEF.Models.Policy());
                context.SaveChanges();

                var count = context.Policy.Count();
                Assert.AreEqual(1, count);
            }
        }

        [TestMethod]
        [TestCategory("Policy")]
        public void MyTest3() {
            using (var context = new PatdocQuantumContextInMemory()) {
                var count = context.Policy.Count();
                Assert.AreEqual(0, count);
            }
        }
    }
}
