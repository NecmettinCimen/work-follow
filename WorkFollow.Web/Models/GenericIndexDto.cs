using OdevTakip.Entities;
using OdevTakip.Services;
using System.Collections.Generic;

namespace OdevTakip.Models
{
    public class GenericIndexDto<T> where T : BaseEntity
    {
        public List<T> list { get; set; }
        public string sort { get; set; }

        public GenericIndexDto(List<T> list, string sort)
        {
            if (list == null)
            {
                this.list = new List<T>();
            }
            else
            {

                SortedList sortedList = new SortedList();

                switch (sort)
                {
                    case "Id":
                        sortedList.SetSortStrategy(new IdSort());
                        break;
                    default:
                    case "Date":
                        sortedList.SetSortStrategy(new DateSort());
                        break;
                }

                this.list = sortedList.Sort<T>(list);

            }

            this.sort = sort;
        }



        public string SortSelect()
        {
            if (sort == "Id")
            {
                return "Date";
            }
            else
            {
                return "Id";
            }
        }
    }
}
