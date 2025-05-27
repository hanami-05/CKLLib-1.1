using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CKLLib.Operations;

namespace CKLLib
{
    public class CKL // объект алгебры динамических отношений
    {
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public string FilePath { get; set; } // путь файла, в котором записан объект
        public TimeInterval GlobalInterval { get; set; } // интервал времени,
                                                         // на котором определено отношение
        public TimeDimentions Dimention { get; set; } // еденица измерения времени интервала
        public HashSet<Pair> Source { get; set; } // множество, на котором задано отношение
        public HashSet<RelationItem> Relation { get; set; } // динамическое отношение
        
        public CKL()
        {
            FilePath = string.Empty;
            GlobalInterval = TimeInterval.ZERO;
            Dimention = TimeDimentions.SECONDS;
			Source = new HashSet<Pair>();
            Relation = new HashSet<RelationItem>();
        }

        public CKL(string filePath, TimeInterval timeInterval, TimeDimentions dimention ,HashSet<Pair> source, HashSet<RelationItem> relation)
        {
            FilePath = filePath;
            GlobalInterval = timeInterval;
            Dimention = dimention;
            Source = source;
            Relation = relation;

            FillRelation();
        }

        private void FillRelation() 
        {
            foreach (Pair item in Source) 
            {
                if (!Relation.Any(x => x.Value.Equals(item))) Relation.Add(new RelationItem(item, 
                    new List<TimeInterval>() { TimeInterval.ZERO } ));
            }
        }

		public override bool Equals(object? obj)
		{
            CKL? ckl = obj as CKL;
            if (ckl is null) return false;


            return GlobalInterval.Equals(ckl.GlobalInterval) && Dimention.Equals(ckl.Dimention)
                && Source.SetEquals(ckl.Source) && Relation.SetEquals(ckl.Relation);
		}

		public override int GetHashCode()
		{
            return HashCode.Combine(GlobalInterval, Dimention, Source, Relation);
		}

        public static void Save(CKL ckl) 
        {
            string s = JsonSerializer.Serialize(ckl);
            File.WriteAllText(ckl.FilePath, s);
        }

        public static CKL? GetFromFile(string path) 
        {
            CKL? ckl  = JsonSerializer.Deserialize<CKL>(File.ReadAllText(path));
            if (ckl is not null) ckl.FilePath = path;
            return ckl;
        }
	}
}
