using System;

namespace CompareXML
{
    public interface IIterator<T> : IDisposable
    {
        bool HasNext { get; }
        T Next();
        void Remove();
    }
}