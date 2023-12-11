using System.Text.RegularExpressions;
public class Let_in: Utiles2
{
    public static string Return_Let_in(string instruction)
    {
        string let = Expresiones.IsValid(instruction, Expresiones.let_in).Groups[0].ToString();

        (string[] variables, string body) = Parse(let);
        if(variables.Length == 0) return body;
        body = body.Trim();

        for (int i = 0; i < variables.Length; i++)
        {
            string[] dos_partes = variables[i].Split('=', 2);
            dos_partes[0] = dos_partes[0].Trim();

            if(!Es_un_nombre_valido(dos_partes[0])) return "\" ! LEXICAL ERROR: Se esperaba un identificador, " + dos_partes[0] + " identificador inválido\"";
            if(Es_palabra_reservada(dos_partes[0])) return "\" ! LEXICAL ERROR: No se pueden utilizar palabras reservadas como identificadores, " + dos_partes[0] + "\"";

            body = Variable_X_Valor(body, dos_partes[0].Trim(), dos_partes[1]);
        }
        return instruction.Replace(let, body);
    }

    
    private static (string[], string) Parse(string instruction)
    {
        instruction = Regex.Replace(instruction, $"^let", "");
        List<string> variables = new List<string>();
        string cuerpo_let_in = "";
        int inicio = 0;
        int balancear = 1;
        bool contador_auxiliar = true;

        for (int i = 0; i < instruction.Length; i++)
        {
            if (instruction[i] == '"')
            {
                i = Be_Ignorant(instruction, i);

            } 
            else if (instruction[i] == ',')
            {
                variables.Add(instruction.Substring(inicio + 1, i - inicio - 1));
                inicio = i;
            }
            else if (i < instruction.Length - 4 && instruction[i] == 'l' && instruction[i + 1] == 'e' && instruction[i + 2] == 't' && instruction[i + 3] == ' ')
            {
                balancear++;
                i += 4;
            }
            else if (i < instruction.Length - 4 && instruction[i] == ' ' && instruction[i + 1] == 'i' && instruction[i + 2] == 'n' && instruction[i + 3] == ' ')
            {
                balancear--;
                if (balancear == 0 && contador_auxiliar == true)
                {
                    variables.Add(instruction.Substring(inicio + 1, i - inicio - 1));
                    cuerpo_let_in = instruction.Substring(i + 3);
                    contador_auxiliar = false;
                }
                else if(balancear <0)
                return (new String[0], "\" ! SEMANTIC ERROR: Se esperaba una estructura let `let-in` \"");
            }
        }

        if(balancear >0)
                return (new String[0], "\"  ! SEMANTIC ERROR: Se esperaba una estructura in `let-in`\"");


        return (variables.ToArray(), cuerpo_let_in);
    }


}