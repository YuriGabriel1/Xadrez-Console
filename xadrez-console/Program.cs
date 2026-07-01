namespace Xadez_Console;

using System;
using Tabuleiro; // chamando  o caminho da classe Posicao
using xadrez;
public class Program
{
    public static void Main(string[] args)
    {
        try // vai testar se o código esta funcionando.
        {
            Tabuleiro tab = new Tabuleiro(8, 8);
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(2, 9));
            tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(2, 4));

            Tela.ImprimirTabuleiro(tab);

        }
        catch (TabuleiroException e)
        {
            Console.WriteLine( e.Message);
        }

        Console.ReadLine();
    }
}