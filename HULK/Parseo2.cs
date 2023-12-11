using System.Text.RegularExpressions;
using System.Text;

public class Utiles2 : Utiles1
{
    public static bool Es_palabra_reservada(string palabra)
    {
        string[] palabras_reservadas = { "print", "let", "in", "if", "else", "function" };

        return palabras_reservadas.Contains(palabra);
    }

    public static bool Es_un_nombre_valido(string identificador)
    {
        string patron = @"^\s*[a-zA-Z_][a-zA-Z0-9_]*?";

        return Expresiones.IsValid(identificador, patron).Success;

    }
    public static string Variable_X_Valor(string cuerpo, string variable, string valor_variable)
    {
        valor_variable = HULK.Identifier(valor_variable);
        for (int i = 0; i < cuerpo.Length; i++)
        {
            if (cuerpo[i] == '"')
                i = Be_Ignorant(cuerpo, i);

            else if (i < cuerpo.Length - 3)
            {
                if (cuerpo[i] == 'l' && cuerpo[i + 1] == 'e' && cuerpo[i + 2] == 't')
                {
                    for (int k = i + 3; k < cuerpo.Length; k++)
                        if (cuerpo[k] == '=')
                        {
                            i = k;
                            break;
                        }
                }

            }
            if (cuerpo[i] == variable[0])
            {
                for (int k = 0; k < variable.Length; k++)
                {
                    if (!(cuerpo[i + k] == variable[k]))
                        break;

                    if (k == variable.Length - 1)
                    {
                        if (i > 0 && i < cuerpo.Length - 1)
                        {
                            Regex regex = new Regex(@"\W");
                            Match match_iz = regex.Match(cuerpo[i - 1].ToString());
                            Match match_de = regex.Match(cuerpo[i + k + 1].ToString());

                            if (match_iz.Success && match_de.Success)
                                {
                                cuerpo = cuerpo.Substring(0, i) + valor_variable + cuerpo.Substring(i + k + 1);
                                return cuerpo = Variable_X_Valor(cuerpo, variable, valor_variable);
                                } 
                        }

                        if (i == 0 || i == cuerpo.Length - 1)
                            cuerpo = cuerpo.Substring(0, i) + valor_variable + cuerpo.Substring(i + k + 1);
                    }
                }
            }
        }
        return cuerpo;
    }



}