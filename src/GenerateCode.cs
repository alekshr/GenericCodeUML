using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GenericCodeUML;

public class GenerateCode
{
    List<Path> paths;
    string parserUML;
    const string patternParam = @"\(([^}]+)\)";
    const string patternPath = @"\{([^}]+)\}";

    const string patternGeneric = @"\<([^}]+)\>";

    const string patternLink = @":+(.*)";

    const char carriage = '\r';

    const char enter = '\n';



    public GenerateCode(string parserUML)
    {
        this.parserUML = parserUML;
        paths = new List<Path>();
    }

    public void AddPath(Path path)
    {
        paths.Add(path);
    }

    public void CreatePath(string localPath)
    {
        foreach (var path in paths)
        {
            var combine = System.IO.Path.Combine(localPath, path.FilePath);
            if (!Directory.Exists(combine))
            {
                Directory.CreateDirectory(combine);
            }
        }
    }

    public void CreateDecloration(string localPath)
    {
        CreatePath(localPath);
        StringBuilder builder = new StringBuilder();
        foreach (var path in paths)
        {
            var combinePath = System.IO.Path.Combine(localPath, path.FilePath);
            foreach (var @class in path.Declarationsis)
            {
                var combine = System.IO.Path.Combine(combinePath, $"{@class.Name}.cs");
                FileStream stream = new FileStream(combine, FileMode.OpenOrCreate);
                if (@class.TypeDeclaration == "class")
                {
                    builder.Append($"{@class.TypeAccess} {@class.TypeDeclaration} {@class.Name}");
                    if (@class.Generics.Count != 0)
                    {
                        for (int index = 0; index < @class.Generics.Count; index++)
                        {
                            if (index + 1 != @class.Generics.Count)
                            {
                                builder.Append($"{@class.Generics[index]}, ");
                            }
                            else
                            {
                                builder.Append($"{@class.Generics[index]}");
                            }
                        }
                    }
                    if (@class.LinkClass.Count != 0)
                    {
                        builder.Append(" : ");
                        for (int index = 0; index < @class.LinkClass.Count; index++)
                        {
                            if (index + 1 != @class.LinkClass.Count)
                            {
                                builder.Append($"{@class.LinkClass[index]}, ");
                            }
                            else
                            {
                                builder.Append($"{@class.LinkClass[index]}");
                            }
                        }
                    }
                    builder.AppendLine();
                    builder.AppendLine("{");
                    GenerateClass(@class, builder, stream);
                }
                if (@class.TypeDeclaration == "interface")
                {
                    builder.Append($"{@class.TypeDeclaration} {@class.Name}");
                    if (@class.Generics.Count != 0)
                    {
                        for (int index = 0; index < @class.Generics.Count; index++)
                        {
                            if (index + 1 != @class.Generics.Count)
                            {
                                builder.Append($"{@class.Generics[index]}, ");
                            }
                            else
                            {
                                builder.Append($"{@class.Generics[index]}");
                            }
                        }
                    }
                    if (@class.LinkClass.Count != 0)
                    {
                        builder.Append(" : ");
                        for (int index = 0; index < @class.LinkClass.Count; index++)
                        {
                            if (index + 1 != @class.LinkClass.Count)
                            {
                                builder.Append($"{@class.LinkClass[index]}, ");
                            }
                            else
                            {
                                builder.Append($"{@class.LinkClass[index]}");
                            }
                        }
                    }
                    builder.AppendLine();
                    builder.AppendLine("{");
                    GenerateInterface(@class, builder, stream);
                }
            }
        }
    }

    public void GenerateClass(Declaration decl, StringBuilder builder, FileStream stream)
    {
        foreach (var property in decl.Propertys)
        {
            builder.AppendLine($"\t{property.TypeAccess} {property.Type} {property.Name};\n");
        }
        foreach (var method in decl.Methods)
        {
            builder.Append($"\t{method.TypeAccess} {method.ReturnType} {method.Name}");
            if (method.Param.Count != 0)
            {
                for (int index = 0; index < method.Param.Count; index++)
                {
                    if (index + 1 != method.Param.Count)
                    {
                        builder.Append($"{method.Param.ElementAt(index).Key} {method.Param.ElementAt(index).Value}, ");

                    }
                    else
                    {
                        builder.Append($"{method.Param.ElementAt(index).Key} {method.Param.ElementAt(index).Value}");
                    }
                }
            }
            else
            {
                builder.Append("()");
            }
            builder.AppendLine("{}");
            builder.AppendLine();

        }
        foreach (var @event in decl.Events)
        {
            builder.Append($"\t{@event.TypeAccess} {@event.TypeModifiers} event {@event.Type} {@event.Name}");
            builder.Append(" = delegate {};");
            builder.AppendLine();
        }
        builder.AppendLine("}");
        using (StreamWriter writer = new StreamWriter(stream))
        {
            foreach (var str in builder.ToString().Split('\r'))
            {
                writer.WriteLine(str);
            }
        }
        builder.Clear();

    }

