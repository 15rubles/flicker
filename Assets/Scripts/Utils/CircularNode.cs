using JetBrains.Annotations;

namespace Utils
{
    public class CircularNode<T>
    {
        public T Value;
        [CanBeNull] public CircularNode<T> Next;

        public CircularNode(T value)
        {
            Value = value;
        }
    }
}