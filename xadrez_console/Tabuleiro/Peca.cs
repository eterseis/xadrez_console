namespace xadrez_console;

public abstract class Peca
{
    public Posicao Posicao { get; set; }
    public Cor Cor { get; protected set; }
    public int QntMovs { get; protected set; }
    public Tabuleiro Tabuleiro { get; protected set; }

    public Peca(Tabuleiro tabuleiro, Cor cor)
    {
        Posicao = null;
        Tabuleiro = tabuleiro;
        Cor = cor;
        QntMovs = 0;
    }
    public void IncrementarQtMovimentos()
    {
        QntMovs++;
    }
    public virtual bool PoderMover(Posicao pos)
    {
        Peca p = Tabuleiro.Peca(pos);
        return p == null || p.Cor != this.Cor;
    }
    public abstract bool[,] MovimentosPossiveis();
}