    public void GenerateInterface(Declaration decl, StringBuilder builder, FileStream stream)
    {
        foreach (var method in decl.Methods)
        {
            builder.Append($"\t{method.ReturnType} {method.Name}");
            if (method.Param.Count != 0)
            {
                for (int index = 0; index < method.Param.Count; index++)
                {
                    if (index + 1 != method.Param.Count)
                    {
                        builder.Append($"{method.Param.ElementAt(index).Key} {method.Param.ElementAt(index).Value}, ");
                    }
                    else
                    {
                        builder.Append($"{method.Param.ElementAt(index).Key} {method.Param.ElementAt(index).Value}");
                    }
                }
            }
            else
            {
                builder.Append("();");
            }
            builder.AppendLine();

        }
        foreach (var @event in decl.Events)
        {
            builder.Append($"\t{@event.TypeModifiers} event {@event.Type} {@event.Name};");
            builder.AppendLine();
        }
        builder.AppendLine("}");
        using (StreamWriter writer = new StreamWriter(stream))
        {
            foreach (var str in builder.ToString().Split(enter))
            {
                writer.WriteLine(str);
            }
        }
        builder.Clear();
    }
    public void GeneratePath()
    {
        var classInPath = Regex.Match(parserUML, patternPath);
        foreach (var uml in parserUML.Split(enter))
        {
            if (uml.Contains("package"))
            {
                var umlStrs = Extansion.StrPars(uml);
                var namePath = Extansion.DeleteSymbol(umlStrs[1]);
                var path = new Path(namePath);
                foreach (var @class in classInPath.Value.Split(enter))
                {
                    if (@class.Contains("class") || @class.Contains("interface"))
                    {
                        var classStrs = Extansion.StrPars(@class);
                        var typeDecl = Extansion.DeclarationTypeString(Extansion.DeleteSymbol(classStrs[0]));
                        var name = Extansion.DeleteSymbol(classStrs[1]);
                        var declaration = new Declaration(typeDecl, name);
                        if (Regex.Match(@class, patternGeneric).Success)
                        {
                            var paramGeneric = Regex.Match(@class, patternGeneric).Value.Split(',');
                            foreach (var param in paramGeneric)
                            {
                                declaration.AddGeneric(param);
                            }
                        }
                        path.Declarationsis.Add(declaration);
                    }
                }
                paths.Add(path);
                classInPath = classInPath.NextMatch();
            }
        }
    }

    public void GenerateProperty()
    {
        foreach (var uml in parserUML.Split(enter))
        {
            if (uml.Contains(":") && !uml.Contains("(") && !uml.Contains("event"))
            {
                var umlStrs = Extansion.StrPars(uml);
                var declName = Extansion.DeleteSymbol(umlStrs[0]);
                var access = Extansion.DeleteSymbol(umlStrs[2]);
                var typeProp = Extansion.DeleteSymbol(umlStrs[3]);
                var nameProp = Extansion.DeleteSymbol(umlStrs[4]);
                var property = new Property(nameProp, Extansion.AccessString(access), typeProp);
                foreach (var path in paths)
                {
                    path.Declarationsis
                    .Find(decl => decl.Name == declName)
                    ?.AddProperty(property);
                }
            }
        }
    }

