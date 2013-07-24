using System;
using System.Collections.Generic;

namespace CodeBuilder {
    public class MethodRenderer : BaseRenderer
    {
        private string _declarationScope;
        private string _declarationReturnType;
        private string _declarationName;
        private List<string> _declarationArguments;
        private List<CodeBlockRenderer> _locs;
        private string _indentToken;

        public MethodRenderer(string name, string scope = "public", string returnType = "void", IEnumerable<string> args = null, string indentToken = "\t")
        {
            _locs = new List<CodeBlockRenderer>();
            _indentToken = indentToken;
            _declarationScope = scope;
            _declarationReturnType = returnType;
            _declarationName = name;
            if (args != null) _declarationArguments = new List<string>(args);
        }

        public MethodRenderer AddLine(string loc)
        {
            _locs.Add(new CodeBlockRenderer(loc, _indentToken));
            return this;
        }

        public CodeBlockRenderer AddBlock(string baseLoc)
        {
            var ret = new CodeBlockRenderer(baseLoc, true, _indentToken);
            _locs.Add(ret);
            return ret;
        }

        public override List<string> EmitLines(string indent = "")
        {
            var ret = new List<string>();

            var argsString = (_declarationArguments != null) ? string.Join(", ", _declarationArguments) : "";
            var decReturnType = _declarationReturnType != null ? " " + _declarationReturnType : "";
            ret.Add(String.Format(indent + "{0}{1} {2}({3}) {{", _declarationScope, decReturnType, _declarationName, argsString));
            foreach (var loc in _locs)
            {
                ret.AddRange(loc.EmitLines(indent + _indentToken));
            }
            ret.Add(indent + "}");

            return ret;
        }
    }
}