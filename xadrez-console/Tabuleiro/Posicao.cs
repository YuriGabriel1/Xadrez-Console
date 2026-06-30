namespace Tabuleiro;

public class Posicao
{
    public int Linha{get;set;} 
    public int  Coluna {get;set;}

    public Posicao(int Linha, int Coluna) // construtor criado
    {
        this.Linha = Linha;
        this.Coluna = Coluna; //this aqui se refere a (autoreferência)
    }
    public override string ToString() // retorna na tela o que eu coloquei 
    {
        return  + Linha
                + ","
                +Coluna;
    }
}