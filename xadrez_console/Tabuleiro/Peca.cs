namespace xadrez_console;

public class Peca
{
    public Posicao Posicao { get; set; }
    public Cor Cor { get; protected set; }
    public int QntMovs { get; protected set; }
    public Tabuleiro Tabuleiro { get; protected set; }

    public Peca(Posicao posicao, Tabuleiro tabuleiro, Cor cor)
    {
        Posicao = posicao;
        Tabuleiro = tabuleiro;
        Cor = cor;
        QntMovs = 0;
    }
}
