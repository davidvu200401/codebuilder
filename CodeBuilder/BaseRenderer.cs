using System;
using System.Collections.Generic;

namespace CodeBuilder
{
    public class BaseRenderer : ILocRenderer
    {
        public virtual List<string> EmitLines(string indent = "")
        {
            throw new NotImplementedException();
        }

        public string Emit()
        {
            return string.Join("\n", EmitLines());
        }
    }
}