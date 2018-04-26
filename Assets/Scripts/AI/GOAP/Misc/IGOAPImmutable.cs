namespace AI.GOAP
{
    public interface IGOAPImmutable<T> where T : IGOAPImmutable<T>
    {
        T Copy();
    }
}