namespace GenericCodeUML;
public class Event
{

    string type;
    string name;
    string typeModifiers;
    string typeAccess;

    public string Type => type;
    public string Name => name;
    public String TypeModifiers => typeModifiers;
    public string TypeAccess => typeAccess;




    public Event(string typeAccess, string type, string name)
    {
        this.typeAccess = typeAccess;
        this.type = type;
        this.name = name;
    }
    public Event(string typeAccess, string type, string name, string typeModifiers)
        : this(typeAccess, type, name)
    {
        this.typeModifiers = typeModifiers;
    }

}
