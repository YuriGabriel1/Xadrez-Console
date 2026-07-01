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
            XadezPosicao pos = new XadezPosicao('a',1);
            Tabuleiro tab = new Tabuleiro(8, 8);
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(2, 3));
            tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(2, 1));
            tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(4, 5));

            Tela.ImprimirTabuleiro(tab);

        }
        catch (TabuleiroException e)
        {
            Console.WriteLine( e.Message);
        }

        Console.ReadLine();
    }
}