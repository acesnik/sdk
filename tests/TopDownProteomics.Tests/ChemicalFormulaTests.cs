﻿using NUnit.Framework;
using System.Linq;
using TopDownProteomics.Chemistry;

namespace TopDownProteomics.Tests.ProForma
{
    [TestFixture]
    public class ChemicalFormulaTests
    {
        [Test]
        public void WaterTest()
        {
            IElementProvider provider = new MockElementProvider();
            IElement h = provider.GetElement("H");
            IElement o = provider.GetElement("O");

            var formula = ChemicalFormula.Water(provider);
            var elements = formula.GetElements();

            Assert.AreEqual(2, elements.Count);
            Assert.AreEqual(2, elements.Single(x => x.Entity.Symbol == "H").Count);
            Assert.AreEqual(1, elements.Single(x => x.Entity.Symbol == "O").Count);

            Assert.AreEqual(h.Isotopes.FirstWithMax(x => x.RelativeAbundance).AtomicMass * 2
                + o.Isotopes.FirstWithMax(x => x.RelativeAbundance).AtomicMass,
                formula.GetMass(MassType.Monoisotopic));
            Assert.AreEqual(h.Isotopes.Sum(x => x.AtomicMass * x.RelativeAbundance) * 2
                + o.Isotopes.Sum(x => x.AtomicMass * x.RelativeAbundance),
                formula.GetMass(MassType.Average));
        }

        [Test]
        public void MassTest()
        {
            // Using simple 100% abundances
            var provider = new MockElementProvider();
            provider.OverwriteElement(new Element(1, "H", new[]
            {
                new Isotope(1, 1.0)
            }));
            provider.OverwriteElement(new Element(8, "O", new[]
            {
                new Isotope(16, 1.0)
            }));

            var formula = ChemicalFormula.Water(provider);
            var elements = formula.GetElements();

            Assert.AreEqual(18, formula.GetMass(MassType.Monoisotopic));
            Assert.AreEqual(18, formula.GetMass(MassType.Average));

            // Switch to 75/25
            provider.OverwriteElement(new Element(8, "O", new[]
            {
                new Isotope(16, 0.75),
                new Isotope(17, 0.25)
            }));

            formula = ChemicalFormula.Water(provider);
            elements = formula.GetElements();

            Assert.AreEqual(18, formula.GetMass(MassType.Monoisotopic));
            Assert.AreEqual(18.25, formula.GetMass(MassType.Average));
        }

        [Test]
        public void ChemicalFormulaCompareTest()
        {
            var provider = new MockElementProvider();
            var formula = ChemicalFormula.Water(provider);

            var formula2 = ChemicalFormula.Water(provider);

            var formula3 = new ChemicalFormula(new[] 
            {
                new EntityCardinality<IElement>(provider.GetElement("H"), 1),
                new EntityCardinality<IElement>(provider.GetElement("O"), 2),
            });

            Assert.AreEqual(formula, formula2);
            Assert.AreNotEqual(formula, formula3);
        }
    }
}