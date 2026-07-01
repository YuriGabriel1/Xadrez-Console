namespace Xadez_Console;

using System;
using Tabuleiro; // chamando  o caminho da classe Posicao
using xadrez;
public class Program
{
    public static void Main(string[] args)
    {
       XadezPosicao pos = new XadezPosicao('a',1);
       System.Console.WriteLine(pos);
        Console.WriteLine(pos.Toposicao());

        Console.ReadLine();
    }
}