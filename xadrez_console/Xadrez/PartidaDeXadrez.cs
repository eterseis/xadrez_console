﻿using System.Diagnostics;
using System.Threading.Tasks.Dataflow;

namespace xadrez_console;

public class PartidaDeXadrez
{
    public Tabuleiro _tabuleiro { get; private set; }
    public int _turno { get; private set; }
    public Cor _jogadorAtual { get; private set; }
    public bool Terminada { get; private set; }
    private HashSet<Peca> Pecas { get; set; }
    private HashSet<Peca> Capturadas { get; set; }
    public bool Xeque { get; private set; }
    public Peca VulneravelEnPassant { get; private set; }

    public PartidaDeXadrez()
    {
        _tabuleiro = new Tabuleiro(8, 8);
        _turno = 1;
        _jogadorAtual = Cor.Branca;
        Terminada = false;
        VulneravelEnPassant = null;
        Pecas = new HashSet<Peca>();
        Capturadas = new HashSet<Peca>();
        ColocarPecas();
    }
    public Peca ExecMovimento(Posicao origem, Posicao destino)
    {
        Peca p = _tabuleiro.RetirarPeca(origem);
        p.IncrementarQtMovimentos();
        Peca pecaCapturada = _tabuleiro.RetirarPeca(destino);
        _tabuleiro.ColocarPeca(p, destino);
        if (pecaCapturada != null)
        {
            Capturadas.Add(pecaCapturada);
        }
        // #jogada especial Roque pequeno
        if (p is Rei && destino.Coluna == origem.Coluna + 2)
        {
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
            Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
            Peca t = _tabuleiro.RetirarPeca(origemT);
            t.IncrementarQtMovimentos();
            _tabuleiro.ColocarPeca(t, destinoT);
        }
        // # jogada especial roque grande
        if (p is Rei && destino.Coluna == origem.Coluna - 2)
        {
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
            Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
            Peca t = _tabuleiro.RetirarPeca(origemT);
            t.IncrementarQtMovimentos();
            _tabuleiro.ColocarPeca(t, destinoT);
        }
        // # jogada especial en passant
        if (p is Peao)
        {
            if (origem.Coluna != destino.Coluna && pecaCapturada == null)
            {
                Posicao posP;
                if (p.Cor == Cor.Branca)
                {
                    posP = new Posicao(destino.Linha + 1, destino.Coluna);
                }
                else
                {
                    posP = new Posicao(destino.Linha - 1, destino.Coluna);
                }
                pecaCapturada = _tabuleiro.RetirarPeca(posP);
                Capturadas.Add(pecaCapturada);
            }
        }

        return pecaCapturada;
    }
    public void RealizaJogada(Posicao origem, Posicao destino)
    {
        Peca pecaCapturada = ExecMovimento(origem, destino);
        if (EstaEmXeque(_jogadorAtual))
        {
            DesfazMovimento(origem, destino, pecaCapturada);
            throw new TabuleiroException("Você não pode se colocar em xeque!");
        }

        Peca p = _tabuleiro.Peca(destino);
        //#jogadaEspecialPromoção
        if (p is Peao)
        {
            if ((p.Cor == Cor.Branca && destino.Linha == 0) || (p.Cor == Cor.Preta && destino.Linha == 7))
            {
                p = _tabuleiro.RetirarPeca(destino);
                Pecas.Remove(p);
                Peca dama = new Dama(_tabuleiro, p.Cor);
                _tabuleiro.ColocarPeca(dama, destino);
                Pecas.Add(dama);
            }
        }

        if (EstaEmXeque(Adversaria(_jogadorAtual)))
        {
            Xeque = true;
        }
        else
        {
            Xeque = false;
        }
        if (TesteXequeMate(Adversaria(_jogadorAtual)))
        {
            Terminada = true;
        }
        else
        {
            _turno++;
            MudarJogador();
        }

        p = _tabuleiro.Peca(destino);
        // # jogada especial en passant
        if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
        {
            VulneravelEnPassant = p;
        }
        else
        {
            VulneravelEnPassant = null;
        }
    }
    public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
    {
        Peca p = _tabuleiro.RetirarPeca(destino);
        p.DecrementarQtMovimentos();
        if (pecaCapturada != null)
        {
            _tabuleiro.ColocarPeca(pecaCapturada, destino);
            Capturadas.Remove(pecaCapturada);
        }
        _tabuleiro.ColocarPeca(p, origem);

        // #jogada especial Roque pequeno
        if (p is Rei && destino.Coluna == origem.Coluna + 2)
        {
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
            Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
            Peca t = _tabuleiro.RetirarPeca(destinoT);
            t.DecrementarQtMovimentos();
            _tabuleiro.ColocarPeca(t, origemT);
        }
        // #jogada especial Roque grande
        if (p is Rei && destino.Coluna == origem.Coluna - 2)
        {
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
            Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
            Peca t = _tabuleiro.RetirarPeca(destinoT);
            t.DecrementarQtMovimentos();
            _tabuleiro.ColocarPeca(t, origemT);
        }
        // # jogada especial en passant
        if (p is Peao)
        {
            if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
            {
                Peca peao = _tabuleiro.RetirarPeca(destino);
                Posicao posP;
                if (p.Cor == Cor.Branca)
                {
                    posP = new Posicao(3, destino.Coluna);
                }
                else
                {
                    posP = new Posicao(4, destino.Coluna);
                }
                _tabuleiro.ColocarPeca(peao, posP);
            }
        }
    }
    public void ValidarOrigem(Posicao pos)
    {
        if (_tabuleiro.Peca(pos) == null)
        {
            throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
        }
        if (_jogadorAtual != _tabuleiro.Peca(pos).Cor)
        {
            throw new TabuleiroException("A peça de origem escolhida não é sua!");
        }
        if (!_tabuleiro.Peca(pos).ExisteMovimentosPossiveis())
        {
            throw new TabuleiroException("Não há movimentos possíveis para essa peça!");
        }
    }
    public void ValidarDestino(Posicao origem, Posicao destino)
    {
        if (!_tabuleiro.Peca(origem).MovimentoPossivel(destino))
        {
            throw new TabuleiroException("Posição de destino inválida!");
        }
    }
    private void MudarJogador()
    {
        if (_jogadorAtual == Cor.Branca)
        {
            _jogadorAtual = Cor.Preta;
        }
        else
        {
            _jogadorAtual = Cor.Branca;
        }
    }
    public HashSet<Peca> PecasCapturadas(Cor cor)
    {
        HashSet<Peca> aux = new HashSet<Peca>();
        foreach (var x in Capturadas)
        {
            if (x.Cor == cor)
            {
                aux.Add(x);
            }
        }
        return aux;
    }
    public HashSet<Peca> PecasEmJogo(Cor cor)
    {
        HashSet<Peca> aux = new HashSet<Peca>();
        foreach (var x in Pecas)
        {
            if (x.Cor == cor)
            {
                aux.Add(x);
            }
        }
        aux.ExceptWith(PecasCapturadas(cor));
        return aux;
    }
    private Cor Adversaria(Cor cor)
    {
        if (cor == Cor.Branca)
        {
            return Cor.Preta;
        }
        return Cor.Branca;
    }
    private Peca Rei(Cor cor)
    {
        foreach (var x in PecasEmJogo(cor))
        {
            if (x is Rei)
            {
                return x;
            }
        }
        return null;
    }
    public bool EstaEmXeque(Cor cor)
    {
        Peca rei = Rei(cor);
        if (rei == null)
        {
            throw new TabuleiroException($"Não há rei da cor {cor} no tabuleiro!");
        }
        foreach (var x in PecasEmJogo(Adversaria(cor)))
        {
            bool[,] mat = x.MovimentosPossiveis();
            if (mat[rei.Posicao.Linha, rei.Posicao.Coluna])
            {
                return true;
            }
        }
        return false;
    }
    public bool TesteXequeMate(Cor cor)
    {
        if (!EstaEmXeque(cor))
        {
            return false;
        }
        foreach (var x in PecasEmJogo(cor))
        {
            bool[,] mat = x.MovimentosPossiveis();
            for (int v = 0; v < _tabuleiro.Linhas; v++)
            {
                for (int k = 0; k < _tabuleiro.Colunas; k++)
                {
                    if (mat[v, k])
                    {
                        Posicao origem = x.Posicao;
                        Posicao destino = new Posicao(v, k);
                        Peca pecaCapturada = ExecMovimento(origem, destino);
                        bool testeXeque = EstaEmXeque(cor);
                        DesfazMovimento(origem, destino, pecaCapturada);
                        if (!testeXeque)
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
    public void ColocarNovaPeca(char coluna, int linha, Peca peca)
    {
        _tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
        Pecas.Add(peca);
    }
    private void ColocarPecas()
    {
        //Brancas
        ColocarNovaPeca('a', 1, new Torre(_tabuleiro, Cor.Branca));
        ColocarNovaPeca('b', 1, new Cavalo(_tabuleiro, Cor.Branca));
        ColocarNovaPeca('c', 1, new Bispo(_tabuleiro, Cor.Branca));
        ColocarNovaPeca('d', 1, new Dama(_tabuleiro, Cor.Branca));
        ColocarNovaPeca('e', 1, new Rei(_tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('f', 1, new Bispo(_tabuleiro, Cor.Branca));
        ColocarNovaPeca('g', 1, new Cavalo(_tabuleiro, Cor.Branca));
        ColocarNovaPeca('h', 1, new Torre(_tabuleiro, Cor.Branca));

        ColocarNovaPeca('a', 2, new Peao(_tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('b', 2, new Peao(_tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('c', 2, new Peao(_tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('d', 2, new Peao(_tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('e', 2, new Peao(_tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('f', 2, new Peao(_tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('g', 2, new Peao(_tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('h', 2, new Peao(_tabuleiro, Cor.Branca, this));

        //Pretas
        ColocarNovaPeca('a', 8, new Torre(_tabuleiro, Cor.Preta));
        ColocarNovaPeca('b', 8, new Cavalo(_tabuleiro, Cor.Preta));
        ColocarNovaPeca('c', 8, new Bispo(_tabuleiro, Cor.Preta));
        ColocarNovaPeca('d', 8, new Dama(_tabuleiro, Cor.Preta));
        ColocarNovaPeca('e', 8, new Rei(_tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('f', 8, new Bispo(_tabuleiro, Cor.Preta));
        ColocarNovaPeca('g', 8, new Cavalo(_tabuleiro, Cor.Preta));
        ColocarNovaPeca('h', 8, new Torre(_tabuleiro, Cor.Preta));

        ColocarNovaPeca('a', 7, new Peao(_tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('b', 7, new Peao(_tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('c', 7, new Peao(_tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('d', 7, new Peao(_tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('e', 7, new Peao(_tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('f', 7, new Peao(_tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('g', 7, new Peao(_tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('h', 7, new Peao(_tabuleiro, Cor.Preta, this));
    }
}
