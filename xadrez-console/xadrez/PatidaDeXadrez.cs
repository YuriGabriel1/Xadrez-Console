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
    public bool xeque{get;private set;}

    public PartidaDeXadrez()
    {
        tab = new Tabuleiro(8, 8);
        turno = 1;
        jogadorAtual = Cor.Branco;
        terminada = false;
        xeque = false;
        pecas = new HashSet<Peca>();
        capturadas = new HashSet<Peca>();
        colocarPecas();
    }

    public Peca ExecutaMovimento(Posicao origem, Posicao destino)
    {
        Peca p = tab.RetirarPeca(origem);
        p.IncrementraQteMovimentos();
        Peca peCapturada = tab.RetirarPeca(destino);
        tab.ColocarPeca(p, destino);
        if (peCapturada != null)
        {
            capturadas.Add(peCapturada);
        }
        return peCapturada;
    }

    public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
    {
        Peca p = tab.RetirarPeca(destino);
        p.decrementraQteMovimentos();
        if (pecaCapturada != null)
        {
            tab.ColocarPeca(pecaCapturada, destino);
            capturadas.Remove(pecaCapturada);
        }
        tab.ColocarPeca(p, origem);
    }

    public void realizaJogada(Posicao origem, Posicao destino)
    {
        Peca pecaCapturada = ExecutaMovimento(origem, destino);

        if (estaEmXeque(jogadorAtual))
        {
            desfazMovimento(origem, destino, pecaCapturada);
            throw new TabuleiroException("Você não pode ser colocar em xeque!");
        }

        if (estaEmXeque(adversario(jogadorAtual)))
        {
            xeque = true;
        }
        else
        {
            xeque = false;
        }
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
        foreach (Peca x in pecas)
        {
            if (x.cor == cor)
            {
                aux.Add(x);
            }
        }
        aux.ExceptWith(pecasCapturadas(cor));
        return aux;
    }

    private Cor adversario(Cor cor)
    {
        if (cor == Cor.Branco)
        {
            return Cor.Preta;
        }
        else
        {
            return Cor.Branco;
        }
    }

    private Peca rei(Cor cor)
    {
        foreach (Peca x in pecasEmjogo(cor))
        {
            if (x is Rei)
            {
                return x;
            }
        }
        return null;
    }

    // o rei desta cor esta em xeque?

    public bool estaEmXeque(Cor cor)
    {
        Peca R = rei(cor);
        if (R == null)
        {
            throw new TabuleiroException("Não tem rei da cor" + cor + " no tabuleiro!");
        }
        foreach (Peca x in pecasEmjogo(adversario(cor)))
        {
            bool[,] mat = x.movimentosPossiveis();
            if (mat[R.posicao.Linha, R.posicao.Coluna])
            {
                return true;
            }
        }
        return false;
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