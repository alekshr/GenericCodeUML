using System.Text.RegularExpressions;

namespace GenericCodeUML;
public static class Extansion
{
 public static TypeAccess Access(string access) => access switch
    {
        "+" => TypeAccess.Public,
        "-" => TypeAccess.Private,
        "#" => TypeAccess.Protected,
        _ => TypeAccess.Private
    };

 public static string AccessString(string access) => access switch
    {
        "+" => "public",
        "-" => "private",
        "#" => "protected",
        _ => "private"
    };

    public static TypeModifiers Modifiers(string modifiers) => modifiers switch
    {
        "abstract" => TypeModifiers.Abstract,
        "<<static>>" => TypeModifiers.Static,
        "<<partial>>" => TypeModifiers.Partial,
        "<<sealed>>" => TypeModifiers.Sealed,
        _ => TypeModifiers.None
    };

    public static string ModifiersString(string modifiers) => modifiers switch
    {
        "abstract" => "abstract",
        "<<static>>" => "static",
        "<<partial>>" => "partial",
        "<<sealed>>" => "sealed",
        _ => ""
    };



    public static TypeDeclaration DeclarationType(string typeDeclaration) => typeDeclaration switch
    {
        "class" => TypeDeclaration.Class,
        "interface" => TypeDeclaration.Interface,
        "<<struct>> class" => TypeDeclaration.Struct,
        "enum" => TypeDeclaration.Enum,
        _ => TypeDeclaration.Class
    };

    public static string DeclarationTypeString(string typeDeclaration) => typeDeclaration switch
    {
        "class" => "class",
        "interface" => "interface",
        "<<struct>> class" => "struct",
        "enum" => "enum",
        _ => "class"
    };

    public static string[] StrPars(string str) => str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

    public static string DeleteSymbol(string str) => Regex.Replace(str, @"[ \r\n\t]", "");
}
