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
    public void ExecMovimento(Posicao origem, Posicao destino)
    {
        Peca p = _tabuleiro.RetirarPeca(origem);
        p.IncrementarQtMovimentos();
        Peca pecaCapturada = _tabuleiro.RetirarPeca(destino);
        _tabuleiro.ColocarPeca(p, destino);
        if (pecaCapturada != null)
        {
            Capturadas.Add(pecaCapturada);
        }

    }
    public void RealizaJogada(Posicao origem, Posicao destino)
    {
        ExecMovimento(origem, destino);
        _turno++;
        MudarJogador();

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
    public void ColocarNovaPeca(char coluna, int linha, Peca peca)
    {
        _tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
        Pecas.Add(peca);
    }
    private void ColocarPecas()
    {
        ColocarNovaPeca('d', 1, new Rei(_tabuleiro, Cor.Branca));
        ColocarNovaPeca('d', 2, new Torre(_tabuleiro, Cor.Branca));
        ColocarNovaPeca('c', 1, new Torre(_tabuleiro, Cor.Branca));
        ColocarNovaPeca('c', 2, new Torre(_tabuleiro, Cor.Branca));
        ColocarNovaPeca('e', 1, new Torre(_tabuleiro, Cor.Branca));
        ColocarNovaPeca('e', 2, new Torre(_tabuleiro, Cor.Branca));

        ColocarNovaPeca('d', 8, new Rei(_tabuleiro, Cor.Preta));
        ColocarNovaPeca('d', 7, new Torre(_tabuleiro, Cor.Preta));
        ColocarNovaPeca('c', 8, new Torre(_tabuleiro, Cor.Preta));
        ColocarNovaPeca('c', 7, new Torre(_tabuleiro, Cor.Preta));
        ColocarNovaPeca('e', 8, new Torre(_tabuleiro, Cor.Preta));
        ColocarNovaPeca('e', 7, new Torre(_tabuleiro, Cor.Preta));
    }
}
