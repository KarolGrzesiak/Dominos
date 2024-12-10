namespace Dominos;

internal static class DominoChainFinder
{
    public static IReadOnlyCollection<Domino>? FindCircularChain(IReadOnlyCollection<Domino> dominos)
    {
        if (dominos.Count == 0 || dominos == null) return null;

        foreach (var startDomino in dominos)
        {
            foreach (var initialDomino in new[] { startDomino, startDomino.Flip() })
            {
                var remainingDominos = new List<Domino>(dominos);
                remainingDominos.Remove(startDomino);

                var chain = TryCreateChain(initialDomino, remainingDominos);
                if (chain != null)
                {
                    chain.Insert(0, initialDomino);
                    if (chain.First().Left == chain.Last().Right)
                        return chain;
                }
            }
        }

        return null;
    }

    private static List<Domino>? TryCreateChain(Domino current, List<Domino> remainingDominos)
    {
        if (remainingDominos.Count == 0)
            return [];

        for (var i = 0; i < remainingDominos.Count; i++)
        {
            var nextDomino = remainingDominos[i];

            if (current.Right == nextDomino.Left)
            {
                var newRemainingDominos = new List<Domino>(remainingDominos);
                newRemainingDominos.RemoveAt(i);

                var subChain = TryCreateChain(nextDomino, newRemainingDominos);
                if (subChain != null)
                {
                    subChain.Insert(0, nextDomino);
                    return subChain;
                }
            }

            var flippedNextDomino = nextDomino.Flip();
            if (current.Right == flippedNextDomino.Left)
            {
                var newRemainingDominos = new List<Domino>(remainingDominos);
                newRemainingDominos.RemoveAt(i);

                var subChain = TryCreateChain(flippedNextDomino, newRemainingDominos);
                if (subChain != null)
                {
                    subChain.Insert(0, flippedNextDomino);
                    return subChain;
                }
            }
        }

        return null;
    }
}