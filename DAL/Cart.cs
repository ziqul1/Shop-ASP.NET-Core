using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projektos.DAL
{
	public class Cart
	{
        public List<int> IdList { get; set; }

        public List<int> Numbers { get; set; }

        public int TotalNumber { get; set; }

        public void Load(string cartCookieString)
        {
            IdList = new List<int>();
            Numbers = new List<int>();

            if (cartCookieString != null)
            {
                var cartCookieSplit = cartCookieString.Split(';', '-');
                int id;
                int number;

                for (int i = 1; i < cartCookieSplit.Length; i++)
                {
                    if (int.TryParse(cartCookieSplit[i++], out id)
                        && int.TryParse(cartCookieSplit[i], out number))
                    {
                        IdList.Add(id);
                        Numbers.Add(number);
                        TotalNumber += number;
                    }
                    else throw new ArgumentException("Problem z koszykiem.");
                }
            }
        }

        public int GetOnlyTotalNumber(string cartCookieString)
        {
            if (int.TryParse(cartCookieString.Substring(0,
                cartCookieString.IndexOf(';')), out int result))
                return result;

            else return -1;
        }

        public string Save()
        {
            if (IdList == null || Numbers == null || TotalNumber == 0)
                return "";

            string result = TotalNumber.ToString();
            for (int i = 0; i < IdList.Count; i++)
            {
                result += $";{IdList[i]}-{Numbers[i]}";
            }
            return result;
        }

        public void AddItem(int id)
        {
            if (id < 0)
                throw new ArgumentException("Nieprawidłowe id.");

            int index = IdList.IndexOf(id);

            if (index == -1)
            {
                IdList.Add(id);
                Numbers.Add(1);
            }
            else
            {
                Numbers[index]++;
            }
            TotalNumber++;
        }

        public void DecreaseItem(int id)
        {
            if (id < 0)
                throw new ArgumentException("Nieprawidłowe id.");

            int index = IdList.IndexOf(id);

            if (index == -1)
            {
                throw new Exception("Brak przedmiotu o takim id w koszyku.");
            }
            else
            {
                if (Numbers[index] > 1)
                    Numbers[index]--;
                else
                {
                    IdList.RemoveAt(index);
                    Numbers.RemoveAt(index);
                }

                TotalNumber--;
            }
        }

        public void DeleteItem(int id)
        {
            if (id < 0)
                throw new ArgumentException("Nieprawidłowe id.");

            int index = IdList.IndexOf(id);

            if (index == -1)
            {
                throw new Exception("Brak przedmiotu o takim id w koszyku.");
            }
            else
            {
                IdList.RemoveAt(index);
                Numbers.RemoveAt(index);

                TotalNumber--;
            }
        }

        public void Clear()
        {
            IdList.Clear();
            Numbers.Clear();
            TotalNumber = 0;
        }

        public bool Empty() => TotalNumber == 0;
    }
}
