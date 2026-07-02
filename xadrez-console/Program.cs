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
                try
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.tab);
                    Console.WriteLine();
                    Console.WriteLine("Turno: " + partida.turno);
                    System.Console.WriteLine("Aguardando jogada:" + partida.jogadorAtual);

                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoxadrez().Toposicao();
                    partida.validarPosicaoDeorigem(origem);

                    bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();

                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.tab, posicoesPossiveis);

                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoxadrez().Toposicao();

                    partida.validarPosicaoDeDestino(origem,destino);

                    partida.realizaJogada(origem, destino);
                }
                catch (TabuleiroException e)
                {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                }
            }


        }
        catch (TabuleiroException e)
        {
            Console.WriteLine(e.Message);
        }

        Console.ReadLine();
    }
}