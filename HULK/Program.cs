using System.Text.RegularExpressions;
using System.Text;
class HULK : Expresiones
{
    static void Main(string[] args)
    {
        while (true)
        {
            string instruction = Console.ReadLine()!;
            instruction = instruction.Trim();


            if (instruction.IndexOf(">") == 0 && instruction.LastIndexOf(";") == instruction.Length - 1)
            {
                instruction = instruction.Remove(0, 1);
                instruction = instruction.Remove(instruction.Length - 1);

                instruction = Utiles2.Variable_X_Valor(instruction, "PI", "3.14");
                string imprimir = Identifier(instruction);

                while ((imprimir.IndexOf('"') == 0 || imprimir.IndexOf('(') == 0) && (imprimir.LastIndexOf('"') == imprimir.Length - 1 || imprimir.LastIndexOf(')') == imprimir.Length - 1))
                {
                    imprimir = imprimir.Remove(0, 1);
                    imprimir = imprimir.Remove(imprimir.Length - 1);
                }

                Console.WriteLine("SALIDA:  " + imprimir);
            }
            else
            {
                if (instruction.IndexOf(">") != 0 && instruction.LastIndexOf(";") != instruction.Length - 1)
                    Console.WriteLine(" ! SYNTAX ERROR: Se esperaba '>'\n Se esperaba ';'");
                else if (instruction.LastIndexOf(";") == instruction.Length - 1)
                    Console.WriteLine("! SYNTAX ERROR: Se esperaba '>'");
                else Console.WriteLine("! SYNTAX ERROR: Se esperaba ';'");
            }
        }

    }
    public static string Identifier(string linea)
    {
        string instruction = linea.Trim();

        //Se trata de definir la linea introducida en un tipo específico. Si es reconocida por algún tipo, se manda a la clase determinada para ese tipo
        //Si es Function
        if (IsValid(instruction, function).Success)
        {
            return Function.DeclararFunction(instruction);
        }//Si es let-in
        else if (IsValid(instruction, let_in).Success)
        {
            return Identifier(Let_in.Return_Let_in(instruction));
        }
        //Si es Print
        else if (IsValid(instruction, print).Success)
        {
            return Identifier(Print.TokenPrint(instruction));
        }
        //Si es if-else
        else if (IsValid(instruction, if_else).Success)
        {
            return Identifier(If_else.ReturnIf_else(instruction));
        }

        //Se agregan las funciones preestablecidas en este punto del código donde ya el usuario declaró funciones, para que las declaradas 
        //por el usuario estén guardadas primero, para que las listas nombre_funcion, parámetros y cuerpos concuerden en el índice con la misma función
        if (instruction.Contains("sen") || instruction.Contains("cos") || instruction.Contains("log"))
        {
            Function.nombre_funciones.Add("sin");
            Function.nombre_funciones.Add("cos");
            Function.nombre_funciones.Add("log");

            instruction = Function.Call_me(instruction, 0);

            Function.nombre_funciones.Remove("sin");
            Function.nombre_funciones.Remove("cos");
            Function.nombre_funciones.Remove("log");

        }
        else if (Function.nombre_funciones.Count != 0)
            instruction = Function.Call_me(instruction, 0);

        //Si es string
        if (IsValid(instruction, text).Success)
        {
            return Operar.OperationString(instruction);
        }
        //si es booleano
        else if (IsValid(instruction, BoolType1).Success)
            return Operar.OperationBoolType1(instruction).ToString();

        //si es booleano
        else if (IsValid(Regex.Replace(instruction, @"\s*", ""), BoolType2).Success)
            return Operar.OperationBoolType2(Regex.Replace(instruction, @"\s*", "")).ToString();

        //si es numero
        else if (IsValid(Regex.Replace(instruction, @"\s*", ""), Number).Success)
            return Operar.OperationNumber(Regex.Replace(instruction, @"\s*", ""));

        //Sino se reconoció la linea. Se trata de identificar el error
        else if (IsValid(instruction, llamado_inexistente).Success)
        {
            GroupCollection groups = IsValid(instruction, llamado_inexistente).Groups;
            return " ! SEMANTIC ERROR: No existe ningún método declarado con el nombre " + groups["nombre_funcion"].Value.ToString();
        }
        else if ((IsValid(instruction, @"@+").Success && IsValid(instruction, @".*[0-9]+.*").Success) || (IsValid(instruction, @"[+|-|*|/|%]+").Success && IsValid(instruction, @"[a-z]|[A-Z]+").Success))
        {
            return "\" ! SEMANTIC ERROR: Operación inválida, tokens de diferente tipo\"";

        }

        return "! SYNTAX ERROR: Expresión inválida";


    }


}