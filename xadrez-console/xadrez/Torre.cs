namespace xadrez;

using Tabuleiro;

public class Torre : Peca
{
    public Torre(Tabuleiro tab, Cor cor) : base(cor, tab)
    {
    }
    public override string ToString()
    {
        return "T";
    }
    private bool podeMover(Posicao pos)
    {
        Peca p = tab.peca(pos);
        return p == null || p.cor != cor;
    }
    public override bool[,] movimentosPossiveis() // por eu qter criado um metodo abstrato a classe também tera que ser abstrata!
    {
        bool[,] mat = new bool[tab.Linhas, tab.Colunas];

        Posicao pos = new Posicao(0, 0);

        //cima
        pos.definirValores(posicao.Linha - 1, posicao.Coluna);
        while (tab.PosicaoValida(pos) && podeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
            {
                break;
            }
            pos.Linha = pos.Linha - 1;
        }
        //abaixo
         pos.definirValores(posicao.Linha + 1, posicao.Coluna);
        while (tab.PosicaoValida(pos) && podeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
            {
                break;
            }
            pos.Linha = pos.Linha  + 1;
        }

        //direita

         pos.definirValores(posicao.Linha, posicao.Coluna + 1);
        while (tab.PosicaoValida(pos) && podeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
            {
                break;
            }
            pos.Coluna = pos.Coluna + 1;
        }

        //esquerda 
         pos.definirValores(posicao.Linha, posicao.Coluna - 1);
        while (tab.PosicaoValida(pos) && podeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
            {
                break;
            }
            pos.Coluna = pos.Coluna - 1;
        }
        return mat;
    }
}