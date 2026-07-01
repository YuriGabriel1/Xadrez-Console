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
    public Peca peca(Posicao pos) // sobrecarga 
    {
        return pecas[pos.Linha, pos.Coluna];
    }
    public bool ExistePeca(Posicao pos)
    {   ValidarPosicao(pos);
        return peca(pos) != null;
    }

    public void ColocarPeca(Peca p, Posicao pos)
    {
        if (ExistePeca(pos)) // vai verifica se existe uma peça no lugar
        {
            throw new TabuleiroException("Já Existe uma Peça nessa posição"); // vai passar uma exerção que existe uma peça aqui!
        }
        pecas[pos.Linha, pos.Coluna] = p;
        p.posicao = pos;
    }
    public bool  PosicaoValida(Posicao pos)
    {
        if(pos.Linha <0 || pos.Linha >=Linhas || pos.Coluna <0 || pos.Coluna >= Colunas)
        {
            return false;
        }
        return true;
    }
    public void  ValidarPosicao(Posicao pos)
    {
        if (!PosicaoValida(pos))
        {
            throw new TabuleiroException("Posição inválida!");
        }
    }
}