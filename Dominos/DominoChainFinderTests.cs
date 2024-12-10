using Xunit;
using FluentAssertions;

namespace Dominos;

public class DominoChainFinderTests
{
    public static IEnumerable<object[]> ValidDominoChainTestData =>
        new List<object[]>
        {
            new object[]
            {
                new List<Domino>
                {
                    new(2, 1),
                    new(2, 3),
                    new(1, 3)
                },
                new List<Domino>
                {
                    new(1, 2),
                    new(2, 3),
                    new(3, 1)
                }
            },
            new object[]
            {
                new List<Domino>
                {
                    new(1, 1)
                },
                new List<Domino>
                {
                    new(1, 1)
                }
            },
            new object[]
            {
                new List<Domino>
                {
                    new(1, 2),
                    new(2, 3),
                    new(4, 3),
                    new(1, 5),
                    new(4, 5)
                },
                new List<Domino>
                {
                    new(1, 2),
                    new(2, 3),
                    new(3, 4),
                    new(4, 5),
                    new(5, 1)
                }
            },
        };

    public static IEnumerable<object[]> InvalidDominoChainTestData =>
        new List<object[]>
        {
            new object[]
            {
                new List<Domino>
                {
                    new(4, 1),
                    new(1, 2),
                    new(2, 3)
                }
            },
            new object[]
            {
                new List<Domino>
                {
                    new(1, 2),
                    new(3, 4),
                    new(5, 6)
                }
            },
            new object[]
            {
                new List<Domino>()
            },
            new object[]
            {
                new List<Domino>
                {
                    new(1, 2),
                }
            }
        };

    [Theory]
    [MemberData(nameof(ValidDominoChainTestData))]
    public void FindCircularChain_WhenValidChainIsPassed_ReturnsCircularChain(List<Domino> dominos,
        List<Domino> expected)
    {
        var chain = DominoChainFinder.FindCircularChain(dominos);

        chain.Should().NotBeNull();
        chain.Should().BeEquivalentTo(expected);
    }


    [Theory]
    [MemberData(nameof(InvalidDominoChainTestData))]
    public void FindCircularChain_WhenInvalidChainIsPassed_ReturnsNull(List<Domino> dominos)
    {
        var chain = DominoChainFinder.FindCircularChain(dominos);

        chain.Should().BeNull();
    }
}