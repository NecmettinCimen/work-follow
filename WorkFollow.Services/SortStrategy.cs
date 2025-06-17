using System.Collections.Generic;
using System.Linq;
using WorkFollow.Entities;

namespace WorkFollow.Services;

public abstract class SortStrategy
{
    public abstract List<T> Sort<T>(List<T> list) where T : BaseEntity;
}

public class IdSort : SortStrategy
{
    public override List<T> Sort<T>(List<T> list)
    {
        return list.OrderBy(x => x.Id).ToList();
    }
}

public class DateSort : SortStrategy
{
    public override List<T> Sort<T>(List<T> list)
    {
        return list.OrderByDescending(x => x.OlusturmaTarihi).ToList();
    }
}

public class SortedList
{
    private SortStrategy _sortstrategy;

    public void SetSortStrategy(SortStrategy sortstrategy)
    {
        _sortstrategy = sortstrategy;
    }

    public List<T> Sort<T>(List<T> list) where T : BaseEntity
    {
        return _sortstrategy.Sort(list);
    }
}