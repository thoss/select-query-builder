using System;
using System.Collections.Generic;

namespace QueryBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var queryBuilder = new SelectQueryBuilder();

            queryBuilder.FromTable("customers")
                .Join(JoinType.InnerJoin, "regions", "zip", Comparison.Equals, "customers", "zip")
                .Columns("customers.name", "customers.firstname", "regions.city")
                .Where("regions.city", Comparison.Like, "do%")
                .GroupBy("regions.city", "customers.name", "customers.firstname")
                .Having("COUNT(*)", Comparison.GreaterThan, 5)
                .OrderBy("customers.name");

            Console.WriteLine(queryBuilder.BuildQuery());
            Console.ReadLine();
        }
    }
}
