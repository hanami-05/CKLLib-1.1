using CKLLib;

namespace CKLLibTests
{
    [TestClass]
    public class DataClassesTests
    {
        [TestMethod]
        public void TestPairEquality1()
        {
            Pair p1 = new Pair("g1", "p1");
            Pair p2 = new Pair("g1", "p1");
            
            bool res = p1.Equals(p2);
            bool exp = true;
            
            Assert.AreEqual(exp, res);
        }

    }
}