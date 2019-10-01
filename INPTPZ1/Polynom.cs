using System.Collections.Generic;

namespace INPTPZ1
{
    class Polynom
    {
        public List<ComplexNumber> CoeficientGroup { get; set; }

        public Polynom()
        {
            CoeficientGroup = new List<ComplexNumber>();
        }

        public Polynom Derive()
        {
            Polynom localPolynom = new Polynom();
            for (int i = 1; i < CoeficientGroup.Count; i++)
            {
                localPolynom.CoeficientGroup.Add(CoeficientGroup[i].Multiply(new ComplexNumber() { RealPart = i }));
            }

            return localPolynom;
        }

        public ComplexNumber Eval(ComplexNumber inputNumber)
        {
            ComplexNumber coeficientCout = ComplexNumber.Zero;
            for (int i = 0; i < CoeficientGroup.Count; i++)
            {
                ComplexNumber coeficient = CoeficientGroup[i];
                ComplexNumber operand = inputNumber;

                if (i > 0)
                {
                    for (int j = 0; j < i - 1; j++)
                        operand = operand.Multiply(inputNumber);

                    coeficient = coeficient.Multiply(operand);
                }

                coeficientCout = coeficientCout.Add(coeficient);
            }

            return coeficientCout;
        }

        public override string ToString()
        {
            string extract = "";
            for (int i = 0; i < CoeficientGroup.Count; i++)
            {
                extract += CoeficientGroup[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        extract += "x";
                    }
                }
                extract += " + ";
            }
            return extract;
        }
    }
}