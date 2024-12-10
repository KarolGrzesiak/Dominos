using System.Diagnostics.CodeAnalysis;

namespace Dominos;

public static class DominoChainFinder
{
    public static IReadOnlyCollection<Domino>? FindCircularChain(IReadOnlyCollection<Domino> dominos)
    {
        if (dominos.Count == 0) return null;

        var dominoList = dominos.ToList();
        for (var i = 0; i < dominoList.Count; i++)
        {
            var startDomino = dominoList[i];
            foreach (var initialDomino in new[] { startDomino, startDomino.Flip() })
            {
                Swap(dominoList, 0, i);

                if (TryCreateChain(initialDomino, dominoList, 1, out var chain))
                {
                    if (chain.First().Left == chain.Last().Right)
                        return chain;
                }

                Swap(dominoList, 0, i);
            }
        }

        return null;
    }

    private static bool TryCreateChain(Domino current, List<Domino> dominos, int depth,
        [NotNullWhen(true)] out List<Domino>? chain)
    {
        if (depth == dominos.Count)
        {
            chain = [current];
            return true;
        }

        for (var i = depth; i < dominos.Count; i++)
        {
            var nextDomino = dominos[i];
            foreach (var candidate in new[] { nextDomino, nextDomino.Flip() })
            {
                if (current.Right == candidate.Left)
                {
                    Swap(dominos, depth, i);

                    if (TryCreateChain(candidate, dominos, depth + 1, out chain))
                    {
                        chain.Insert(0, current);
                        return true;
                    }

                    Swap(dominos, depth, i);
                }
            }
        }

        chain = null;
        return false;
    }

    private static void Swap(List<Domino> list, int i, int j)
    {
        if (i == j) return;
        (list[i], list[j]) = (list[j], list[i]);
    }
}