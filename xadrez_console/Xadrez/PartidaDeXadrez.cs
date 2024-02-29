using System.Diagnostics;
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

    public PartidaDeXadrez()
    {
        _tabuleiro = new Tabuleiro(8, 8);
        _turno = 1;
        _jogadorAtual = Cor.Branca;
        Terminada = false;
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
        if (!_tabuleiro.Peca(origem).PodeMoverPara(destino))
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
        ColocarNovaPeca('d', 1, new Rei(_tabuleiro, Cor.Branca));
        ColocarNovaPeca('c', 1, new Torre(_tabuleiro, Cor.Branca));
        ColocarNovaPeca('h', 7, new Torre(_tabuleiro, Cor.Branca));


        ColocarNovaPeca('a', 8, new Rei(_tabuleiro, Cor.Preta));
        ColocarNovaPeca('b', 8, new Torre(_tabuleiro, Cor.Preta));
    }
}
