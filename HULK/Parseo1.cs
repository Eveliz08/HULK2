public class Utiles1
{

    public static int Parentesis_que_cierra(int inicio, string instruction)
    {
        int final = 0;
        bool cerró = true;
        for (int i = inicio + 1; i < instruction.Length; i++)
        {
            if (instruction[i] == '"')
                i = Be_Ignorant(instruction, i);

            if (instruction[i] == '(')
            {
                final++;
                cerró = false;
            }
            if (instruction[i] == ')')
            {
                cerró = true;
                if (final == 0)
                {
                    final = i;
                    break;
                }
                else
                    final--;
            }
        }
        if (cerró == true)
            return final;
        else
            return 0;

    }


    public static int Be_Ignorant(string instruction, int i)
    {
        int k = i + 1;

        while (k < instruction.Length)
        {
            if (instruction[k] == '"')
            {
                i = k;
                break;
            }
            k++;
        }
        return k;
    }
}