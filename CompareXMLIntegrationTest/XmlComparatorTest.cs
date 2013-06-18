using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using CompareXML;
using NUnit.Framework;

namespace CompareXMLIntegrationTest
{
    [TestFixture]
    public class XmlComparatorTest
    {
        [Test]
        public void ShouldReturnTrueIfXmlsMatch()
        {
            var doc1 = XElement.Load("C:\\Users\\607366462\\desktop\\CompareXML\\CompareXMLIntegrationTest\\Document1.xml");
//            doc1.Load("C:\\Users\\607366462\\desktop\\CompareXML\\CompareXMLIntegrationTest\\Document1.xml");
            var doc2 = XElement.Load("C:\\Users\\607366462\\desktop\\CompareXML\\CompareXMLIntegrationTest\\Document2.xml");
//            doc2.Load("C:\\Users\\607366462\\desktop\\CompareXML\\CompareXMLIntegrationTest\\Document2.xml");
            var result = XmlComparator.Compare(doc1, doc2);

            Assert.That(result,Is.True);
        }
    }
}
 