namespace xadrez;

using Tabuleiro;
public class PartidaDeXadrez
{
    public Tabuleiro tab { get; private set; }
    public int turno { get; private set; }
    public Cor jogadorAtual { get; set; }
    public bool terminada { get; private set; }
    private HashSet<Peca> pecas;
    private HashSet<Peca> capturadas;

    public PartidaDeXadrez()
    {
        tab = new Tabuleiro(8, 8);
        turno = 1;
        jogadorAtual = Cor.Branco;
        terminada = false;
        pecas = new HashSet<Peca>();
        capturadas = new HashSet<Peca>();
        colocarPecas();
    }

    public void ExecutaMovimento(Posicao origem, Posicao destino)
    {
        Peca p = tab.RetirarPeca(origem);
        p.IncrementraQteMovimentos();
        Peca peCapturada = tab.RetirarPeca(destino);
        tab.ColocarPeca(p, destino);
        if (peCapturada != null)
        {
            capturadas.Add(peCapturada);
        }
    }
    public void realizaJogada(Posicao origem, Posicao destino)
    {
        ExecutaMovimento(origem, destino);
        turno++;
        mudaJogador();
    }

    public void validarPosicaoDeorigem(Posicao pos)
    {
        if (tab.peca(pos) == null)
        {
            throw new TabuleiroException("Não exsite peça na posição de origem escolhida!");
        }
        if (jogadorAtual != tab.peca(pos).cor)
        {
            throw new TabuleiroException("A peça de origem escolhida não é sua!");
        }
        if (!tab.peca(pos).existeMovimentosPossiveis())
        {
            throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
        }
    }

    public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
    {
        if (!tab.peca(origem).podeMoverPara(destino))
        {
            throw new TabuleiroException("Posição de destino inválida!");
        }
    }

    private void mudaJogador()
    {
        if (jogadorAtual == Cor.Branco)
        {
            jogadorAtual = Cor.Preta;
        }
        else
        {
            jogadorAtual = Cor.Branco;
        }
    }

    public HashSet<Peca> pecasCapturadas(Cor cor)
    {
        HashSet<Peca> aux = new HashSet<Peca>();
        foreach (Peca x in capturadas)
        {
            if (x.cor == cor)
            {
                aux.Add(x);
            }
        }
        return aux;
    }

    public HashSet<Peca> pecasEmjogo(Cor cor)
    {
        HashSet<Peca> aux = new HashSet<Peca>();
        foreach (Peca x in  pecas)
        {
            if (x.cor == cor)
            {
                aux.Add(x);
            }
        }
        aux.ExceptWith(pecasCapturadas(cor));
        return aux;
    }

    public void colocarNovaPeca(char coluna, int linha, Peca peca)
    {
        tab.ColocarPeca(peca, new XadezPosicao(coluna, linha).Toposicao());
        pecas.Add(peca);
    }

    private void colocarPecas()
    {
        colocarNovaPeca('c', 1, new Torre(tab, Cor.Branco));
        colocarNovaPeca('c', 2, new Torre(tab, Cor.Branco));
        colocarNovaPeca('d', 2, new Torre(tab, Cor.Branco));
        colocarNovaPeca('e', 2, new Torre(tab, Cor.Branco));
        colocarNovaPeca('e', 1, new Torre(tab, Cor.Branco));
        colocarNovaPeca('d', 1, new Rei(tab, Cor.Branco));

        colocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
        colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
        colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
        colocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
        colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
        colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));

    }
}