namespace Xadez_Console;
using System;
using Tabuleiro; // chamando  o caminho da classe Posicao
public class Program 
{
    public static void Main(string[] args)
    {
        Tabuleiro tab = new Tabuleiro(8, 8);
        Tela.ImprimirTabuleiro(tab);

        Console.ReadLine();
    }
}