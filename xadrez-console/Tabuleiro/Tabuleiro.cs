namespace Tabuleiro;

public class Tabuleiro
{
    public int Linhas{get;protected set;}
    public int Colunas{get;set;}
    private  Peca[,] pecas; // só ele pode modificar , mais  ninguém  pode modificar 

    public Tabuleiro(int Linhas, int Colunas)
    {
       this.Linhas = Linhas;
       this.Colunas = Colunas;
       pecas = new Peca[Linhas, Colunas]; 
    }

    public Peca peca(int Linhas, int Colunas)
    {
        return pecas[Linhas, Colunas];
    }

    public void ColocarPeca(Peca p, Posicao pos)
    {
        pecas[pos.Linha, pos.Coluna] = p;
        p.posicao = pos;
    }
}