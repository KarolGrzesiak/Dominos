using Dominos;

TestDominoChain([
    new Domino(1, 2),
    new Domino(2, 3),
    new Domino(3, 1)
]);

TestDominoChain([
    new Domino(4, 1),
    new Domino(1, 2),
    new Domino(2, 3)
]);

TestDominoChain([
    new Domino(1, 2),
    new Domino(2, 4),
    new Domino(4, 3),
    new Domino(3, 1)
]);
return;


void TestDominoChain(List<Domino> dominos)
{
    Console.WriteLine("Input Dominos: " + string.Join(", ", dominos));
    var chain = DominoChainFinder.FindCircularChain(dominos);

    if (chain != null)
    {
        Console.WriteLine("Circular Chain Found: " + string.Join(" ", chain));
    }
    else
    {
        Console.WriteLine("No Circular Chain Possible");
    }

    Console.WriteLine();
}