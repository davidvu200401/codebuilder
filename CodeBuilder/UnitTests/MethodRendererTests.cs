using NUnit.Framework;

namespace CodeBuilder.UnitTests {
    [TestFixture]
    public class MethodRendererTests
    {

        private MethodRenderer _publicMethod;
        private MethodRenderer _privateMethod;
        private MethodRenderer _returnMethod;
        private MethodRenderer _argMethod;

        [SetUp]
        public void SetUp()
        {
            _publicMethod = new MethodRenderer("TestMethod");
            _privateMethod = new MethodRenderer("TestMethod", "private");
            _returnMethod = new MethodRenderer("TestMethod", returnType: "int");
            _argMethod = new MethodRenderer("TestMethod", args: new[] {
                "int a",
                "int b"
            });
        }

        [TearDown]
        public void TearDown()
        {
            _publicMethod = null;
            _privateMethod = null;
            _returnMethod = null;
            _argMethod = null;
        }

        [Test]
        public void Test_PublicMethod()
        {
            var locs = _publicMethod.EmitLines();
            Assert.AreEqual(2, locs.Count);
            Assert.AreEqual(locs[0], "public void TestMethod() {");
        }

        [Test]
        public void Test_PrivateMethod()
        {
            var locs = _privateMethod.EmitLines();
            Assert.AreEqual(2, locs.Count);
            Assert.AreEqual(locs[0], "private void TestMethod() {");
        }

        [Test]
        public void Test_ReturnMethod()
        {
            var locs = _returnMethod.EmitLines();
            Assert.AreEqual(2, locs.Count);
            Assert.AreEqual(locs[0], "public int TestMethod() {");
        }

        [Test]
        public void Test_ArgMethod()
        {
            var locs = _argMethod.EmitLines();
            Assert.AreEqual(2, locs.Count);
            Assert.AreEqual(locs[0], "public void TestMethod(int a, int b) {");
        }

        [Test]
        public void Test_AddLine()
        {
            var x = _publicMethod.AddLine("var x = 1;");
            var locs = _publicMethod.EmitLines();
            Assert.AreEqual(_publicMethod, x);
            Assert.AreEqual(3, locs.Count);
            Assert.AreEqual("\tvar x = 1;", locs[1]);
        }

        [Test]
        public void Test_AddBlock()
        {
            var x = _publicMethod.AddBlock("if(true)");
            var locs = _publicMethod.EmitLines();
            Assert.IsInstanceOf(typeof(CodeBlockRenderer), x);
            Assert.AreEqual(4, locs.Count);
        }
    }
}