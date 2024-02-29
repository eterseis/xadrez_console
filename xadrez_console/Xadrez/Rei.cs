namespace xadrez_console;

public class Rei : Peca
{
    private PartidaDeXadrez _partida;
    public Rei(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : base(tabuleiro, cor)
    {
        _partida = partida;
    }

    private bool TesteTorreParaRoque(Posicao pos)
    {
        Peca p = Tabuleiro.Peca(pos);
        return p != null && p is Torre && p.Cor == Cor && p.QntMovs == 0;
    }
    public override bool[,] MovimentosPossiveis()
    {
        bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

        Posicao pos = new Posicao(0, 0);
        //acima
        pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }
        //ne
        pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }
        //direita
        pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }
        //se
        pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }
        //abaixo
        pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }
        //so
        pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }
        //esquerda
        pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }
        //no
        pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
        }
        // # Jogada especial: ROQUE
        if (QntMovs == 0 && !_partida.Xeque)
        {
            //jogada roque pequeno
            Posicao posT1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
            if (TesteTorreParaRoque(posT1))
            {
                Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
                if (Tabuleiro.Peca(p1) == null && Tabuleiro.Peca(p2) == null)
                {
                    mat[Posicao.Linha, Posicao.Coluna + 2] = true;
                }
            }
            //jogada roque grande
            Posicao posT2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
            if (TesteTorreParaRoque(posT2))
            {
                Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);
                if (Tabuleiro.Peca(p1) == null && Tabuleiro.Peca(p2) == null && Tabuleiro.Peca(p3) == null)
                {
                    mat[Posicao.Linha, Posicao.Coluna - 2] = true;
                }
            }
        }
        return mat;
    }
    public override string ToString()
    {
        return "R";
    }
}
