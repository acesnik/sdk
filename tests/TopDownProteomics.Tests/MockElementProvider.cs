﻿using System.Linq;
using TopDownProteomics.Chemistry;

namespace TopDownProteomics.Tests
{
    public class MockElementProvider : IElementProvider
    {
        private IElement[] _elements;

        public MockElementProvider()
        {
            _elements = new IElement[128];
            _elements[1] = new Element(1, "H", new[]
            {
                new Isotope(1.007825032, 0.999885),
                new Isotope(2.014101778, 0.000115)
            });
            _elements[6] = new Element(6, "C", new[]
            {
                new Isotope(12.0, 0.9893),
                new Isotope(13.0033548378, 0.0107)
            });
            _elements[7] = new Element(7, "N", new[]
            {
                new Isotope(14.0030740052, 0.99632),
                new Isotope(15.0001088984, 0.00368)
            });
            _elements[8] = new Element(8, "O", new[]
            {
                new Isotope(15.99491463, 0.99757),
                new Isotope(16.9991312, 0.00038),
                new Isotope(17.9991603, 0.00205)
            });
            _elements[15] = new Element(15, "P", new[] 
            {
                new Isotope(30.97376163, 1.0000)
            });
            _elements[16] = new Element(16, "S", new[]
            {
                new Isotope(31.97207100, 0.9499),
                new Isotope(32.97145876, 0.0075),
                new Isotope(33.96786690, 0.0425),
                new Isotope(35.96708076, 0.0001)
            });
            _elements[34] = new Element(34, "Se", new[]
            {
                new Isotope(73.9224764, 0.0089),
                new Isotope(75.9192136, 0.0937),
                new Isotope(76.9199140, 0.0763),
                new Isotope(77.9173091, 0.2377),
                new Isotope(79.9165213, 0.4961),
                new Isotope(81.9166994, 0.0873)
            });
        }

        public void OverwriteElement(IElement element)
        {
            _elements[element.AtomicNumber] = element;
        }

        public IElement GetElement(int atomicNumber)
        {
            return _elements[atomicNumber];
        }

        public IElement GetElement(string symbol)
        {
            return _elements.Single(x => x?.Symbol == symbol);
        }
    }
}