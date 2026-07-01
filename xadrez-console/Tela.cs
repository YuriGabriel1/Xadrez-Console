namespace Xadez_Console;

using System.Security.Cryptography.X509Certificates;
using Tabuleiro;

public class Tela
{
    public static void ImprimirTabuleiro(Tabuleiro tab)
    {
        for (int i = 0; i < tab.Linhas; i++)
        {
            Console.Write(8 - i + " ");
            for (int j = 0; j < tab.Colunas; j++)
            {
                if (tab.peca(i, j) == null)
                {
                    Console.Write("- ");
                }
                else
                {
                  ImprimirPeca(tab.peca(i,j));
                }
            }
            System.Console.WriteLine();
        }
        Console.WriteLine("  a b c d e f g h");
    }

    public static void ImprimirPeca(Peca peca)
    {
        if (peca.cor == Cor.Branco)
        {
            Console.WriteLine(peca);
        }
        else
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(peca);
            Console.ForegroundColor = aux;
        }
    }
}