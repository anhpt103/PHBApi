﻿create or replace PROCEDURE PHA_BM101_TT343 (P_MA_DBHC IN NVARCHAR2,P_LOAI_BAOCAO IN VARCHAR2, P_CONGTHUC VARCHAR2, TUNGAY_KS IN DATE, DENNGAY_KS IN DATE, TUNGAY_HL IN DATE, DENNGAY_HL IN DATE, P_CAP VARCHAR2, DONVI_TIEN number, cur OUT SYS_REFCURSOR) as
   QUERY_STR  CLOB;
   QUERY_STR1  CLOB; 
   QUERY_STR2  CLOB;
   INSERT_STR  CLOB; 
   INSERT_STR1  CLOB;
   QUERY_STR_CHI CLOB;
   QUERY_STR_DUTOAN CLOB;
   P_CT VARCHAR2(32767);   
   P_SQL_INSERT VARCHAR2(32767); 
   TEMP VARCHAR2(32767);
   P_SQL_CREATE_TABLE_TH  VARCHAR2(32767);  
    N_COUNT NUMBER(17,4):=0;
    P_SELECT_DBHC VARCHAR2(32767);


   CT_46110 VARCHAR2(32767);   CT_46120 VARCHAR2(32767);   CT_DT_VDT VARCHAR2(32767);   CT_DT_VSN VARCHAR2(32767);
   CT_DT_CTMT VARCHAR2(32767);   CT_QT_46110 VARCHAR2(32767);   CT_QT_46120 VARCHAR2(32767);   CT_QT_VDT VARCHAR2(32767);
   CT_QT_VSN VARCHAR2(32767);   CT_QT_CTMT VARCHAR2(32767);  

   BEGIN
    IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA_HCSN(P_CONGTHUC) INTO P_CT from dual;       
        P_CT:= ' AND ' || P_CT;
    END IF;

    --Gán công thức
    IF(UPPER(P_LOAI_BAOCAO) = 'DH') THEN 
        -------Chi đầu tư
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_46110 from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='BM67_TT343' AND MA_COT='46110';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_46120 from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='BM67_TT343' AND MA_COT='46120';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_VDT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='BM67_TT343' AND MA_COT='DT_VDT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_VSN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='BM67_TT343' AND MA_COT='DT_VSN';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CTMT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='BM67_TT343' AND MA_COT='DT_CTMT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_QT_46110 from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='BM67_TT343' AND MA_COT='QT_46110';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_QT_46120 from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='BM67_TT343' AND MA_COT='QT_46120';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_QT_VDT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='BM67_TT343' AND MA_COT='QT_VDT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_QT_VSN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='BM67_TT343' AND MA_COT='QT_VSN';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_QT_CTMT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='BM67_TT343' AND MA_COT='QT_CTMT';

    END IF;



     P_SQL_CREATE_TABLE_TH := 'CREATE TABLE BM101_TT343(
        LOAI_CHITIEU NUMBER,MA_TKTN NVARCHAR2(100), MA_DIABAN NVARCHAR2(100),TEN_DIABAN NVARCHAR2(2000), MA_CAP NVARCHAR2(100),MA_CAPMLNS NVARCHAR2(100),MA_CHUONG NVARCHAR2(100),TEN_CHUONG NVARCHAR2(2000),MA_NGANHKT NVARCHAR2(100),MA_LOAI NVARCHAR2(100),
        MA_TIEUMUC NVARCHAR2(100), MA_MUC NVARCHAR2(100), MA_TIEUNHOM NVARCHAR2(100),MA_NHOM NVARCHAR2(100),MA_CTMTQG NVARCHAR2(100),MA_KBNN NVARCHAR2(100), NGAY_HIEU_LUC DATE, NGAY_KET_SO DATE,LOAI_DU_TOAN NVARCHAR2(100),
        MA_NGUON_NSNN NVARCHAR2(100),MA_DVQHNS NVARCHAR2(100), TEN_DVQHNS NVARCHAR2(2000), MA_NVC NVARCHAR2(100), GIA_TRI_HACH_TOAN NUMBER)SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  ';          

    QUERY_STR_CHI := 'SELECT 2,MA_TKTN ,MA_DIABAN, TEN_DIABAN,MA_CAP,MA_CAPMLNS,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,MA_LOAI,MA_TIEUMUC,MA_MUC,MA_TIEUNHOM,MA_NHOM,MA_CTMTQG,MA_KBNN,MA_NGUON_NSNN,MA_DVQHNS, TEN_DVQHNS,MA_NVC,TO_DATE(to_char(NGAY_HIEU_LUC,''ddMMyyyy''), ''ddMMyyyy''),TO_DATE (to_char(NGAY_KET_SO,''ddMMyyyy''), ''ddMMyyyy''),ATTRIBUTE8
        ,SUM (GIA_TRI_HACH_TOAN) FROM PHA_HACHTOAN_CHI VC 
         GROUP BY  MA_TKTN,MA_DIABAN, TEN_DIABAN,MA_CAP,MA_CAPMLNS,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,MA_LOAI,MA_TIEUMUC,MA_MUC,MA_TIEUNHOM,MA_NHOM,MA_CTMTQG,MA_KBNN,MA_NGUON_NSNN,MA_DVQHNS, TEN_DVQHNS,MA_NVC,NGAY_HIEU_LUC,NGAY_KET_SO,ATTRIBUTE8';   
        INSERT_STR := 'INSERT INTO BM101_TT343(LOAI_CHITIEU,MA_TKTN ,MA_DIABAN, TEN_DIABAN,MA_CAP,MA_CAPMLNS,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,MA_LOAI, MA_TIEUMUC,MA_MUC,MA_TIEUNHOM,MA_NHOM,MA_CTMTQG,MA_KBNN,MA_NGUON_NSNN,MA_DVQHNS, TEN_DVQHNS,MA_NVC,NGAY_HIEU_LUC,NGAY_KET_SO,LOAI_DU_TOAN,GIA_TRI_HACH_TOAN)('||QUERY_STR_CHI||')';

   QUERY_STR_DUTOAN := 'SELECT 3,MA_TKTN ,MA_DIABAN, TEN_DIABAN,MA_CAP,MA_CAPMLNS,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,MA_LOAI,MA_TIEUMUC,MA_MUC,MA_TIEUNHOM,MA_NHOM,MA_CTMTQG,MA_KBNN,MA_NGUON_NSNN,MA_DVQHNS, TEN_DVQHNS,MA_NVC,TO_DATE(to_char(NGAY_HIEU_LUC,''ddMMyyyy''), ''ddMMyyyy''),TO_DATE (to_char(NGAY_KET_SO,''ddMMyyyy''), ''ddMMyyyy''),ATTRIBUTE8
        ,SUM (GIA_TRI_HACH_TOAN) FROM PHA_DUTOAN VC 
         GROUP BY  MA_TKTN,MA_DIABAN, TEN_DIABAN,MA_CAP,MA_CAPMLNS,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,MA_LOAI,MA_TIEUMUC,MA_MUC,MA_TIEUNHOM,MA_NHOM,MA_CTMTQG,MA_KBNN,MA_NGUON_NSNN,MA_DVQHNS, TEN_DVQHNS,MA_NVC,NGAY_HIEU_LUC,NGAY_KET_SO,ATTRIBUTE8';   
        INSERT_STR1 := 'INSERT INTO BM101_TT343(LOAI_CHITIEU,MA_TKTN ,MA_DIABAN, TEN_DIABAN,MA_CAP,MA_CAPMLNS,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,MA_LOAI, MA_TIEUMUC,MA_MUC,MA_TIEUNHOM,MA_NHOM,MA_CTMTQG,MA_KBNN,MA_NGUON_NSNN,MA_DVQHNS, TEN_DVQHNS,MA_NVC,NGAY_HIEU_LUC,NGAY_KET_SO,LOAI_DU_TOAN,GIA_TRI_HACH_TOAN)('||QUERY_STR_DUTOAN||')';
  BEGIN
        SELECT COUNT(*)  INTO N_COUNT  FROM ALL_TAB_COLUMNS  WHERE TABLE_NAME = 'BM101_TT343';
        EXCEPTION WHEN OTHERS THEN N_COUNT:=0;
    END;
  IF(N_COUNT IS NULL OR N_COUNT<=0) THEN
    BEGIN
        EXECUTE IMMEDIATE P_SQL_CREATE_TABLE_TH;  
        EXECUTE IMMEDIATE INSERT_STR; 
        EXECUTE IMMEDIATE INSERT_STR1;   
    END;
    END IF;

        P_SELECT_DBHC:= ' AND MA_DIABAN IN (SELECT MA_DBHC FROM DM_DBHC WHERE MA_DBHC IN (SELECT MA_DBHC FROM DM_DBHC WHERE MA_DBHC = '''||P_MA_DBHC||''' OR MA_DBHC_CHA = '''||P_MA_DBHC||''') OR MA_DBHC_CHA IN (SELECT MA_DBHC FROM DM_DBHC WHERE MA_DBHC = '''||P_MA_DBHC||''' OR MA_DBHC_CHA = '''||P_MA_DBHC||'''))';

        IF(P_SELECT_DBHC IS NOT NULL) THEN
        P_CT:=P_CT || P_SELECT_DBHC;
        ELSE
        P_CT:=P_CT;
        END IF;

            QUERY_STR:= '
            select * from (
            SELECT HT.MA_DIABAN, HT.TEN_DIABAN
                        , NVL(HT.CT_46110,0) as CT_46110, NVL(HT.CT_46120,0) as CT_46120, NVL(HT.CT_DT_VDT,0) as CT_DT_VDT, NVL(HT.CT_DT_VSN,0) as CT_DT_VSN, NVL(HT.CT_DT_CTMT,0) as CT_DT_CTMT
                        , NVL(HT.CT_QT_46110,0) as CT_QT_46110, NVL(HT.CT_QT_46120,0) as CT_QT_46120, NVL(HT.CT_QT_VDT,0) as CT_QT_VDT, NVL(HT.CT_QT_VSN,0) as CT_QT_VSN
                        , NVL(HT.CT_QT_CTMT,0) as CT_QT_CTMT
                        FROM
                        (
                        SELECT A.MA_DIABAN , A.TEN_DIABAN 

                        ,NVL(SUM (CASE WHEN ('|| CT_46110 ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CT_46110
                        ,NVL(SUM (CASE WHEN ('|| CT_46120 ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CT_46120
                        ,SUM(0) AS CT_DT_VDT
                        ,SUM(0) AS CT_DT_VSN
                        ,SUM(0) AS CT_DT_CTMT
                        ,NVL(SUM (CASE WHEN ('|| CT_QT_46110 ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CT_QT_46110
                        ,NVL(SUM (CASE WHEN ('|| CT_QT_46120 ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CT_QT_46120
                        ,SUM(0) AS CT_QT_VDT
                        ,SUM(0) AS CT_QT_VSN
                        ,SUM(0) AS CT_QT_CTMT

                        FROM BM101_TT343 A
                        WHERE A.NGAY_HIEU_LUC >= TO_DATE ('''||to_char(TUNGAY_HL,'ddMMyyyy')||''', ''ddMMyyyy'')
                        AND A.NGAY_HIEU_LUC <= TO_DATE ('''||to_char(DENNGAY_HL,'ddMMyyyy')||''', ''ddMMyyyy'')     
                        and A.NGAY_KET_SO <=TO_DATE ('''||to_char(DENNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'')
                        and A.NGAY_KET_SO >=TO_DATE ('''||to_char(TUNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'')
                        AND NOT A.MA_CAP IN (''1'',''2'') AND NOT A.MA_CHUONG LIKE ''000'''
                        || P_CT
                        || ' GROUP BY A.MA_DIABAN,A.TEN_DIABAN
                        ) HT
                        WHERE 1=1 ORDER BY HT.MA_DIABAN   )
                        ';


DBMS_OUTPUT.put_line(QUERY_STR); 

BEGIN
EXECUTE IMMEDIATE QUERY_STR;

OPEN cur FOR QUERY_STR;
EXCEPTION
  WHEN NO_DATA_FOUND
  THEN
     DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
--    DBMS_OUTPUT.ENABLE(200000);
    DBMS_OUTPUT.ENABLE (buffer_size => NULL);
   DBMS_OUTPUT.put_line ('<your message>'  || SQLERRM); 
     --DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 

END;   
END PHA_BM101_TT343;