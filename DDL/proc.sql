

create type emp_obj is object (id number, first_name varchar(50), last_name varchar(100), phone_number varchar(30));
create type emp_tab is table of emp_obj;

create or replace function get_all_employees return emp_tab
is
   l_emp_tab emp_tab := emp_tab();
   n integer := 0;
begin
   for r in (select id, first_name, last_name, phone_number from employee)
   loop
      l_emp_tab.extend;
      n := n + 1;
     l_emp_tab(n) := emp_obj(r.id, r.first_name, r.last_name, r.phone_number);
   end loop;
   return l_emp_tab;
end;

create or replace procedure add_employee(fn in varchar2, ln in varchar2, pn in varchar2)
is
begin
    insert into employee(employee.first_name, employee.last_name, employee.phone_number)
    values(fn,ln,pn);
    commit;
end add_employee;

create or replace procedure delete_employee(ident in number)
is
begin
    delete from employee where id=ident;
    commit;
end delete_employee;


