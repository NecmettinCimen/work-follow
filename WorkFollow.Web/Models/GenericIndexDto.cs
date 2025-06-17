using System.Collections.Generic;
using WorkFollow.Entities;
using WorkFollow.Services;

namespace WorkFollow.Web.Models;

public class GenericIndexDto<T> where T : BaseEntity
{
    public GenericIndexDto(List<T> list, string sort)
    {
        if (list == null)
        {
            this.list = new List<T>();
        }
        else
        {
            var sortedList = new SortedList();

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

    public List<T> list { get; set; }
    public string sort { get; set; }


    public string SortSelect()
    {
        if (sort == "Id") return "Date";

        return "Id";
    }
}