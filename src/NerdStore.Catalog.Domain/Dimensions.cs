using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain
{
    public class Dimensions
    {
        public decimal Height { get; private set; }
        public decimal Width { get; private set; }
        public decimal Length { get; private set; }

        public Dimensions(decimal height, decimal width, decimal length)
        {

            AssertionConcern.ValidateIfLowerMin(height, 1,"Height must be more than 0");
            AssertionConcern.ValidateIfLowerMin(width, 1, "Width must be more than 0");
            AssertionConcern.ValidateIfLowerMin(length, 1, "Length must be more than 0");

            Height = height;
            Width = width;
            Length = length;
        }

        public string Description()
        {
            return $" WxHxL: {Width} x {Height} x {Length}";
        }

        public override string ToString()
        {
            return Description();
        }

    }
}
