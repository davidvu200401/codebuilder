using System.Collections.Generic;

namespace CodeBuilder {
    public class CodeBlockRenderer : BaseRenderer
    {
        private string _baseLOC;
        private bool _isBlock;
        private List<CodeBlockRenderer> _codeBlock;
        private string _indentToken;

        public CodeBlockRenderer(string baseLoc, string token = "\t")
        {
            _baseLOC = baseLoc;
            _indentToken = token;
        }

        public CodeBlockRenderer(string baseLoc, bool isBlock, string token = "\t")
        {
            _baseLOC = baseLoc;
            _isBlock = isBlock;
            if (isBlock) _codeBlock = new List<CodeBlockRenderer>();
            _indentToken = token;
        }

        public CodeBlockRenderer AddLine(string loc)
        {
            if (!_isBlock) return this;
            _codeBlock.Add(new CodeBlockRenderer(loc, _indentToken));
            return this;
        }

        public override List<string> EmitLines(string indent = "")
        {
            var ret = new List<string>();
            if (!_isBlock)
            {
                ret.Add(indent + _baseLOC);
            }
            else
            {
                ret.Add(indent + _baseLOC + " {");
                var cIndent = indent + _indentToken;
                foreach (var b in _codeBlock)
                {
                    ret.AddRange(b.EmitLines(cIndent));
                }
                ret.Add(indent + "}");
            }

            return ret;
        }
    }
}