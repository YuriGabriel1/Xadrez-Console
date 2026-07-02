namespace Xadez_Console;
using Tabuleiro;
using xadrez;
public class Tela
{
    public static void ImprimirTabuleiro(Tabuleiro tab)
    {
        for (int i = 0; i < tab.Linhas; i++)
        {
            Console.Write(8 - i + " ");
            for (int j = 0; j < tab.Colunas; j++)
            {
                ImprimirPeca(tab.peca(i, j));
                

            }
            System.Console.WriteLine();
        }
        Console.WriteLine("  a b c d e f g h");
    }

     public static void ImprimirTabuleiro(Tabuleiro tab, bool[,]posicoesPossiveis)
    {   
        ConsoleColor fundoOriginal = Console.BackgroundColor;
        ConsoleColor FundoAlterado = ConsoleColor.DarkGray;

        for (int i = 0; i < tab.Linhas; i++)
        {
            Console.Write(8 - i + " ");
            for (int j = 0; j < tab.Colunas; j++)
            {
                if (posicoesPossiveis[i, j])
                {
                    Console.BackgroundColor =  FundoAlterado;
                }
                else
                {
                    
                    Console.BackgroundColor = fundoOriginal;
                }
                ImprimirPeca(tab.peca(i, j));
                Console.BackgroundColor = fundoOriginal;

            }
            System.Console.WriteLine();
        }
        Console.WriteLine("  a b c d e f g h");
        Console.BackgroundColor = fundoOriginal;
    }

    public static XadezPosicao LerPosicaoxadrez()
    {
        string s = Console.ReadLine();
        char coluna = s[0];
        int linha = int.Parse(s[1] + "");
        return new XadezPosicao(coluna, linha);
    }

    public static void ImprimirPeca(Peca peca)
    {
        if (peca == null)
        {
            Console.Write("- ");
        }
        else
        {


            if (peca.cor == Cor.Branco)
            {
                Console.Write(peca);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
            Console.Write(" ");
        }
    }
}