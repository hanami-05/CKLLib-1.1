using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CKLLib.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace CKLLib.tests
{
	[TestClass]
	public class MathOperationsTest
    {

        [TestMethod]
        public void ItemProjection_Operation_Test_1() 
        {
            CKL data = new CKL()
            {
                FilePath = "C:\\Users\\user\\Ciclograms\\test1.ckl",
                GlobalInterval = new TimeInterval(0, 1500),
                Dimention = TimeDimentions.MICROSECONDS,
                Source = new HashSet<Pair>() { new Pair ("A1", "B1"), new Pair("A1", "B2"), new Pair("A1", "B3"),
                    new Pair("A2", "B1"), new Pair("A2", "B2"), new Pair("A2", "B3")},
                Relation = new HashSet<RelationItem>() 
                {
                    new RelationItem(new Pair("A1", "B1"), [new TimeInterval(200, 400), new TimeInterval(600, 900), 
                        new TimeInterval(1400, 1450)]),
                    new RelationItem(new Pair("A1", "B3"), [new TimeInterval(0, 400), new TimeInterval(1000, 1400)]),
                    new RelationItem(new Pair("A2", "B2"), [new TimeInterval(100, 800), new TimeInterval(1100, 1250)]),
                    new RelationItem(new Pair("A2", "B3"), [new TimeInterval(0, 1200)])
                }
            };

            TimeInterval interval = new TimeInterval(300, 500);


			CKL res = CKLMath.ItemProjection(data, "B3", (object obj1, object obj2) => obj1.Equals(obj2), 
                interval);

            CKL exp = new CKL()
            {
                FilePath = "",
                GlobalInterval = interval,
                Dimention = TimeDimentions.MICROSECONDS,
                Source = new HashSet<Pair>() { new Pair("A2, B3")},
                Relation = new HashSet<RelationItem>() { new RelationItem(new Pair("A2", "B3"), [new TimeInterval(300, 500)]) }
            };

            Assert.AreEqual(exp, res, new CKLEqualityComparer());
        }
    }
}
