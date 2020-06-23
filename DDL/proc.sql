

create type emp_obj is object (id number, first_name varchar(50), last_name varchar(100), phone_number varchar(30));
create type emp_tab is table of emp_obj;

create or replace function get_all_employees return emp_tab
  2  is
  3     l_emp_tab emp_tab := emp_tab();
  4     n integer := 0;
  5  begin
  6     for r in (select id, first_name, last_name, phone_number from employee)
  7     loop
  8        l_emp_tab.extend;
  9        n := n + 1;
 10       l_emp_tab(n) := emp_obj(r.id, r.first_name, r.last_name, r.phone_number);
 11     end loop;
 12     return l_emp_tab;
 13  end;
 14  /