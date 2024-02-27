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
    public bool ExisteMovimentosPossiveis()
    {
        bool[,] mat = MovimentosPossiveis();
        for (int v = 0; v < Tabuleiro.Linhas; v++)
        {
            for (int j = 0; j < Tabuleiro.Colunas; j++)
            {
                if (mat[v, j])
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool PodeMoverPara(Posicao pos)
    {
        return MovimentosPossiveis()[pos.Linha, pos.Coluna];
    }
    public abstract bool[,] MovimentosPossiveis();
}
