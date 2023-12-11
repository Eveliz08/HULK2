using System.Text.RegularExpressions;
public class If_else: Utiles1
{
    public static string ReturnIf_else(string instruction)
    {
        Match match = Expresiones.IsValid(instruction, Expresiones.if_else);
        int inicio = match.Index;
        (string condition, int end_condition, int Beginning_else) = Parse_condition(instruction, inicio);

        if (end_condition == 0 && Beginning_else == 0) return condition;

        condition = HULK.Identifier(condition);
        bool conditionBool;
        if (bool.TryParse(condition, out conditionBool))
        {
            if (conditionBool) { return If_Condition_is_True(instruction, inicio, end_condition, Beginning_else); }
            else { return If_Condition_is_False(instruction, inicio, Beginning_else); }
        }
        else return "\" ! SEMANTIC ERROR: Condición booleana inválida\"";
    }

    private static (string, int, int) Parse_condition(string instruction, int index)
    {
        int inicio = instruction.IndexOf('(', index);
        int final = Parentesis_que_cierra(inicio, instruction);

        if (final == 0) return ("\"! SYNTAX ERROR: Se esperaba ')'\"", 0, 0);

        string condition = instruction.Substring(inicio + 1, final - inicio - 1);
        inicio = final + 1;
        final = 0;
        int contador = 0;
        bool se_encontro_el_principal = true;

        for (int i = inicio; i < instruction.Length; i++)
        {
            if (instruction[i] == '"')
                i = Be_Ignorant(instruction, i);
            else if (instruction[i] == 'i' && instruction[i + 1] == 'f')
                contador++;
            else if (instruction[i] == 'e' && instruction[i + 1] == 'l' && instruction[i + 2] == 's' && instruction[i + 3] == 'e')
            {
                if (contador == 0 && se_encontro_el_principal == true)
                {
                    final = i - 1;
                    se_encontro_el_principal = false;
                }
                else
                    contador--;
            }
        }

        if (contador > 0) return ("\" ! SYNTAX ERROR: Se requieren ambas estructuras: if y else. Faltaron: " + contador + " estructura/s 'else'\"", 0, 0);
        if (contador < 0) return ("\" ! SYNTAX ERROR: Se requieren ambas estructuras: if y else. Faltaron: " + Math.Abs(contador) + " estructura/s 'if'\"", 0, 0);

        return (condition, inicio, final);
    }

    private static string If_Condition_is_True(string instruction, int inicio, int end_condition, int beginning_else)
    {
        int final = instruction.Length - 1;
        int contador = 0;
        //toda la extructura esté dentro de ecuaciones aritméticas
        for (int i = inicio - 1; i >= 0; i--)
        {
            if (instruction[i] == '"')
                i = Be_Ignorant(instruction, i);
            if (instruction[i] == ')')
                contador--;
            else if (instruction[i] == '(')
            {
                contador++;
                if (contador > 0)
                {
                    final = Parentesis_que_cierra(beginning_else, instruction) - 1;

                    if (final == -1)
                        return "\"! SYNTAX ERROR: Se esperaba ')'\"";

                    goto Found;
                }

            }
        }

        //Caso uno que el if-else no sea hasta el final de la cadena, esté dentro de un llamado de función
        for (int i = beginning_else; i < instruction.Length; i++)
        {
            if (instruction[i] == '"')
                i = Be_Ignorant(instruction, i);
            else if (instruction[i] == '(')
            {
                i = Parentesis_que_cierra(i, instruction);
            }
            if (instruction[i] == ',')
            {
                final = i - 1;
                break;
            }
        }

    Found:


        string respuesta = instruction.Substring(end_condition, beginning_else - end_condition);
        instruction = instruction.Remove(inicio, final - inicio + 1);
        instruction = instruction.Insert(inicio, respuesta);

        return instruction;

    }
    private static string If_Condition_is_False(string instruction, int inicio, int index2)
    {
        return instruction.Remove(inicio, index2 + 5 - inicio);
    }

 



}