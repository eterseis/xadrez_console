﻿namespace xadrez_console;

public class Torre : Peca
{
    public Torre(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

    public override bool[,] MovimentosPossiveis()
    {
        bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

        Posicao pos = new Posicao(0, 0);
        //acima
        pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != this.Cor)
            {
                break;
            }
            pos.Linha = pos.Linha - 1;
        }
        //abaixo
        pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != this.Cor)
            {
                break;
            }
            pos.Linha = pos.Linha + 1;
        }
        //direita
        pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != this.Cor)
            {
                break;
            }
            pos.Coluna = pos.Coluna + 1;
        }
        //esquerda
        pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != this.Cor)
            {
                break;
            }
            pos.Coluna = pos.Coluna - 1;
        }
        return mat;
    }
    public override string ToString()
    {
        return "T";
    }
}
