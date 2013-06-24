using System.Xml.Linq;
using CompareXML;
using NUnit.Framework;

namespace CrmService.FunctionalTests.XmlComparatorTest
{
    [TestFixture]
    public class XmlComparatorTest
    {
        [Test]
        public void ShouldFailIfElementsAreNotOrdered()
        {
            const string xml1 = @"<A><B></B><C></C></A>";
            const string xml2 = @"<A><C></C><B></B></A>";

            var result = XmlComparator.Compare(XElement.Parse(xml1), XElement.Parse(xml2));
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldFailIfElementsNamesAreOfDifferentCases()
        {
            const string xml1 = @"<A><B></B><C></C></A>";
            const string xml2 = @"<A><B></B><c></c></A>";

            var result = XmlComparator.Compare(XElement.Parse(xml1), XElement.Parse(xml2));
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldPassIfXmLsAreIdentical()
        {
            const string xml1 = @"<A><B></B><C></C></A>";
            const string xml2 = @"<A><B></B><C></C></A>";

            var result = XmlComparator.Compare(XElement.Parse(xml1), XElement.Parse(xml2));
            Assert.IsTrue(result);
        }
    }
}
