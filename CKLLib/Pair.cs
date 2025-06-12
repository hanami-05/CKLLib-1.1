using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Timers;

namespace CKLLib
{
    public class Pair
    {
        public object FirstValue { get; set; }
        public object? SecondValue { get; set; }
        public object? ThirdValue { get; set; }

        public List<object> Values { get; set; }

        public Pair() { }
		public Pair(object firstValue)
		{
            if (firstValue == null) throw new ArgumentNullException("first value can not be null");
			FirstValue = firstValue;
            
            Values = new List<object> { firstValue };
		}
		public Pair(object firstValue, object secondValue): this(firstValue)
        {
            if (secondValue == null) throw new ArgumentNullException("second value can not be null");
            SecondValue = secondValue;

            Values.Add(secondValue);
        }

        public Pair(object firstValue, object secondValue, object thirdValue) : this(firstValue, secondValue) 
        {
            if (thirdValue == null) throw new ArgumentNullException("third value can not be null");
            ThirdValue = thirdValue;
            Values.Add(thirdValue);
        }

        public Pair (IEnumerable<object> values) 
        {
            if (values == null) throw new ArgumentNullException("Values collection can not be null");
            if (values.Count() == 0) throw new ArgumentException("Values can not be empty");

            Values = values.ToList();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Pair) return false;

            Pair? pair = obj as Pair;
            if (pair == null) return false;

            // return pair.ToString().Equals(ToString());
            return pair.Values.SequenceEqual(Values);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();

            //TODO: переделать хеш код под много значений
        }

        public override string ToString()
        {
            if (ThirdValue != null) 
                return $"({FirstValue!.ToString()};{SecondValue!.ToString()};{ThirdValue.ToString()})";
			
            if (SecondValue != null)
                return $"({FirstValue.ToString()};{SecondValue.ToString()})";
            if (FirstValue != null) return FirstValue.ToString()!;

            if (Values != null)
            {
                string res = "(";

                foreach (object value in Values)
                {
                    res += $"{value};";
                }

                if (res.Length > 1)
                {
                    res = res.Substring(0, res.Length - 1);
                    res += ")";
                }

                return res;
            }

            return string.Empty;
		}

		public class PairEqualityComparer : IEqualityComparer<Pair>
		{
			public bool Equals(Pair? x, Pair? y)
			{
                if (x == null && y == null) return true;
                if (x == null) return false;

                return x.Equals(y);

			}

			public int GetHashCode([DisallowNull] Pair obj)
			{
                return obj.GetHashCode();
			}
		}

		public bool HasObject(object obj, Func<object, object, bool> comp) 
        {
            if (SecondValue == null && FirstValue != null)
                return comp(FirstValue, obj);


            if (ThirdValue == null && SecondValue != null)
                return comp(SecondValue, obj) || comp(FirstValue, obj);

            if (ThirdValue != null)
                return comp(ThirdValue, obj) || comp(SecondValue, null) || comp(FirstValue, null);
            if (Values != null) 
            {
                foreach (object value in Values) if (comp(value, obj)) return true;
            }
            return false;
        }
    }
}
