CREATE TABLE "BTSTC"."PHA_CAUHINH" 
   (	
  "ID" NUMBER(10,0) NOT NULL ENABLE, 
	"PROCEDURE_NAME" NVARCHAR2(2000), 
	"CONGTHUC" NVARCHAR2(2000) NOT NULL ENABLE, 
	"NAM" NUMBER(10,0), 
	"LOAI" NVARCHAR2(4), 
	 CONSTRAINT "PK_PHA_CAUHINH" PRIMARY KEY ("ID")
  );
/
CREATE SEQUENCE SQ_PHA_CAUHINH
  MINVALUE 1
  MAXVALUE 2000
  START WITH 1
  INCREMENT BY 1
  CACHE 20;
/
  CREATE OR REPLACE TRIGGER "BTSTC"."TR_PHA_CAUHINH" 
before insert on "BTSTC"."PHA_CAUHINH"
for each row
begin
  select "BTSTC"."SQ_PHA_CAUHINH".nextval into :new."ID" from dual;
end;
/
ALTER TRIGGER "BTSTC"."TR_PHA_CAUHINH" ENABLE;
/
 
