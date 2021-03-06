﻿create or replace PROCEDURE "PHA_THU2_BG" (P_MA_DBHC IN NVARCHAR2, P_CONGTHUC VARCHAR2, TUNGAY_KS IN DATE, DENNGAY_KS IN DATE, TUNGAY_HL IN DATE, DENNGAY_HL IN DATE, DONVI_TIEN number, cur OUT SYS_REFCURSOR) as
  QUERY_STR  CLOB; 
  QUERY_STR1  CLOB; 
   INSERT_STR  CLOB; 
   INSERT_STR1  CLOB;
   QUERY_STR_THU CLOB;
   QUERY_STR_DUTOAN CLOB;
   P_CT VARCHAR2(32767);   
   P_SQL_INSERT VARCHAR2(32767); 
   P_SQL_CREATE_TABLE_TH  VARCHAR2(32767);  
    N_COUNT NUMBER(17,4):=0;
    P_SELECT_DBHC VARCHAR2(32767);

   --THU
   CT_32110 VARCHAR2(32767);   CT_32120 VARCHAR2(32767);   CT_32150 VARCHAR2(32767);   CT_33000 VARCHAR2(32767); CT_35000 VARCHAR2(32767); CT_36000 VARCHAR2(32767);
   CT_38000 VARCHAR2(32767);   CT_39000 VARCHAR2(32767);   CT_40000 VARCHAR2(32767);   CT_47010 VARCHAR2(32767);
   CT_47050 VARCHAR2(32767);   CT_56110 VARCHAR2(32767);   CT_65110_H VARCHAR2(32767);   CT_65120_H VARCHAR2(32767);

   BEGIN
    IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA_HCSN(P_CONGTHUC) INTO P_CT from dual;       
        P_CT:= ' AND ' || P_CT;
    END IF;

    --Gán công thức 
    -------Chi đầu tư    
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_32110 from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='THU2' AND MA_COT='32110' AND NGAY_HL <= TO_DATE (''||to_char(TUNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND NGAY_HET_HL >= TO_DATE (''||to_char(DENNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND MA_DBHC LIKE (SELECT MA_T FROM DM_DBHC WHERE MA_DBHC LIKE ''||P_MA_DBHC||'');
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_32120 from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='THU2' AND MA_COT='32120' AND NGAY_HL <= TO_DATE (''||to_char(TUNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND NGAY_HET_HL >= TO_DATE (''||to_char(DENNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND MA_DBHC LIKE (SELECT MA_T FROM DM_DBHC WHERE MA_DBHC LIKE ''||P_MA_DBHC||'');
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_32150 from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='THU2' AND MA_COT='32150' AND NGAY_HL <= TO_DATE (''||to_char(TUNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND NGAY_HET_HL >= TO_DATE (''||to_char(DENNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND MA_DBHC LIKE (SELECT MA_T FROM DM_DBHC WHERE MA_DBHC LIKE ''||P_MA_DBHC||'');
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_33000 from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='THU2' AND MA_COT='33000' AND NGAY_HL <= TO_DATE (''||to_char(TUNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND NGAY_HET_HL >= TO_DATE (''||to_char(DENNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND MA_DBHC LIKE (SELECT MA_T FROM DM_DBHC WHERE MA_DBHC LIKE ''||P_MA_DBHC||'');
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_35000 from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='THU2' AND MA_COT='35000' AND NGAY_HL <= TO_DATE (''||to_char(TUNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND NGAY_HET_HL >= TO_DATE (''||to_char(DENNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND MA_DBHC LIKE (SELECT MA_T FROM DM_DBHC WHERE MA_DBHC LIKE ''||P_MA_DBHC||'');
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_36000 from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='THU2' AND MA_COT='36000' AND NGAY_HL <= TO_DATE (''||to_char(TUNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND NGAY_HET_HL >= TO_DATE (''||to_char(DENNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND MA_DBHC LIKE (SELECT MA_T FROM DM_DBHC WHERE MA_DBHC LIKE ''||P_MA_DBHC||'');
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_38000 from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='THU2' AND MA_COT='38000' AND NGAY_HL <= TO_DATE (''||to_char(TUNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND NGAY_HET_HL >= TO_DATE (''||to_char(DENNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND MA_DBHC LIKE (SELECT MA_T FROM DM_DBHC WHERE MA_DBHC LIKE ''||P_MA_DBHC||'');
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_39000 from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='THU2' AND MA_COT='39000' AND NGAY_HL <= TO_DATE (''||to_char(TUNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND NGAY_HET_HL >= TO_DATE (''||to_char(DENNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND MA_DBHC LIKE (SELECT MA_T FROM DM_DBHC WHERE MA_DBHC LIKE ''||P_MA_DBHC||'');
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_40000 from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='THU2' AND MA_COT='40000' AND NGAY_HL <= TO_DATE (''||to_char(TUNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND NGAY_HET_HL >= TO_DATE (''||to_char(DENNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND MA_DBHC LIKE (SELECT MA_T FROM DM_DBHC WHERE MA_DBHC LIKE ''||P_MA_DBHC||'');
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_47010 from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='THU2' AND MA_COT='47010' AND NGAY_HL <= TO_DATE (''||to_char(TUNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND NGAY_HET_HL >= TO_DATE (''||to_char(DENNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND MA_DBHC LIKE (SELECT MA_T FROM DM_DBHC WHERE MA_DBHC LIKE ''||P_MA_DBHC||'');
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_47050 from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='THU2' AND MA_COT='47050' AND NGAY_HL <= TO_DATE (''||to_char(TUNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND NGAY_HET_HL >= TO_DATE (''||to_char(DENNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND MA_DBHC LIKE (SELECT MA_T FROM DM_DBHC WHERE MA_DBHC LIKE ''||P_MA_DBHC||'');
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_56110 from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='THU2' AND MA_COT='56110' AND NGAY_HL <= TO_DATE (''||to_char(TUNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND NGAY_HET_HL >= TO_DATE (''||to_char(DENNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND MA_DBHC LIKE (SELECT MA_T FROM DM_DBHC WHERE MA_DBHC LIKE ''||P_MA_DBHC||'');
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_65110_H from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='THU2' AND MA_COT='65110_H' AND NGAY_HL <= TO_DATE (''||to_char(TUNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND NGAY_HET_HL >= TO_DATE (''||to_char(DENNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND MA_DBHC LIKE (SELECT MA_T FROM DM_DBHC WHERE MA_DBHC LIKE ''||P_MA_DBHC||'');
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_65120_H from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='THU2' AND MA_COT='65120_H' AND NGAY_HL <= TO_DATE (''||to_char(TUNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND NGAY_HET_HL >= TO_DATE (''||to_char(DENNGAY_HL,'ddMMyyyy')||'', 'ddMMyyyy') AND MA_DBHC LIKE (SELECT MA_T FROM DM_DBHC WHERE MA_DBHC LIKE ''||P_MA_DBHC||'');
    

    P_SQL_CREATE_TABLE_TH := 'CREATE TABLE THU2_BG(
        LOAI_CHITIEU NUMBER,MA_TKTN NVARCHAR2(100), MA_DIABAN NVARCHAR2(100),TEN_DIABAN NVARCHAR2(2000),MA_CAP NVARCHAR2(100),MA_CAPMLNS NVARCHAR2(100),MA_CHUONG NVARCHAR2(100),TEN_CHUONG NVARCHAR2(2000),MA_NGANHKT NVARCHAR2(100),MA_LOAI NVARCHAR2(100),
        MA_TIEUMUC NVARCHAR2(100), MA_MUC NVARCHAR2(100), MA_TIEUNHOM NVARCHAR2(100),MA_NHOM NVARCHAR2(100),MA_CTMTQG NVARCHAR2(100),MA_KBNN NVARCHAR2(100), NGAY_HIEU_LUC DATE, NGAY_KET_SO DATE,LOAI_DU_TOAN NVARCHAR2(100),
        MA_NGUON_NSNN NVARCHAR2(100),MA_DVQHNS NVARCHAR2(100),TEN_DVQHNS NVARCHAR2(2000), MA_NVC NVARCHAR2(100), GIA_TRI_HACH_TOAN NUMBER)SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  ';         

     QUERY_STR_THU := 'SELECT 2,MA_TKTN ,MA_DIABAN, TEN_DIABAN,MA_CAP,MA_CAPMLNS,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,MA_LOAI,MA_TIEUMUC,MA_MUC,MA_TIEUNHOM,MA_NHOM,MA_CTMTQG,MA_KBNN,MA_NGUON_NSNN,MA_DVQHNS, TEN_DVQHNS,MA_NVC,TO_DATE(to_char(NGAY_HIEU_LUC,''ddMMyyyy''), ''ddMMyyyy''),TO_DATE (to_char(NGAY_KET_SO,''ddMMyyyy''), ''ddMMyyyy''),ATTRIBUTE8
        ,SUM (GIA_TRI_HACH_TOAN) FROM PHA_HACHTOAN_THU VC 
         GROUP BY  MA_TKTN,MA_DIABAN, TEN_DIABAN,MA_CAP,MA_CAPMLNS,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,MA_LOAI,MA_TIEUMUC,MA_MUC,MA_TIEUNHOM,MA_NHOM,MA_CTMTQG,MA_KBNN,MA_NGUON_NSNN,MA_DVQHNS, TEN_DVQHNS,MA_NVC,NGAY_HIEU_LUC,NGAY_KET_SO,ATTRIBUTE8';   
        INSERT_STR := 'INSERT INTO THU2_BG(LOAI_CHITIEU,MA_TKTN ,MA_DIABAN, TEN_DIABAN,MA_CAP,MA_CAPMLNS,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,MA_LOAI, MA_TIEUMUC,MA_MUC,MA_TIEUNHOM,MA_NHOM,MA_CTMTQG,MA_KBNN,MA_NGUON_NSNN,MA_DVQHNS, TEN_DVQHNS,MA_NVC,NGAY_HIEU_LUC,NGAY_KET_SO,LOAI_DU_TOAN,GIA_TRI_HACH_TOAN)('||QUERY_STR_THU||')';

   QUERY_STR_DUTOAN := 'SELECT 3,MA_TKTN ,MA_DIABAN, TEN_DIABAN,MA_CAP,MA_CAPMLNS,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,MA_LOAI,MA_TIEUMUC,MA_MUC,MA_TIEUNHOM,MA_NHOM,MA_CTMTQG,MA_KBNN,MA_NGUON_NSNN,MA_DVQHNS, TEN_DVQHNS,MA_NVC,TO_DATE(to_char(NGAY_HIEU_LUC,''ddMMyyyy''), ''ddMMyyyy''),TO_DATE (to_char(NGAY_KET_SO,''ddMMyyyy''), ''ddMMyyyy''),ATTRIBUTE8
        ,SUM (GIA_TRI_HACH_TOAN) FROM PHA_DUTOAN VC 
         GROUP BY  MA_TKTN,MA_DIABAN, TEN_DIABAN,MA_CAP,MA_CAPMLNS,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,MA_LOAI,MA_TIEUMUC,MA_MUC,MA_TIEUNHOM,MA_NHOM,MA_CTMTQG,MA_KBNN,MA_NGUON_NSNN,MA_DVQHNS, TEN_DVQHNS,MA_NVC,NGAY_HIEU_LUC,NGAY_KET_SO,ATTRIBUTE8';   
        INSERT_STR1 := 'INSERT INTO THU2_BG(LOAI_CHITIEU,MA_TKTN ,MA_DIABAN, TEN_DIABAN,MA_CAP,MA_CAPMLNS,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,MA_LOAI, MA_TIEUMUC,MA_MUC,MA_TIEUNHOM,MA_NHOM,MA_CTMTQG,MA_KBNN,MA_NGUON_NSNN,MA_DVQHNS, TEN_DVQHNS,MA_NVC,NGAY_HIEU_LUC,NGAY_KET_SO,LOAI_DU_TOAN,GIA_TRI_HACH_TOAN)('||QUERY_STR_DUTOAN||')';
  BEGIN
        SELECT COUNT(*)  INTO N_COUNT  FROM ALL_TAB_COLUMNS  WHERE TABLE_NAME = 'THU2_BG';
        EXCEPTION WHEN OTHERS THEN N_COUNT:=0;
    END;
  IF(N_COUNT IS NULL OR N_COUNT<=0) THEN
    BEGIN
        EXECUTE IMMEDIATE P_SQL_CREATE_TABLE_TH;  
        EXECUTE IMMEDIATE INSERT_STR; 
        EXECUTE IMMEDIATE INSERT_STR1;  
    END;
    END IF;
        P_SELECT_DBHC:= ' AND A.MA_DIABAN IN (SELECT MA_DBHC FROM DM_DBHC WHERE MA_DBHC IN (SELECT MA_DBHC FROM DM_DBHC WHERE MA_DBHC = '''||P_MA_DBHC||''' OR MA_DBHC_CHA = '''||P_MA_DBHC||''') OR MA_DBHC_CHA IN (SELECT MA_DBHC FROM DM_DBHC WHERE MA_DBHC = '''||P_MA_DBHC||''' OR MA_DBHC_CHA = '''||P_MA_DBHC||'''))';

        IF(P_SELECT_DBHC IS NOT NULL) THEN
        P_CT:=P_CT || P_SELECT_DBHC;
        ELSE
        P_CT:=P_CT;
        END IF;
            QUERY_STR:='SELECT HT.MA_DIABAN, HT.TEN_DIABAN
                        , NVL(HT.CT_32110,0) as CT_32110, NVL(HT.CT_32120,0) as CT_32120, NVL(HT.CT_32150,0) as CT_32150, NVL(HT.CT_33000,0) as CT_33000, NVL(HT.CT_35000,0) as CT_35000
                        , NVL(HT.CT_36000,0) as CT_36000, NVL(HT.CT_38000,0) as CT_38000, NVL(HT.CT_39000,0) as CT_39000, NVL(HT.CT_40000,0) as CT_40000
                        , NVL(HT.CT_47010,0) as CT_47010, NVL(HT.CT_47050,0) as CT_47050, NVL(HT.CT_56110,0) as CT_56110, NVL(HT.CT_65110_H,0) as CT_65110_H
                        , NVL(HT.CT_65120_H,0) as CT_65120_H
                        FROM
                        (
                        SELECT A.MA_DIABAN, A.TEN_DIABAN
                        ,NVL(SUM (CASE WHEN ('|| CT_32110 ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CT_32110
                        ,NVL(SUM (CASE WHEN ('|| CT_32120 ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CT_32120
                        ,NVL(SUM (CASE WHEN ('|| CT_32150 ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CT_32150
                        ,NVL(SUM (CASE WHEN ('|| CT_33000 ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CT_33000
                        ,NVL(SUM (CASE WHEN ('|| CT_35000 ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CT_35000
                        ,NVL(SUM (CASE WHEN ('|| CT_36000 ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CT_36000
                        ,NVL(SUM (CASE WHEN ('|| CT_38000 ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CT_38000
                        ,NVL(SUM (CASE WHEN ('|| CT_39000 ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CT_39000
                        ,NVL(SUM (CASE WHEN ('|| CT_40000 ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CT_40000
                        ,NVL(SUM (CASE WHEN ('|| CT_47010 ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CT_47010
                        ,NVL(SUM (CASE WHEN ('|| CT_47050 ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CT_47050
                        ,NVL(SUM (CASE WHEN ('|| CT_56110 ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CT_56110
                        ,NVL(SUM (CASE WHEN ('|| CT_65110_H ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CT_65110_H
                        ,NVL(SUM (CASE WHEN ('|| CT_65120_H ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CT_65120_H
                        FROM THU2_BG A
                        WHERE A.NGAY_HIEU_LUC >= TO_DATE ('''||to_char(TUNGAY_HL,'ddMMyyyy')||''', ''ddMMyyyy'')
                        AND A.NGAY_HIEU_LUC <= TO_DATE ('''||to_char(DENNGAY_HL,'ddMMyyyy')||''', ''ddMMyyyy'')     
                        and A.NGAY_KET_SO <=TO_DATE ('''||to_char(DENNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'')
                        and A.NGAY_KET_SO >=TO_DATE ('''||to_char(TUNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'')'
                        || P_CT || ' GROUP BY A.MA_DIABAN, A.TEN_DIABAN
                        ) HT 
                        WHERE 1=1 ORDER BY HT.MA_DIABAN';
                        DBMS_OUTPUT.put_line (QUERY_STR); 
BEGIN
EXECUTE IMMEDIATE QUERY_STR;

OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      --DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
END;    
END PHA_THU2_BG;