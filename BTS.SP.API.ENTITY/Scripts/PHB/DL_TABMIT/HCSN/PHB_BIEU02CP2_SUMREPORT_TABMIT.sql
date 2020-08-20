--------------------------------------------------------
--  File created - Friday-May-04-2018   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Procedure PHB_BIEU02CP2_SUMREPORT_TABMIT
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PHB_BIEU02CP2_SUMREPORT_TABMIT" 
(USER_NAME IN NVARCHAR2,LOAIBC IN NUMBER,CHITIET IN NUMBER, TUNGAY_KS IN DATE,
  DENNGAY_KS IN DATE,
  TUNGAY_HL IN DATE,
  DENNGAY_HL IN DATE,DSDVQHNS IN NVARCHAR2,CUR OUT SYS_REFCURSOR)
AS 
P_QUERY VARCHAR2(32767);
P_DELETE VARCHAR2(32767);
P_INSERT VARCHAR2(32767);
P_INSERT2 VARCHAR2(32767);
P_SQL_CREATE_TABLE_TH VARCHAR2(32767);
P_SQL_CREATE_TABLE_TH2 VARCHAR2(32767);
P_WHERE VARCHAR2(2000);
P_WHERE2 VARCHAR2(2000);

P_NAMBC VARCHAR2(255);
P_KYBC VARCHAR2(255);
P_LSTQHNS VARCHAR2(500);
LST_QHNS VARCHAR2(5000);
P_TABLENAME_TH VARCHAR2(2000):='PHB_BIEU02CP2_TABMIT';
P_TABLENAME_TH2 VARCHAR2(2000):='PHB_BIEU02CP2_TABMIT_TKTN';

N_COUNT NUMBER(17,4):=0;
P_UPDATE_COT_NS_TRONGNUOC VARCHAR2(32767);
P_UPDATE_COT_VAY_NO_NN VARCHAR2(32767);
P_UPDATE_COT_VIEN_TRO VARCHAR2(32767);
P_UPDATE_COT_PHI_KT_DELAI VARCHAR2(32767);
P_UPDATE_COT_NGUON_KHAC VARCHAR2(32767);

