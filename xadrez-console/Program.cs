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
            PartidaDeXadrez partida = new PartidaDeXadrez();

           while (!partida.terminada)
            {
                Console.Clear();
                Console.WriteLine();
                Tela.ImprimirTabuleiro(partida.tab);
                Console.WriteLine();
                
                Console.Write("Origem:");
                Posicao origem = Tela.LerPosicaoxadrez().Toposicao();
                Console.Write("Destino:");
                Posicao destino = Tela.LerPosicaoxadrez().Toposicao();

                partida.ExecutaMovimento(origem, destino);
            }
            

        }
        catch (TabuleiroException e)
        {
            Console.WriteLine( e.Message);
        }

        Console.ReadLine();
    }
}