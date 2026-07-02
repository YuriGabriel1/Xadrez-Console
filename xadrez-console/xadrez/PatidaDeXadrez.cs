namespace xadrez;

using Tabuleiro;
public class PartidaDeXadrez
{
    public Tabuleiro tab { get; private set; }
    public int turno { get; private set; }
    public Cor jogadorAtual { get; set; }
    public bool terminada { get; private set; }

    public PartidaDeXadrez()
    {
        tab = new Tabuleiro(8, 8);
        turno = 1;
        jogadorAtual = Cor.Branco;
        terminada = false;
        colocarPecas();
    }

    public void ExecutaMovimento(Posicao origem, Posicao destino)
    {
        Peca p = tab.RetirarPeca(origem);
        p.IncrementraQteMovimentos();
        Peca peCapturada = tab.RetirarPeca(destino);
        tab.ColocarPeca(p, destino);
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

    private void colocarPecas()
    {

        tab.ColocarPeca(new Torre(tab, Cor.Branco), new XadezPosicao('c', 1).Toposicao());
        tab.ColocarPeca(new Torre(tab, Cor.Branco), new XadezPosicao('c', 2).Toposicao());
        tab.ColocarPeca(new Torre(tab, Cor.Branco), new XadezPosicao('d', 2).Toposicao());
        tab.ColocarPeca(new Torre(tab, Cor.Branco), new XadezPosicao('e', 2).Toposicao());
        tab.ColocarPeca(new Torre(tab, Cor.Branco), new XadezPosicao('e', 1).Toposicao());
        tab.ColocarPeca(new Rei(tab, Cor.Branco), new XadezPosicao('d', 1).Toposicao());

        tab.ColocarPeca(new Torre(tab, Cor.Preta), new XadezPosicao('c', 7).Toposicao());
        tab.ColocarPeca(new Torre(tab, Cor.Preta), new XadezPosicao('c', 8).Toposicao());
        tab.ColocarPeca(new Torre(tab, Cor.Preta), new XadezPosicao('d', 7).Toposicao());
        tab.ColocarPeca(new Torre(tab, Cor.Preta), new XadezPosicao('e', 7).Toposicao());
        tab.ColocarPeca(new Torre(tab, Cor.Preta), new XadezPosicao('e', 8).Toposicao());
        tab.ColocarPeca(new Rei(tab, Cor.Preta), new XadezPosicao('d', 8).Toposicao());

    }
}