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
    public bool xeque { get; private set; }

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
        if (testexequemate(adversario(jogadorAtual)))
        {
            terminada = true;
        }
        else
        {
            turno++;
            mudaJogador();
        }
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
        if (!tab.peca(origem).MovimentoPossivel(destino))
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

    public bool testexequemate(Cor cor)
    {
        if (!estaEmXeque(cor))
        {
            return false;
        }
        foreach (Peca x in pecasEmjogo(cor))
        {
            bool[,] mat = x.movimentosPossiveis();
            for (int i = 0; i < tab.Linhas; i++)
            {
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (mat[i, j])
                    {   Posicao origem = x.posicao;
                        Posicao destino = new Posicao(i, j);
                        Peca peCapturada = ExecutaMovimento(origem, destino);
                        bool testexeque = estaEmXeque(cor);
                        desfazMovimento(origem, destino, peCapturada);
                        if (!testexeque)
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
    public void colocarNovaPeca(char coluna, int linha, Peca peca)
    {
        tab.ColocarPeca(peca, new XadezPosicao(coluna, linha).Toposicao());
        pecas.Add(peca);
    }

    private void colocarPecas()
    {
       colocarNovaPeca('c',1, new Torre(tab,Cor.Branco));
       colocarNovaPeca('d',1, new Rei(tab,Cor.Branco));
       colocarNovaPeca('h',7, new Torre(tab,Cor.Branco));

       colocarNovaPeca('a',8, new Rei(tab, Cor.Preta));
       colocarNovaPeca('b',8, new Torre(tab,Cor.Preta));

    }
}