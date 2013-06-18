using System;
using System.Collections.Generic;

namespace CompareXML
{
    public sealed class EnumeratorAdapter<T> : IIterator<T>
    {
        private readonly IEnumerator<T> enumerator;

        private bool fetchedNext = false;
        private bool nextAvailable = false;
        private T next;

        public EnumeratorAdapter(IEnumerator<T> enumerator)
        {
            this.enumerator = enumerator;
        }

        public bool HasNext
        {
            get
            {
                CheckNext();
                return nextAvailable;
            }
        }

        public T Next()
        {
            CheckNext();
            if (!nextAvailable)
            {
                throw new InvalidOperationException();
            }
            fetchedNext = false; // We've consumed this now
            return next;
        }

        void CheckNext()
        {
            if (!fetchedNext)
            {
                nextAvailable = enumerator.MoveNext();
                if (nextAvailable)
                {
                    next = enumerator.Current;
                }
                fetchedNext = true;
            }
        }

        public void Remove()
        {
            throw new NotSupportedException();
        }

        public void Dispose()
        {
            enumerator.Dispose();
        }
    }
}