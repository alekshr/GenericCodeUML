namespace GenericCodeUML;

public class Property
{
    string name;
    string typeAccess;
    string type;


    public string Name => name;
    public string TypeAccess => typeAccess;
    public string Type => type;

    public Property(string name, string typeAccess, string type)
    {
        this.name = name;
        this.type = type;
        this.typeAccess = typeAccess;
    }
}
