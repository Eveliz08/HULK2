using System;
using System.Reflection;
using System.Text.RegularExpressions;

public class Expresiones
{
    public static Match IsValid(string instruction, string patronER)
    {
        Regex regex = new Regex(patronER);
        Match match = regex.Match(instruction);

        return match;
    }

   public static string function
    {
        get => @"^function\s+(?<nombre_funcion>(.*))\((?<parametros>(.*))\)\s*=>(?<cuerpo_funcion>(.*))$";
    }
    public static string if_else
    {
        get => @"if\s*\((?<expresion_boleana>.*)\)\s*(?<condicion_if>.*)else\s*(?<condicion_else>.*)$";
    }
    public static string text
    {
        get => "^\\(*?\\\".*?\\\"(@+(\\\".*?\\\"))*?\\)*?$";
    }
    public static string BoolType2
    {
        get => @"^\(*?([\(\-+)])*\d+(\.\d+)?([)])*(([\(\-\+\*\/\^\)])*\d+(\.\d+)?([)])*)*([^<>!=]*([<>!]=?|==)[^<>!=]*)([\(\-+)])*\d+(\.\d+)?([)])*(([\(\-\+\*\/\^\)])*\d+(\.\d+)?([)])*)*(\s*(&|\|)\s*([\(\-+)])*\d+(\.\d+)?([)])*(([\(\-\+\*\/\^\)])*\d+(\.\d+)?([)])*)*([^<>!=]*([<>!]=?|==)[^<>!=]*)([\(\-+)])*\d+(\.\d+)?([)])*(([\(\-\+\*\/\^\)])*\d+(\.\d+)?([)])*)*)*?\)*?$";
    }
    public static string BoolType1
    {
        get => @"^\(*?(!*(true|True|false|False)\s*((&|\|)\s*!*(true|True|false|False))*?)\)*?$";
    }
    public static string Number
    {
        get => @"^([\(\-+)])*\d+(\.\d+)?([)])*(([\(\-\+\*\/\^\%\)])*\d+(\.\d+)?([)])*)*$";
    }
    public static string let_in
    {
        get => @"let\s+(.*)=(.*)";
    }
    public static string print
    {
        get => @"print\((?<cuerpo_print>.*)\)";
    }
    public static string llamado_inexistente
    {
        get => @"(?<nombre_funcion>[a-zA-Z_][a-zA-Z0-9_]*)\s*\((.*)\)";
    }

}