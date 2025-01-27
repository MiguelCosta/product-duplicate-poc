namespace ProductDuplicatePoC.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;

public class Page<T>
{
    public Page(IEnumerable<T> entries, int page, int pageSize, int total)
    {
        this.TotalPages = total <= 0 || pageSize <= 0 ? 1 : (int) Math.Ceiling((double) total / (double) pageSize);
        this.Number = page;
        this.TotalItems = total;
        this.Entries = entries.ToList();
    }

    public int Number { get; set; }

    public int TotalPages { get; set; }

    public int TotalItems { get; set; }

    public List<T> Entries { get; set; }
}
