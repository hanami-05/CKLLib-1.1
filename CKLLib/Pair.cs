using System.Timers;

namespace CKLLib
{
    public class Pair
    {
        public object FirstValue { get; set; }
        public object? SecondValue { get; set; }
        public object? ThirdValue { get; set; }

        public Pair() { }
		public Pair(object firstValue)
		{
			FirstValue = firstValue;
		}
		public Pair(object firstValue, object secondValue): this(firstValue)
        {
            SecondValue = secondValue;
        }

        public Pair(object firstValue, object secondValue, object thirdValue) : this(firstValue, secondValue) 
        {
            ThirdValue = thirdValue;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Pair) return false;

            Pair? pair = obj as Pair;
            if (pair == null) return false;

            return pair.ToString().Equals(ToString());
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstValue, SecondValue);
        }

        public override string ToString()
        {
            if (ThirdValue != null) 
                return $"({FirstValue.ToString()};{SecondValue!.ToString()};{ThirdValue.ToString()})";
			
            else if (SecondValue != null)
                return $"({FirstValue.ToString()};{SecondValue.ToString()})";
           
            return FirstValue.ToString()!;
        }
    }
}
