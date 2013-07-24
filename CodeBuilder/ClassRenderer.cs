using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeBuilder {
    public class ClassRenderer : BaseRenderer
    {
        // fields 
        private List<string> _directiveRefs;
        private List<string> _fieldRefs;
        private string _namespaceRef;
        private List<MethodRenderer> _constructorRefs;
        private List<MethodRenderer> _methodRefs;
        private string _className;
        private string _classScope;
        private List<string> _interfaceRefs;
        private string _indentToken;

        // const fields
        private const string NAMESPACE_FMT = "namespace {0} {{";
        private const string CLASS_FMT = "{0}{1} class {2}{3} {{";
        private const string FIELD_FMT = "{0} {1} {2};";

        public ClassRenderer(string namespaceRef, string className, string classScope = "public", string indentToken = "\t")
        {
            _directiveRefs = new List<string>();
            _fieldRefs = new List<string>();
            _namespaceRef = namespaceRef;
            _constructorRefs = new List<MethodRenderer>();
            _methodRefs = new List<MethodRenderer>();
            _className = className;
            _classScope = classScope;
            _interfaceRefs = new List<string>();
            _indentToken = indentToken;
        }

        public ClassRenderer AddDirective(string directive) {
            var d = String.Format("using {0};", directive);
            if (!_directiveRefs.Contains(d))
                _directiveRefs.Add("using " + directive + ";");
            return this;
        }

        public ClassRenderer AddInterface(string inter)
        {
            if (!_interfaceRefs.Contains(inter))
                _interfaceRefs.Add(inter);
            return this;
        }

        public ClassRenderer AddField(string type, string name, string scope = "private")
        {
            _fieldRefs.Add(String.Format(FIELD_FMT, scope, type, name));
            return this;
        }

        public MethodRenderer AddConstructor(string[] args = null)
        {
            return AddMethod("public", null, _className, args, true);
        }

        public MethodRenderer AddMethod(string scope, string returnType, string name, string[] args, bool isConstructor = false)
        {
            var newMethod = new MethodRenderer(name, scope, returnType, args, _indentToken);
            if (isConstructor)
            {
                _constructorRefs.Add(newMethod);
            }
            else
            {
                _methodRefs.Add(newMethod);
            }
            return newMethod;
        }

        public MethodRenderer AddPublicMethod(string name, string returnType = "void", string[] args = null)
        {
            return AddMethod("public", returnType, name, args);
        }

        public MethodRenderer AddProtectedMethod(string name, string returnType = "void", string[] args = null)
        {
            return AddMethod("protected", returnType, name, args);
        }

        public MethodRenderer AddPrivateMethod(string name, string returnType = "void", string[] args = null)
        {
            return AddMethod("private", returnType, name, args);
        }

        public override List<string> EmitLines(string indent = "")
        {
            // First, put all the directives
            var ret = _directiveRefs.ToList();
            indent = indent + _indentToken;

            ret.Add(String.Format(NAMESPACE_FMT, _namespaceRef));
            // create _interfaceRefs if there are any
            var interfaces = _interfaceRefs.LongCount() > 0 ? " : " + string.Join(", ", _interfaceRefs) : "";
            ret.Add(String.Format(CLASS_FMT, indent, _classScope, _className, interfaces));

            // add fields
            indent += _indentToken;
            ret.AddRange(_fieldRefs.Select(s => indent + s));

            // Add Constructors
            foreach (MethodRenderer ctx in _constructorRefs)
            {
                ret.AddRange(ctx.EmitLines(indent));
            }

            // Add Methods
            foreach (MethodRenderer ctx in _methodRefs)
            {
                ret.AddRange(ctx.EmitLines(indent));
            }
            // close class 
            ret.Add(_indentToken + "}");
            // close namespace
            ret.Add("}");

            return ret;
        }
    }
}