using System;
using NUnit.Framework;


namespace CodeBuilder.UnitTests {
    [TestFixture]
    public class ClassRendererTests
    {

        // Testing fields
        private ClassRenderer _basic;
        [SetUp]
        public void SetUp()
        {
            _basic = new ClassRenderer("TestNamespace", "TestClass");
        }

        [TearDown]
        public void TearDown()
        {
            _basic = null;
        }

        [Test]
        public void Test_BasicConstruction()
        {
            var locs = _basic.EmitLines();
            Assert.AreEqual(4, locs.Count);
        }

        [Test]
        public void Test_AddDirective()
        {
            var x = _basic.AddDirective("System");
            var locs = _basic.EmitLines();
            Assert.AreEqual(_basic, x);
            Assert.AreEqual(5, locs.Count);
            Assert.AreEqual("using System;", locs[0]);
        }

        [Test]
        public void Test_AddInterface()
        {
            var x = _basic.AddInterface("ITest");
            var locs = _basic.EmitLines();
            Assert.AreEqual(_basic, x);
            Assert.AreEqual(4, locs.Count);
            Assert.AreEqual("\tpublic class TestClass : ITest {", locs[1]);
        }

        [Test]
        public void Test_AddField()
        {
            var x = _basic.AddField("int", "privateField")
                         .AddField("int", "publicField", "public");
            var locs = _basic.EmitLines();
            Assert.AreEqual(_basic, x);
            Assert.AreEqual(6, locs.Count);
            Assert.AreEqual("\t\tprivate int privateField;", locs[2]);
            Assert.AreEqual("\t\tpublic int publicField;", locs[3]);
        }

        [Test]
        public void Test_AddConstructor()
        {
            var x = _basic.AddConstructor();
            var y = _basic.AddConstructor(new[] {
                "int x",
                "int y"
            });
            var locs = _basic.EmitLines();
            Assert.IsInstanceOf(typeof(MethodRenderer), x);
            Assert.IsInstanceOf(typeof(MethodRenderer), y);
            Assert.AreEqual(8, locs.Count);
            Assert.AreEqual("\t\tpublic TestClass() {", locs[2]);
            Console.WriteLine(_basic.Emit());
            Assert.AreEqual("\t\tpublic TestClass(int x, int y) {", locs[4]);
        }

        [Test]
        public void Test_AddPublicMethod()
        {
            var x = _basic.AddPublicMethod("TestMethod");
            var locs = _basic.EmitLines();
            Assert.IsInstanceOf(typeof(MethodRenderer), x);
            Assert.AreEqual(6, locs.Count);
            Assert.AreEqual("\t\tpublic void TestMethod() {", locs[2]);
        }

        [Test]
        public void Test_AddProtectedMethod()
        {
            var x = _basic.AddProtectedMethod("TestMethod");
            var locs = _basic.EmitLines();
            Assert.IsInstanceOf(typeof(MethodRenderer), x);
            Assert.AreEqual(6, locs.Count);
            Assert.AreEqual("\t\tprotected void TestMethod() {", locs[2]);
        }

        [Test]
        public void Test_AddPrivateMethod()
        {
            var x = _basic.AddPrivateMethod("TestMethod");
            var locs = _basic.EmitLines();
            Assert.IsInstanceOf(typeof(MethodRenderer), x);
            Assert.AreEqual(6, locs.Count);
            Assert.AreEqual("\t\tprivate void TestMethod() {", locs[2]);
        }
    }
}