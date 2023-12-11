public class Print : Utiles1
{
    public static string TokenPrint(string instruction)
    {
        string print = Expresiones.IsValid(instruction, Expresiones.print).Groups[0].ToString();
        int inicio0 = instruction.IndexOf(print);
        int inicio = instruction.IndexOf('(', inicio0);
        int final = Parentesis_que_cierra(inicio, instruction);

        if (final == 0)
            return "\"! SYNTAX ERROR: Se esperaba ')'\"";

        string cuerpo_print = instruction.Substring(inicio + 1, final - inicio - 1);
        cuerpo_print = HULK.Identifier(cuerpo_print);

        instruction = instruction.Remove(inicio0, final - inicio0 + 1);
        instruction = instruction.Insert(inicio0, cuerpo_print);

        return instruction;
    }


}