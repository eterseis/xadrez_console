using System.Security.Cryptography;

namespace xadrez_console;

class Program
{
    static void Main(string[] args)
    {
        // bool x = true;
        // Console.WriteLine(!x);
        try
        {
            Tabuleiro tabuleiro = new Tabuleiro(8, 8);
            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(0, 0));
            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(1, 3));
            tabuleiro.ColocarPeca(new Rei(tabuleiro, Cor.Preta), new Posicao(8, 8));

            Tela.ImprimirTabuleiro(tabuleiro);
        }
        catch (TabuleiroException x)
        {
            Console.WriteLine(x.Message);
        }

        int[,] matriz = new int[3, 4];
    }
}
