namespace xadrez_console;

public class PartidaDeXadrez
{
    public Tabuleiro _tabuleiro { get; private set; }
    private int _turno;
    private Cor _jogadorAtual;
    public bool Terminada { get; private set; }

    public PartidaDeXadrez()
    {
        _tabuleiro = new Tabuleiro(8, 8);
        _turno = 1;
        _jogadorAtual = Cor.Branca;
        Terminada = false;
        ColocarPecas();
    }
    public void ExecMovimento(Posicao origem, Posicao destino)
    {
        Peca p = _tabuleiro.RetirarPeca(origem);
        p.IncrementarQtMovimentos();
        Peca pecaCapturada = _tabuleiro.RetirarPeca(destino);
        _tabuleiro.ColocarPeca(p, destino);

    }
    private void ColocarPecas()
    {
        _tabuleiro.ColocarPeca(new Rei(_tabuleiro, Cor.Branca), new PosicaoXadrez('d', 1).ToPosicao());
        _tabuleiro.ColocarPeca(new Torre(_tabuleiro, Cor.Branca), new PosicaoXadrez('d', 2).ToPosicao());
        _tabuleiro.ColocarPeca(new Torre(_tabuleiro, Cor.Branca), new PosicaoXadrez('c', 1).ToPosicao());
        _tabuleiro.ColocarPeca(new Torre(_tabuleiro, Cor.Branca), new PosicaoXadrez('c', 2).ToPosicao());
        _tabuleiro.ColocarPeca(new Torre(_tabuleiro, Cor.Branca), new PosicaoXadrez('e', 1).ToPosicao());
        _tabuleiro.ColocarPeca(new Torre(_tabuleiro, Cor.Branca), new PosicaoXadrez('e', 2).ToPosicao());

        _tabuleiro.ColocarPeca(new Rei(_tabuleiro, Cor.Preta), new PosicaoXadrez('d', 8).ToPosicao());
        _tabuleiro.ColocarPeca(new Torre(_tabuleiro, Cor.Preta), new PosicaoXadrez('d', 7).ToPosicao());
        _tabuleiro.ColocarPeca(new Torre(_tabuleiro, Cor.Preta), new PosicaoXadrez('c', 8).ToPosicao());
        _tabuleiro.ColocarPeca(new Torre(_tabuleiro, Cor.Preta), new PosicaoXadrez('c', 7).ToPosicao());
        _tabuleiro.ColocarPeca(new Torre(_tabuleiro, Cor.Preta), new PosicaoXadrez('e', 8).ToPosicao());
        _tabuleiro.ColocarPeca(new Torre(_tabuleiro, Cor.Preta), new PosicaoXadrez('e', 7).ToPosicao());
    }
}
