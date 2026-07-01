namespace Tabuleiro;
public class Peca
{
    public Cor cor{get; protected set;} // associação feita  com Enum && só pode ser modificada por  ele mesmo ou pela sua subclasse.
    public Posicao posicao{get;set;} // associação entre classes  só pode ser modificado por ele mesmo ou sua sublaclasse.
    public int qtdMovimentos{get;protected set;}
    public Tabuleiro tab{get;protected set;}

    public Peca(Cor cor,Tabuleiro tab)
    {
        this.posicao = null;
        this.tab = tab;
        this.cor = cor;
        this.qtdMovimentos = 0;
    }
}