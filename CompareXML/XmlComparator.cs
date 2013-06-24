using System;
using System.Linq;
using System.Xml.Linq;

//Expected => Doc1
//Actual => Doc2

namespace CompareXML
{
    public class XmlComparator
    {
        private static bool isMatchSuccessful;
        public static bool Compare(XElement expected, XElement actual)
        {
            var expectedNodeEnumerator = new EnumeratorAdapter<XElement>(expected.DescendantsAndSelf().GetEnumerator());
            var actualNodeEnumerator = new EnumeratorAdapter<XElement>(actual.DescendantsAndSelf().GetEnumerator());
            while (expectedNodeEnumerator.HasNext && actualNodeEnumerator.HasNext)
            {
                var expectedElement = expectedNodeEnumerator.Next();
                var actualElement = actualNodeEnumerator.Next();

                if (expectedElement != null && actualElement != null)
                {
                    isMatchSuccessful = expectedElement.Name == actualElement.Name;

                    if (!isMatchSuccessful)
                    {
                        Console.Write("Expected node {0} was not present in actual for parent {1}",
                                      expectedElement.Name.LocalName, expectedElement.Parent != null ? expectedElement.Parent.Name.LocalName : null);
                        return isMatchSuccessful;
                    }

                    if (expectedElement.HasElements)
                        continue;

                    var xElementName = actualElement.Name.LocalName;
                    var xelementValue = actualElement.Value;
                    if ((!string.IsNullOrEmpty(expectedElement.Value) ||
                         !string.IsNullOrEmpty(xelementValue)) && xelementValue != expectedElement.Value)
                    {
                        if (!actualElement.HasElements)
                            Console.Write("Expected {0}:{1} but actual was {2}:{3} for parent {4}",
                                          xElementName, xelementValue,
                                          expectedElement.Name.LocalName,
                                          expectedElement.Value, expectedElement.Parent.Name.LocalName);
                        else
                            Console.Write(
                                "Expected does not contain the node {0} that is in actual for parent {1}",
                                actualElement.Elements().First().Name.LocalName, actualElement.Name.LocalName);
                        return false;
                    }
                }
            }

            if (!expectedNodeEnumerator.HasNext && actualNodeEnumerator.HasNext)
            {
                var element2 = actualNodeEnumerator.Next();
                if (element2 != null)
                    Console.Write(
                        "Expected does not contain the node {0} that is in actual for parent {1}",
                        element2.Name.LocalName, element2.Parent != null ? element2.Parent.Name.LocalName : null);
                return false;
            }

            if (expectedNodeEnumerator.HasNext && !actualNodeEnumerator.HasNext)
            {
                var element1 = expectedNodeEnumerator.Next();
                Console.Write("Expected node {0} was not present in actual for parent {1}",
                                      element1.Name.LocalName, element1.Parent != null ? element1.Parent.Name.LocalName : null);
                return false;
            }

            return isMatchSuccessful;
        }
    }
}
