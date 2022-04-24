using System.Collections.Generic;

namespace GenericCodeUML;

public enum TypeDeclaration
{
    Interface,
    Struct,
    Class,
    Enum
}

public class Declaration
{
    string name;
    string typeDeclaration;
    string typeModifiers;
    string typeAccess;
    List<Method> methods;
    List<Property> propertys;
    List<Event> events;

    List<string> linkClass;
    List<string> generics;
    // List<string> notes;

    public string Name => name;

    public string TypeAccess => typeAccess;

    public string TypeDeclaration => typeDeclaration;

    public string TypeModifiers => typeModifiers;

    public List<Property> Propertys => propertys;

    public List<Method> Methods => methods;
    public List<Event> Events => events;

    public List<string> Generics => generics;

    public List<string> LinkClass => linkClass;

    public Declaration(string typeDeclaration, string name)
    {
        this.name = name;
        this.typeAccess = "public";
        this.typeDeclaration = typeDeclaration;
        this.propertys = new List<Property>();
        this.events = new List<Event>();
        this.linkClass = new List<string>();
        methods = new List<Method>();
        generics = new List<string>();
    }

    public Declaration(string typeDeclaration, string name, string typeAccess)
      : this(typeDeclaration, name)
    {
        this.typeAccess = typeAccess;

    }
    public Declaration(string typeDeclaration, string name,  string typeAccess, string typeModifiers)
        : this(typeDeclaration, name, typeAccess)
    {
        this.typeModifiers = typeModifiers;
    }


    public void AddMethod(Method method)
    {
        methods.Add(method);
    }
    public void AddProperty(Property property)
    {
        propertys.Add(property);
    }
    public void AddEvent(Event @event)
    {
        events.Add(@event);
    }

    public void AddGeneric(string generic)
    {
        generics.Add(generic);
    }

    // public void AddNote(string note)
    // {
    //     notes.Add(note);
    // }

    public void AddLink(string link)
    {
        linkClass.Add(link);
    }
    
}
