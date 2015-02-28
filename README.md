Select Query Builder
======================
***

###WHAT

>With this Class you can build complex and flexible SQL Queries in C#

###SAMPLES
```
var queryBuilder = new SelectQueryBuilder();
```
Code:
```
queryBuilder
	.FromTable("customers")
	.Join(JoinType.InnerJoin, "regions", "zip", Comparison.Equals, "customers", "zip")
	.Columns("customers.name", "customers.firstname", "regions.city")
	.Where("regions.city", Comparison.Like, "do%")
	.GroupBy("regions.city", "customers.name", "customers.firstname")
	.Having("COUNT(*)", Comparison.GreaterThan, 5)
	.OrderBy("customers.name");
```
Results in Query:
>SELECT customers.name,customers.firstname,regions.city FROM [dbo].[customers] 
INNER JOIN [dbo].[regions] ON customers.zip = regions.zip 
WHERE regions.city LIKE 'do%' 
GROUP BY regions.city, customers.name, customers.firstname 
HAVING COUNT(*) > 5 
ORDER BY customers.name ASC

Code:
```
queryBuilder
	.FromTable("customers")
	.Where("plz", Comparison.In, new SqlLiteral("'58965','47841','12569'"))
	.OrderBy("name", Order.Descending);
```
Results in Query
>SELECT * FROM [dbo].[customers] 
WHERE plz IN ('58965','47841','12569') 
ORDER BY name DESC

Code:
```
queryBuilder
	.Top(100)
	.FromTable("customers")
	.Where("age",Comparison.LessThan, 55,LogicOperator.And)
	.Where(new GroupWhereClause(new List<WhereClause>
	{
		new WhereClause("name", Comparison.Like, "jo%", LogicOperator.Or),
		new WhereClause("name", Comparison.Like, "pe%"),
	}));
```
Results in Query
>SELECT TOP 100 * FROM [dbo].[customers] 
WHERE age < 55 AND 
(name LIKE 'jo%' OR name LIKE 'pe%')