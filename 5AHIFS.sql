DROP TABLE streetplan CASCADE CONSTRAINTS;

CREATE TABLE streetplan (
  id varchar2(5) PRIMARY KEY,
	name VARCHAR2(50),
	objekt SDO_GEOMETRY
);
drop table route;

CREATE TABLE route (
  sid1 varchar2(5),
  sid2 varchar2(5),
  primary key(sid1,sid2),
  foreign key (sid1) references streetplan (id),
  foreign key (sid2) references streetplan (id)
);


INSERT INTO user_sdo_geom_metadata

VALUES 
(	's1',
	'objekt',
	SDO_GEOMETRY(
    2002,
    NULL,
    NULL,
    SDO_ELEM_INFO_ARRAY(1,4,2, 1,2,1, 3,2,2), -- compound line string
    SDO_ORDINATE_ARRAY(10,50,100,50)
  ), NULL
);

CREATE INDEX Situation_spatial_idx
	ON BSCS005_Situation(object)
	INDEXTYPE IS MDSYS.SPATIAL_INDEX;


select * from streetplan;

INSERT INTO streetplan VALUES(
  's2',
	's2',
	SDO_GEOMETRY(
    2002,
    NULL,
    NULL,
    SDO_ELEM_INFO_ARRAY(1,4,2, 1,2,1, 3,2,2), -- compound line string
    SDO_ORDINATE_ARRAY(100,50,150,70)
  )
);

INSERT INTO streetplan VALUES(
  's3',
	's3',
	SDO_GEOMETRY(
    2002,
    NULL,
    NULL,
    SDO_ELEM_INFO_ARRAY(1,4,2, 1,2,1, 3,2,2), -- compound line string
    SDO_ORDINATE_ARRAY(150,70,200,70)
  )
);

INSERT INTO streetplan VALUES(
  's4',
	's4',
	SDO_GEOMETRY(
    2002,
    NULL,
    NULL,
    SDO_ELEM_INFO_ARRAY(1,4,2, 1,2,1, 3,2,2), -- compound line string
    SDO_ORDINATE_ARRAY(200,70,250,40)
  )
);
INSERT INTO streetplan VALUES(
  's5',
	's5',
	SDO_GEOMETRY(
    2002,
    NULL,
    NULL,
    SDO_ELEM_INFO_ARRAY(1,4,2, 1,2,1, 3,2,2), -- compound line string
    SDO_ORDINATE_ARRAY(250,40,300,80)
  )
);
INSERT INTO streetplan VALUES(
  's6',
	's6',
	SDO_GEOMETRY(
    2002,
    NULL,
    NULL,
    SDO_ELEM_INFO_ARRAY(1,4,2, 1,2,1, 3,2,2), -- compound line string
    SDO_ORDINATE_ARRAY(300,80,350,100)
  )
);
INSERT INTO streetplan VALUES(
  's7',
	's7',
	SDO_GEOMETRY(
    2002,
    NULL,
    NULL,
    SDO_ELEM_INFO_ARRAY(1,4,2, 1,2,1, 3,2,2), -- compound line string
    SDO_ORDINATE_ARRAY(100,50,200,30)
  )
);


delete from streetplan where id = 's7';
INSERT INTO streetplan VALUES(
  'r3',
	'r3',
	null
);

commit;

insert into route values(
  'r3','s6'
);

select * from route;

select count(*) from route where sid1 = 'r3';

SELECT SDO_GEOM.SDO_LENGTH(objekt, 0.05) as length
   FROM streetplan
   WHERE id = 's2';

begin

calculateLength('r1');

end;

set serveroutput on;

create or replace procedure calculateLength ( sid1 in streetplan.id%Type ) is

begin
  DBMS_OUTPUT.PUT_LINE(calcLen(sid1));
  
end;


create or replace function calcLen (id1 in streetplan.id%Type) return integer is
  len integer := 0;
  obj streetplan.objekt%Type;
  cursorId streetplan.id%Type;
  cursor c1 is select * from route where sid1 = cursorId;
begin
select objekt into obj from streetplan where id = id1;



if(obj is null) then
cursorId := id1;
 for rec_c1 in c1 loop 
   dbms_output.put_line('is a route'||rec_c1.sid2);
   len := len + calcLen(rec_c1.sid2);
 end loop;
 else
 dbms_output.put_line('Length of street'||id1);
end if;

return len;
end;







DELETE FROM BSCS005_SITUATION WHERE ID = 2;


SELECT c.name, t.X, t.Y, t.id
FROM BSCS005_Situation c,
TABLE(SDO_UTIL.GETVERTICES(c.objekt)) t
ORDER BY c.name,t.id;

Select * from BSCS005_Situation;
Commit;



***************************************************************************************Point************************************************************************************


DROP TABLE BSCS005_ALARM CASCADE CONSTRAINTS;

CREATE TABLE BSCS005_ALARM (
  id NUMBER PRIMARY KEY,
	text VARCHAR2(50),
	objekt SDO_GEOMETRY
);

INSERT INTO user_sdo_geom_metadata
(	TABLE_NAME,
	COLUMN_NAME,
	DIMINFO,
	SRID
)
VALUES 
(	'streetplan',
	'objekt',
	SDO_DIM_ARRAY( -- 20X20 grid
		SDO_DIM_ELEMENT('X', 0, 20, 0.05),
		SDO_DIM_ELEMENT('Y', 0, 20, 0.05)
	),
	NULL -- SRID
);


drop index streetplan_spatial_idx;
CREATE INDEX streetplan_spatial_idx
	ON streetplan(objekt)
	INDEXTYPE IS MDSYS.SPATIAL_INDEX;

INSERT INTO BSCS005_ALARM VALUES 
(2,'Something',
SDO_GEOMETRY(
  2001, 
  null, 
  null, 
  SDO_ELEM_INFO_ARRAY (1,1,1),
  SDO_ORDINATE_ARRAY (10,5))
);

SELECT c.id, c.text, t.X, t.Y, t.id
FROM BSCS005_ALARM c,
TABLE(SDO_UTIL.GETVERTICES(c.objekt)) t
ORDER BY c.text,t.id;

Select * from BSCS005_ALARM;
Commit;