BEGIN
        P_SQL_CREATE_TABLE_TH := 'CREATE TABLE '||P_TABLENAME_TH||'(
       	USER_NAME NVARCHAR2(100), 
       	MA_MUC NVARCHAR2(50), 
        TEN_TIEUMUC NVARCHAR2(2000), 
        MA_TIEUMUC NVARCHAR2(50), 
       	MA_CHUONG NVARCHAR2(50), 
       	MA_LOAI NVARCHAR2(50), 
       	MA_NGANHKT NVARCHAR2(50), 
        NS_TRONGNUOC NUMBER(18,2), 
        VAY_NO_NN NUMBER(18,2), 
        VIEN_TRO NUMBER(18,2), 
        PHI_KT_DELAI NUMBER(18,2), 
        NGUON_KHAC NUMBER(18,2)
        )SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)'; 
          P_SQL_CREATE_TABLE_TH2 := 'CREATE TABLE '||P_TABLENAME_TH2||'(
       	USER_NAME NVARCHAR2(100), 
       	MA_MUC NVARCHAR2(50), 
        TEN_TIEUMUC NVARCHAR2(2000), 
        MA_TIEUMUC NVARCHAR2(50), 
       	MA_CHUONG NVARCHAR2(50), 
       	MA_LOAI NVARCHAR2(50), 
       	MA_NGANHKT NVARCHAR2(50), 
        MA_TKTN NVARCHAR2(50), 
        GIA_TRI_HACH_TOAN NUMBER(18,2)
        )SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)';                      

    BEGIN
        SELECT COUNT(*)  INTO N_COUNT  FROM ALL_TAB_COLUMNS  WHERE TABLE_NAME = UPPER(P_TABLENAME_TH);
        EXCEPTION WHEN OTHERS THEN N_COUNT:=0;
    END;
    IF(N_COUNT IS NULL OR N_COUNT<=0) THEN
    BEGIN
        EXECUTE IMMEDIATE P_SQL_CREATE_TABLE_TH;  
    END;
    END IF;
    IF(N_COUNT>0) THEN
    BEGIN
        P_DELETE:= 'DELETE FROM '||P_TABLENAME_TH||' WHERE USER_NAME ='''||USER_NAME||'''';
        --Dbms_Output.Put_Line(P_DELETE);
        EXECUTE IMMEDIATE P_DELETE;  
    END;
    END IF;

    BEGIN
        SELECT COUNT(*)  INTO N_COUNT  FROM ALL_TAB_COLUMNS  WHERE TABLE_NAME = UPPER(P_TABLENAME_TH2);
        EXCEPTION WHEN OTHERS THEN N_COUNT:=0;
    END;
    IF(N_COUNT IS NULL OR N_COUNT<=0) THEN
    BEGIN
        EXECUTE IMMEDIATE P_SQL_CREATE_TABLE_TH2;  
    END;
    END IF;
    IF(N_COUNT>0) THEN
    BEGIN
        P_DELETE:= 'DELETE FROM '||P_TABLENAME_TH2||' WHERE USER_NAME ='''||USER_NAME||'''';
        --Dbms_Output.Put_Line(P_DELETE);
        EXECUTE IMMEDIATE P_DELETE;  
    END;
    END IF;

P_NAMBC:=' AND NGAY_KET_SO >= '''||TUNGAY_KS||''' 
            AND NGAY_KET_SO <= '''||DENNGAY_KS||'''
            AND NGAY_HIEU_LUC >= '''||TUNGAY_HL||'''
            AND NGAY_HIEU_LUC <= '''||DENNGAY_HL||'''';P_KYBC:='';P_WHERE:='';
P_WHERE:=' AND bi.MA_TKTN IN (8113,8116,8955,8954,8953,8959) ';
IF LOAIBC = 1 THEN
BEGIN
LST_QHNS:='';
P_LSTQHNS:='SELECT MA_QHNS FROM AU_NGUOIDUNG WHERE USERNAME =:1';
EXECUTE IMMEDIATE P_LSTQHNS INTO LST_QHNS USING USER_NAME;
    IF(LST_QHNS IS NOT NULL) THEN
    P_WHERE:=P_WHERE||' AND (bi.MA_DVQHNS IN('||LST_QHNS||'))';
    ELSE P_WHERE:=P_WHERE||' AND (1=2)';
    END IF;
END;
ELSIF LOAIBC = 2 THEN
BEGIN
    P_WHERE:=' AND (bi.MA_DVQHNS IN('||DSDVQHNS||')';
    IF CHITIET = 1 THEN P_WHERE:=P_WHERE||' OR bi.MA_DVQHNS IN('||DSDVQHNS||') )'; 
    ELSE P_WHERE:=P_WHERE||')';
    END IF;
END;
ELSIF LOAIBC = 3 THEN P_WHERE:=' AND bi.MA_DVQHNS IN('||DSDVQHNS||')';
END IF;


P_QUERY := 'SELECT * FROM '||P_TABLENAME_TH||'';

 P_INSERT2 := 'INSERT INTO '||P_TABLENAME_TH2||' (USER_NAME,MA_CHUONG,MA_LOAI,MA_NGANHKT,MA_MUC,MA_TIEUMUC,TEN_TIEUMUC,MA_TKTN,GIA_TRI_HACH_TOAN) 
SELECT Cast('''||USER_NAME||''' as nvarchar2(50)) as USER_NAME,bi.MA_CHUONG,bi.MA_LOAI, bi.MA_NGANHKT, bi.MA_MUC, bi.MA_TIEUMUC, bi.TEN_TIEUMUC,bi.MA_TKTN,
SUM(bi.GIA_TRI_HACH_TOAN)
from PHA_HACHTOAN_CHI bi
where 1 = 1 '||P_NAMBC||P_WHERE||'
 GROUP BY bi.MA_CHUONG,bi.MA_LOAI, bi.MA_NGANHKT, bi.MA_MUC, bi.MA_TIEUMUC, bi.TEN_TIEUMUC,bi.MA_TKTN';

P_INSERT := 'INSERT INTO '||P_TABLENAME_TH||' (USER_NAME,MA_CHUONG,MA_LOAI,MA_NGANHKT,MA_MUC,MA_TIEUMUC,TEN_TIEUMUC) 
SELECT Cast('''||USER_NAME||''' as nvarchar2(50)) as USER_NAME,bi.MA_CHUONG,bi.MA_LOAI, bi.MA_NGANHKT, bi.MA_MUC, bi.MA_TIEUMUC, bi.TEN_TIEUMUC
from '||P_TABLENAME_TH2||' bi
GROUP BY bi.MA_CHUONG,bi.MA_LOAI, bi.MA_NGANHKT, bi.MA_MUC, bi.MA_TIEUMUC, bi.TEN_TIEUMUC';

 BEGIN
          DBMS_OUTPUT.put_line (P_INSERT2);
      EXECUTE IMMEDIATE  P_INSERT2;

    DBMS_OUTPUT.put_line (P_INSERT);
      EXECUTE IMMEDIATE  P_INSERT;

END;

FOR item IN (SELECT * FROM PHB_BIEU02CP2_TABMIT)
LOOP
    P_UPDATE_COT_NS_TRONGNUOC :='UPDATE '||P_TABLENAME_TH||' SET NS_TRONGNUOC = 
    (SELECT SUM (GIA_TRI_HACH_TOAN) from '||P_TABLENAME_TH2||' bi WHERE bi.MA_TKTN IN (8113,8116)
    AND bi.MA_CHUONG = '''||item.MA_CHUONG||''' AND bi.MA_LOAI = '''||item.MA_LOAI||''' AND bi.MA_NGANHKT = '''||item.MA_NGANHKT||''' AND bi.MA_MUC = '''||item.MA_MUC||''' AND bi.MA_TIEUMUC = '''||item.MA_TIEUMUC||''')
    WHERE USER_NAME = '''||USER_NAME||'''
    AND MA_CHUONG = '''||item.MA_CHUONG||'''
    AND MA_LOAI = '''||item.MA_LOAI||'''
    AND MA_NGANHKT = '''||item.MA_NGANHKT||'''
    AND MA_MUC = '''||item.MA_MUC||'''
    AND MA_TIEUMUC = '''||item.MA_TIEUMUC||'''';
    P_UPDATE_COT_VAY_NO_NN :='UPDATE '||P_TABLENAME_TH||' SET VAY_NO_NN = 
    (SELECT SUM (GIA_TRI_HACH_TOAN) from '||P_TABLENAME_TH2||' bi WHERE bi.MA_TKTN IN (8954)
    AND bi.MA_CHUONG = '''||item.MA_CHUONG||''' AND bi.MA_LOAI = '''||item.MA_LOAI||''' AND bi.MA_NGANHKT = '''||item.MA_NGANHKT||''' AND bi.MA_MUC = '''||item.MA_MUC||''' AND bi.MA_TIEUMUC = '''||item.MA_TIEUMUC||''')
    WHERE USER_NAME = '''||USER_NAME||'''
    AND MA_CHUONG = '''||item.MA_CHUONG||'''
    AND MA_LOAI = '''||item.MA_LOAI||'''
    AND MA_NGANHKT = '''||item.MA_NGANHKT||'''
    AND MA_MUC = '''||item.MA_MUC||'''
    AND MA_TIEUMUC = '''||item.MA_TIEUMUC||'''';
    P_UPDATE_COT_VIEN_TRO :='UPDATE '||P_TABLENAME_TH||' SET VIEN_TRO = 
    (SELECT SUM (GIA_TRI_HACH_TOAN) from '||P_TABLENAME_TH2||' bi WHERE bi.MA_TKTN IN (8955)
    AND bi.MA_CHUONG = '''||item.MA_CHUONG||''' AND bi.MA_LOAI = '''||item.MA_LOAI||''' AND bi.MA_NGANHKT = '''||item.MA_NGANHKT||''' AND bi.MA_MUC = '''||item.MA_MUC||''' AND bi.MA_TIEUMUC = '''||item.MA_TIEUMUC||''')
    WHERE USER_NAME = '''||USER_NAME||'''
    AND MA_CHUONG = '''||item.MA_CHUONG||'''
    AND MA_LOAI = '''||item.MA_LOAI||'''
    AND MA_NGANHKT = '''||item.MA_NGANHKT||'''
    AND MA_MUC = '''||item.MA_MUC||'''
    AND MA_TIEUMUC = '''||item.MA_TIEUMUC||'''';
    P_UPDATE_COT_PHI_KT_DELAI :='UPDATE '||P_TABLENAME_TH||' SET PHI_KT_DELAI = 
    (SELECT SUM (GIA_TRI_HACH_TOAN) from '||P_TABLENAME_TH2||' bi WHERE bi.MA_TKTN IN (8953)
    AND bi.MA_CHUONG = '''||item.MA_CHUONG||''' AND bi.MA_LOAI = '''||item.MA_LOAI||''' AND bi.MA_NGANHKT = '''||item.MA_NGANHKT||''' AND bi.MA_MUC = '''||item.MA_MUC||''' AND bi.MA_TIEUMUC = '''||item.MA_TIEUMUC||''')
    WHERE USER_NAME = '''||USER_NAME||'''
    AND MA_CHUONG = '''||item.MA_CHUONG||'''
    AND MA_LOAI = '''||item.MA_LOAI||'''
    AND MA_NGANHKT = '''||item.MA_NGANHKT||'''
    AND MA_MUC = '''||item.MA_MUC||'''
    AND MA_TIEUMUC = '''||item.MA_TIEUMUC||'''';
    P_UPDATE_COT_NGUON_KHAC :='UPDATE '||P_TABLENAME_TH||' SET NGUON_KHAC = 
    (SELECT SUM (GIA_TRI_HACH_TOAN) from '||P_TABLENAME_TH2||' bi WHERE bi.MA_TKTN IN (8959)
    AND bi.MA_CHUONG = '''||item.MA_CHUONG||''' AND bi.MA_LOAI = '''||item.MA_LOAI||''' AND bi.MA_NGANHKT = '''||item.MA_NGANHKT||''' AND bi.MA_MUC = '''||item.MA_MUC||''' AND bi.MA_TIEUMUC = '''||item.MA_TIEUMUC||''')
    WHERE USER_NAME = '''||USER_NAME||'''
    AND MA_CHUONG = '''||item.MA_CHUONG||'''
    AND MA_LOAI = '''||item.MA_LOAI||'''
    AND MA_NGANHKT = '''||item.MA_NGANHKT||'''
    AND MA_MUC = '''||item.MA_MUC||'''
    AND MA_TIEUMUC = '''||item.MA_TIEUMUC||'''';

    begin
      EXECUTE IMMEDIATE  P_UPDATE_COT_NS_TRONGNUOC;
      EXECUTE IMMEDIATE  P_UPDATE_COT_VAY_NO_NN;
      EXECUTE IMMEDIATE  P_UPDATE_COT_VIEN_TRO;
      EXECUTE IMMEDIATE  P_UPDATE_COT_PHI_KT_DELAI;
      EXECUTE IMMEDIATE  P_UPDATE_COT_NGUON_KHAC;

    end;
END LOOP;

OPEN cur FOR P_QUERY;
END PHB_BIEU02CP2_SUMREPORT_TABMIT;

/
