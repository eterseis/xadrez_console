using System.Security.Cryptography;

namespace xadrez_console;

class Program
{
    static void Main(string[] args)
    {
        // bool x = true;
        // Console.WriteLine(!x);
        // int x = 'c' - 'a';
        // int y = 'b' - 'a';
        PosicaoXadrez pos = new PosicaoXadrez('c', 7);
        Console.WriteLine(pos);
        System.Console.WriteLine(pos.ToPosicao());
        int[,] matriz = new int[3, 4];
    }
}
