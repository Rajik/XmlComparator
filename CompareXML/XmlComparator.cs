using System;
using System.Linq;
using System.Xml.Linq;

//Expected => Doc1
//Actual => Doc2

namespace CompareXML
{
    public class XmlComparator
    {
        private static bool flag;
        public static bool Compare(XElement doc1, XElement doc2)
        {
//            var enumerator1 = doc1.DescendantsAndSelf().GetEnumerator();
//            var enumerator2 = doc2.DescendantsAndSelf().GetEnumerator();
            var enumerator1 = new EnumeratorAdapter<XElement>(doc1.DescendantsAndSelf().GetEnumerator());
            var enumerator2 = new EnumeratorAdapter<XElement>(doc2.DescendantsAndSelf().GetEnumerator());
            while (enumerator1.HasNext && enumerator2.HasNext)
                    {
                        var element1 = enumerator1.Next();
                        var element2 = enumerator2.Next();

                        if (element1 != null && element2 != null)
                        {
                            flag = element1.Name == element2.Name;

                            if (!flag)
                            {
                                Console.Write("Expected node {0} was not present in actual for parent {1}",
                                              element1.Name.LocalName, element1.Parent != null ? element1.Parent.Name.LocalName : null);
                                return false;
                            }

                            if (element1.HasElements)
                            {
                                continue;
                            }
                            string xElementName = element2.Name.LocalName;
                            string xelementValue = element2.Value;
                            if ((!string.IsNullOrEmpty(element1.Value) ||
                                 !string.IsNullOrEmpty(xelementValue)) && xelementValue != element1.Value)
                            {
                                if (!element2.HasElements)
                                    Console.Write("Expected {0}: {1} but actual was {2}:{3} \n Parent Node was {4}",
                                                  xElementName, xelementValue,
                                                  element1.Name.LocalName,
                                                  element1.Value, element1.Parent.Name.LocalName);
                                else
                                    Console.Write(
                                        "Expected does not contain the node {0} that is in actual for parent {1}",
                                        element2.Elements().First().Name.LocalName, element2.Name.LocalName);
                                return false;
                            }
                        }

                        
                    }

                    if (!enumerator1.HasNext && enumerator2.HasNext)
                    {
                        var element2 = enumerator2.Next();
                        if (element2 != null)
                            Console.Write(
                                "Expected does not contain the node {0} that is in actual for parent {1}",
                                element2.Name.LocalName, element2.Parent != null ? element2.Parent.Name.LocalName : null);
                        return false;
                    }

                    if (enumerator1.HasNext && !enumerator2.HasNext)
                    {
                        var element1 = enumerator1.Next();
                        Console.Write("Expected node {0} was not present in actual for parent {1}",
                                              element1.Name.LocalName, element1.Parent != null ? element1.Parent.Name.LocalName : null);
                        return false;
                    }

                    if (!enumerator1.HasNext && !enumerator2.HasNext)
                    {
                        var element1 = enumerator1.Next();
                        var element2 = enumerator2.Next();

                        flag = element1.Name == element2.Name;
                        
                        if (element1 != null && element2 != null)
                        if (!flag)
                        {
                            Console.Write("Expected node {0} was not present in actual for parent {1}",
                                          element1.Name.LocalName, element1.Parent != null ? element1.Parent.Name.LocalName : null);
                            return false;
                        }
                    }

            return flag;
        }
    }

//    <head>
//    <body>
//    <flower> Rose </flower>
//    <flower> Lily </flower>
//    <flower> Jasmine </flower>
//    </body>
//    </head>

    public interface IIterable<T>
    {
        IIterator<T> Iterator();
    }

    // Mimics Java's Iterator interface - but
    // implements IDisposable for the sake of
    // parity with IEnumerator.
}
