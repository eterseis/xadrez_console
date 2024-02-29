namespace xadrez_console;

public class Bispo : Peca
{
    public Bispo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }
    public override bool[,] MovimentosPossiveis()
    {
        bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
        Posicao pos = new Posicao(0, 0);

        //NO
        pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor)
            {
                break;
            }
            pos.DefinirValores(pos.Linha - 1, pos.Coluna - 1);
        }
        //NE
        pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor)
            {
                break;
            }
            pos.DefinirValores(pos.Linha - 1, pos.Coluna + 1);
        }
        //SE
        pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor)
            {
                break;
            }
            pos.DefinirValores(pos.Linha + 1, pos.Coluna + 1);
        }
        //SO
        pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor)
            {
                break;
            }
            pos.DefinirValores(pos.Linha + 1, pos.Coluna - 1);
        }
        return mat;
    }
    public override string ToString()
    {
        return "B";
    }
}
