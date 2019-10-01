namespace INPTPZ1
{
    class ComplexNumber
    {
        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            RealPart = 0,
            ImaginaryPart = 0
        };

        public ComplexNumber Multiply(ComplexNumber inputNumber)
        {
            ComplexNumber baseComplexNumber = this;

            return new ComplexNumber()
            {
                RealPart = baseComplexNumber.RealPart * inputNumber.RealPart - baseComplexNumber.ImaginaryPart * inputNumber.ImaginaryPart,
                ImaginaryPart = baseComplexNumber.RealPart * inputNumber.ImaginaryPart + baseComplexNumber.ImaginaryPart * inputNumber.RealPart
            };
        }

        public ComplexNumber Add(ComplexNumber inputNumber)
        {
            ComplexNumber baseComplexNumber = this;
            return new ComplexNumber()
            {
                RealPart = baseComplexNumber.RealPart + inputNumber.RealPart,
                ImaginaryPart = baseComplexNumber.ImaginaryPart + inputNumber.ImaginaryPart
            };
        }
        public ComplexNumber Subtract(ComplexNumber b)
        {
            ComplexNumber baseComplexNumber = this;
            return new ComplexNumber()
            {
                RealPart = baseComplexNumber.RealPart - b.RealPart,
                ImaginaryPart = baseComplexNumber.ImaginaryPart - b.ImaginaryPart
            };
        }

        public override string ToString()
        {
            return $"({RealPart} + {ImaginaryPart}i)";
        }

        internal ComplexNumber Divide(ComplexNumber inputNumber)
        {
            var multiplyWithBase = Multiply(new ComplexNumber() { RealPart = inputNumber.RealPart, ImaginaryPart = -inputNumber.ImaginaryPart });
            var multiplyWithInput = inputNumber.RealPart * inputNumber.RealPart + inputNumber.ImaginaryPart * inputNumber.ImaginaryPart;

            return new ComplexNumber()
            {
                RealPart = multiplyWithBase.RealPart / multiplyWithInput,
                ImaginaryPart = multiplyWithBase.ImaginaryPart / multiplyWithInput
            };
        }
    }
}
