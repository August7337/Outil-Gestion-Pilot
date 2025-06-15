namespace Outil_Gestion_Pilot.Models.Tests
{
    [TestClass()]
    public class OrderTests
    {
        [TestMethod()]
        public void OrderTest()
        {
            Order orderTest = new Order(1, "Reseller Test", DateTime.Now, "Delivery Test");
            Assert.AreEqual(1, orderTest.CommandeId);
            Assert.AreEqual("Reseller Test", orderTest.Reseller);
            Assert.AreEqual("Delivery Test", orderTest.Delivery);
            Assert.IsNotNull(orderTest.Products);
        }
    }
}