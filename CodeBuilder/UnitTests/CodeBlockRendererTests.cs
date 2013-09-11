using NUnit.Framework;

namespace CodeBuilder.UnitTests {
    [TestFixture]
    public class CodeBlockRendererTests
    {

        private CodeBlockRenderer _basic;
        private CodeBlockRenderer _blockBasic;
        private string _basicLoc;
        private string _blockBaseString;
        private string _blockBlockBaseString;

        [SetUp]
        public void Setup()
        {
            // Basic LOC params
            _basicLoc = "var x = 1;";
            _basic = new CodeBlockRenderer(_basicLoc);

            // Block Code params
            _blockBaseString = "if (true)";
            _blockBasic = new CodeBlockRenderer(_blockBaseString, true);

            // block in block string
            _blockBlockBaseString = "if (false)";
        }

        [TearDown]
        public void TearDown()
        {
            _basic = null;
            _blockBasic = null;
            _basicLoc = null;
            _blockBaseString = null;
        }


        [Test]
        public void Test_BasicConstruction()
        {
            var locs = _basic.EmitLines();
            Assert.AreEqual(locs.Count, 1);
            Assert.AreEqual(locs[0], _basicLoc);
        }

        [Test]
        public void Test_BlockConstruction()
        {
            _blockBasic.AddLine(_basicLoc);
            var locs = _blockBasic.EmitLines();
            Assert.AreEqual(locs.Count, 3);
            Assert.AreEqual(locs[0], _blockBaseString + " {");
            Assert.AreEqual(locs[1], "\t" + _basicLoc);
            Assert.AreEqual(locs[2], "}");
        }

        [Test]
        public void Test_BlockInBlockConstruction() {
            // block in a block
            var blockInBlock = _blockBasic.addBlock(_blockBlockBaseString);
            blockInBlock.AddLine(_basicLoc);
            var locs = _blockBasic.EmitLines();
            Assert.AreEqual(5, locs.Count);
            Assert.AreEqual(_blockBaseString + " {", locs[0]);
            Assert.AreEqual("\t"+_blockBlockBaseString + " {", locs[1]);
            Assert.AreEqual("\t\t" + _basicLoc, locs[2]);
            Assert.AreEqual("\t}", locs[3]);
            Assert.AreEqual("}", locs[4]);
        }
    }
}