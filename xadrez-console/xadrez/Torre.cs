namespace xadrez;
using Tabuleiro;

public class Torre : Peca
{
     public Torre(Tabuleiro tab, Cor cor) :base(cor,  tab)
    {
    }
    public override string ToString()
    {
        return "T";
    }
}