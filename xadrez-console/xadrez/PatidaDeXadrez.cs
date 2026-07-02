namespace xadrez;

using Tabuleiro;
public class PartidaDeXadrez
{
    public Tabuleiro tab { get; private set; }
    private int turno;
    private Cor jogadorAtual;
    public bool terminada{get;private set;}

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