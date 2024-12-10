namespace Dominos;

public class Domino
{
    public int Left { get; }
    public int Right { get; }

    public Domino(int left, int right)
    {
        Left = left;
        Right = right;
    }

    public Domino Flip() => new(Right, Left);

    public override string ToString() => $"[{Left}|{Right}]";
        
    public override bool Equals(object obj)
    {
        if (obj is Domino other)
        {
            return (Left == other.Left && Right == other.Right) || 
                   (Left == other.Right && Right == other.Left);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Math.Min(Left, Right), Math.Max(Left, Right));
    }
}