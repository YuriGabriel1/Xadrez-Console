namespace xadrez;
using Tabuleiro; 
public class XadezPosicao
{
    public char coluna{get;set;}
    public int linha{get;set;}

    public XadezPosicao(char coluna, int linha)
    {
        this.coluna = coluna;
        this.linha = linha;
    }
    public Posicao Toposicao()
    {
        return new Posicao(8 - linha, coluna - 'a');
    }
    public override string ToString()
    {
        return "" + coluna + linha;
    }
}