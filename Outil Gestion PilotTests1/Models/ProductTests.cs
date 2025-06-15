namespace Outil_Gestion_Pilot.Models.Tests
{
    [TestClass()]
    public class ProductTests
    {
        [TestMethod()]
        public void ProductTest()
        {
            Product product = new Product("image","C0101","Produit test",19.99);
            Assert.AreEqual("image", product.ImagePath);
            Assert.AreEqual("C0101", product.Code);
            Assert.AreEqual("Produit test", product.Name);
            Assert.AreEqual(19.99, product.SellingPrice);
        }


    }
}