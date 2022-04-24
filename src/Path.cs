using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GenericCodeUML;

public class Path
{
    string filePath;
    List<Declaration> declarationsis;

    public string FilePath => filePath;
    public List<Declaration> Declarationsis => declarationsis;

    public Path(string filePath) 
    {
        declarationsis = new List<Declaration>();
        this.filePath = filePath;
    }

    public void AddDeclaration(Declaration declarations)
    {
        declarationsis.Add(declarations);
    }
}
