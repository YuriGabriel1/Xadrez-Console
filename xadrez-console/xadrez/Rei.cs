namespace xadrez;

using Tabuleiro;

public class Rei : Peca
{
    public Rei(Tabuleiro tab, Cor cor) : base(cor, tab)
    {
    }
    public override string ToString()
    {
        return "R";
    }

    private  bool podeMover(Posicao pos)
    {
        Peca p = tab.peca(pos);
        return p == null || p.cor != cor;
    }
    public override  bool[,] movimentosPossiveis() // por eu qter criado um metodo abstrato a classe também tera que ser abstrata!
    {
        bool[,] mat = new bool [tab.Linhas, tab.Colunas];

        Posicao pos = new Posicao(0, 0);
         //acima
        pos.definirValores(posicao.Linha - 1, posicao.Coluna);
        if(tab.PosicaoValida(pos) && podeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }
        //ne
         pos.definirValores(posicao.Linha - 1, posicao.Coluna + 1);
        if(tab.PosicaoValida(pos) && podeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }
        //direita 
         pos.definirValores(posicao.Linha , posicao.Coluna + 1);
        if(tab.PosicaoValida(pos) && podeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }
        //se
         pos.definirValores(posicao.Linha + 1, posicao.Coluna +1 );
        if(tab.PosicaoValida(pos) && podeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }
        //abaixo
         pos.definirValores(posicao.Linha + 1, posicao.Coluna);
        if(tab.PosicaoValida(pos) && podeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }

        //so
         pos.definirValores(posicao.Linha + 1, posicao.Coluna - 1);
        if(tab.PosicaoValida(pos) && podeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }
        //esquerda
         pos.definirValores(posicao.Linha , posicao.Coluna - 1);
        if(tab.PosicaoValida(pos) && podeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }
        //no
         pos.definirValores(posicao.Linha - 1, posicao.Coluna - 1);
        if(tab.PosicaoValida(pos) && podeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }
        return mat;
    }
}