    public void GenerateMethods()
    {
        foreach (var uml in parserUML.Split(enter))
        {
            if (uml.Contains(":") && uml.Contains("(") && !uml.Contains("event"))
            {
                var umlStrs = Extansion.StrPars(uml);
                var declName = Extansion.DeleteSymbol(umlStrs[0]);
                var access = Extansion.DeleteSymbol(umlStrs[2]);
                var typeReturnMeth = Extansion.DeleteSymbol(umlStrs[3]);
                var nameMethod = Extansion.DeleteSymbol(umlStrs[4]);
                var paramMethod = Regex.Match(uml, patternParam).Value.Split(',');
                var method = new Method(Extansion.AccessString(access), typeReturnMeth, nameMethod);
                foreach (var param in paramMethod)
                {
                    var paramSplit = Extansion.StrPars(param);
                    if (paramSplit.Length == 2)
                    {
                        method.AddParam((paramSplit[0], paramSplit[1]));
                    }

                }
                foreach (var path in paths)
                {
                    path.Declarationsis
                    .Find(decl => decl.Name == declName)
                    ?.AddMethod(method);
                }
            }
        }
    }

    public void GenerateEvents()
    {
        foreach (var uml in parserUML.Split(enter))
        {
            if (uml.Contains(":") && uml.Contains("event"))
            {
                var umlStrs = Extansion.StrPars(uml);
                var declName = Extansion.DeleteSymbol(umlStrs[0]);
                var access = Extansion.DeleteSymbol(umlStrs[2]);
                var typeReturnMeth = Extansion.DeleteSymbol(umlStrs[4]);
                var nameEvent = Extansion.DeleteSymbol(umlStrs[5]);
                var paramEvent = Regex.Match(uml, patternParam).Value.Split(',');
                var @event = new Event(Extansion.AccessString(access), typeReturnMeth, nameEvent);
                foreach (var param in paramEvent)
                {
                    var paramSplit = Extansion.StrPars(param);
                }
                foreach (var path in paths)
                {
                    path.Declarationsis
                    .Find(decl => decl.Name == declName)
                    ?.AddEvent(@event);
                }
            }
        }
    }

    public void GenerateLink()
    {
        foreach (var uml in parserUML.Split(enter))
        {
            if (uml.Contains("-->"))
            {
                var umlStrs = Extansion.StrPars(uml);
                var leftClass = Extansion.DeleteSymbol(umlStrs[0]);
                var rightClass = Extansion.DeleteSymbol(umlStrs[2]);
                foreach (var path in paths)
                {
                    path.Declarationsis
                    .Find(decl => decl.Name == leftClass)
                    ?.AddLink(rightClass);
                }
            }
        }
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        foreach (var path in paths)
        {
            builder.AppendLine(path.FilePath);
            foreach (var @class in path.Declarationsis)
            {
                builder.Append($"\t{@class.TypeAccess} {@class.TypeDeclaration} {@class.Name}");
                if (@class.Generics.Count != 0)
                {
                    for (int index = 0; index < @class.Generics.Count; index++)
                    {
                        if (index + 1 != @class.Generics.Count)
                        {
                            builder.Append($"{@class.Generics[index]}, ");
                        }
                        else
                        {
                            builder.Append($"{@class.Generics[index]}");
                        }
                    }
                }
                builder.AppendLine();
                builder.AppendLine("{");
                foreach (var property in @class.Propertys)
                {
                    builder.AppendLine($"\t\t{property.TypeAccess} {property.Type} {property.Name};\n");
                }
                foreach (var method in @class.Methods)
                {
                    builder.Append($"\t\t{method.TypeAccess} {method.ReturnType} {method.Name}");
                    if (method.Param.Count != 0)
                    {
                        for (int index = 0; index < method.Param.Count; index++)
                        {
                            if (index + 1 != method.Param.Count)
                            {
                                builder.Append($"{method.Param.ElementAt(index).Key} {method.Param.ElementAt(index).Value}, ");
                            }
                            else
                            {
                                builder.Append($"{method.Param.ElementAt(index).Key} {method.Param.ElementAt(index).Value}");
                            }
                        }
                    }
                    else
                    {
                        builder.Append("()");
                    }
                    builder.AppendLine("{}");
                    builder.AppendLine();

                }
                foreach (var @event in @class.Events)
                {
                    builder.Append($"\t\t{@event.TypeAccess} {@event.TypeModifiers} event {@event.Type} {@event.Name}");
                    builder.Append(" = delegate {};");
                    builder.AppendLine();
                }
                builder.AppendLine("}");

            }
        }
        return builder.ToString();
    }
}
