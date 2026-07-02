namespace Tabuleiro;

public abstract class Peca
{
    public Cor cor { get; protected set; } // associação feita  com Enum && só pode ser modificada por  ele mesmo ou pela sua subclasse.
    public Posicao posicao { get; set; } // associação entre classes  só pode ser modificado por ele mesmo ou sua sublaclasse.
    public int qtdMovimentos { get; protected set; }
    public Tabuleiro tab { get; protected set; }

    public Peca(Cor cor, Tabuleiro tab)
    {
        this.posicao = null;
        this.tab = tab;
        this.cor = cor;
        this.qtdMovimentos = 0;
    }

    public void IncrementraQteMovimentos()
    {
        qtdMovimentos++;
    }

    public bool existeMovimentosPossiveis()
    {
        bool[,] mat = movimentosPossiveis();
        for (int i = 0; i < tab.Linhas; i++)
        {
            for (int j = 0; j < tab.Linhas; j++)
            {
                if (mat[i, j])
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool podeMoverPara(Posicao pos)
    {
        return movimentosPossiveis()[pos.Linha, pos.Coluna];
    }
    public abstract bool[,] movimentosPossiveis(); // por eu qter criado um metodo abstrato a classe também tera que ser abstrata!

}