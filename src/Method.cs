using System.Collections.Generic;

namespace GenericCodeUML;

public class Method
{
    Dictionary<string, string> param;
    string typeAccess;
    string returnType;

    string name;

    public Dictionary<string, string> Param => param;
    public string TypeAccess => typeAccess;
    public string ReturnType => returnType;

    public string Name => name;

    public Method(string typeAccess, string returnType, string name)
    {
        this.typeAccess = typeAccess;
        this.returnType = returnType;
        this.name = name;
        this.param = new Dictionary<string, string>();
    }



    public void AddParam((string, string) param)
    {
        this.param.Add(param.Item1, param.Item2);
    }
}
