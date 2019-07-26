using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.WordsMaker.Logic.Models
{
    public class WordDictionary
    {
	    private Dictionary<string, string[]> WordsAnagrams = new Dictionary<string, string[]>();

	    public WordDictionary()
	    {
		    string[] str =
		    {
				"ЗАКОН",
				"КОЗНИ",
				"ИКОНА",
				"КИНАЗ",
				"НИЗКА",
				"КАНО",
				"КАОН",
				"ЗНАК",
				"КОЗА",
				"ЗОНА",
				"АЗИН",
				"ИНОК",
				"КИНО",
				"КАИН",
				"НОА",
				"КОН",
				"НОК",
				"ОАЗ",
				"ИОН",
				"КИН",
				"НИЗ",
				"АЗ"
			};
		    WordsAnagrams.Add(key: "КАЗИНО", value: str);
		    str = new string[]
		    {
				"КОЛО",
				"ЛОКО",
				"КОМ",
				"ЛОМ",
				"МОЛ",
				"ОКО",
				"КОЛ",
				"МО",
				"ОМ"
			};
		    WordsAnagrams.Add(key: "МОЛОКО", value: str);
		    str = new string[]
		    {
				"ЦИНГА",
				"АНГАР",
				"ГАРНА",
				"НАГАР",
				"ЦАНГА",
				"РАИНА",
				"НАГАИ",
				"ГАНА",
				"НАГА",
				"ИГРА",
				"ИРГА",
				"РИГА",
				"РАНА",
				"РАНИ",
				"ЦИАН",
				"РАГА",
				"РИНГ",
				"ГРАН",
				"РАНГ",
				"НАР",
				"АРА",
				"АИР",
				"АР"
			};
		    WordsAnagrams.Add(key: "ГРАНИЦА", value: str);
		}

	    public string GetRandomKeyWord()
	    {
			Random rnd = new Random();
			int key_position = rnd.Next(0, WordsAnagrams.Count - 1);
			return (WordsAnagrams.Keys.ToArray()[key_position]);
	    }
        public bool Contains(string key, string value)      
        {
	        if (WordsAnagrams.ContainsKey(key))
	        {
		        if (WordsAnagrams[key].Contains(value))
		        {
			        return true;
		        }
	        }
            return false;
        }
    }
}
