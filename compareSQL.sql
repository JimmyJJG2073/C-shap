select * from sys.all_tab_cols where table_name = 'table_name';
--抓取 owner
SELECT owner, table_name FROM all_tables where owner IN('owner','owner1')


select nvl(s1.table_name, s2.table_name) as table_name,
       nvl(s1.column_name, s2.column_name) as column_name,
       s1.column_name as schema_1,
       s2.column_name as schema_2
from ( select table_name,
              column_name
       from sys.all_tab_cols
       where owner = 'GROUP6'       -- put schema name to compare here
) s1
full join ( select table_name,
                   column_name
            from sys.all_tab_cols
            where owner = 'GROUP5'  -- put schema name to compare here
) s2 on s2.table_name = s1.table_name
     and s2.column_name = s1.column_name
where s1.column_name is null
      or s2.column_name is null
order by table_name,
         column_name;



--https://stackoverflow.com/questions/40130814/compare-data-types-of-two-table-columns-in-oracle
