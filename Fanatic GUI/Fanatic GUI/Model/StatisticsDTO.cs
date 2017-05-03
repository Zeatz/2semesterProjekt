using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanatic_GUI.Model
{
    public class StatisticsDTO
    {
        private int _id;
        private Dictionary<int, bool> _booleanDic;

        public Dictionary<int, bool> BooleanDic
        {
            get { return _booleanDic; }
            set
            {
                // tjekker om value dictionary'en er null
                if (value == null)
                {
                    throw new NullReferenceException("Kontakt medarbejder, program defekt.");
                }
                // tjekker om all værdier i value dictionary'en er false
                if (value.Count(x => !x.Value) == value.Count)
                {
                    throw new ArgumentException("Vælg venligst et formål.\nEller tryk på tilbage knappen.");
                }
                _booleanDic = value;
            }
        }

        public int ID
        {
            get { return _id; }
            set
            {
                // tjekker om value id'et er større end 0
                if (value < 0)
                {
                    throw new ArgumentException("ID kan ikke være mindre end 0");
                }
                _id = value;
            }
        }
    }
}
