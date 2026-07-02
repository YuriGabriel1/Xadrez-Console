namespace Xadez_Console;

using Tabuleiro;
using xadrez;
public class Tela
{

    public static void imprimirPartida(PartidaDeXadrez partida)
    {
        ImprimirTabuleiro(partida.tab);
        Console.WriteLine();
        imprimirpecasCapturadas(partida);
        Console.WriteLine();
        Console.WriteLine("Turno: " + partida.turno);
        Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual);
    }

    public static void imprimirpecasCapturadas(PartidaDeXadrez partida)
    {
        Console.WriteLine("Peças capturadas:");
        System.Console.Write("Brancas:");
        imprimirConjuto(partida.pecasCapturadas(Cor.Branco));
        System.Console.WriteLine();
        System.Console.Write("Pretas:");
        ConsoleColor aux = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        imprimirConjuto(partida.pecasCapturadas(Cor.Preta));
        Console.ForegroundColor = aux;
        System.Console.WriteLine();
    }
    public static void imprimirConjuto(HashSet<Peca> conjunto)
    {
        Console.Write("[");
        foreach (Peca x in conjunto)
        {
            Console.Write(x + " ");
        }
        Console.Write("]");
    }
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

    public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
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
                    Console.BackgroundColor = FundoAlterado;
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
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
            Console.Write(" ");
        }
    }
}