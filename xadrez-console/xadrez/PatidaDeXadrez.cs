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
    public   Peca vulneravelEnPassant{get;private set;}

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
        //jogadaEspecial roque pequeno
        if (p is Rei && destino.Coluna == origem.Coluna + 2)
        {
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
            Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
            Peca T = tab.RetirarPeca(origemT);
            T.IncrementraQteMovimentos();
            tab.ColocarPeca(T, destinoT);
        }
        //jogadaEspecial roque Grande
        if (p is Rei && destino.Coluna == origem.Coluna - 2)
        {
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
            Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
            Peca T = tab.RetirarPeca(origemT);
            T.IncrementraQteMovimentos();
            tab.ColocarPeca(T, destinoT);
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

        //jogadaEspecial roque pequeno
        if (p is Rei && destino.Coluna == origem.Coluna + 2)
        {
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
            Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
            Peca T = tab.RetirarPeca(destinoT);
            T.decrementraQteMovimentos();
            tab.ColocarPeca(T, origemT);
        }
        //JogadaEspecial roque Grande
        if (p is Rei && destino.Coluna == origem.Coluna - 2)
        {
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
            Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
            Peca T = tab.RetirarPeca(destinoT);
            T.decrementraQteMovimentos();
            tab.ColocarPeca(T, origemT);
        }
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

        Peca p = tab.peca(destino);

        //jogadaEspecial en passant
        if(p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
        {
            vulneravelEnPassant = p;
        }
        else
        {
            vulneravelEnPassant = null;
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
                    {
                        Posicao origem = x.posicao;
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
        colocarNovaPeca('a', 1, new Torre(tab, Cor.Branco));
        colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branco));
        colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branco));
        colocarNovaPeca('d', 1, new Dama(tab, Cor.Branco));
        colocarNovaPeca('e', 1, new Rei(tab, Cor.Branco, this));
        colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branco));
        colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branco));
        colocarNovaPeca('h', 1, new Torre(tab, Cor.Branco));
        colocarNovaPeca('a', 2, new Peao(tab, Cor.Branco, this));
        colocarNovaPeca('b', 2, new Peao(tab, Cor.Branco, this));
        colocarNovaPeca('c', 2, new Peao(tab, Cor.Branco, this));
        colocarNovaPeca('d', 2, new Peao(tab, Cor.Branco, this));
        colocarNovaPeca('e', 2, new Peao(tab, Cor.Branco, this));
        colocarNovaPeca('f', 2, new Peao(tab, Cor.Branco, this));
        colocarNovaPeca('g', 2, new Peao(tab, Cor.Branco, this));
        colocarNovaPeca('h', 2, new Peao(tab, Cor.Branco, this));

        colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
        colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
        colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
        colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
        colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
        colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
        colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
        colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
        colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
        colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
        colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
        colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
        colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
        colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
        colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
        colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));

    }
}