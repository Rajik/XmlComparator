namespace CompareXML
{
    public interface IIterable<T>
    {
        IIterator<T> Iterator();
    }
}