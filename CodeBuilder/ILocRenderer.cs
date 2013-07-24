using System.Collections.Generic;

namespace CodeBuilder {
    public interface ILocRenderer
    {
        List<string> EmitLines(string indent);
        string Emit();
    }
}