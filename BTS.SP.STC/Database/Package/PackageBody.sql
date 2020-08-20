--------------------------------------------------------
--  File created - Monday-July-03-2017   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for View PHA_CHI_VW
--------------------------------------------------------

  CREATE OR REPLACE FORCE VIEW "BTSTC"."PHA_CHI_VW" ("LOAI_NS", "MA_TKTN", "TEN_TKTN", "MA_DVQHNS", "TEN_DVQHNS", "MA_DIABAN", "TEN_DIABAN", "MA_CAP", "TEN_CAP", "MA_CAPMLNS", "TEN_CAPNS", "MA_CHUONG", "TEN_CHUONG", "MA_NGANHKT", "TEN_NGANHKT", "MA_LOAI", "TEN_LOAI", "MA_TIEUMUC", "TEN_TIEUMUC", "MA_MUC", "TEN_MUC", "MA_TIEUNHOM", "TEN_TIEUNHOM", "MA_NHOM", "TEN_NHOM", "MA_CTMTQG", "TEN_CTMTQG", "MA_KBNN", "TEN_KBNN", "MA_NGUON_NSNN", "TEN_NGUON_NSNN", "NGAY_KET_SO", "NGAY_HACH_TOAN", "GIA_TRI_HACH_TOAN", "LOAI_DU_TOAN") AS 
  SELECT STC_PA_SYS.FNC_GET_LOAI_NS(htc.SEGMENT3) AS LOAI_NS,  
htc.SEGMENT2 as MA_TKTN,STC_PA_SYS.FNC_GET_TEN_DM('DM_TKTN','TEN_TKTN','MA_TKTN',htc.SEGMENT2,htc.EFFECTIVE_DATE) AS TEN_TKTN, 
htc.SEGMENT5 AS MA_DVQHNS,'TEN_DVQHNS' AS TEN_DVQHNS, 
htc.SEGMENT6 AS MA_DIABAN,'TEN_DIABAN' AS TEN_DIABAN, 
htc.SEGMENT4 AS MA_CAP, STC_PA_SYS.FNC_GET_DICTIONARY('MA_CAP',htc.SEGMENT4,htc.EFFECTIVE_DATE) AS TEN_CAP,
STC_PA_SYS.FNC_GET_CONVERT_CHUONG2CAP(htc.SEGMENT7) AS MA_CAPMLNS,STC_PA_SYS.FNC_GET_DICTIONARY('MA_CAP',STC_PA_SYS.FNC_GET_CONVERT_CHUONG2CAP(htc.SEGMENT7),htc.EFFECTIVE_DATE) AS TEN_CAPNS,
htc.SEGMENT7 AS MA_CHUONG, STC_PA_SYS.FNC_GET_TEN_DM('DM_CHUONG','TEN_CHUONG','MA_CHUONG',htc.SEGMENT7,htc.EFFECTIVE_DATE) AS TEN_CHUONG,
htc.SEGMENT8 AS MA_NGANHKT, STC_PA_SYS.FNC_GET_TEN_DM('DM_NGANHKT','TEN_NGANHKT','MA_NGANHKT',htc.SEGMENT8,htc.EFFECTIVE_DATE) AS TEN_NGANHKT, 
STC_PA_SYS.FNC_GET_CONVERT_NKT2LOAI(htc.SEGMENT8) AS MA_LOAI,STC_PA_SYS.FNC_GET_DICTIONARY('MA_LOAI',STC_PA_SYS.FNC_GET_CONVERT_NDKT2MUC(htc.SEGMENT8),htc.EFFECTIVE_DATE) AS TEN_LOAI, 
htc.SEGMENT3 AS MA_TIEUMUC, STC_PA_SYS.FNC_GET_TEN_DM('DM_TIEUMUC','TEN_TIEUMUC','MA_TIEUMUC',htc.SEGMENT3,htc.EFFECTIVE_DATE) AS TEN_TIEUMUC,
STC_PA_SYS.FNC_GET_CONVERT_NDKT2MUC(htc.SEGMENT3) AS MA_MUC,STC_PA_SYS.FNC_GET_TEN_DM('DM_MUC','TEN_MUC','MA_MUC',STC_PA_SYS.FNC_GET_CONVERT_NDKT2MUC(htc.SEGMENT3),htc.EFFECTIVE_DATE) AS TEN_MUC,
STC_PA_SYS.FNC_GET_CONVERT_NDKT2TNHOM(htc.SEGMENT3) AS MA_TIEUNHOM, STC_PA_SYS.FNC_GET_TEN_DM('PHA_DM_TIEUNHOM','TEN_TIEUNHOM','MA_TIEUNHOM',STC_PA_SYS.FNC_GET_CONVERT_NDKT2TNHOM(htc.SEGMENT3),htc.EFFECTIVE_DATE) AS TEN_TIEU_NHOM, 
STC_PA_SYS.FNC_GET_CONVERT_NDKT2NHOM(htc.SEGMENT3) AS MA_NHOM,STC_PA_SYS.FNC_GET_DICTIONARY('MA_NHOM',STC_PA_SYS.FNC_GET_CONVERT_NDKT2NHOM(htc.SEGMENT3),htc.EFFECTIVE_DATE) AS TEN_NHOM, 
htc.SEGMENT9 AS MA_CTMTQG, STC_PA_SYS.FNC_GET_TEN_DM('DM_CTMTQG','TEN_CTMTQG','MA_CTMTQG',htc.SEGMENT9,htc.EFFECTIVE_DATE) AS TEN_CTMTQG, 
htc.SEGMENT10 AS MA_KBNN, STC_PA_SYS.FNC_GET_TEN_DM('DM_KBNN','TEN_KBNN','MA_KBNN',htc.SEGMENT10,htc.EFFECTIVE_DATE) AS TEN_KBNN,htc.SEGMENT11 AS MA_NGUON_NSNN,STC_PA_SYS.FNC_GET_TEN_DM('DM_NGUON_NSNN','TEN_NGUON_NSNN','MA_NGUON_NSNN',htc.SEGMENT11,htc.EFFECTIVE_DATE) AS TEN_NGUON_NSNN, 
htc.POSTED_DATE AS NGAY_KET_SO,htc.EFFECTIVE_DATE AS NGAY_HACH_TOAN,(HTC.ENTERED_DR - HTC.ENTERED_CR) AS GIA_TRI_HACH_TOAN, htc.ATTRIBUTE8 AS LOAI_DU_TOAN
FROM PHA_HACHTOAN_CHI htc
;
--------------------------------------------------------
--  DDL for View PHA_THU_VW
--------------------------------------------------------

  CREATE OR REPLACE FORCE VIEW "BTSTC"."PHA_THU_VW" ("LOAI_NS", "MA_TKTN", "TEN_TKTN", "MA_DVQHNS", "TEN_DVQHNS", "MA_DIABAN", "TEN_DIABAN", "MA_CAP", "TEN_CAP", "MA_CAPMLNS", "TEN_CAPNS", "MA_CHUONG", "TEN_CHUONG", "MA_NGANHKT", "TEN_NGANHKT", "MA_LOAI", "TEN_LOAI", "MA_TIEUMUC", "TEN_TIEUMUC", "MA_MUC", "TEN_MUC", "MA_TIEUNHOM", "TEN_TIEUNHOM", "MA_NHOM", "TEN_NHOM", "MA_CTMTQG", "TEN_CTMTQG", "MA_KBNN", "TEN_KBNN", "MA_NGUON_NSNN", "TEN_NGUON_NSNN", "NGAY_KET_SO", "NGAY_HACH_TOAN", "GIA_TRI_HACH_TOAN", "LOAI_DU_TOAN") AS 
  SELECT STC_PA_SYS.FNC_GET_LOAI_NS(htc.SEGMENT3) AS LOAI_NS,  
htc.SEGMENT2 as MA_TKTN,STC_PA_SYS.FNC_GET_TEN_DM('DM_TKTN','TEN_TKTN','MA_TKTN',htc.SEGMENT2,htc.EFFECTIVE_DATE) AS TEN_TKTN, 
htc.SEGMENT5 AS MA_DVQHNS,'TEN_DVQHNS' AS TEN_DVQHNS, 
htc.SEGMENT6 AS MA_DIABAN,'TEN_DIABAN' AS TEN_DIABAN, 
htc.SEGMENT4 AS MA_CAP, STC_PA_SYS.FNC_GET_DICTIONARY('MA_CAP',htc.SEGMENT4,htc.EFFECTIVE_DATE) AS TEN_CAP,
STC_PA_SYS.FNC_GET_CONVERT_CHUONG2CAP(htc.SEGMENT7) AS MA_CAPMLNS,STC_PA_SYS.FNC_GET_DICTIONARY('MA_CAP',STC_PA_SYS.FNC_GET_CONVERT_CHUONG2CAP(htc.SEGMENT7),htc.EFFECTIVE_DATE) AS TEN_CAPNS,
htc.SEGMENT7 AS MA_CHUONG, STC_PA_SYS.FNC_GET_TEN_DM('DM_CHUONG','TEN_CHUONG','MA_CHUONG',htc.SEGMENT7,htc.EFFECTIVE_DATE) AS TEN_CHUONG,
htc.SEGMENT8 AS MA_NGANHKT, STC_PA_SYS.FNC_GET_TEN_DM('DM_NGANHKT','TEN_NGANHKT','MA_NGANHKT',htc.SEGMENT8,htc.EFFECTIVE_DATE) AS TEN_NGANHKT, 
STC_PA_SYS.FNC_GET_CONVERT_NKT2LOAI(htc.SEGMENT8) AS MA_LOAI,STC_PA_SYS.FNC_GET_DICTIONARY('MA_LOAI',STC_PA_SYS.FNC_GET_CONVERT_NDKT2MUC(htc.SEGMENT8),htc.EFFECTIVE_DATE) AS TEN_LOAI, 
htc.SEGMENT3 AS MA_TIEUMUC, STC_PA_SYS.FNC_GET_TEN_DM('DM_TIEUMUC','TEN_TIEUMUC','MA_TIEUMUC',htc.SEGMENT3,htc.EFFECTIVE_DATE) AS TEN_TIEUMUC,
STC_PA_SYS.FNC_GET_CONVERT_NDKT2MUC(htc.SEGMENT3) AS MA_MUC,STC_PA_SYS.FNC_GET_TEN_DM('DM_MUC','TEN_MUC','MA_MUC',STC_PA_SYS.FNC_GET_CONVERT_NDKT2MUC(htc.SEGMENT3),htc.EFFECTIVE_DATE) AS TEN_MUC,
STC_PA_SYS.FNC_GET_CONVERT_NDKT2TNHOM(htc.SEGMENT3) AS MA_TIEUNHOM, STC_PA_SYS.FNC_GET_TEN_DM('PHA_DM_TIEUNHOM','TEN_TIEUNHOM','MA_TIEUNHOM',STC_PA_SYS.FNC_GET_CONVERT_NDKT2TNHOM(htc.SEGMENT3),htc.EFFECTIVE_DATE) AS TEN_TIEU_NHOM, 
STC_PA_SYS.FNC_GET_CONVERT_NDKT2NHOM(htc.SEGMENT3) AS MA_NHOM,STC_PA_SYS.FNC_GET_DICTIONARY('MA_NHOM',STC_PA_SYS.FNC_GET_CONVERT_NDKT2NHOM(htc.SEGMENT3),htc.EFFECTIVE_DATE) AS TEN_NHOM, 
htc.SEGMENT9 AS MA_CTMTQG, STC_PA_SYS.FNC_GET_TEN_DM('DM_CTMTQG','TEN_CTMTQG','MA_CTMTQG',htc.SEGMENT9,htc.EFFECTIVE_DATE) AS TEN_CTMTQG, 
htc.SEGMENT10 AS MA_KBNN, STC_PA_SYS.FNC_GET_TEN_DM('DM_KBNN','TEN_KBNN','MA_KBNN',htc.SEGMENT10,htc.EFFECTIVE_DATE) AS TEN_KBNN,htc.SEGMENT11 AS MA_NGUON_NSNN,STC_PA_SYS.FNC_GET_TEN_DM('DM_NGUON_NSNN','TEN_NGUON_NSNN','MA_NGUON_NSNN',htc.SEGMENT11,htc.EFFECTIVE_DATE) AS TEN_NGUON_NSNN, 
htc.POSTED_DATE AS NGAY_KET_SO,htc.EFFECTIVE_DATE AS NGAY_HACH_TOAN,(HTC.ACCOUNTED_CR - HTC.ACCOUNTED_DR) AS GIA_TRI_HACH_TOAN, htc.ATTRIBUTE8 AS LOAI_DU_TOAN
FROM PHA_HACHTOAN_THU htc
;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PRC_GET_DATA_TEST" (CONGTHUC IN VARCHAR2 ,outNumber OUT SYS_REFCURSOR) AS
  SQL_QUERRY VARCHAR(32264);
  P_CONGTHUC VARCHAR(32264);
BEGIN 
    P_CONGTHUC := STC_PA_SYS.FNC_CONVERT_FORMULA (CONGTHUC);
    SQL_QUERRY :=  'SELECT NVL( SUM (GIA_TRI_HACH_TOAN),0) AS GIATRI FROM TEMP_MLNS WHERE 1=1 AND (' || P_CONGTHUC || ')';        
BEGIN
DBMS_OUTPUT.put_line (SQL_QUERRY);
OPEN outNumber FOR SQL_QUERRY;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line (SQL_QUERRY  || SQLERRM);
END;
END PRC_GET_DATA_TEST;

/
--------------------------------------------------------
--  DDL for Procedure PROC_BCDT_CHI_DTXD_CB
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_BCDT_CHI_DTXD_CB" (P_CONGTHUC VARCHAR2,P_TUNGAY_HL DATE, P_DENNGAY_HL DATE, CUR OUT SYS_REFCURSOR)
as
    QUERY_STR VARCHAR(20000);
    P_INSERT VARCHAR(20000);
    P_CT VARCHAR2(32767);
begin
   IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC) INTO P_CT from dual;
        P_INSERT:= ' '||P_INSERT ||' and '||P_CT;
        END IF;
 QUERY_STR := 'SELECT *
    FROM (SELECT DT.MA_DVQHNS, 
                 DT.TEN_DVQHNS,
                 DT.LoaiDuToan,
                 DT.So_DuToan,
                 TH.So_ThucHien
            FROM (  SELECT DT.MA_DVQHNS  AS MA_DVQHNS,
                           CAST (''00'' AS NVARCHAR2 (15)) AS LoaiDuToan,
                           dvqh.TEN_DVQHNS as TEN_DVQHNS,
                           SUM (DT.GIA_TRI_HACH_TOAN) AS So_DuToan
                      FROM PHA_DUTOAN DT inner join SYS_DVQHNS dvqh on DT.MA_DVQHNS = dvqh. MA_DVQHNS 
                     WHERE     DT.MA_NGHIEPVU = ''DUTOAN_XDCB'' 
                        and DT.MA_DVQHNS like ''7%'' 
                           AND DT.NGAY_HIEU_LUC >=
                                  TO_DATE ('''||P_TUNGAY_HL||''', ''DD/MM/YY'')
                           AND DT.NGAY_HIEU_LUC <=
                                  TO_DATE ('''||P_DENNGAY_HL||''', ''DD/MM/YY'')
                  GROUP BY DT.MA_DVQHNS,dvqh.TEN_DVQHNS) DT
                 -- Left join Chi
                 LEFT JOIN
                 (  SELECT MA_DVQHNS,
                           SUM (GIA_TRI_HACH_TOAN) AS So_ThucHien
                      FROM PHA_HACHTOAN_CHI 
                     WHERE     1 = 1
                           AND NGAY_HIEU_LUC >= TO_DATE ('''||P_TUNGAY_HL||''', ''DD/MM/YY'')
                           AND NGAY_HIEU_LUC <= TO_DATE ('''||P_DENNGAY_HL||''', ''DD/MM/YY'')
                  GROUP BY MA_DVQHNS) TH
                    ON DT.MA_DVQHNS = TH.MA_DVQHNS 
          UNION all
            SELECT DT.MA_DVQHNS,
                   dvqh.TEN_DVQHNS as TEN_DVQHNS,
                   DT.ATTRIBUTE8          AS LoaiDuToan,
                   SUM (DT.GIA_TRI_HACH_TOAN) AS So_DuToan,
                   SUM(TH.GIA_TRI_HACH_TOAN)  AS So_ThucHien
              FROM PHA_DUTOAN DT inner join SYS_DVQHNS dvqh on DT.MA_DVQHNS = dvqh. MA_DVQHNS
              inner join PHA_HACHTOAN_CHI TH on dvqh.MA_DVQHNS = TH.MA_DVQHNS
             WHERE     DT.MA_NGHIEPVU = ''DUTOAN_XDCB''
                   AND DT.MA_DVQHNS like ''7%'' 
                   AND DT.NGAY_HIEU_LUC >= TO_DATE ('''||P_TUNGAY_HL||''', ''DD/MM/YY'')
                   AND DT.NGAY_HIEU_LUC <= TO_DATE ('''||P_DENNGAY_HL||''', ''DD/MM/YY'')
                   AND TH.NGAY_HIEU_LUC >= TO_DATE ('''||P_TUNGAY_HL||''', ''DD/MM/YY'')
                   AND TH.NGAY_HIEU_LUC <= TO_DATE ('''||P_DENNGAY_HL||''', ''DD/MM/YY'')
          GROUP BY DT.MA_DVQHNS, DT.ATTRIBUTE8,dvqh.TEN_DVQHNS)
          WHERE 1 = 1 '||P_INSERT||' AND LOAIDUTOAN <>''00''
ORDER BY MA_DVQHNS, LoaiDuToan ';
 BEGIN
 --DBMS_OUTPUT.put_line (QUERY_STR);
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
END;
END PROC_BCDT_CHI_DTXD_CB;

/
--------------------------------------------------------
--  DDL for Procedure PROC_BCDT_CHI_DTXD_CB_DV
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_BCDT_CHI_DTXD_CB_DV" (P_CONGTHUC VARCHAR2,P_TUNGAY_HL DATE, P_DENNGAY_HL DATE, CUR OUT SYS_REFCURSOR)
as
    QUERY_STR VARCHAR(20000);
    P_INSERT VARCHAR(20000);
    P_CT VARCHAR2(32767);
begin
    IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC) INTO P_CT from dual;
        P_INSERT:= ' '||P_INSERT ||' and '||P_CT;
        END IF;
 QUERY_STR := 'select * from (select sy.MA_DVQHNS,
                                     sy.TEN_DVQHNS,
                                     DT.TEN_NGUON_NSNN,
                                     sy.MA_DVQHNS_CHA,
                                     TongDuToan,
                                     So_ThucHien 
                             from (SELECT DT.MA_DVQHNS AS MA_DVQHNS, 
                                          dm.TEN_NGUON_NSNN as TEN_NGUON_NSNN,
                                          SUM (DT.GIA_TRI_HACH_TOAN) AS TongDuToan
                                   FROM PHA_DUTOAN DT inner join DM_NGUON_NSNN dm on dt.MA_NGUON_NSNN = dm.MA_NGUON_NSNN
                                       AND DT.MA_DVQHNS like ''7%''  
                                      WHERE 
                                      DT.NGAY_HIEU_LUC >=TO_DATE ('''||P_TUNGAY_HL||''', ''DD/MM/YY'')
                                            AND DT.NGAY_HIEU_LUC <=TO_DATE ('''||P_DENNGAY_HL||''', ''DD/MM/YY'')
                                   GROUP BY DT.MA_DVQHNS,dm.TEN_NGUON_NSNN) DT
                            -- Left join Chi
                                LEFT JOIN
                                  (SELECT MA_DVQHNS,
                                          SUM (GIA_TRI_HACH_TOAN) AS So_ThucHien
                                   FROM PHA_HACHTOAN_CHI
                                   WHERE     1 = 1 AND NGAY_HIEU_LUC >= TO_DATE ('''||P_TUNGAY_HL||''', ''DD/MM/YY'')
                                                    AND NGAY_HIEU_LUC <= TO_DATE ('''||P_DENNGAY_HL||''', ''DD/MM/YY'')
                                   GROUP BY MA_DVQHNS) TH
                                ON DT.MA_DVQHNS = TH.MA_DVQHNS 
                                left join 
                                SYS_DVQHNS sy on dt.MA_DVQHNS = sy.MA_DVQHNS
                                where 1=1 '||P_INSERT||' 
                                 )
                            order by MA_DVQHNS';
 BEGIN
 DBMS_OUTPUT.put_line (QUERY_STR);
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
END;
END PROC_BCDT_CHI_DTXD_CB_DV;

/
--------------------------------------------------------
--  DDL for Procedure PROC_BCDT_CHI_DTXD_CB_NG
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_BCDT_CHI_DTXD_CB_NG" (P_CONGTHUC VARCHAR2,P_TUNGAY_HL DATE, P_DENNGAY_HL DATE, CUR OUT SYS_REFCURSOR)
as
    QUERY_STR VARCHAR(20000);
    P_INSERT VARCHAR(20000);
    P_CT VARCHAR2(32767);
begin
    IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC) INTO P_CT from dual;
        P_INSERT:= ' '||P_INSERT ||' and '||P_CT;
        END IF;
 QUERY_STR := 'select * from (select DT.MA_DVQHNS,
                                     sy.TEN_DVQHNS,
                                     DT.TEN_NGUON_NSNN,
                                     DT.LoaiDuToan,
                                     TongDuToan,
                                     TH.So_ThucHien 
                             from (SELECT DT.MA_DVQHNS  AS MA_DVQHNS,
                           CAST (''00'' AS NVARCHAR2 (15)) AS LoaiDuToan,
                           dvqh.TEN_DVQHNS as TEN_DVQHNS,
                           SUM (DT.GIA_TRI_HACH_TOAN) AS TongDuToan,
                           dm.TEN_NGUON_NSNN as TEN_NGUON_NSNN
                      FROM PHA_DUTOAN DT inner join SYS_DVQHNS dvqh on DT.MA_DVQHNS = dvqh. MA_DVQHNS 
                      
                      inner join DM_NGUON_NSNN dm on dt.MA_NGUON_NSNN = dm.MA_NGUON_NSNN
                     WHERE     DT.MA_NGHIEPVU = ''DUTOAN_XDCB'' 
                        and DT.MA_DVQHNS like ''7%'' 
                           AND DT.NGAY_HIEU_LUC >=
                                  TO_DATE ('''||P_TUNGAY_HL||''', ''DD/MM/YY'')
                           AND DT.NGAY_HIEU_LUC <=
                                  TO_DATE ('''||P_DENNGAY_HL||''', ''DD/MM/YY'')
                  GROUP BY DT.MA_DVQHNS,dvqh.TEN_DVQHNS,dm.TEN_NGUON_NSNN) DT
                                   
                            -- Left join Chi
                                LEFT JOIN
                                  (SELECT MA_DVQHNS,
                                          SUM (GIA_TRI_HACH_TOAN) AS So_ThucHien
                                   FROM PHA_HACHTOAN_CHI
                                   WHERE     1 = 1 AND NGAY_HIEU_LUC >= TO_DATE ('''||P_TUNGAY_HL||''', ''DD/MM/YY'')
                                                    AND NGAY_HIEU_LUC <= TO_DATE ('''||P_DENNGAY_HL||''', ''DD/MM/YY'')
                                   GROUP BY MA_DVQHNS) TH
                                ON DT.MA_DVQHNS = TH.MA_DVQHNS 
                                left join 
                                SYS_DVQHNS sy on dt.MA_DVQHNS = sy.MA_DVQHNS
                                where 1=1 '||P_INSERT||' 
                                UNION all
            SELECT DT.MA_DVQHNS,
                   dvqh.TEN_DVQHNS as TEN_DVQHNS,
                   DT.TEN_NGUON_NSNN,
                   DT.ATTRIBUTE8          AS LoaiDuToan,
                   SUM (DT.GIA_TRI_HACH_TOAN) AS TongDuToan,
                   SUM (TH.GIA_TRI_HACH_TOAN) AS So_ThucHien
              FROM PHA_DUTOAN DT inner join SYS_DVQHNS dvqh on DT.MA_DVQHNS = dvqh. MA_DVQHNS inner join PHA_HACHTOAN_CHI TH on DT.MA_DVQHNS = TH.MA_DVQHNS
             WHERE     DT.MA_NGHIEPVU = ''DUTOAN_XDCB''  '||P_INSERT||' 
                   AND DT.MA_DVQHNS like ''7%'' 
                   AND DT.NGAY_HIEU_LUC >= TO_DATE ('''||P_TUNGAY_HL||''', ''DD/MM/YY'')
                   AND DT.NGAY_HIEU_LUC <= TO_DATE ('''||P_DENNGAY_HL||''', ''DD/MM/YY'')
                   AND TH.NGAY_HIEU_LUC >= TO_DATE ('''||P_TUNGAY_HL||''', ''DD/MM/YY'')
                   AND TH.NGAY_HIEU_LUC <= TO_DATE ('''||P_DENNGAY_HL||''', ''DD/MM/YY'')
          GROUP BY DT.MA_DVQHNS, DT.TEN_NGUON_NSNN, DT.ATTRIBUTE8,dvqh.TEN_DVQHNS)
          where LoaiDuToan <> ''00'' 
                            order by MA_DVQHNS,LoaiDuToan';
 BEGIN
 DBMS_OUTPUT.put_line (QUERY_STR);
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
END;
END PROC_BCDT_CHI_DTXD_CB_NG;

/
--------------------------------------------------------
--  DDL for Procedure PROC_BCDT_CHI_DV
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_BCDT_CHI_DV" (P_CONGTHUC VARCHAR2,P_TUNGAY_HL DATE, P_DENNGAY_HL DATE, CUR OUT SYS_REFCURSOR)
as
    QUERY_STR VARCHAR(20000);
    P_INSERT VARCHAR(20000);
    V_TK_GTGC VARCHAR(500) := '''8953/8954/8955/8958/8956/8957/''';
    P_CT VARCHAR2(32767);
begin
   IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC) INTO P_CT from dual;
        P_INSERT:= ' '||P_INSERT ||' and '||P_CT;
        END IF;
 QUERY_STR := 'SELECT *
    FROM (SELECT DT.MA_DVQHNS,
                 DT.TEN_DVQHNS,
                 DT.LoaiDuToan,
                 DT.So_DuToan,
                 TH.So_ThucHien,
                 TH.So_GhiThuGhiChi
            FROM (  SELECT DT.MA_DVQHNS              AS MA_DVQHNS,
                           dvqh.TEN_DVQHNS as TEN_DVQHNS,
                           CAST (''00'' AS NVARCHAR2 (15)) AS LoaiDuToan,                          
                           SUM (DT.GIA_TRI_HACH_TOAN) AS So_DuToan
                      FROM PHA_DUTOAN DT inner join SYS_DVQHNS dvqh on DT.MA_DVQHNS = dvqh.MA_DVQHNS
                     WHERE     DT.MA_NGHIEPVU = ''DUTOAN_XDCB''
                     AND DT.MA_DVQHNS <> ''0000000''
                           AND DT.NGAY_HIEU_LUC >=
                                  TO_DATE ('''||P_TUNGAY_HL||''', ''DD/MM/YY'')
                           AND DT.NGAY_HIEU_LUC <=
                                  TO_DATE ('''||P_DENNGAY_HL||''', ''DD/MM/YY'')
                  GROUP BY DT.MA_DVQHNS,dvqh.TEN_DVQHNS) DT
                 -- Left join Chi
                 LEFT JOIN
                 (  SELECT MA_DVQHNS,
                           SUM (GIA_TRI_HACH_TOAN) AS So_ThucHien,
                           SUM (CASE
                                   WHEN Instr(' || V_TK_GTGC || ',MA_TKTN || ''/'') >0
                                   THEN
                                      GIA_TRI_HACH_TOAN
                                   ELSE
                                      0
                                END) AS
                              So_GhiThuGhiChi
                      FROM PHA_HACHTOAN_CHI
                     WHERE     1 = 1
                           AND NGAY_HIEU_LUC >= TO_DATE ('''||P_TUNGAY_HL||''', ''DD/MM/YY'')
                           AND NGAY_HIEU_LUC <= TO_DATE ('''||P_DENNGAY_HL||''', ''DD/MM/YY'')
                  GROUP BY MA_DVQHNS) TH
                    ON DT.MA_DVQHNS = TH.MA_DVQHNS
          UNION all
            SELECT DT.MA_DVQHNS,
                    dvqh.TEN_DVQHNS as TEN_DVQHNS,
                   DT.ATTRIBUTE8          AS LoaiDuToan, 
                   SUM (DT.GIA_TRI_HACH_TOAN) AS So_DuToan,
                   SUM (TH.GIA_TRI_HACH_TOAN) AS So_ThucHien,
                    SUM (CASE WHEN Instr(' || V_TK_GTGC || ',TH.MA_TKTN || ''/'') >0
                                   THEN
                                      TH.GIA_TRI_HACH_TOAN
                                   ELSE
                                      0
                                END) AS So_GhiThuGhiChi
              FROM PHA_DUTOAN DT inner join SYS_DVQHNS dvqh on DT.MA_DVQHNS = dvqh. MA_DVQHNS 
              inner join PHA_HACHTOAN_CHI TH on DT.MA_DVQHNS = TH.MA_DVQHNS
             WHERE     DT.MA_NGHIEPVU = ''DUTOAN_XDCB''
             AND DT.MA_DVQHNS <> ''0000000''
                   AND DT.NGAY_HIEU_LUC >= TO_DATE ('''||P_TUNGAY_HL||''', ''DD/MM/YY'')
                   AND DT.NGAY_HIEU_LUC <= TO_DATE ('''||P_DENNGAY_HL||''', ''DD/MM/YY'')
                   AND TH.NGAY_HIEU_LUC >= TO_DATE ('''||P_TUNGAY_HL||''', ''DD/MM/YY'')
                   AND TH.NGAY_HIEU_LUC <= TO_DATE ('''||P_DENNGAY_HL||''', ''DD/MM/YY'')
          GROUP BY DT.MA_DVQHNS, DT.ATTRIBUTE8,dvqh.TEN_DVQHNS)
   WHERE 1 = 1 '||P_INSERT||' AND LOAIDUTOAN <>''00''
ORDER BY MA_DVQHNS, LOAIDUTOAN ';
--DBMS_OUTPUT.put_line ('<your message>' || QUERY_STR);
 BEGIN
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
END;
END PROC_BCDT_CHI_DV;

/
--------------------------------------------------------
--  DDL for Procedure PROC_PHA_B202
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_PHA_B202" (P_CONGTHUC VARCHAR2, TUNGAY_KS IN DATE, DENNGAY_KS IN DATE, TUNGAY_HL IN DATE, DENNGAY_HL IN DATE,DONVI_TIEN number, cur OUT SYS_REFCURSOR) as
   QUERY_STR  VARCHAR2(32767); 
   P_SQL_INSERT VARCHAR2(32767);
   P_CT VARCHAR2(32767);
   V_TU_NAM VARCHAR(4):= to_char(TUNGAY_HL,'yyyy');
   BEGIN
   IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC) INTO P_CT from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_CT;
        END IF;
   
QUERY_STR:='select 
        (case MA_NGHIEPVU when Cast(''TAMTHU''  as nvarchar2(50)) then Cast(''NGOAICANDOI'' as nvarchar2(50)) else  Cast(''TRONGCANDOI'' as nvarchar2(50)) end) as MA_NGHIEPVU,
         MA_CHUONG,        
         MA_MUC,
         MA_TIEUMUC,
         MA_CAPMLNS,
         MA_LOAI,
         MA_KBNN,
         MA_DVQHNS,
         MA_NGUON_NSNN,         
         MA_NGANHKT,
         TW_PS,
         TINH_PS,
         HUYEN_PS,
         XA_PS,
         TW_LK,
         TINH_LK,
         HUYEN_LK,
         XA_LK 
FROM (SELECT 
         MA_NGHIEPVU,
         MA_CHUONG,
         MA_CAP,
         MA_MUC,
         MA_TIEUMUC,
         MA_CAPMLNS,
         MA_LOAI,
         MA_KBNN,
         MA_DVQHNS,
         MA_NGUON_NSNN,         
         MA_NGANHKT,
         SUM (
            CASE
               WHEN (    NGAY_HIEU_LUC >= TO_DATE ('''|| to_char(TUNGAY_HL,'ddMMyyyy') ||''', ''ddMMyyyy'')
                     AND NGAY_HIEU_LUC <= TO_DATE ('''||to_char(DENNGAY_HL,'ddMMyyyy')||''', ''ddMMyyyy'')
                     and NGAY_KET_SO >=TO_DATE ('''||to_char(TUNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'') 
                     and NGAY_KET_SO <=TO_DATE ('''||to_char(DENNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy''))
               THEN
                  GIA_TRI_HACH_TOAN /NVL('|| DONVI_TIEN ||',1)
               ELSE
                  0
            END)
            AS PS,
         SUM (NVL(GIA_TRI_HACH_TOAN,0))/NVL('|| DONVI_TIEN ||',1)
            AS LK
    FROM PHA_HACHTOAN_THU b
    WHERE 1=1 and '||
    '  NGAY_HIEU_LUC >= TO_DATE ('''|| '0101' || v_tu_nam  ||''', ''ddMMyyyy'')
    AND NGAY_HIEU_LUC <= TO_DATE ('''||to_char(DENNGAY_HL,'ddMMyyyy')||''', ''ddMMyyyy'')     
    and NGAY_KET_SO <=TO_DATE ('''||to_char(DENNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'')'||P_SQL_INSERT||'
GROUP BY MA_NGHIEPVU,MA_CHUONG, MA_CAP, MA_CAPMLNS, MA_MUC, MA_TIEUMUC,MA_LOAI,MA_NGANHKT,MA_KBNN,MA_DVQHNS,MA_NGUON_NSNN
)
PIVOT ( sum(PS) as PS, Sum(LK) as LK
          FOR MA_CAP
          IN (''1'' AS TW, ''2'' AS TINH, ''3'' AS HUYEN, ''4'' AS XA)
          )order by MA_NGHIEPVU desc,MA_CAPMLNS asc, MA_CHUONG asc'; 
DBMS_OUTPUT.put_line (QUERY_STR);
BEGIN
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
END;
END PROC_PHA_B202;

/
--------------------------------------------------------
--  DDL for Procedure PROC_PHA_B202_HINH_CAY
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_PHA_B202_HINH_CAY" (P_CONGTHUC VARCHAR2, TUNGAY_KS IN DATE, DENNGAY_KS IN DATE, TUNGAY_HL IN DATE, DENNGAY_HL IN DATE,DONVI_TIEN number, cur OUT SYS_REFCURSOR) as
   QUERY_STR  VARCHAR2(32767); 
   P_SQL_INSERT VARCHAR2(32767);
   P_CT VARCHAR2(32767);
   V_TU_NAM VARCHAR(4):= to_char(TUNGAY_HL,'yyyy');
   BEGIN

   IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC) INTO P_CT from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_CT;
        END IF;
     
QUERY_STR:='select 
         MA_NGHIEPVU,
         MA_CHUONG,
         MA_MUC,
         MA_TIEUMUC,
         MA_CAPMLNS,
         MA_LOAI,
         MA_KBNN,
         MA_DVQHNS,
         MA_NGUON_NSNN,         
         MA_NGANHKT,
         TEN_CAP,
         TEN_CHUONG,
         TEN_LOAI,
         TEN_NGANHKT,
         TEN_MUC,
         TEN_TIEUMUC,
         TW_PS,
         TINH_PS,
         HUYEN_PS,
         XA_PS

FROM (SELECT 
         MA_NGHIEPVU,
         MA_CHUONG,
         MA_CAP,
         MA_MUC,
         MA_TIEUMUC,
         MA_CAPMLNS,
         MA_LOAI,
         MA_KBNN,
         MA_DVQHNS,
         MA_NGUON_NSNN,         
         MA_NGANHKT,
         TEN_CAP,
         TEN_CHUONG,
         TEN_LOAI,
         TEN_NGANHKT,
         TEN_MUC,
         TEN_TIEUMUC,
         SUM (
            CASE
               WHEN (    NGAY_HIEU_LUC >= TO_DATE ('''||TUNGAY_HL||''', ''DD/MM/YY'')
                     AND NGAY_HIEU_LUC <= TO_DATE ('''||DENNGAY_HL||''', ''DD/MM/YY'')
                     AND NGAY_KET_SO >= TO_DATE ('''||TUNGAY_KS||''', ''DD/MM/YY'') 
                     AND NGAY_KET_SO <= TO_DATE ('''||DENNGAY_KS||''', ''DD/MM/YY''))
               THEN
                  GIA_TRI_HACH_TOAN /'|| DONVI_TIEN ||'
               ELSE
                  0
            END)
            AS PS
    FROM PHA_HACHTOAN_THU b
    WHERE 1=1 and '||
    ' NGAY_HIEU_LUC >= TO_DATE ('''|| '0101' || v_tu_nam  ||''', ''ddMMyyyy'')
    AND NGAY_HIEU_LUC <= TO_DATE ('''||to_char(DENNGAY_HL,'ddMMyyyy')||''', ''ddMMyyyy'')
    AND NGAY_KET_SO <= TO_DATE ('''||to_char(DENNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'')'||P_SQL_INSERT||'    
GROUP BY MA_NGHIEPVU,MA_CHUONG, MA_CAP, MA_CAPMLNS, MA_MUC, MA_TIEUMUC,MA_LOAI,MA_NGANHKT,MA_KBNN,MA_DVQHNS,MA_NGUON_NSNN,TEN_CAP,TEN_CHUONG,TEN_LOAI,TEN_NGANHKT,TEN_MUC,TEN_TIEUMUC
)
PIVOT ( sum(PS) as PS
          FOR MA_CAP
          IN (''1'' AS TW, ''2'' AS TINH, ''3'' AS HUYEN, ''4'' AS XA)
          )
order by  MA_CHUONG'; 
DBMS_OUTPUT.put_line (QUERY_STR);
BEGIN
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
END;    
END PROC_PHA_B202_HINH_CAY;

/
--------------------------------------------------------
--  DDL for Procedure PROC_PHA_B202_HOP_MUC2
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_PHA_B202_HOP_MUC2" (P_CONGTHUC VARCHAR2, TUNGAY_KS IN DATE, DENNGAY_KS IN DATE, TUNGAY_HL IN DATE, DENNGAY_HL IN DATE,DONVI_TIEN number, cur OUT SYS_REFCURSOR) as
   QUERY_STR  VARCHAR2(32767); 
   P_SQL_INSERT VARCHAR2(32767);
   P_CT VARCHAR2(32767);
   V_TU_NAM VARCHAR(4):= to_char(TUNGAY_HL,'yyyy');
   BEGIN
   IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
         select STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC) into P_CT from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_CT;
        END IF;
  
QUERY_STR:='select * FROM (SELECT 
         MA_NGHIEPVU,
         MA_MUC,
         MA_TIEUMUC,
         MA_CAP,
         MA_CAPMLNS,
         MA_KBNN,
         MA_DVQHNS,
         MA_NGUON_NSNN,
         MA_NHOM,
         MA_TIEUNHOM,
         SUM (
            CASE
               WHEN (    NGAY_HIEU_LUC >= TO_DATE ('''|| to_char(TUNGAY_HL,'ddMMyyyy') ||''', ''ddMMyyyy'')
                     AND NGAY_HIEU_LUC <= TO_DATE ('''||to_char(DENNGAY_HL,'ddMMyyyy')||''', ''ddMMyyyy'')
                     and NGAY_KET_SO >=TO_DATE ('''||to_char(TUNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'') 
                     and NGAY_KET_SO <=TO_DATE ('''||to_char(DENNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy''))
               THEN
                  GIA_TRI_HACH_TOAN /NVL('|| DONVI_TIEN ||',1)
               ELSE
                  0
            END)
            AS PS
    FROM PHA_HACHTOAN_THU b
    WHERE 1=1 and  '||
    ' NGAY_HIEU_LUC >= TO_DATE ('''|| '0101' || v_tu_nam  ||''', ''ddMMyyyy'')
    AND NGAY_HIEU_LUC <= TO_DATE ('''||to_char(DENNGAY_HL,'ddMMyyyy')||''', ''ddMMyyyy'')
    and NGAY_KET_SO <=TO_DATE ('''||to_char(DENNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'')'
    ||P_SQL_INSERT||'
GROUP BY MA_NGHIEPVU, MA_CAP,MA_CAPMLNS, MA_MUC, MA_TIEUMUC, MA_NHOM, MA_TIEUNHOM,MA_KBNN,MA_DVQHNS,MA_NGUON_NSNN
)
PIVOT ( sum(PS) as PS
          FOR MA_CAP
          IN (''1'' AS TW, ''2'' AS TINH, ''3'' AS HUYEN, ''4'' AS XA)
          )order by MA_NGHIEPVU desc,MA_CAPMLNS asc'; 
DBMS_OUTPUT.put_line (QUERY_STR);
BEGIN
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
END;
END PROC_PHA_B202_HOP_MUC2;

/
--------------------------------------------------------
--  DDL for Procedure PROC_PHA_B202_PHANII
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_PHA_B202_PHANII" (P_CONGTHUC VARCHAR2, TUNGAY_KS IN DATE, DENNGAY_KS IN DATE, TUNGAY_HL IN DATE, DENNGAY_HL IN DATE,DONVI_TIEN number, cur OUT SYS_REFCURSOR) as
   QUERY_STR  VARCHAR2(32767); 
   P_SQL_INSERT VARCHAR2(32767);
   P_CT VARCHAR2(32767);
   V_TU_NAM VARCHAR(4):= to_char(TUNGAY_HL,'yyyy');
   BEGIN
   
   IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC) INTO P_CT from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_CT;
        END IF;
   
QUERY_STR:='select 
         (CASE MA_NGHIEPVU WHEN CAST(''TAMTHU''  AS NVARCHAR2(50)) THEN CAST(''NGOAICANDOI'' AS NVARCHAR2(50)) ELSE  CAST(''TRONGCANDOI'' AS NVARCHAR2(50)) END) AS MA_NGHIEPVU,
         MA_MUC,
         MA_TIEUMUC,
         MA_KBNN,
         MA_DVQHNS,
         MA_NGUON_NSNN,
         MA_NHOM,
         MA_TIEUNHOM, 
         TW_PS,
         TINH_PS,
         HUYEN_PS,
         XA_PS,
         TW_LK,
         TINH_LK,
         HUYEN_LK,
         XA_LK 
FROM (SELECT 
         MA_NGHIEPVU,
         MA_MUC,
         MA_TIEUMUC,
         MA_CAP,
         MA_KBNN,
         MA_DVQHNS,
         MA_NGUON_NSNN,
         MA_NHOM,
         MA_TIEUNHOM,
         SUM (
            CASE
               WHEN (    NGAY_HIEU_LUC >= TO_DATE ('''|| to_char(TUNGAY_HL,'ddMMyyyy') ||''', ''ddMMyyyy'')
                     AND NGAY_HIEU_LUC <= TO_DATE ('''||to_char(DENNGAY_HL,'ddMMyyyy')||''', ''ddMMyyyy'')
                     AND NGAY_KET_SO >=TO_DATE ('''||to_char(TUNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'') 
                     AND NGAY_KET_SO <=TO_DATE ('''||to_char(DENNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy''))
               THEN
                  GIA_TRI_HACH_TOAN /NVL('|| DONVI_TIEN ||',1)
               ELSE
                  0
            END)
            AS PS,
         SUM (NVL(GIA_TRI_HACH_TOAN,0))/NVL('|| DONVI_TIEN ||',1)
            AS LK
    FROM PHA_HACHTOAN_THU b
    WHERE 1=1 and  '||
    ' NGAY_HIEU_LUC >= TO_DATE ('''|| '0101' || v_tu_nam  ||''', ''ddMMyyyy'')
    AND NGAY_HIEU_LUC <= TO_DATE ('''||to_char(DENNGAY_HL,'ddMMyyyy')||''', ''ddMMyyyy'') 
    AND NGAY_KET_SO <=TO_DATE ('''||to_char(DENNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'')'||P_SQL_INSERT||'    
GROUP BY MA_NGHIEPVU, MA_CAP, MA_MUC, MA_TIEUMUC, MA_NHOM, MA_TIEUNHOM,MA_KBNN,MA_DVQHNS,MA_NGUON_NSNN
)
PIVOT ( sum(PS) as PS, sum(LK) as LK
          FOR MA_CAP
          IN (''1'' AS TW, ''2'' AS TINH, ''3'' AS HUYEN, ''4'' AS XA)
          )order by  MA_NGHIEPVU DESC';
DBMS_OUTPUT.put_line (QUERY_STR);
BEGIN
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
END;
END PROC_PHA_B202_PhanII;

/
--------------------------------------------------------
--  DDL for Procedure PROC_PHA_B303
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_PHA_B303" (P_CONGTHUC VARCHAR2, TUNGAY_KS IN DATE, DENNGAY_KS IN DATE, TUNGAY_HL IN DATE, DENNGAY_HL IN DATE,DONVI_TIEN number, cur OUT SYS_REFCURSOR) as
   QUERY_STR  VARCHAR2(32767); 
   P_SQL_INSERT VARCHAR2(32767);
   P_Q VARCHAR2(32767);
   V_TU_NAM VARCHAR(4):= to_char(TUNGAY_HL,'yyyy');
   BEGIN
   IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC) into P_Q from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;
QUERY_STR:='select (case MA_NGHIEPVU when Cast(''CHITAMUNG''  as nvarchar2(50)) then Cast(''NGOAICANDOI'' as nvarchar2(50)) else  Cast(''TRONGCANDOI'' as nvarchar2(50)) end) as MA_NGHIEPVU,
         MA_CHUONG,        
         MA_MUC,
         MA_TIEUMUC,
         MA_CAPMLNS,
         MA_LOAI,
         MA_KBNN,
         MA_DVQHNS,
         MA_NGUON_NSNN,         
         MA_NGANHKT,
         TW_PS,
         TINH_PS,
         HUYEN_PS,
         XA_PS,
         TW_LK,
         TINH_LK,
         HUYEN_LK,
         XA_LK 
FROM (SELECT 
         MA_NGHIEPVU,
         MA_CHUONG,
         MA_CAP,
         MA_MUC,
         MA_TIEUMUC,
         MA_CAPMLNS,
         MA_LOAI,
         MA_KBNN,
         MA_DVQHNS,
         MA_NGUON_NSNN,         
         MA_NGANHKT,
         SUM (
            CASE
               WHEN (    NGAY_HIEU_LUC >= TO_DATE ('''|| to_char(TUNGAY_HL,'ddMMyyyy') ||''', ''ddMMyyyy'')
                     AND NGAY_HIEU_LUC <= TO_DATE ('''||to_char(DENNGAY_HL,'ddMMyyyy')||''', ''ddMMyyyy'')
                     and NGAY_KET_SO >=TO_DATE ('''||to_char(TUNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'') 
                     and NGAY_KET_SO <=TO_DATE ('''||to_char(DENNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy''))
                    
               THEN
                  GIA_TRI_HACH_TOAN /NVL('|| DONVI_TIEN ||',1)
               ELSE
                  0
            END)
            AS PS,
         SUM (NVL(GIA_TRI_HACH_TOAN,0))/NVL('|| DONVI_TIEN ||',1)
            AS LK
    FROM PHA_HACHTOAN_CHI b
    WHERE 1=1 and '||
    ' NGAY_HIEU_LUC >= TO_DATE ('''|| '0101' || v_tu_nam  ||''', ''ddMMyyyy'')
    AND NGAY_HIEU_LUC <= TO_DATE ('''||to_char(DENNGAY_HL,'ddMMyyyy')||''', ''ddMMyyyy'')
    and NGAY_KET_SO <=TO_DATE ('''||to_char(DENNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'')
     ' ||P_SQL_INSERT||'
GROUP BY MA_NGHIEPVU,MA_CHUONG, MA_CAP, MA_CAPMLNS, MA_MUC, MA_TIEUMUC,MA_LOAI,MA_NGANHKT,MA_KBNN,MA_DVQHNS,MA_NGUON_NSNN
)
PIVOT ( sum(PS) as PS, Sum(LK) as LK
          FOR MA_CAP
          IN (''1'' AS TW, ''2'' AS TINH, ''3'' AS HUYEN, ''4'' AS XA)
          ) 
order by  MA_NGHIEPVU DESC';
DBMS_OUTPUT.put_line (QUERY_STR);
BEGIN
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
END;    
END PROC_PHA_B303;

/
--------------------------------------------------------
--  DDL for Procedure PROC_PHA_B303_HINH_CAY
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_PHA_B303_HINH_CAY" (P_CONGTHUC VARCHAR2,TUNGAY_KS IN DATE, DENNGAY_KS IN DATE, TUNGAY_HL IN DATE, DENNGAY_HL IN DATE,DONVI_TIEN number, cur OUT SYS_REFCURSOR) as
   QUERY_STR  VARCHAR2(32767); 
   P_SQL_INSERT VARCHAR2(32767);
   P_Q VARCHAR2(32767);
   V_TU_NAM VARCHAR(4):= to_char(TUNGAY_HL,'yyyy');
   BEGIN
   IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC) into P_Q from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;

QUERY_STR:='select * FROM (SELECT 
         MA_NGHIEPVU,
         MA_CHUONG,
         MA_CAP,
         MA_MUC,
         MA_TIEUMUC,
         MA_CAPMLNS,
         MA_LOAI,
         MA_KBNN,
         MA_DVQHNS,
         MA_NGUON_NSNN,         
         MA_NGANHKT,
         TEN_CAP,
         TEN_CHUONG,
         TEN_LOAI,
         TEN_NGANHKT,
         TEN_MUC,
         TEN_TIEUMUC,
          SUM (
            CASE
               WHEN (    NGAY_HIEU_LUC >= TO_DATE ('''||TUNGAY_HL||''', ''DD/MM/YY'')
                     AND NGAY_HIEU_LUC <= TO_DATE ('''||DENNGAY_HL||''', ''DD/MM/YY'')
                     AND NGAY_KET_SO >= TO_DATE ('''||TUNGAY_KS||''', ''DD/MM/YY'') 
                     AND NGAY_KET_SO <= TO_DATE ('''||DENNGAY_KS||''', ''DD/MM/YY''))
               THEN
                  GIA_TRI_HACH_TOAN /'|| DONVI_TIEN ||'
               ELSE
                  0
            END)
            AS PS
    FROM PHA_HACHTOAN_CHI b
    WHERE 1=1 and MA_CAP in (''2'',''3'',''4'') and '||
    ' NGAY_HIEU_LUC >= TO_DATE ('''|| '0101' || v_tu_nam  ||''', ''ddMMyyyy'')
    AND NGAY_HIEU_LUC <= TO_DATE ('''||to_char(DENNGAY_HL,'ddMMyyyy')||''', ''ddMMyyyy'') 
     and NGAY_KET_SO <=TO_DATE ('''||to_char(DENNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'') '||P_SQL_INSERT||'     
GROUP BY MA_NGHIEPVU,MA_CHUONG, MA_CAP, MA_CAPMLNS, MA_MUC, MA_TIEUMUC,MA_LOAI,MA_NGANHKT,MA_KBNN,MA_DVQHNS,MA_NGUON_NSNN,TEN_CAP,TEN_CHUONG,TEN_LOAI,TEN_NGANHKT,TEN_MUC,TEN_TIEUMUC
)
PIVOT ( sum(PS) as PS
          FOR MA_CAP
          IN (''1'' AS TW, ''2'' AS TINH, ''3'' AS HUYEN, ''4'' AS XA)
          )
order by  MA_CHUONG'; 
DBMS_OUTPUT.put_line (QUERY_STR);
BEGIN
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
END;    
END PROC_PHA_B303_HINH_CAY;

/
--------------------------------------------------------
--  DDL for Procedure PROC_PHA_B303_PHANII
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_PHA_B303_PHANII" (P_CONGTHUC VARCHAR2, TUNGAY_KS IN DATE, DENNGAY_KS IN DATE, TUNGAY_HL IN DATE, DENNGAY_HL IN DATE,DONVI_TIEN number, cur OUT SYS_REFCURSOR) as
   QUERY_STR  VARCHAR2(32767); 
   P_SQL_INSERT VARCHAR2(32767);
   P_Q VARCHAR2(32767);
   V_TU_NAM VARCHAR(4):= to_char(TUNGAY_HL,'yyyy');
   BEGIN
   IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC) into P_Q from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;
  -- IF DONVI_TIEN IS NULL THEN 
    --    DONVI_TIEN:= 1;
     --   END IF;
QUERY_STR:='select   (case MA_NGHIEPVU when Cast(''CHITAMUNG''  as nvarchar2(50)) then Cast(''NGOAICANDOI'' as nvarchar2(50)) else  Cast(''TRONGCANDOI'' as nvarchar2(50)) end) as MA_NGHIEPVU,
         MA_MUC,
         MA_TIEUMUC,
         MA_KBNN,
         MA_DVQHNS,
         MA_NGUON_NSNN,
         MA_NHOM,
         MA_TIEUNHOM,
         TW_PS,
         TINH_PS,
         HUYEN_PS,
         XA_PS,
         TW_LK,
         TINH_LK,
         HUYEN_LK,
         XA_LK 
FROM (SELECT 
         MA_NGHIEPVU,
         MA_MUC,
         MA_TIEUMUC,
         MA_CAP,
         MA_KBNN,
         MA_DVQHNS,
         MA_NGUON_NSNN,
         MA_NHOM,
         MA_TIEUNHOM,
         SUM (
            CASE
               WHEN (    NGAY_HIEU_LUC >= TO_DATE ('''|| to_char(TUNGAY_HL,'ddMMyyyy') ||''', ''ddMMyyyy'')
                     AND NGAY_HIEU_LUC <= TO_DATE ('''||to_char(DENNGAY_HL,'ddMMyyyy')||''', ''ddMMyyyy'')
                     and NGAY_KET_SO >=TO_DATE ('''||to_char(TUNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'') 
                     and NGAY_KET_SO <=TO_DATE ('''||to_char(DENNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy''))
               THEN
                  GIA_TRI_HACH_TOAN /NVL('|| DONVI_TIEN ||',1)
               ELSE
                  0
            END)
            AS PS,
         SUM (NVL(GIA_TRI_HACH_TOAN,0))/NVL('|| DONVI_TIEN ||',1)
            AS LK
    FROM PHA_HACHTOAN_CHI b
    WHERE 1=1 and '||
    ' NGAY_HIEU_LUC >= TO_DATE ('''|| '0101' || v_tu_nam  ||''', ''ddMMyyyy'')
    AND NGAY_HIEU_LUC <= TO_DATE ('''||to_char(DENNGAY_HL,'ddMMyyyy')||''', ''ddMMyyyy'')     
    and NGAY_KET_SO <=TO_DATE ('''||to_char(DENNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'') '||P_SQL_INSERT||'
GROUP BY MA_NGHIEPVU, MA_CAP, MA_MUC, MA_TIEUMUC, MA_NHOM, MA_TIEUNHOM,MA_KBNN,MA_DVQHNS,MA_NGUON_NSNN
)
PIVOT ( sum(PS) as PS, Sum(LK) as LK
          FOR MA_CAP
          IN (''1'' AS TW, ''2'' AS TINH, ''3'' AS HUYEN, ''4'' AS XA)
          )'; 
--DBMS_OUTPUT.put_line (QUERY_STR);
--RETURN;
BEGIN
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
END;
END PROC_PHA_B303_PhanII;

/
--------------------------------------------------------
--  DDL for Procedure PROC_PHA_BC_THEO_DMCT
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_PHA_BC_THEO_DMCT" (
   LOAI_BC      IN     VARCHAR2,
   mabaocao     IN     NVARCHAR2,
   DONVI_TIEN   IN     NUMBER,
   TUNGAY_HL    IN     DATE,
   DENNGAY_HL   IN     DATE,
   TUNGAY_KS    IN     DATE,
   DENNGAY_KS   IN     DATE,
   P_CONGTHUC   IN     VARCHAR2,
   CUR1            OUT SYS_REFCURSOR)
AS
   Row_Type              DM_CHITIEU_BAOCAO%ROWTYPE; -- Chuyen doi FEET sang day
   CUR                   SYS_REFCURSOR;
   STT                   NVARCHAR2 (500);
   SAPXEP                NVARCHAR2 (100);
   MA_CHITIEU            VARCHAR2 (50);
   NGAY_HL               DATE;
   NGAY_HET_HL           DATE;
   TEN_CHITIEU           VARCHAR2 (500);
   TRANGTHAI             VARCHAR2 (1);
   MA_DONG               VARCHAR2 (100);
   INDAM                 NUMBER (10, 0);
   INNGHIENG             VARCHAR2 (1);
   HIENTHI               VARCHAR2 (1);
   LOAI                  NUMBER (10, 0);
   DAU                   NUMBER (10, 0);
   ILV                   NUMBER (10, 0);
   TW_PS                 NUMBER (15, 0);
   TW_LK                 NUMBER (15, 0);
   TINH_PS               NUMBER (15, 0);
   TINH_LK               NUMBER (15, 0);
   HUYEN_PS              NUMBER (15, 0);
   HUYEN_LK              NUMBER (15, 0);
   XA_PS                 NUMBER (15, 0);
   XA_LK                 NUMBER (15, 0);

   --
   DMCT_BC_INDAM         DM_CHITIEU_BAOCAO.INDAM%TYPE;
   DMCT_BC_INNGHIENG     DM_CHITIEU_BAOCAO.INNGHIENG%TYPE;
   DMCT_BC_HIENTHI       DM_CHITIEU_BAOCAO.HIENTHI%TYPE;
   DMCT_BC_STT           DM_CHITIEU_BAOCAO.STT%TYPE;
   DMCT_BC_SAPXEP        DM_CHITIEU_BAOCAO.SAPXEP%TYPE;
   DMCT_BC_MA_DONG       DM_CHITIEU_BAOCAO.MA_DONG%TYPE;
   DMCT_BC_TEN_CHITIEU   DM_CHITIEU_BAOCAO.TEN_CHITIEU%TYPE;
   DMCT_BC_NGAY_HL       DM_CHITIEU_BAOCAO.NGAY_HL%TYPE;
   DMCT_BC_DAU           DM_CHITIEU_BAOCAO.DAU%TYPE;
BEGIN
   DELETE TEMPTB_DMCT;
    -- Tong há»£p dá»¯ liá»‡u vÃ o báº£ng temp
   --STC_PA_SYS.PRC_TH_MLNS_SYS (TUNGAY_HL, DENNGAY_HL, TUNGAY_KS, DENNGAY_KS, LOAI_BC,P_CONGTHUC);
   STC_PA_SYS.PRC_TH_MLNS (TUNGAY_HL, DENNGAY_HL, TUNGAY_KS, DENNGAY_KS, LOAI_BC);
   --return;
   -- 1. Lay du lieu [Chi tieu, so lieu TW, Tinh, Huyen, Xa (sau khi convert)] vao Con tro

   OPEN CUR FOR
      SELECT DMCT_BC.INDAM,
             DMCT_BC.INNGHIENG,
             DMCT_BC.HIENTHI,
             DMCT_BC.STT,
             DMCT_BC.SAPXEP,
             DMCT_BC.MA_DONG,
             DMCT_BC.TEN_CHITIEU,
             DMCT_BC.NGAY_HL,
             DMCT_BC.DAU,
             NVL (GT.TW_PS, 0),
             NVL (GT.TW_LK, 0),
             NVL (GT.TINH_PS, 0),
             NVL (GT.TINH_LK, 0),
             NVL (GT.HUYEN_PS, 0),
             NVL (GT.HUYEN_LK, 0),
             NVL (GT.XA_PS, 0),
             NVL (GT.XA_LK, 0)
        FROM DM_CHITIEU_BAOCAO DMCT_BC,
             TABLE (STC_PHA_BCDMCT.FCN_GET_PHA_BCDMCT (LOAI_BC,
                                                       DMCT_BC.CONGTHUC,
                                                       DMCT_BC.CONGTHUC_SEG1,
                                                       DMCT_BC.CONGTHUC_SEG2,
                                                       TUNGAY_HL,
                                                       DENNGAY_HL,
                                                       TUNGAY_KS,
                                                       DENNGAY_KS,
                                                       DONVI_TIEN)) GT
       WHERE DMCT_BC.MA_BAOCAO = mabaocao;

   -- End Of 1.
   --2. Lap Con tro sau do insert tung dong du lieu vao bang Temp
   LOOP
      FETCH CUR
         INTO /*INDAM,
              INNGHIENG,
              HIENTHI,
              STT,
              SAPXEP,
              MA_DONG,
              TEN_CHITIEU,
              NGAY_HL,
              DAU,*/
              DMCT_BC_INDAM, 
              DMCT_BC_INNGHIENG,
              DMCT_BC_HIENTHI,
              DMCT_BC_STT,
              DMCT_BC_SAPXEP,
              DMCT_BC_MA_DONG,
              DMCT_BC_TEN_CHITIEU,
              DMCT_BC_NGAY_HL,
              DMCT_BC_DAU,
              TW_PS,
              TW_LK,
              TINH_PS,
              TINH_LK,
              HUYEN_PS,
              HUYEN_LK,
              XA_PS,
              XA_LK;

      IF CUR%FOUND
      THEN
         BEGIN
            INSERT INTO temptb_dmct (STT,
                                     SAPXEP,
                                     NGAY_HL,
                                     TEN_CHITIEU,
                                     MA_DONG,
                                     INDAM,
                                     INNGHIENG,
                                     HIENTHI, 
                                     DAU,
                                     TW_PS,
                                     TW_LK,
                                     TINH_PS,
                                     TINH_LK,
                                     HUYEN_PS,
                                     HUYEN_LK,
                                     XA_PS,
                                     XA_LK)
                 VALUES (DMCT_BC_STT,
                         DMCT_BC_SAPXEP,
                         DMCT_BC_NGAY_HL,
                         DMCT_BC_TEN_CHITIEU,
                         DMCT_BC_MA_DONG,
                         DMCT_BC_INDAM,
                         DMCT_BC_INNGHIENG,
                         DMCT_BC_HIENTHI,
                         DMCT_BC_DAU,
                         TW_PS,
                         TW_LK,
                         TINH_PS,
                         TINH_LK,
                         HUYEN_PS,
                         HUYEN_LK,
                         XA_PS,
                         XA_LK);
         END;
      ELSE
         EXIT;
      END IF;
   END LOOP;

   -- End Of 2.
   -- 3. Tra du lieu ve con tro Out de len bao cao
   STC_PA_SYS.PRC_SUM_UP;

   OPEN CUR1 FOR
        SELECT *
          FROM temptb_dmct
      ORDER BY MA_DONG;
--OPEN CUR1 FOR
--     SELECT t.STT,
--            t.SAPXEP,
--            t.NGAY_HL,
--            t.TEN_CHITIEU,
--            t.MA_DONG,
--            t.INDAM,
--            t.INNGHIENG,
--            t.HIENTHI,
--            t.DAU,
--            CASE
--               WHEN DAU <> 0
--               THEN
--                  (SELECT SUM (CASE DAU WHEN 0 THEN 0 ELSE TW_PS * DAU END)
--                     FROM temptb_dmct
--                    WHERE MA_DONG LIKE t.MA_DONG || '%')
--               ELSE
--                  TW_PS
--            END
--               AS TW_PS,
--            CASE
--               WHEN DAU <> 0
--               THEN
--                  (SELECT SUM (CASE DAU WHEN 0 THEN 0 ELSE TW_LK * DAU END)
--                     FROM temptb_dmct
--                    WHERE MA_DONG LIKE t.MA_DONG || '%')
--               ELSE
--                  TW_LK
--            END
--               AS TW_LK,
--            CASE
--               WHEN DAU <> 0
--               THEN
--                  (SELECT SUM (
--                             CASE DAU WHEN 0 THEN 0 ELSE TINH_PS * DAU END)
--                     FROM temptb_dmct
--                    WHERE MA_DONG LIKE t.MA_DONG || '%')
--               ELSE
--                  TINH_PS
--            END
--               AS TINH_PS,
--            CASE
--               WHEN DAU <> 0
--               THEN
--                  (SELECT SUM (
--                             CASE DAU WHEN 0 THEN 0 ELSE TINH_LK * DAU END)
--                     FROM temptb_dmct
--                    WHERE MA_DONG LIKE t.MA_DONG || '%')
--               ELSE
--                  TINH_LK
--            END
--               AS TINH_LK,
--            CASE
--               WHEN DAU <> 0
--               THEN
--                  (SELECT SUM (
--                             CASE DAU WHEN 0 THEN 0 ELSE HUYEN_PS * DAU END)
--                     FROM temptb_dmct
--                    WHERE MA_DONG LIKE t.MA_DONG || '%')
--               ELSE
--                  HUYEN_PS
--            END
--               AS HUYEN_PS,
--            CASE
--               WHEN DAU <> 0
--               THEN
--                  (SELECT SUM (
--                             CASE DAU WHEN 0 THEN 0 ELSE HUYEN_LK * DAU END)
--                     FROM temptb_dmct
--                    WHERE MA_DONG LIKE t.MA_DONG || '%')
--               ELSE
--                  HUYEN_LK
--            END
--               AS HUYEN_LK,
--            CASE
--               WHEN DAU <> 0
--               THEN
--                  (SELECT SUM (CASE DAU WHEN 0 THEN 0 ELSE XA_PS * DAU END)
--                     FROM temptb_dmct
--                    WHERE MA_DONG LIKE t.MA_DONG || '%')
--               ELSE
--                  XA_PS
--            END
--               AS XA_PS,
--            CASE
--               WHEN DAU <> 0
--               THEN
--                  (SELECT SUM (CASE DAU WHEN 0 THEN 0 ELSE XA_LK * DAU END)
--                     FROM temptb_dmct
--                    WHERE MA_DONG LIKE t.MA_DONG || '%')
--               ELSE
--                  XA_LK
--            END
--               AS XA_LK
--       FROM temptb_dmct t
--   ORDER BY MA_DONG;
--exception when others then raise
--insert into tb_log (m) values (error_message);
END PROC_PHA_BC_THEO_DMCT;

/
--------------------------------------------------------
--  DDL for Procedure PROC_PHA_CHI_QT_NSNN
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_PHA_CHI_QT_NSNN" (P_CONGTHUC VARCHAR2,TUNGAY_KS IN DATE, DENNGAY_KS IN DATE, TUNGAY_HL IN DATE, DENNGAY_HL IN DATE,DONVI_TIEN number, cur OUT SYS_REFCURSOR) as
   QUERY_STR  VARCHAR2(32767); 
   P_SQL_INSERT VARCHAR2(32767);
   P_CT VARCHAR2(32767);
  
   BEGIN  
   IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC) INTO P_CT from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_CT;
        END IF;
  
QUERY_STR:='SELECT 
         MA_LOAI,MA_NGANHKT,MA_MUC,MA_TIEUMUC,SUM(NVL(GIA_TRI_HACH_TOAN,0))/NVL('|| DONVI_TIEN ||',1) AS SO_TIEN
    FROM PHA_HACHTOAN_CHI
    WHERE 1=1 and '||
    ' NGAY_HIEU_LUC >= TO_DATE ('''|| to_char(TUNGAY_HL,'ddMMyyyy') ||''', ''ddMMyyyy'')
      AND NGAY_HIEU_LUC <= TO_DATE ('''||to_char(DENNGAY_HL,'ddMMyyyy')||''', ''ddMMyyyy'') 
      AND NGAY_KET_SO >= TO_DATE ('''|| to_char(TUNGAY_KS,'ddMMyyyy') ||''', ''ddMMyyyy'')
      AND NGAY_KET_SO <= TO_DATE ('''||to_char(DENNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'')'||P_SQL_INSERT||' 
      
GROUP BY  MA_LOAI,MA_NGANHKT,MA_MUC,MA_TIEUMUC
ORDER BY MA_LOAI
'; 
DBMS_OUTPUT.put_line (QUERY_STR);
BEGIN
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
END;
END PROC_PHA_CHI_QT_NSNN;

/
--------------------------------------------------------
--  DDL for Procedure PROC_PHA_PL06_B50
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_PHA_PL06_B50" (  
   P_CONGTHUC      IN     nVARCHAR2,   
   CUR1            OUT SYS_REFCURSOR)
AS
BEGIN
    -- láº¥y dá»¯ liá»‡u
    OPEN CUR1 FOR SELECT  STC_PHA_BCDMCT.FCN_GET_DATA_FROM_CT(P_CONGTHUC) AS GIA_TRI FROM DUAL;
END PROC_PHA_PL06_B50;

/
--------------------------------------------------------
--  DDL for Procedure PROC_PHA_PL06_B52
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_PHA_PL06_B52" (  
   P_CONGTHUC      IN     nVARCHAR2,   
   CUR1            OUT SYS_REFCURSOR)
AS
BEGIN
  
    ---tá»•ng há»£p dá»¯ liá»‡u
    --STC_PA_SYS.PRC_TH_MLNS (TO_DATE(TO_CHAR('0101'||2015),'ddMMyyyy'), TO_DATE(TO_CHAR('3112'||2015),'ddMMyyyy'), 'DC');
    -- láº¥y dá»¯ liá»‡u
    OPEN CUR1 FOR SELECT  STC_PHA_BCDMCT.FCN_GET_DATA_FROM_CT(P_CONGTHUC) AS GIA_TRI FROM DUAL;
END PROC_PHA_PL06_B52;

/
--------------------------------------------------------
--  DDL for Procedure PROC_PHA_PL08_B04
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_PHA_PL08_B04" (P_NGUONNS VARCHAR2,P_KHOAN VARCHAR2,P_MAKB VARCHAR2,P_CAP VARCHAR2,P_CHUONG VARCHAR2, P_LOAI VARCHAR2,P_MUC VARCHAR2, P_TIEUMUC VARCHAR2, P_NAM VARCHAR2,DONVI_TIEN number, cur OUT SYS_REFCURSOR) as
   QUERY_STR  VARCHAR2(32767); 
   P_SQL_INSERT VARCHAR2(32767);
   P_Q VARCHAR2(32767);
   BEGIN
   IF TRIM(P_CAP) IS NOT NULL THEN 
        P_SQL_INSERT:= P_SQL_INSERT ||' AND b.MA_CAPMLNS in ('||P_CAP||')';
        END IF;
   IF TRIM(P_CHUONG) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_CHUONG) into P_Q from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;
   IF TRIM(P_LOAI) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_LOAI) into P_Q from dual; 
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;
   IF TRIM(P_MUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_MUC) into P_Q from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;
   IF TRIM(P_TIEUMUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_TIEUMUC) into P_Q from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;
     ---lá»�c mÃ£ kho báº¡c
   IF TRIM(P_MAKB) IS NOT NULL THEN 
        P_SQL_INSERT:= P_SQL_INSERT ||' AND b.MA_KBNN in ('||P_MAKB||')';
        END IF;
   IF TRIM(P_NGUONNS) IS NOT NULL THEN 
        P_SQL_INSERT:= P_SQL_INSERT ||' AND b.MA_DVQHNS in ('||P_NGUONNS||')';
        END IF;
   IF TRIM(P_KHOAN) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_KHOAN) into P_Q from dual; 
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;
   
QUERY_STR:='select * FROM (SELECT 
         MA_NGHIEPVU,
         MA_CHUONG,
         MA_CAP,
         MA_MUC,
         MA_TIEUMUC,
         MA_CAPMLNS,
         MA_LOAI,
         MA_KBNN,
         MA_DVQHNS,
         MA_NGUON_NSNN,         
         MA_NGANHKT,
         SUM (NVL(GIA_TRI_HACH_TOAN,0) /nvl('|| DONVI_TIEN ||',1))AS LK
    FROM PHA_HACHTOAN_THU b
    WHERE 1=1 '||P_SQL_INSERT|| 
    ' And To_Char(NGAY_HIEU_LUC,' ||    Chr(39) || 'yyyy' || Chr(39) || ') = ' || P_NAM ||
    ' And MA_NGHIEPVU In (Select MA_NGHIEPVU From Dm_NghiepVu Where SUBSTR(CQD,2,1) = ' || Chr(39)|| '1' || Chr(39) ||  ')' ||    
' GROUP BY MA_NGHIEPVU,MA_CHUONG, MA_CAP, MA_CAPMLNS, MA_MUC, MA_TIEUMUC,MA_LOAI,MA_NGANHKT,MA_KBNN,MA_DVQHNS,MA_NGUON_NSNN
)
PIVOT ( Sum(LK) as LK
          FOR MA_CAP
          IN (''1'' AS TW, ''2'' AS TINH, ''3'' AS HUYEN, ''4'' AS XA)
          )
order by  MA_NGHIEPVU DESC'; 
DBMS_OUTPUT.put_line (QUERY_STR);
BEGIN
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
END;    
END PROC_PHA_PL08_B04;

/
--------------------------------------------------------
--  DDL for Procedure PROC_PHA_PL08_B05
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_PHA_PL08_B05" (P_NGUONNS VARCHAR2,P_KHOAN VARCHAR2,P_MAKB VARCHAR2,P_CAP VARCHAR2,P_CHUONG VARCHAR2, P_LOAI VARCHAR2,P_MUC VARCHAR2, P_TIEUMUC VARCHAR2, P_NAM VARCHAR2,DONVI_TIEN number,PHANLOAI_CAP VARCHAR2, cur OUT SYS_REFCURSOR) as
   QUERY_STR  VARCHAR2(32767); 
   P_SQL_INSERT VARCHAR2(32767);
   P_Q VARCHAR2(32767);
   BEGIN
   IF TRIM(P_CAP) IS NOT NULL THEN 
        P_SQL_INSERT:= P_SQL_INSERT ||' AND b.MA_CAPMLNS in ('||P_CAP||')';
        END IF;
   IF TRIM(P_CHUONG) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_CHUONG) into P_Q from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;
   IF TRIM(P_LOAI) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_LOAI) into P_Q from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;
   IF TRIM(P_MUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_MUC) into P_Q from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;
   IF TRIM(P_TIEUMUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_TIEUMUC) into P_Q from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;
    ---lá»�c mÃ£ kho báº¡c
   IF TRIM(P_MAKB) IS NOT NULL THEN 
        P_SQL_INSERT:= P_SQL_INSERT ||' AND b.MA_KBNN in ('||P_MAKB||')';
        END IF;
   IF TRIM(P_NGUONNS) IS NOT NULL THEN 
        P_SQL_INSERT:= P_SQL_INSERT ||' AND b.MA_DVQHNS in ('||P_NGUONNS||')';
        END IF;     
   IF TRIM(P_KHOAN) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_KHOAN) into P_Q from dual; 
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;
   -- PHANLOAI_CAP = 0 lÃ  cáº¥p sá»Ÿ
   -- PHANLOAI_CAP = 1 lÃ  cáº¥p Ä‘á»‹a phÆ°Æ¡ng
IF PHANLOAI_CAP = '0' THEN 
QUERY_STR:='select * FROM (SELECT 
         MA_NGHIEPVU,
         MA_CHUONG,
         MA_CAP,
         MA_MUC,
         MA_TIEUMUC,
         MA_CAPMLNS,
         MA_LOAI,
         MA_KBNN,
         MA_DVQHNS,
         MA_NGUON_NSNN,         
         MA_NGANHKT,
         SUM (NVL(GIA_TRI_HACH_TOAN,0) /nvl('|| DONVI_TIEN ||',1))AS LK
    FROM PHA_HACHTOAN_CHI b
    WHERE 1=1 and MA_CAP IN (''2'',''3'',''4'') '||P_SQL_INSERT|| 
    ' And To_Char(NGAY_HIEU_LUC,' ||    Chr(39) || 'yyyy' || Chr(39) || ') = ' || P_NAM ||
    -- Dá»® LIá»†U HIá»†N Táº I TRONG Báº¢NG Dm_NghiepVu chÆ°a cÃ³ MA_NGHIEPVU = Chi_Test
 ' And MA_NGHIEPVU In (Select MA_NGHIEPVU From Dm_NghiepVu Where SUBSTR(CQD,2,1) = ' || Chr(39)|| '1' || Chr(39) ||  ')' ||
  ' GROUP BY MA_NGHIEPVU,MA_CHUONG, MA_CAP, MA_CAPMLNS, MA_MUC, MA_TIEUMUC,MA_LOAI,MA_NGANHKT,MA_KBNN,MA_DVQHNS,MA_NGUON_NSNN
)
order by  MA_NGHIEPVU DESC'; 
ELSE
QUERY_STR:='select * FROM (SELECT 
         MA_NGHIEPVU,
         MA_CHUONG,
         MA_CAP,
         MA_MUC,
         MA_TIEUMUC,
         MA_CAPMLNS,
         MA_LOAI,
         MA_KBNN,
         MA_DVQHNS,
         MA_NGUON_NSNN,         
         MA_NGANHKT,
         SUM (NVL(GIA_TRI_HACH_TOAN,0) /nvl('|| DONVI_TIEN ||',1))AS LK
    FROM PHA_HACHTOAN_CHI b
    WHERE 1=1 and MA_CAP IN (''3'',''4'') '||P_SQL_INSERT|| 
    ' And To_Char(NGAY_HIEU_LUC,' ||    Chr(39) || 'yyyy' || Chr(39) || ') = ' || P_NAM ||
    ' And MA_NGHIEPVU In (Select MA_NGHIEPVU From Dm_NghiepVu Where SUBSTR(CQD,2,1) = ' || Chr(39)|| '1' || Chr(39) ||  ')' || 
  'GROUP BY MA_NGHIEPVU,MA_CHUONG, MA_CAP, MA_CAPMLNS, MA_MUC, MA_TIEUMUC,MA_LOAI,MA_NGANHKT,MA_KBNN,MA_DVQHNS,MA_NGUON_NSNN
)
order by  MA_NGHIEPVU DESC'; 
END IF;

DBMS_OUTPUT.put_line (QUERY_STR);
BEGIN
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
END;    
END PROC_PHA_PL08_B05;

/
--------------------------------------------------------
--  DDL for Procedure PROC_PHA_PL08_B05_XA_PHUONG1
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_PHA_PL08_B05_XA_PHUONG1" (P_MADIABAN VARCHAR2,P_KHOAN VARCHAR2,P_CAP VARCHAR2,P_CHUONG VARCHAR2, P_LOAI VARCHAR2,P_MUC VARCHAR2, P_TIEUMUC VARCHAR2, P_NAM VARCHAR2,DONVI_TIEN number, cur OUT SYS_REFCURSOR) 
AS 
   QUERY_STR  VARCHAR2(32767); 
   P_SQL_INSERT VARCHAR2(32767);
   P_Q VARCHAR2(32767);
   str_pivot    VARCHAR2(32767);
   BEGIN
   IF TRIM(P_CAP) IS NOT NULL THEN 
        P_SQL_INSERT:= P_SQL_INSERT ||' AND b.MA_CAPMLNS in ('||P_CAP||')';
        END IF;
   IF TRIM(P_CHUONG) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_CHUONG) into P_Q from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;
   IF TRIM(P_LOAI) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_LOAI) into P_Q from dual; 
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;
   IF TRIM(P_MUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_MUC) into P_Q from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;
   IF TRIM(P_TIEUMUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_TIEUMUC) into P_Q from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;
   IF TRIM(P_KHOAN) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_KHOAN) into P_Q from dual; 
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;

INSERT INTO TEMP_PL08BM05 
(MA_NGHIEPVU,MA_CHUONG,MA_CAP,MA_DIABAN,TEN_DIABAN,MA_MUC,MA_TIEUMUC,
MA_CAPMLNS,MA_LOAI,MA_KBNN,MA_DVQHNS,MA_NGUON_NSNN,MA_NGANHKT,LK)
select * FROM (SELECT 
         MA_NGHIEPVU,
         MA_CHUONG,
         MA_CAP,         
         MA_DIABAN,
         TEN_DIABAN,
         MA_MUC,
         MA_TIEUMUC,
         MA_CAPMLNS,
         MA_LOAI,
         MA_KBNN,
         MA_DVQHNS,
         MA_NGUON_NSNN,         
         MA_NGANHKT,
         SUM (NVL(GIA_TRI_HACH_TOAN,0) /nvl(DONVI_TIEN,1))AS LK
    FROM PHA_HACHTOAN_THU b
    WHERE 1=1 and MA_CAP IN (4)  
    AND To_Char(NGAY_HIEU_LUC,'yyyy') = P_NAM
    GROUP BY MA_NGHIEPVU,MA_CHUONG, MA_CAP,MA_DIABAN,TEN_DIABAN, MA_CAPMLNS, MA_MUC, MA_TIEUMUC,MA_LOAI,MA_NGANHKT,MA_KBNN,MA_DVQHNS,MA_NGUON_NSNN
);
DECLARE    
  c_madiaban VARCHAR2(50);    
  c_tendiaban VARCHAR2(50);      
  CURSOR C_TEMP_PL08BM05 is       
    SELECT DISTINCT MA_DIABAN, TEN_DIABAN FROM TEMP_PL08BM05;
BEGIN    
  OPEN C_TEMP_PL08BM05;    
  LOOP    
  FETCH C_TEMP_PL08BM05 into c_madiaban, c_tendiaban;       
   str_pivot := str_pivot || Chr(39) || c_madiaban || Chr(39) ||'AS XA' || c_madiaban;
    EXIT WHEN C_TEMP_PL08BM05%notfound;
    --ten dia ban dang null tam thoi fix cung
    str_pivot := str_pivot || ',';
    --dbms_output.put_line(str_pivot);    
  END LOOP;    
CLOSE C_TEMP_PL08BM05; 
END;

QUERY_STR:='SELECT * FROM TEMP_PL08BM05
            PIVOT (Sum(LK) as LK
            FOR MA_DIABAN
            IN ('||str_pivot||'))';      
--DBMS_OUTPUT.put_line (QUERY_STR);
BEGIN
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
END;
     
END PROC_PHA_PL08_B05_XA_PHUONG1;

/
--------------------------------------------------------
--  DDL for Procedure PROC_PHA_PL08_BM04_C_N_TN_M_TM
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_PHA_PL08_BM04_C_N_TN_M_TM" (P_CONGTHUC VARCHAR2, P_NAM VARCHAR2,DONVI_TIEN number, cur OUT SYS_REFCURSOR) AS 
 QUERY_STR  VARCHAR2(32767); 
   P_SQL_INSERT VARCHAR2(32767);
   P_CT VARCHAR2(32767);
BEGIN
  IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC) INTO P_CT from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_CT;
        END IF;
QUERY_STR:='select * FROM (SELECT 
         MA_NGHIEPVU,
         MA_MUC,
         MA_CAP,
         MA_TIEUMUC,
         MA_CAPMLNS,
         MA_KBNN,
         MA_DVQHNS,
         MA_NGUON_NSNN,
         MA_NHOM,
         MA_TIEUNHOM,
         SUM (GIA_TRI_HACH_TOAN /'|| DONVI_TIEN ||') AS SOQUYETTOAN
    FROM PHA_HACHTOAN_THU b
    WHERE 1=1 '||P_SQL_INSERT||
    ' And To_Char(NGAY_HIEU_LUC,' ||    Chr(39) || 'yyyy' || Chr(39) || ') = ' || P_NAM ||
    ' And MA_NGHIEPVU In (Select MA_NGHIEPVU From Dm_NghiepVu Where SUBSTR(CQD,2,1) = ' || Chr(39)|| '1' || Chr(39) ||  ')' ||    
' GROUP BY MA_NGHIEPVU,MA_MUC, MA_CAP, MA_CAPMLNS, MA_MUC, MA_TIEUMUC,MA_NHOM,MA_TIEUNHOM,MA_KBNN,MA_DVQHNS,MA_NGUON_NSNN
)
PIVOT ( sum(SOQUYETTOAN) as SOQUYETTOAN
          FOR MA_CAP
          IN (''1'' AS TW, ''2'' AS TINH, ''3'' AS HUYEN, ''4'' AS XA)
          )'; 
DBMS_OUTPUT.put_line (QUERY_STR);
BEGIN
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
      END;
END PROC_PHA_PL08_BM04_C_N_TN_M_TM;

/
--------------------------------------------------------
--  DDL for Procedure PROC_PHA_PL08_BM04_HM1
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_PHA_PL08_BM04_HM1" (P_CONGTHUC VARCHAR2, P_NAM VARCHAR2,DONVI_TIEN number, cur OUT SYS_REFCURSOR) as
   QUERY_STR  VARCHAR2(32767); 
   P_SQL_INSERT VARCHAR2(32767);
   P_CT VARCHAR2(32767);
   BEGIN
  IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC) INTO P_CT from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_CT;
        END IF;
QUERY_STR:='select * FROM (SELECT 
         MA_NGHIEPVU,
         MA_MUC,
         MA_CAP,
         MA_TIEUMUC,
         MA_CAPMLNS,
         MA_KBNN,
         MA_DVQHNS,
         MA_NGUON_NSNN,
         MA_NHOM,
         MA_TIEUNHOM,
         SUM (GIA_TRI_HACH_TOAN /'|| DONVI_TIEN ||') AS SOQUYETTOAN
    FROM PHA_HACHTOAN_THU b
    WHERE 1=1 '||P_SQL_INSERT||
    ' And To_Char(NGAY_HIEU_LUC,' ||    Chr(39) || 'yyyy' || Chr(39) || ') = ' || P_NAM ||
    ' And MA_NGHIEPVU In (Select MA_NGHIEPVU From Dm_NghiepVu Where SUBSTR(CQD,2,1) = ' || Chr(39)|| '1' || Chr(39) ||  ')' ||    
' GROUP BY MA_NGHIEPVU,MA_MUC, MA_CAP, MA_CAPMLNS, MA_MUC, MA_TIEUMUC,MA_NHOM,MA_TIEUNHOM,MA_KBNN,MA_DVQHNS,MA_NGUON_NSNN
)
PIVOT ( sum(SOQUYETTOAN) as SOQUYETTOAN
          FOR MA_CAP
          IN (''1'' AS TW, ''2'' AS TINH, ''3'' AS HUYEN, ''4'' AS XA)
          )'; 
DBMS_OUTPUT.put_line (QUERY_STR);
BEGIN
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
END;
END PROC_PHA_PL08_BM04_HM1;

/
--------------------------------------------------------
--  DDL for Procedure PROC_PHA_PL08_BM05_C_C_M_TM
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_PHA_PL08_BM05_C_C_M_TM" (P_CONGTHUC VARCHAR2, P_NAM VARCHAR2,DONVI_TIEN number, cur OUT SYS_REFCURSOR) as
   QUERY_STR  VARCHAR2(32767); 
   P_SQL_INSERT VARCHAR2(32767);
   P_CT VARCHAR2(32767);
   BEGIN
    IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC) INTO P_CT from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_CT;
        END IF;
QUERY_STR:='select * FROM (SELECT 
         MA_NGHIEPVU,
         MA_CHUONG,
         MA_CAP,
         MA_MUC,
         MA_TIEUMUC,
         MA_CAPMLNS,
         MA_LOAI,
         MA_KBNN,
         MA_DVQHNS,
         MA_NGUON_NSNN,         
         MA_NGANHKT,
         SUM (NVL(GIA_TRI_HACH_TOAN,0) /nvl('|| DONVI_TIEN ||',1))AS LK
    FROM PHA_HACHTOAN_CHI b
    WHERE 1=1 and MA_CAP IN (''2'',''3'',''4'') '||P_SQL_INSERT|| 
    ' And To_Char(NGAY_HIEU_LUC,' ||    Chr(39) || 'yyyy' || Chr(39) || ') = ' || P_NAM ||
    --' And MA_NGHIEPVU In (Select MA_NGHIEPVU From Dm_NghiepVu Where SUBSTR(CQD,2,1) = ' || Chr(39)|| '1' || Chr(39) ||  ')' ||    
' GROUP BY MA_NGHIEPVU,MA_CHUONG, MA_CAP, MA_CAPMLNS, MA_MUC, MA_TIEUMUC,MA_LOAI,MA_NGANHKT,MA_KBNN,MA_DVQHNS,MA_NGUON_NSNN
)
order by  MA_NGHIEPVU DESC'; 

DBMS_OUTPUT.put_line (QUERY_STR);
BEGIN
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
END;    
END PROC_PHA_PL08_BM05_C_C_M_TM;

/
--------------------------------------------------------
--  DDL for Procedure PROC_PHA_THDL
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_PHA_THDL" (
LOAI_DL         NUMBER,
TEN_BANG        VARCHAR2,
TUNGAY          IN DATE,
DENNGAY         IN DATE ,
CUR1            OUT SYS_REFCURSOR
)
AS 
 V_SQL    VARCHAR2(32000);
BEGIN
  -- xoa du lieu bang hien tai
    EXECUTE IMMEDIATE 'TRUNCATE TABLE '||TEN_BANG||'';       
    -- Tong hop du lieu moi
    STC_PA_SYS.PRC_GET_DATA(LOAI_DL,TEN_BANG,TO_DATE(TO_CHAR(TUNGAY,'ddMMyyyy'),'ddMMyyyy'),TO_DATE(TO_CHAR(DENNGAY,'ddMMyyyy'),'ddMMyyyy'),NULL);  
    -- dem so ban ghi da tong hop duoc   
    V_SQL := 'SELECT COUNT(*) AS SO_LUONG FROM '||TEN_BANG||'';
    OPEN CUR1 FOR V_SQL;
     
END PROC_PHA_THDL;

/
--------------------------------------------------------
--  DDL for Procedure PROC_PL08_BM05_C_C_M_TM
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_PL08_BM05_C_C_M_TM" (P_NGUONNS VARCHAR2,P_KHOAN VARCHAR2,P_MAKB VARCHAR2, P_CAP VARCHAR2,P_CHUONG VARCHAR2, P_LOAI VARCHAR2,P_MUC VARCHAR2, P_TIEUMUC VARCHAR2, P_NAM VARCHAR2,DONVI_TIEN number, cur OUT SYS_REFCURSOR) as
   QUERY_STR  VARCHAR2(32767); 
   P_SQL_INSERT VARCHAR2(32767);
   P_Q VARCHAR2(32767);
   BEGIN
  IF TRIM(P_CAP) IS NOT NULL THEN 
        P_SQL_INSERT:= P_SQL_INSERT ||' AND b.MA_CAPMLNS in ('||P_CAP||')';
        END IF;
   IF TRIM(P_CHUONG) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_CHUONG) into P_Q from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;
   IF TRIM(P_LOAI) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_LOAI) into P_Q from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;
   IF TRIM(P_MUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_MUC) into P_Q from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;
   IF TRIM(P_TIEUMUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_TIEUMUC) into P_Q from dual;
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;
        ---lá»�c mÃ£ kho báº¡c
   IF TRIM(P_MAKB) IS NOT NULL THEN 
        P_SQL_INSERT:= P_SQL_INSERT ||' AND b.MA_KBNN in ('||P_MAKB||')';
        END IF;
   IF TRIM(P_NGUONNS) IS NOT NULL THEN 
        P_SQL_INSERT:= P_SQL_INSERT ||' AND b.MA_DVQHNS in ('||P_NGUONNS||')';
        END IF;     
   IF TRIM(P_KHOAN) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA (P_KHOAN) into P_Q from dual; 
        P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_Q;
        END IF;
QUERY_STR:='select * FROM (SELECT 
         MA_NGHIEPVU,
         MA_CHUONG,
         MA_CAP,
         MA_MUC,
         MA_TIEUMUC,
         MA_CAPMLNS,
         MA_LOAI,
         MA_KBNN,
         MA_DVQHNS,
         MA_NGUON_NSNN,         
         MA_NGANHKT,
         SUM (NVL(GIA_TRI_HACH_TOAN,0) /nvl('|| DONVI_TIEN ||',1))AS LK
    FROM PHA_HACHTOAN_CHI b
    WHERE 1=1 and MA_CAP IN (''3'',''4'') '||P_SQL_INSERT|| 
    ' And To_Char(NGAY_HIEU_LUC,' ||    Chr(39) || 'yyyy' || Chr(39) || ') = ' || P_NAM ||
    ' And MA_NGHIEPVU In (Select MA_NGHIEPVU From Dm_NghiepVu Where SUBSTR(CQD,2,1) = ' || Chr(39)|| '1' || Chr(39) ||  ')' ||    
' GROUP BY MA_NGHIEPVU,MA_CHUONG, MA_CAP, MA_CAPMLNS, MA_MUC, MA_TIEUMUC,MA_LOAI,MA_NGANHKT,MA_KBNN,MA_DVQHNS,MA_NGUON_NSNN
)
order by  MA_NGHIEPVU DESC'; 

DBMS_OUTPUT.put_line (QUERY_STR);
BEGIN
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
END;    
END PROC_PL08_BM05_C_C_M_TM;

/
--------------------------------------------------------
--  DDL for Procedure PROC_TESTDL
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PROC_TESTDL" (LOAI IN varchar:= 'thu', CUR OUT SYS_REFCURSOR)
as
    rowtype Test_DL%ROWTYPE;
begin
    delete TEMP_MLNS; 
    select * into rowtype from test_dl where name = loai and rownum = 1;
    STC_PA_SYS.PRC_TH_MLNS (To_DATE('0101' || rowtype.nam,'ddMMyyyy'),
                          To_DATE('3112' || rowtype.nam,'ddMMyyyy'),
                          case Loai when 'thu' then 2 else 1 end); 
    open cur for select * from Table(STC_PHA_BCDMCT.FCN_TESTDL(LOAI)); 
end;

/
--------------------------------------------------------
--  DDL for Procedure PRO_DELETE_DATA_PHA_MLNS
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PRO_DELETE_DATA_PHA_MLNS" (P_USER        NVARCHAR2)
    AS 
    BEGIN
        DELETE FROM PHA_MLNS WHERE USERID = P_USER;
END PRO_DELETE_DATA_PHA_MLNS;

/
--------------------------------------------------------
--  DDL for Procedure PRO_GET_DATA_PL08BM05_XP
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "BTSTC"."PRO_GET_DATA_PL08BM05_XP" (P_CONGTHUC      NVARCHAR2,
                                                        P_USER        NVARCHAR2,
                                                        outRef  OUT SYS_REFCURSOR)
    AS
      QUERY_STR       VARCHAR2 (30000);
        GIA_TRI         NUMBER:=0;
    P_CT VARCHAR2(32767);
        
BEGIN
         IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
            SELECT STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC) INTO P_CT FROM dual;
        END IF;
        QUERY_STR := 'SELECT nvl( SUM (GIA_TRI_HACH_TOAN),0) as GIATRI FROM PHA_MLNS WHERE 1=1 AND ('|| P_CT ||') AND USERID = '''||P_USER||''' '; 
        DBMS_OUTPUT.PUT_LINE(QUERY_STR);
        OPEN outRef FOR QUERY_STR;
END PRO_GET_DATA_PL08BM05_XP;

/
--------------------------------------------------------
--  DDL for Package Body STC_PA_SYS
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "BTSTC"."STC_PA_SYS" 
AS
  FUNCTION FNC_GET_TEN_DM(
      TAB_NAME IN VARCHAR,
      F_NAME VARCHAR,
      F_CODE VARCHAR,
      S_VALUE IN VARCHAR,
      EFF_DATE DATE)
    RETURN NVARCHAR2
  IS
    V_Return_Val NVARCHAR2 (500);
    V_Sql VARCHAR (2000);
    P_RESULT SYS_REFCURSOR;
  BEGIN
    V_Sql := ' Select ' || F_NAME
    --|| ' into V_Return_Val'
    || ' From ' || TAB_NAME || ' Where 1=1 ' || ' And ' || ' TRANG_THAI = ' || CHR (39) || 'A' || CHR (39) || ' And ' || F_CODE || ' = ' || CHR (39) || S_VALUE || CHR (39) || ' And Ngay_HL = ( Select max(Ngay_HL) from ' || TAB_NAME || ' Where 1=1 And ' || F_CODE || ' = ' || CHR (39) || S_VALUE || CHR (39) || ' And Ngay_HL <= TO_DATE(' || CHR (39) || TO_CHAR(EFF_DATE,'yyyyMMdd') || CHR (39) || ',' || CHR (39) || 'yyyyMMdd' || CHR (39) || '))';
    OPEN P_RESULT FOR V_Sql;
    LOOP
      FETCH P_RESULT INTO V_Return_Val;
      EXIT
    WHEN P_RESULT%NOTFOUND;
    END LOOP;
    RETURN V_Return_Val;
    --Execute immediate V_Sql;
  EXCEPTION
  WHEN OTHERS THEN
    NULL;
    --DBMS_OUTPUT.PUT_LINE ('Lá»—i: ' || V_Sql);
    --DBMS_OUTPUT.PUT_LINE ('Lá»—i: ' || V_Sql);
    RETURN V_Return_Val;
  END;
  FUNCTION FNC_GET_DICTIONARY(
      F_NAME   VARCHAR,
      F_CODE   VARCHAR,
      EFF_DATE DATE)
    RETURN NVARCHAR2
  IS
    V_Return_Val NVARCHAR2 (500);
  BEGIN
    SELECT Mo_ta
    INTO V_Return_Val
    FROM Sys_TuDien
    WHERE 1        = 1
    AND TRANG_THAI = 'A'
    AND FIELDNAME  = CHR (39) 
      || F_NAME
      || CHR (39)
    AND MA_TUDIEN = F_CODE
    AND NGAY_HL   =
      (SELECT MAX (NGAY_HL)
      FROM Sys_TuDien
      WHERE 1       = 1
      AND FIELDNAME = CHR (39)
        || F_NAME
        || CHR (39)
      AND MA_TUDIEN = F_CODE
      AND NGAY_HL  <= EFF_DATE
      );
    RETURN V_Return_Val;
  END;
  FUNCTION FNC_GET_CONVERT_NDKT2NHOM(
      F_NDKT NVARCHAR2)
    RETURN NVARCHAR2
  IS
  BEGIN
    IF (LENGTH (TRIM (F_NDKT)) = 0) THEN
      RETURN '';
    END IF;
    IF (INSTR ( '0050,0051,0052,0053,0054,0055,0056,0057,0058,0061,0062,0063,0064,0065,0099,', F_NDKT || ',') > 0) THEN
      RETURN 'AAAA';
    END IF;
    IF (INSTR ('0001,0002,0003,0004,0005,0006,0007,0008,0011,0012,0049,', F_NDKT || ',') > 0) THEN
      RETURN 'BBBB';
    END IF;
    IF (INSTR ( '0800,0801,0802,0803,0804,0805,0806,0807,0808,0811,0812,0813,0814,0817,0818,0819,0820,0821,0822,0823,0824,0825,0826,0827,0828,0831,0832,0833,0834,0835,0839,', F_NDKT || ',') > 0) THEN
      RETURN 'CCCC';
    END IF;
    IF (INSTR ( '0840,0841,0842,0843,0844,0859,0860,0861,0862,0863,0864,0879,0880,0881,0882,0883,0884,0899,', F_NDKT || ',') > 0) THEN
      RETURN 'DDDD';
    END IF;
    IF (INSTR ('0900,0901,0902,0903,0904,', F_NDKT || ',') > 0) THEN
      RETURN 'EEEE';
    END IF;
    IF (INSTR ('0121,0950,0951,0952,0953,0954,', F_NDKT || ',') > 0) THEN
      RETURN 'FFFF';
    END IF;
    IF (INSTR (
      '0110,0111,0112,0113,0114,0118,0122,1000,1001,1002,1003,1004,1005,1006,1007,1008,1011,1012,1013,1014,1049,1050,1051,1052,1053,1054,1055,1056,1057,1058,1099,1100,      
1101,1102,1103,1149,1150,1151,1152,1153,1199,1250,1251,1300,1301,1302,1349,1350,1351,1352,1353,1354,1399,1400,1401,1402,1403,1404,1405,1406,1449,1450,1451,1499,1500,1501,1502,      
1503,1549,1550,1551,1552,1553,1554,1555,1556,1557,1558,1561,1562,1563,1599,1600,1601,1602,1603,1649,1700,1701,1702,1703,1704,1705,1749,1750,1751,1752,1753,1754,1755,1756,1757,      
1758,1761,1762,1763,1764,1765,1766,1767,1799,1800,1801,1802,1803,1804,1805,1806,1849,1850,1851,1852,1899,1900,1901,1902,1903,1949,1950,1951,1952,1953,1999,2000,2001,2002,2003,      
2004,2005,2006,2007,2008,2009,2019,2031,2032,2033,2034,2035,2036,2037,2038,2039,2041,2042,2043,2044,2045,2049,2100,2101,2102,2103,2104,2105,2106,2107,2108,2111,2150,2151,2152,      
2153,2154,2155,2156,2157,2158,2161,2162,2163,2164,2165,2166,2167,2200,2201,2202,2203,2204,2205,2206,2207,2208,2211,2250,2251,2252,2253,2254,2255,2256,2257,2258,2261,2262,2263,      
2264,2265,2266,2267,2300,2301,2302,2303,2304,2305,2306,2307,2308,2311,2312,2313,2314,2315,2316,2317,2318,2321,2322,2323,2324,2350,2351,2352,2353,2354,2355,2356,2357,2358,2361,      
2362,2363,2364,2365,2366,2367,2368,2400,2401,2402,2403,2404,2405,2406,2407,2408,2411,2412,2413,2414,2415,2416,2417,2418,2421,2422,2450,2451,2452,2453,2454,2455,2456,2457,2458,      
2500,2501,2502,2503,2504,2505,2506,2507,2508,2511,2512,2513,2550,2551,2552,2553,2554,2555,2556,2557,2558,2561,2562,2563,2564,2565,2566,2567,2600,2601,2602,2603,2604,2605,2606,      
2607,2608,2611,2612,2613,2614,2615,2616,2617,2618,2621,2622,2623,2624,2625,2626,2627,2628,2631,2632,2633,2634,2635,2636,2637,2650,2651,2652,2653,2654,2655,2656,2657,2658,2661,      
2662,2663,2664,2665,2700,2701,2702,2703,2704,2705,2706,2707,2708,2711,2712,2713,2714,2715,2716,2717,2718,2721,2722,2750,2751,2752,2753,2754,2755,2756,2757,2758,2761,2762,2763,      
2764,2765,2766,2767,2768,2771,2772,2773,2774,2800,2801,2802,2803,2804,2805,2806,2807,2808,2811,2812,2813,2814,2815,2816,2817,2818,2821,2822,2823,2824,2825,2826,2827,2828,2831,      
2850,2851,2852,2853,2854,2855,2856,2857,2858,2861,2862,2863,2864,2865,2866,2867,2868,2871,2872,3000,3001,3002,3003,3004,3005,3006,3007,3008,3009,3050,3051,3052,3053,3054,3055,      
3057,3058,3061,3062,3063,3064,3065,3066,3067,3068,3071,3072,3073,'
      , F_NDKT || ',') > 0) THEN
      RETURN '0110';
    END IF;
    IF (INSTR (
      '0115,0116,0117,0120,0200,3200,3201,3202,3203,3204,3249,3250,3251,3252,3253,3254,3299,3300,3301,3302,3349,3350,3351,3352,3353,3354,3355,3356,3357,3358,3361,3362,3363,      
3364,3365,3399,3400,3401,3402,3403,3404,3405,3406,3449,3450,3451,3452,3453,3499,3600,3601,3602,3603,3604,3605,3606,3607,3608,3649,3650,3651,3652,3653,3654,3699,3700,3701,3702,3703,      
3704,3705,3706,3749,3750,3751,3752,3753,3754,3755,3799,3800,3801,3802,3803,3849,3850,3851,3852,3853,3854,3899,3900,3901,3902,3903,3949,3950,3951,3952,3953,3954,3955,3999,4050,4051,      
4052,4053,4054,4099,4100,4101,4102,4103,4104,4149,4250,4251,4252,4253,4254,4255,4256,4257,4258,4261,4262,4263,4264,4265,4266,4267,4268,4271,4272,4273,4274,4275,4299,4300,4301,4302,      
4303,4304,4305,4306,4307,4308,4311,4312,4313,4349,4450,4451,4499,4500,4501,4502,4503,4504,4505,4506,4507,4549,4650,4651,4652,4653,4654,4655,4699,4700,4701,4702,4703,4749,4750,4751,      
4800,4801,4850,4851,4900,4901,4902,4903,4904,4905,4906,4907,4908,4917,4911,4912,4913,4914,4949,4918,4927,4928,4931,4935,4936,4943,4944,4947,'
      , F_NDKT || ',') > 0) THEN
      RETURN '0200';
    END IF;
    IF (INSTR ( '0123,0300,5050,5051,5052,5053,5054,5099,5100,5101,5102,5103,5104,5149,5150,5151,5152,5153,5154,5199,5200,5201,5202,5203,5204,5249,', F_NDKT || ',') > 0) THEN
      RETURN '0300';
    END IF;
    IF (INSTR ( '0124,0125,0126,0400,5350,5351,5352,5399,5450,5451,5452,5453,5499,5550,5551,5552,', F_NDKT || ',') > 0) THEN
      RETURN '0400';
    END IF;
    IF (INSTR (
      '0129,0130,0131,0132,0133,0500,6000,6001,6002,6003,6004,6049,6050,6051,6099,6100,6101,6102,6103,6104,6105,6106,6107,6108,6111,6112,6113,6114,6115,6116,6117,6118,6121,6122,      
6123,6124,6125,6149,6150,6151,6152,6153,6154,6155,6199,6200,6201,6202,6203,6249,6250,6251,6252,6253,6254,6255,6256,6257,6299,6300,6301,6302,6303,6304,6349,6350,6351,6352,6353,6399,      
6400,6401,6402,6403,6404,6405,6406,6449,6500,6501,6502,6503,6504,6505,6549,6550,6551,6552,6553,6599,6600,6601,6602,6603,6604,6605,6606,6607,6608,6611,6612,6613,6614,6615,6616,6617,      
6618,6649,6650,6651,6652,6653,6654,6655,6656,6657,6658,6699,6700,6701,6702,6703,6704,6705,6749,6750,6751,6752,6753,6754,6755,6756,6757,6758,6761,6799,6800,6801,6802,6803,6804,6805,      
6806,6849,6850,6851,6852,6853,6854,6855,6856,6899,6900,6901,6902,6903,6904,6905,6906,6907,6908,6911,6912,6913,6914,6915,6916,6917,6918,6921,6922,6923,6949,7000,7001,7002,7003,7004,      
7005,7006,7007,7008,7011,7012,7013,7014,7015,7016,7017,7049,7100,7101,7102,7103,7104,7149,7150,7151,7152,7153,7154,7155,7156,7157,7158,7161,7162,7163,7164,7165,7166,7167,7168,7199,      
7200,7201,7202,7203,7249,7250,7251,7252,7253,7254,7255,7256,7257,7258,7261,7262,7299,7300,7301,7302,7303,7304,7305,7349,7350,7351,7352,7353,7354,7355,7399,7400,7401,7402,7403,7404,      
7405,7406,7449,7500,7501,7549,7550,7551,7552,7599,7600,7601,7602,7603,7649,7650,7651,7652,7653,7654,7655,7699,7700,7701,7702,7749,7750,7751,7752,7753,7754,7755,7756,7757,7758,7761,      
7762,7763,7764,7765,7766,7799,7850,7851,7852,7853,7854,7899,7900,7901,7902,7949,7950,7951,7952,7953,7954,7999,8000,8001,8002,8003,8004,8005,8006,8007,8008,8011,8012,8049,8050,8051,      
8052,8053,8054,8055,8099,8100,8101,8102,8103,8104,8149,8150,8151,8152,8153,8154,8199,8300,8301,8302,8303,8304,8305,8306,8307,8308,8311,8312,8313,8314,8349,8350,8351,8352,8353,8354,      
8355,8356,8357,8358,8361,8362,8363,8364,8365,8399,8400,8401,8402,8403,8404,8449,8450,8451,8452,8453,8454,8499,8500,8501,8502,8503,8504,8549,8550,8551,8552,8553,8554,8555,8556,8557,8558,8599,'
      , F_NDKT || ',') > 0) THEN
      RETURN '0500';
    END IF;
    IF (INSTR ( '0134,0135,0136,0600,8750,8751,8752,8753,8754,8799,8800,8801,8802,8803,8804,8849,8950,8951,8952,8953,8999,9000,9001,9002,9003,9004,9049,9050,9051,9052,9053,9054,9055,9056,      
9057,9058,9061,9062,9063,9064,9065,9066,9099,9100,9101,9102,9103,9104,9105,9106,9107,9108,9111,9112,9113,9114,9115,9116,9117,9118,9121,9122,9123,9149,9200,9201,9202,9203,9204,9249,      
9250,9251,9252,9253,9254,9255,9299,9300,9301,9302,9303,9304,9305,9349,9350,9351,9352,9353,9354,9355,9399,9400,9401,9402,9403,9404,9449,', F_NDKT || ',') > 0) THEN
      RETURN '0600';
    END IF;
    IF (INSTR ( '0137,0138,0700,9500,9501,9502,9549,9650,9651,9652,9653,9699,9700,9701,9702,9703,9704,9749,', F_NDKT || ',') > 0) THEN
      RETURN '0700';
    END IF;
    RETURN NULL;
  END;
  FUNCTION FNC_GET_CONVERT_NDKT2TNHOM(
      F_NDKT NVARCHAR2)
    RETURN NVARCHAR2
  IS
  BEGIN
    IF (LENGTH (TRIM (F_NDKT)) = 0) THEN
      RETURN '';
    END IF;
    IF (INSTR ( '0050,0051,0052,0053,0054,0055,0056,0057,0058,0061,0062,0063,0064,0065,0099,', F_NDKT || ',') > 0) THEN
      RETURN 'AAAA';
    END IF;
    IF (INSTR ('0001,0002,0003,0004,0005,0006,0007,0008,0011,0012,0049,', F_NDKT || ',') > 0) THEN
      RETURN 'BBBB';
    END IF;
    IF (INSTR ( '0800,0801,0802,0803,0804,0805,0806,0807,0808,0811,0812,0813,0814,0817,0818,0819,0820,0821,0822,0823,0824,0825,0826,0827,0828,0831,0832,0833,0834,0835,0839,', F_NDKT || ',') > 0) THEN
      RETURN 'CCCC';
    END IF;
    IF (INSTR ( '0840,0841,0842,0843,0844,0859,0860,0861,0862,0863,0864,0879,0880,0881,0882,0883,0884,0899,', F_NDKT || ',') > 0) THEN
      RETURN 'DDDD';
    END IF;
    IF (INSTR ('0900,0901,0902,0903,0904,', F_NDKT || ',') > 0) THEN
      RETURN 'EEEE';
    END IF;
    IF (INSTR ('0121,0950,0951,0952,0953,0954,', F_NDKT || ',') > 0) THEN
      RETURN 'FFFF';
    END IF;
    IF (INSTR ('0400,0500,0600,0700,', F_NDKT || ',') > 0) THEN
      RETURN '0000';
    END IF;
    IF (INSTR ('3009,', F_NDKT || ',') > 0) THEN
      RETURN '0014';
    END IF;
    IF (INSTR ('1250,', F_NDKT || ',') > 0) THEN
      RETURN '0110';
    END IF;
    IF (INSTR ( '0111,1000,1001,1002,1003,1004,1005,1006,1007,1008,1011,1012,1013,1014,1049,1050,1051,1052,1053,1054,1055,1056,1057,1058,1099,1100,1101,1102,1103,1149,1150,1151,1152,1153,1199,1251,', F_NDKT || ',') > 0) THEN
      RETURN '0111';
    END IF;
    IF (INSTR ( '0112,1300,1301,1302,1349,1350,1351,1352,1353,1354,1399,1400,1401,1402,1403,1404,1405,1406,1449,1450,1451,1499,1500,1501,1502,1503,1549,1550,1551,1552,1553,1554,1555,1556,1557,1558,      
1561,1562,1563,1599,1600,', F_NDKT || ',') > 0) THEN
      RETURN '0112';
    END IF;
    IF (INSTR ( '0113,1601,1602,1603,1649,1700,1701,1702,1703,1704,1705,1749,1750,1751,1752,1753,1754,1755,1756,1757,1758,1761,1762,1763,1764,1765,1766,1767,1799,1800,1801,1802,1803,1804,1805,1806,      
1849,1850,1851,1852,1899,1900,1901,1902,1903,1949,1950,1951,1952,1953,1999,2000,2001,2002,2003,2004,2005,2006,2007,2008,2009,2019,2031,2032,2033,2034,2035,2036,2037,2038,2039,2041,2042,2043,      
2044,2045,2049,', F_NDKT || ',') > 0) THEN
      RETURN '0113';
    END IF;
    IF (INSTR (
      '0110,0114,0118,0122,2100,2101,2102,2103,2104,2105,2106,2107,2108,2111,2150,2151,2152,2153,2154,2155,2156,2157,2158,2161,2162,2163,2164,2165,2166,2167,2200,2201,2202,2203,2204,2205,      
2206,2207,2208,2211,2250,2251,2252,2253,2254,2255,2256,2257,2258,2261,2262,2263,2264,2265,2266,2267,2300,2301,2302,2303,2304,2305,2306,2307,2308,2311,2312,2313,2314,2315,2316,2317,2318,2321,      
2322,2323,2324,2350,2351,2352,2353,2354,2355,2356,2357,2358,2361,2362,2363,2364,2365,2366,2367,2368,2400,2401,2402,2403,2404,2405,2406,2407,2408,2411,2412,2413,2414,2415,2416,2417,2418,2421,      
2422,2450,2451,2452,2453,2454,2455,2456,2457,2458,2500,2501,2502,2503,2504,2505,2506,2507,2508,2511,2512,2513,2550,2551,2552,2553,2554,2555,2556,2557,2558,2561,2562,2563,2564,2565,2566,2567,      
2600,2601,2602,2603,2604,2605,2606,2607,2608,2611,2612,2613,2614,2615,2616,2617,2618,2621,2622,2623,2624,2625,2626,2627,2628,2631,2632,2633,2634,2635,2636,2637,2650,2651,2652,2653,2654,2655,      
2656,2657,2658,2661,2662,2663,2664,2665,2700,2701,2702,2703,2704,2705,2706,2707,2708,2711,2712,2713,2714,2715,2716,2717,2718,2721,2722,2750,2751,2752,2753,2754,2755,2756,2757,2758,2761,2762,      
2763,2764,2765,2766,2767,2768,2771,2772,2773,2774,2800,2801,2802,2803,2804,2805,2806,2807,2808,2811,2812,2813,2814,2815,2816,2817,2818,2821,2822,2823,2824,2825,2826,2827,2828,2831,2850,2851,      
2852,2853,2854,2855,2856,2857,2858,2861,2862,2863,2864,2865,2866,2867,2868,2871,2872,3000,3001,3002,3003,3004,3005,3006,3007,3008,3050,3051,3052,3053,3054,3055,3057,3058,3061,3062,3063,3064,      
3065,3066,3067,3068,3071,3072,3073,'
      , F_NDKT || ',') > 0) THEN
      RETURN '0114';
    END IF;
    IF (INSTR ( '0115,3200,3201,3202,3203,3204,3249,3250,3251,3252,3253,3254,3299,3300,3301,3302,3349,3350,3351,3352,3353,3354,3355,3356,3357,3358,3361,3362,3363,3364,3365,3399,3400,3401,3402,3403,      
3404,3405,3406,3449,3450,3451,3452,3453,3499,', F_NDKT || ',') > 0) THEN
      RETURN '0115';
    END IF;
    IF (INSTR ( '0116,3600,3601,3602,3603,3604,3605,3606,3607,3608,3649,3650,3651,3652,3653,3654,3699,3700,3701,3702,3703,3704,3705,3706,3749,3750,3751,3752,3753,3754,3755,3799,3800,3801,3802,3803,      
3849,3850,3851,3852,3853,3854,3899,3900,3901,3902,3903,3949,3950,3951,3952,3953,3954,3955,3999,', F_NDKT || ',') > 0) THEN
      RETURN '0116';
    END IF;
    IF (INSTR ( '0117,4050,4051,4052,4053,4054,4099,4100,4101,4102,4103,4104,4149,', F_NDKT || ',') > 0) THEN
      RETURN '0117';
    END IF;
    IF (INSTR ( '4250,4251,4252,4253,4254,4255,4256,4257,4258,4261,4262,4263,4264,4265,4266,4267,4268,4271,4272,4273,4274,4275,4299,4300,4301,4302,4303,4304,4305,4306,4307,4308,4311,4312,4313,4349,', F_NDKT || ',') > 0) THEN
      RETURN '0118';
    END IF;
    IF (INSTR ( '0120,4450,4451,4499,4500,4501,4502,4503,4504,4505,4506,4507,4549,', F_NDKT || ',') > 0) THEN
      RETURN '0120';
    END IF;
    IF (INSTR ( '4650,4651,4652,4653,4654,4655,4699,4700,4701,4702,4703,4749,4750,4751,4800,4801,4850,4851,', F_NDKT || ',') > 0) THEN
      RETURN '0121';
    END IF;
    IF (INSTR ( '0200,4900,4901,4902,4903,4904,4905,4906,4907,4908,4911,4912,4913,4914,4949,4917,4918,4927,4928,4931,4935,4936,4943,4944,4947,', F_NDKT || ',') > 0) THEN
      RETURN '0122';
    END IF;
    IF (INSTR ( '0123,0300,5050,5051,5052,5053,5054,5099,5100,5101,5102,5103,5104,5149,5150,5151,5152,5153,5154,5199,5200,5201,5202,5203,5204,5249,', F_NDKT || ',') > 0) THEN
      RETURN '0123';
    END IF;
    IF (INSTR ('0124,5350,5351,5352,5399,', F_NDKT || ',') > 0) THEN
      RETURN '0124';
    END IF;
    IF (INSTR ('0125,5450,5451,5452,5453,5499,', F_NDKT || ',') > 0) THEN
      RETURN '0125';
    END IF;
    IF (INSTR ('0126,5550,5551,5552,', F_NDKT || ',') > 0) THEN
      RETURN '0126';
    END IF;
    IF (INSTR ( '0129,6000,6001,6002,6003,6004,6049,6050,6051,6099,6100,6101,6102,6103,6104,6105,6106,6107,6108,6111,6112,6113,6114,6115,6116,6117,      
6118,6121,6122,6123,6124,6125,6149,6150,6151,6152,6153,6154,6155,6199,6200,6201,6202,6203,6249,6250,6251,6252,6253,6254,6255,6256,6257,6299,      
6300,6301,6302,6303,6304,6349,6350,6351,6352,6353,6399,6400,6401,6402,6403,6404,6405,6406,6449,', F_NDKT || ',') > 0) THEN
      RETURN '0129';
    END IF;
    IF (INSTR ( '0130,6500,6501,6502,6503,6504,6505,6549,6550,6551,6552,6553,6599,6600,6601,6602,6603,6604,6605,6606,6607,6608,6611,6612,6613,6614,      
6615,6616,6617,6618,6649,6650,6651,6652,6653,6654,6655,6656,6657,6658,6699,6700,6701,6702,6703,6704,6705,6749,6750,6751,6752,6753,6754,6755,      
6756,6757,6758,6761,6799,6800,6801,6802,6803,6804,6805,6806,6849,6850,6851,6852,6853,6854,6855,6856,6899,6900,6901,6902,6903,6904,6905,6906,      
6907,6908,6911,6912,6913,6914,6915,6916,6917,6918,6921,6922,6923,6949,7000,7001,7002,7003,7004,7005,7006,7007,7008,7011,7012,7013,7014,7015,7016,7017,7049,', F_NDKT || ',') > 0) THEN
      RETURN '0130';
    END IF;
    IF (INSTR ( '0131,7100,7101,7102,7103,7104,7149,7150,7151,7152,7153,7154,7155,7156,7157,7158,7161,7162,7163,7164,7165,7166,7167,7168,7199,7200,      
7201,7202,7203,7249,7250,7251,7252,7253,7254,7255,7256,7257,7258,7261,7262,7299,7300,7301,7302,7303,7304,7305,7349,7350,7351,7352,7353,7354,      
7355,7399,7400,7401,7402,7403,7404,7405,7406,7449,', F_NDKT || ',') > 0) THEN
      RETURN '0131';
    END IF;
    IF (INSTR ( '0132,7500,7501,7549,7550,7551,7552,7599,7600,7601,7602,7603,7649,7650,7651,7652,7653,7654,7655,7699,7700,7701,7702,7749,7750,7751,      
7752,7753,7754,7755,7756,7757,7758,7761,7762,7763,7764,7765,7766,7799,7850,7851,7852,7853,7854,7899,7900,7901,7902,7949,7950,7951,7952,7953,      
7954,7999,8000,8001,8002,8003,8004,8005,8006,8007,8008,8011,8012,8049,8050,8051,8052,8053,8054,8055,8099,8100,8101,8102,8103,8104,8149,8150,8151,8152,8153,8154,8199,,', F_NDKT || ',') > 0) THEN
      RETURN '0132';
    END IF;
    IF (INSTR ( '0133,8300,8301,8302,8303,8304,8305,8306,8307,8308,8311,8312,8313,8314,8349,8350,8351,8352,8353,8354,8355,8356,8357,8358,8361,8362,      
8363,8364,8365,8399,8400,8401,8402,8403,8404,8449,8450,8451,8452,8453,8454,8499,8500,8501,8502,8503,8504,8549,8550,8551,8552,8553,8554,8555,8556,8557,8558,8599,', F_NDKT || ',') > 0) THEN
      RETURN '0133';
    END IF;
    IF (INSTR ('0134,8750,8751,8752,8753,8754,8799,8800,8801,8802,8803,8804,8849,',F_NDKT || ',') > 0) THEN
      RETURN '0134';
    END IF;
    IF (INSTR ( '0135,8950,8951,8952,8953,8999,9000,9001,9002,9003,9004,9049,9050,9051,9052,9053,9054,9055,9056,9057,9058,9061,9062,9063,9064,9065,      
9066,9099,9100,9101,9102,9103,9104,9105,9106,9107,9108,9111,9112,9113,9114,9115,9116,9117,9118,9121,9122,9123,9149,', F_NDKT || ',') > 0) THEN
      RETURN '0135';
    END IF;
    IF (INSTR ( '0136,9200,9201,9202,9203,9204,9249,9250,9251,9252,9253,9254,9255,9299,9300,9301,9302,9303,9304,9305,9349,9350,9351,9352,9353,9354,      
9355,9399,9400,9401,9402,9403,9404,9449,', F_NDKT || ',') > 0) THEN
      RETURN '0136';
    END IF;
    IF (INSTR ('0137,9500,9501,9502,9549,', F_NDKT || ',') > 0) THEN
      RETURN '0137';
    END IF;
    IF (INSTR ( '0138,9650,9651,9652,9653,9699,9700,9701,9702,9703,9704,9749,', F_NDKT || ',') > 0) THEN
      RETURN '0138';
    END IF;
    RETURN NULL;
  END;
  FUNCTION FNC_GET_CONVERT_CHUONG2CAP(
      F_CHUONG NVARCHAR2)
    RETURN NVARCHAR2
  IS
    V_Return_Val NVARCHAR2 (1) := '4';
    V_Number NUMBER;
  BEGIN
    IF (LENGTH (TRIM (F_CHUONG)) = 0) THEN
      RETURN '';
    END IF;
    V_Number    := TO_NUMBER (F_CHUONG);
    IF (V_Number < 400) THEN
      RETURN '1';
    END IF;
    IF (V_Number < 600) THEN
      RETURN '2';
    END IF;
    IF (V_Number < 800) THEN
      RETURN '3';
    END IF;
    RETURN V_Return_Val;
  END;
  FUNCTION FNC_GET_CONVERT_NKT2LOAI(
      F_NKT NVARCHAR2)
    RETURN NVARCHAR2
  IS
    V_Return_Val NVARCHAR2 (10) := '4';
    V_Sub NVARCHAR2 (2)         := SUBSTR (F_NKT, 1, 2);
  BEGIN
    CASE
    WHEN V_Sub IN ('01', '02') THEN
      V_Return_Val := '010';
    WHEN V_Sub IN ('04', '05', '06') THEN
      V_Return_Val := '040';
    WHEN V_Sub IN ('07', '08', '09', '10', '11', '12') THEN
      V_Return_Val := '070';
    WHEN V_Sub IN ('13', '14', '15') THEN
      V_Return_Val := '130';
    WHEN V_Sub IN ('16', '17', '18') THEN
      V_Return_Val := '160';
    WHEN V_Sub IN ('19', '20', '21') THEN
      V_Return_Val := '190';
    WHEN V_Sub IN ('22', '23', '24') THEN
      V_Return_Val := '220';
    WHEN V_Sub IN ('25', '26', '27') THEN
      V_Return_Val := '250';
    WHEN V_Sub IN ('28', '29', '30') THEN
      V_Return_Val := '280';
    WHEN V_Sub IN ('31', '32', '33') THEN
      V_Return_Val := '310';
    WHEN V_Sub IN ('34', '35', '36') THEN
      V_Return_Val := '340';
    WHEN V_Sub IN ('37', '38', '39') THEN
      V_Return_Val := '370';
    WHEN V_Sub IN ('40', '41', '42') THEN
      V_Return_Val := '400';
    WHEN V_Sub IN ('43', '44', '45') THEN
      V_Return_Val := '430';
    WHEN V_Sub IN ('46', '47', '48') THEN
      V_Return_Val := '460';
    WHEN V_Sub IN ('49', '50', '51') THEN
      V_Return_Val := '490';
    WHEN V_Sub IN ('52', '53', '54') THEN
      V_Return_Val := '520';
    WHEN V_Sub IN ('55', '56', '57') THEN
      V_Return_Val := '550';
    WHEN V_Sub IN ('58', '59', '60') THEN
      V_Return_Val := '580';
    WHEN V_Sub IN ('61', '62', '63') THEN
      V_Return_Val := '610';
    WHEN V_Sub IN ('64', '65', '66') THEN
      V_Return_Val := '640';
    ELSE
      V_Return_Val := '000';
    END CASE;
    RETURN V_Return_Val;
  END;
  FUNCTION FNC_GET_CONVERT_NDKT2MUC(
      F_NDKT NVARCHAR2)
    RETURN NVARCHAR2
  IS
    V_Right NVARCHAR2 (2) := SUBSTR (F_NDKT, -2, 2);
    V_Mid NVARCHAR2 (2)   := SUBSTR (F_NDKT, 3, 1);
    V_Return_Val NVARCHAR2 (2);
    V_Number NUMBER;
  BEGIN
    IF V_Right       <> '00' THEN
      V_Number       := TO_NUMBER (V_Mid);
      IF (V_Number    < 5) THEN
        V_Return_Val := '00';
      ELSIF (V_Number < 10) THEN
        V_Return_Val := '50';
      ELSE
        V_Return_Val := 'NA';
      END IF;
    ELSE
      V_Return_Val := SUBSTR (F_NDKT, -2, 2);
    END IF;
    RETURN SUBSTR (F_NDKT, 1, 2) || V_Return_Val;
  END;
  FUNCTION FNC_GET_LOAI_NS(
      F_MUC NVARCHAR2)
    RETURN NVARCHAR2
  IS
    V_LEFT NVARCHAR2 (2);
    V_Return_Val NVARCHAR2 (10);
  BEGIN
    IF LENGTH (F_MUC) > 2 THEN
      V_LEFT         := SUBSTR (F_MUC, 0, 2);
    END IF;
    IF (V_LEFT     <> '00') THEN
      V_Return_Val := 'TRONG_NS';
    ELSE
      V_Return_Val := 'TAMCHI_NS';
    END IF;
    RETURN V_Return_Val;
  END;
  FUNCTION FNC_CONVERT_FORMULA(
      F_FORMULA NVARCHAR2)
    RETURN NVARCHAR2
  IS
    V_WHERE NVARCHAR2 (15000);
  BEGIN
    --DBMS_OUTPUT.put_line ('CT:' || F_FORMULA);
    IF LENGTH(NVL(F_FORMULA,'')) = 0 THEN
      V_WHERE                   := ' 1=1 ';
    ELSE
      V_WHERE := '';
      V_WHERE := REPLACE (F_FORMULA, 'CA', ' MA_CAP like ');
      V_WHERE := REPLACE (V_WHERE, 'CH', ' MA_CHUONG like ');
      V_WHERE := REPLACE (V_WHERE, 'L', ' MA_LOAI like ');      
      V_WHERE := REPLACE (V_WHERE, 'MC', ' MA_MUC like ');
      V_WHERE := REPLACE (V_WHERE, 'TM', ' MA_TIEUMUC like ');
      V_WHERE := REPLACE (V_WHERE, 'NH', ' MA_NHOM like ');
      V_WHERE := REPLACE (V_WHERE, 'TN', ' MA_TIEUNHOM like ');
      V_WHERE := REPLACE (V_WHERE, 'NS', ' MA_CAPNS like ');
      V_WHERE := REPLACE (V_WHERE, 'TK', ' MA_TKTN like ');
      V_WHERE := REPLACE (V_WHERE, 'NV', ' MA_NGUON_NSNN like ');
      V_WHERE := REPLACE (V_WHERE, 'DV', ' MA_DVQHNS like ');
      V_WHERE := REPLACE (V_WHERE, 'CT', ' MA_CTMTQG like ');
      V_WHERE := REPLACE (V_WHERE, 'DB', ' MA_DIABAN like ');
      V_WHERE := REPLACE (V_WHERE, 'KB', ' MA_KBNN like ');
      V_WHERE := REPLACE (V_WHERE, 'KH', ' MA_NGANHKT like ');
      V_WHERE := REPLACE (V_WHERE, ',', ' OR ');
      V_WHERE := REPLACE (V_WHERE, '+', ' AND ');
      V_WHERE := REPLACE (V_WHERE, '-', ' AND NOT ');
      V_WHERE := REPLACE (V_WHERE, '[', ' BETWEEN ''');
      V_WHERE := REPLACE (V_WHERE, '~', ''' AND ''');
      V_WHERE := REPLACE (V_WHERE, ']', '''');
      V_WHERE := REPLACE (V_WHERE, 'like  BETWEEN', ' BETWEEN ');
    END IF;
    --DBMS_OUTPUT.ENABLE(500000);
    --DBMS_OUTPUT.put_line (V_WHERE);
    RETURN V_WHERE;
    -- Insert into TB_LOG(M) Values (V_WHERE);
    --Return '1=1';
  END;
  FUNCTION FNC_CONVERT_FORMULA_FROM_TAB(
      F_FORMULA VARCHAR2)
    RETURN VARCHAR2
  IS
    V_WHERE VARCHAR2 (20000);
  BEGIN
    IF F_FORMULA           IS NULL THEN
      V_WHERE              := ' 1=1 ';
    ELSIF (TRIM (F_FORMULA) = '') THEN
      V_WHERE              := '1=1';
    ELSE
      V_WHERE := REPLACE (F_FORMULA, 'CA', ' SEGMENT4 like ');
      V_WHERE := REPLACE (V_WHERE, 'LDT', ' ATTRIBUTE8 like ');
      V_WHERE := REPLACE (V_WHERE, 'KH', ' SEGMENT8 like ');
      V_WHERE := REPLACE (V_WHERE, 'L', ' SEGMENT3 like ');
      V_WHERE := REPLACE (V_WHERE, 'TM', ' SEGMENT3 like ');
      V_WHERE := REPLACE (V_WHERE, 'TK', ' SEGMENT2 like ');
      V_WHERE := REPLACE (V_WHERE, 'NV', ' SEGMENT11 like ');
      V_WHERE := REPLACE (V_WHERE, 'DV', ' SEGMENT5 like ');
      V_WHERE := REPLACE (V_WHERE, 'CT', ' SEGMENT9 like ');
      V_WHERE := REPLACE (V_WHERE, 'DB', ' SEGMENT6 like ');
      V_WHERE := REPLACE (V_WHERE, ',', ' OR ');
      V_WHERE := REPLACE (V_WHERE, '+', ' AND ');
      V_WHERE := REPLACE (V_WHERE, '-', ' AND NOT ');
      V_WHERE := REPLACE (V_WHERE, '[', ' BETWEEN ''');
      V_WHERE := REPLACE (V_WHERE, '~', ''' AND ''');
      V_WHERE := REPLACE (V_WHERE, ']', '''');
      V_WHERE := REPLACE (V_WHERE, 'like  BETWEEN', ' BETWEEN ');
    END IF;
    RETURN NVL (V_WHERE, '');
  END;
  FUNCTION FNC_GET_TEN_CHITIEU(
      p_Ma_CHITIEU IN VARCHAR2,
      p_NGAY_HL    IN DATE)
    RETURN VARCHAR2
  IS
    v_return VARCHAR2 (20000);
  BEGIN
    SELECT A.TEN_CHITIEU
    INTO v_return
    FROM DM_CHITIEU A
    WHERE A.TRANG_THAI = 'A'
    AND A.MA_CHITIEU   = p_Ma_CHITIEU
    AND A.NGAY_HL      =
      (SELECT MAX (NGAY_HL)
      FROM DM_CHITIEU SQ
      WHERE SQ.MA_CHITIEU = A.MA_CHITIEU
      AND SQ.NGAY_HL     <= p_NGAY_HL
      );
    IF v_return IS NOT NULL THEN
      RETURN v_return;
    ELSE
      RETURN '';
    END IF;
  END;
  FUNCTION FNC_GET_CONGTHUC_CHITIEU(
      p_Ma_CHITIEU IN VARCHAR2,
      p_NGAY_HL    IN DATE)
    RETURN VARCHAR2
  IS
    v_return VARCHAR2 (5000);
  BEGIN
    SELECT A.CONGTHUC
    INTO v_return
    FROM DM_CHITIEU A
    WHERE A.TRANG_THAI = 'A'
    AND A.MA_CHITIEU   = p_Ma_CHITIEU
    AND A.NGAY_HL      =
      (SELECT MAX (NGAY_HL)
      FROM DM_CHITIEU SQ
      WHERE SQ.MA_CHITIEU = A.MA_CHITIEU
      AND SQ.NGAY_HL     <= p_NGAY_HL
      );
    IF v_return IS NOT NULL THEN
      RETURN v_return;
    ELSE
      RETURN '';
    END IF;
  END;
  -------------------------------------------------
    PROCEDURE PRC_TH_MLNS_SYS (P_BNGAY_HACHTOAN    DATE,
                          P_ENGAY_HACHTOAN    DATE,
                          P_BNGAY_KETSO       DATE,
                          P_ENGAY_KETSO       DATE,
                          P_LOAI              VARCHAR2,
                          P_CONGTHUC          VARCHAR2)
    IS
        P_CT VARCHAR2(32767);
        SQL_QUERRY VARCHAR2(32767);
        INSERT_QUERRY VARCHAR2(32767);
        V_TU_NAM VARCHAR(4):= to_char(P_BNGAY_HACHTOAN,'yyyy');
    BEGIN
        IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
            SELECT STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC) INTO P_CT FROM dual;
        END IF;
        IF INSTR(P_LOAI,'C') > 0 THEN
          SQL_QUERRY := 'SELECT MA_NGHIEPVU,MA_TKTN, TEN_TKTN,MA_DVQHNS,TEN_DVQHNS,MA_DIABAN,TEN_DIABAN,MA_CAP,TEN_CAP,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,TEN_NGANHKT,MA_LOAI,TEN_LOAI,MA_TIEUMUC,TEN_TIEUMUC,MA_MUC,
              TEN_MUC,MA_TIEUNHOM,TEN_TIEUNHOM,MA_NHOM,TEN_NHOM,MA_CTMTQG,TEN_CTMTQG,MA_KBNN,TEN_KBNN,MA_NGUON_NSNN,TEN_NGUON_NSNN,NGAY_KET_SO, NGAY_HIEU_LUC,SUM (GIA_TRI_HACH_TOAN),ATTRIBUTE8
              FROM PHA_HACHTOAN_CHI VC WHERE VC.NGAY_HIEU_LUC >= TO_DATE ('''|| '0101' || V_TU_NAM  ||''', ''ddMMyyyy'') AND VC.NGAY_HIEU_LUC <= TO_DATE('''||P_ENGAY_HACHTOAN||''',''DD/MM/YYYY'') AND ' ||' VC.NGAY_KET_SO <= TO_DATE('''||P_ENGAY_KETSO||''',''DD/MM/YYYY'') AND '
              ||P_CT||
              ' GROUP BY MA_NGHIEPVU,MA_TKTN, TEN_TKTN,MA_DVQHNS,TEN_DVQHNS,MA_DIABAN,TEN_DIABAN,MA_CAP,TEN_CAP,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,
              TEN_NGANHKT,MA_LOAI,TEN_LOAI,MA_TIEUMUC,TEN_TIEUMUC,MA_MUC,TEN_MUC,MA_TIEUNHOM,TEN_TIEUNHOM,MA_NHOM,TEN_NHOM,MA_CTMTQG,TEN_CTMTQG,
              MA_KBNN,TEN_KBNN,MA_NGUON_NSNN,TEN_NGUON_NSNN,NGAY_KET_SO, NGAY_HIEU_LUC,ATTRIBUTE8 ';     
              INSERT_QUERRY := 'INSERT INTO TEMP_MLNS(LOAI_NS, MA_TKTN,TEN_TKTN, MA_DVQHNS, TEN_DVQHNS,MA_DIABAN, TEN_DIABAN,MA_CAP,TEN_CAP,MA_CHUONG,TEN_CHUONG, MA_NGANHKT, TEN_NGANHKT,MA_LOAI, 
              TEN_LOAI, MA_TIEUMUC, TEN_TIEUMUC, MA_MUC, TEN_MUC, MA_TIEUNHOM, 
              TEN_TIEUNHOM, MA_NHOM, TEN_NHOM, MA_CTMTQG, TEN_CTMTQG, MA_KBNN, TEN_KBNN, MA_NGUON_NSNN, TEN_NGUON_NSNN, NGAY_KET_SO,NGAY_HACH_TOAN,GIA_TRI_HACH_TOAN, LOAI_DU_TOAN)('||SQL_QUERRY||')';
           DBMS_OUTPUT.PUT_LINE(INSERT_QUERRY);        
            BEGIN 
                EXECUTE IMMEDIATE INSERT_QUERRY;
            END;
        END IF;
        IF INSTR(P_LOAI,'T') > 0 THEN
          SQL_QUERRY := 'SELECT MA_NGHIEPVU,MA_TKTN, TEN_TKTN,MA_DVQHNS,TEN_DVQHNS,MA_DIABAN,TEN_DIABAN,MA_CAP,TEN_CAP,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,TEN_NGANHKT,MA_LOAI,TEN_LOAI,MA_TIEUMUC,TEN_TIEUMUC,MA_MUC,
              TEN_MUC,MA_TIEUNHOM,TEN_TIEUNHOM,MA_NHOM,TEN_NHOM,MA_CTMTQG,TEN_CTMTQG,MA_KBNN,TEN_KBNN,MA_NGUON_NSNN,TEN_NGUON_NSNN,NGAY_KET_SO, NGAY_HIEU_LUC,SUM (GIA_TRI_HACH_TOAN),ATTRIBUTE8
              FROM PHA_HACHTOAN_THU VC WHERE VC.NGAY_HIEU_LUC >= TO_DATE ('''|| '0101' || V_TU_NAM  ||''', ''ddMMyyyy'') AND VC.NGAY_HIEU_LUC <= TO_DATE('''||P_ENGAY_HACHTOAN||''',''DD/MM/YYYY'') AND ' ||' VC.NGAY_KET_SO <= TO_DATE('''||P_ENGAY_KETSO||''',''DD/MM/YYYY'') AND '
              ||P_CT||
              ' GROUP BY MA_NGHIEPVU,MA_TKTN, TEN_TKTN,MA_DVQHNS,TEN_DVQHNS,MA_DIABAN,TEN_DIABAN,MA_CAP,TEN_CAP,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,
              TEN_NGANHKT,MA_LOAI,TEN_LOAI,MA_TIEUMUC,TEN_TIEUMUC,MA_MUC,TEN_MUC,MA_TIEUNHOM,TEN_TIEUNHOM,MA_NHOM,TEN_NHOM,MA_CTMTQG,TEN_CTMTQG,
              MA_KBNN,TEN_KBNN,MA_NGUON_NSNN,TEN_NGUON_NSNN,NGAY_KET_SO, NGAY_HIEU_LUC,ATTRIBUTE8 ';     
              INSERT_QUERRY := 'INSERT INTO TEMP_MLNS(LOAI_NS, MA_TKTN,TEN_TKTN, MA_DVQHNS, TEN_DVQHNS,MA_DIABAN, TEN_DIABAN,MA_CAP,TEN_CAP,MA_CHUONG,TEN_CHUONG, MA_NGANHKT, TEN_NGANHKT,MA_LOAI, 
              TEN_LOAI, MA_TIEUMUC, TEN_TIEUMUC, MA_MUC, TEN_MUC, MA_TIEUNHOM, 
              TEN_TIEUNHOM, MA_NHOM, TEN_NHOM, MA_CTMTQG, TEN_CTMTQG, MA_KBNN, TEN_KBNN, MA_NGUON_NSNN, TEN_NGUON_NSNN, NGAY_KET_SO,NGAY_HACH_TOAN,GIA_TRI_HACH_TOAN, LOAI_DU_TOAN)('||SQL_QUERRY||')';
           DBMS_OUTPUT.PUT_LINE(INSERT_QUERRY);        
            BEGIN 
                EXECUTE IMMEDIATE INSERT_QUERRY;
            END;
        END IF;
        IF INSTR(P_LOAI,'D') > 0 THEN
          SQL_QUERRY := 'SELECT MA_NGHIEPVU,MA_TKTN, TEN_TKTN,MA_DVQHNS,TEN_DVQHNS,MA_DIABAN,TEN_DIABAN,MA_CAP,TEN_CAP,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,TEN_NGANHKT,MA_LOAI,TEN_LOAI,MA_TIEUMUC,TEN_TIEUMUC,MA_MUC,
              TEN_MUC,MA_TIEUNHOM,TEN_TIEUNHOM,MA_NHOM,TEN_NHOM,MA_CTMTQG,TEN_CTMTQG,MA_KBNN,TEN_KBNN,MA_NGUON_NSNN,TEN_NGUON_NSNN,NGAY_KET_SO, NGAY_HIEU_LUC,SUM (GIA_TRI_HACH_TOAN),ATTRIBUTE8
              FROM PHA_DUTOAN VC WHERE VC.NGAY_HIEU_LUC >= TO_DATE ('''|| '0101' || V_TU_NAM  ||''', ''ddMMyyyy'') AND VC.NGAY_HIEU_LUC <= TO_DATE('''||P_ENGAY_HACHTOAN||''',''DD/MM/YYYY'') AND ' ||' VC.NGAY_KET_SO <= TO_DATE('''||P_ENGAY_KETSO||''',''DD/MM/YYYY'') AND '
              ||P_CT||
              ' GROUP BY MA_NGHIEPVU,MA_TKTN, TEN_TKTN,MA_DVQHNS,TEN_DVQHNS,MA_DIABAN,TEN_DIABAN,MA_CAP,TEN_CAP,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,
              TEN_NGANHKT,MA_LOAI,TEN_LOAI,MA_TIEUMUC,TEN_TIEUMUC,MA_MUC,TEN_MUC,MA_TIEUNHOM,TEN_TIEUNHOM,MA_NHOM,TEN_NHOM,MA_CTMTQG,TEN_CTMTQG,
              MA_KBNN,TEN_KBNN,MA_NGUON_NSNN,TEN_NGUON_NSNN,NGAY_KET_SO, NGAY_HIEU_LUC,ATTRIBUTE8 ';     
              INSERT_QUERRY := 'INSERT INTO TEMP_MLNS(LOAI_NS, MA_TKTN,TEN_TKTN, MA_DVQHNS, TEN_DVQHNS,MA_DIABAN, TEN_DIABAN,MA_CAP,TEN_CAP,MA_CHUONG,TEN_CHUONG, MA_NGANHKT, TEN_NGANHKT,MA_LOAI, 
              TEN_LOAI, MA_TIEUMUC, TEN_TIEUMUC, MA_MUC, TEN_MUC, MA_TIEUNHOM, 
              TEN_TIEUNHOM, MA_NHOM, TEN_NHOM, MA_CTMTQG, TEN_CTMTQG, MA_KBNN, TEN_KBNN, MA_NGUON_NSNN, TEN_NGUON_NSNN, NGAY_KET_SO,NGAY_HACH_TOAN,GIA_TRI_HACH_TOAN, LOAI_DU_TOAN)('||SQL_QUERRY||')';
           DBMS_OUTPUT.PUT_LINE(INSERT_QUERRY);        
            BEGIN 
                EXECUTE IMMEDIATE INSERT_QUERRY;
            END;
        END IF;
    END;
  --------------------------------------------------
  PROCEDURE PRC_TH_MLNS(
      P_BNGAY_HACHTOAN DATE,
      P_ENGAY_HACHTOAN DATE,
      P_BNGAY_KETSO DATE,
      P_ENGAY_KETSO DATE,
      P_LOAI           VARCHAR2)
  IS
  V_TU_NAM VARCHAR(4):= to_char(P_BNGAY_HACHTOAN,'yyyy');
  BEGIN
    IF INSTR(P_LOAI,'C') > 0 THEN
      INSERT INTO TEMP_MLNS
      SELECT MA_NGHIEPVU,
        MA_TKTN, 
        TEN_TKTN,
        MA_DVQHNS,
        TEN_DVQHNS,
        MA_DIABAN,
        TEN_DIABAN,
        MA_CAP,
        TEN_CAP,
        MA_CAP,
        TEN_CAP,
        --MA_CAPMLNS,
        --TEN_CAPMLNS,
        MA_CHUONG,
        TEN_CHUONG,
        MA_NGANHKT,
        TEN_NGANHKT,
        MA_LOAI,
        TEN_LOAI,
        MA_TIEUMUC,
        TEN_TIEUMUC,
        MA_MUC,
        TEN_MUC,
        MA_TIEUNHOM,
        TEN_TIEUNHOM,
        MA_NHOM,
        TEN_NHOM,
        MA_CTMTQG,
        TEN_CTMTQG,
        MA_KBNN,
        TEN_KBNN,
        MA_NGUON_NSNN,
        TEN_NGUON_NSNN,
        NGAY_KET_SO,
        NGAY_HIEU_LUC,
        SUM (GIA_TRI_HACH_TOAN),
        ATTRIBUTE8
      FROM PHA_HACHTOAN_CHI VC
      WHERE 
      VC.NGAY_HIEU_LUC BETWEEN TO_DATE (''|| 0101 || V_TU_NAM  ||'', 'ddMMyyyy') AND P_ENGAY_HACHTOAN 
      AND VC.NGAY_KET_SO <= P_ENGAY_KETSO         
      GROUP BY MA_NGHIEPVU,
        MA_TKTN,
        TEN_TKTN,
        MA_DVQHNS,
        TEN_DVQHNS,
        MA_DIABAN,
        TEN_DIABAN,
        MA_CAP,
        TEN_CAP,
        MA_CAPMLNS,
        TEN_CAPMLNS,
        MA_CHUONG,
        TEN_CHUONG,
        MA_NGANHKT,
        TEN_NGANHKT,
        MA_LOAI,
        TEN_LOAI,
        MA_TIEUMUC,
        TEN_TIEUMUC,
        MA_MUC,
        TEN_MUC,
        MA_TIEUNHOM,
        TEN_TIEUNHOM,
        MA_NHOM,
        TEN_NHOM,
        MA_CTMTQG,
        TEN_CTMTQG,
        MA_KBNN,
        TEN_KBNN,
        MA_NGUON_NSNN,
        TEN_NGUON_NSNN,
        NGAY_KET_SO,
        NGAY_HIEU_LUC,
        ATTRIBUTE8;
    End IF;
    IF INSTR(P_LOAI,'T') > 0 THEN
      INSERT INTO TEMP_MLNS
      SELECT MA_NGHIEPVU,
        MA_TKTN,
        TEN_TKTN,
        MA_DVQHNS,
        TEN_DVQHNS,
        MA_DIABAN,
        TEN_DIABAN,
        MA_CAP,
        TEN_CAP,
        MA_CAP,
        TEN_CAP,
        --MA_CAPMLNS,
        --TEN_CAPMLNS,
        MA_CHUONG,
        TEN_CHUONG,
        MA_NGANHKT,
        TEN_NGANHKT,
        MA_LOAI,
        TEN_LOAI,
        MA_TIEUMUC,
        TEN_TIEUMUC,
        MA_MUC,
        TEN_MUC,
        MA_TIEUNHOM,
        TEN_TIEUNHOM,
        MA_NHOM,
        TEN_NHOM,
        MA_CTMTQG,
        TEN_CTMTQG,
        MA_KBNN,
        TEN_KBNN,
        MA_NGUON_NSNN,
        TEN_NGUON_NSNN,
        NGAY_KET_SO,
        NGAY_HIEU_LUC,
        SUM (GIA_TRI_HACH_TOAN),
        ATTRIBUTE8
      FROM PHA_HACHTOAN_THU VT
      --WHERE VT.NGAY_HIEU_LUC BETWEEN P_BNGAY_HACHTOAN AND P_ENGAY_HACHTOAN
      --AND VT.NGAY_KET_SO BETWEEN P_BNGAY_KETSO AND P_ENGAY_KETSO
       WHERE VT.NGAY_HIEU_LUC BETWEEN TO_DATE (''|| 0101 || V_TU_NAM  ||'', 'ddMMyyyy') AND P_ENGAY_HACHTOAN 
      AND VT.NGAY_KET_SO <= P_ENGAY_KETSO
      GROUP BY MA_NGHIEPVU,
        MA_TKTN,
        TEN_TKTN,
        MA_DVQHNS,
        TEN_DVQHNS,
        MA_DIABAN,
        TEN_DIABAN,
        MA_CAPMLNS,
        TEN_CAPMLNS,
        MA_CAP,
        TEN_CAP,
        MA_CHUONG,
        TEN_CHUONG,
        MA_NGANHKT,
        TEN_NGANHKT,
        MA_LOAI,
        TEN_LOAI,
        MA_TIEUMUC,
        TEN_TIEUMUC,
        MA_MUC,
        TEN_MUC,
        MA_TIEUNHOM,
        TEN_TIEUNHOM,
        MA_NHOM,
        TEN_NHOM,
        MA_CTMTQG,
        TEN_CTMTQG,
        MA_KBNN,
        TEN_KBNN,
        MA_NGUON_NSNN,
        TEN_NGUON_NSNN,
        NGAY_KET_SO,
        NGAY_HIEU_LUC,
        ATTRIBUTE8; 
    End If;
    
    IF INSTR(P_LOAI,'D') > 0 THEN
      INSERT INTO TEMP_MLNS
      SELECT MA_NGHIEPVU,
        MA_TKTN,
        TEN_TKTN,
        MA_DVQHNS,
        TEN_DVQHNS,
        MA_DIABAN,
        TEN_DIABAN,
        MA_CAP,
        TEN_CAP,
        MA_CAP,
        TEN_CAP,
        --MA_CAPMLNS,
        --TEN_CAPMLNS,
        MA_CHUONG,
        TEN_CHUONG,
        MA_NGANHKT,
        TEN_NGANHKT,
        MA_LOAI,
        TEN_LOAI,
        MA_TIEUMUC,
        TEN_TIEUMUC,
        MA_MUC,
        TEN_MUC,
        MA_TIEUNHOM,
        TEN_TIEUNHOM,
        MA_NHOM,
        TEN_NHOM,
        MA_CTMTQG,
        TEN_CTMTQG,
        MA_KBNN,
        TEN_KBNN,
        MA_NGUON_NSNN,
        TEN_NGUON_NSNN,
        NGAY_KET_SO,
        NGAY_HIEU_LUC,
        SUM (GIA_TRI_HACH_TOAN),
        ATTRIBUTE8
      FROM PHA_DUTOAN VT
      --WHERE VT.NGAY_HIEU_LUC BETWEEN P_BNGAY_HACHTOAN AND P_ENGAY_HACHTOAN
      --AND VT.NGAY_KET_SO BETWEEN P_BNGAY_KETSO AND P_ENGAY_KETSO
      WHERE VT.NGAY_HIEU_LUC BETWEEN TO_DATE (''|| 0101 || V_TU_NAM  ||'', 'ddMMyyyy') AND P_ENGAY_HACHTOAN 
      AND VT.NGAY_KET_SO <= P_ENGAY_KETSO
      GROUP BY MA_NGHIEPVU,
        MA_TKTN,
        TEN_TKTN,
        MA_DVQHNS,
        TEN_DVQHNS,
        MA_DIABAN,
        TEN_DIABAN,
        --MA_CAPMLNS,
        --TEN_CAPMLNS,
        MA_CAP,
        TEN_CAP,
        MA_CAP,
        TEN_CAP,
        MA_CHUONG,
        TEN_CHUONG,
        MA_NGANHKT,
        TEN_NGANHKT,
        MA_LOAI,
        TEN_LOAI,
        MA_TIEUMUC,
        TEN_TIEUMUC,
        MA_MUC,
        TEN_MUC,
        MA_TIEUNHOM,
        TEN_TIEUNHOM,
        MA_NHOM,
        TEN_NHOM,
        MA_CTMTQG,
        TEN_CTMTQG,
        MA_KBNN,
        TEN_KBNN,
        MA_NGUON_NSNN,
        TEN_NGUON_NSNN,
        NGAY_KET_SO,
        NGAY_HIEU_LUC,
        ATTRIBUTE8; 
    End If;    
      
  END;
  
  PROCEDURE PRC_TH_MLNS_EXCELL(
      P_BNGAY_HACHTOAN DATE,
      P_ENGAY_HACHTOAN DATE,
      P_BNGAY_KETSO DATE,
      P_ENGAY_KETSO DATE,
      P_LOAI           VARCHAR2,
      P_USERID  VARCHAR2
      )
  IS
  V_TU_NAM VARCHAR(4):= to_char(P_BNGAY_HACHTOAN,'yyyy');
  BEGIN
   Delete PHA_MLNS c where c.USERID = P_USERID; 
    IF INSTR(P_LOAI,'C') > 0 THEN
      INSERT INTO PHA_MLNS
      SELECT MA_NGHIEPVU,
        MA_TKTN, 
        TEN_TKTN,
        MA_DVQHNS,
        TEN_DVQHNS,
        MA_DIABAN,
        TEN_DIABAN,
        MA_CAP,
        TEN_CAP,
        MA_CAP,
        TEN_CAP,
        --MA_CAPMLNS,
        --TEN_CAPMLNS,
        MA_CHUONG,
        TEN_CHUONG,
        MA_NGANHKT,
        TEN_NGANHKT,
        MA_LOAI,
        TEN_LOAI,
        MA_TIEUMUC,
        TEN_TIEUMUC,
        MA_MUC,
        TEN_MUC,
        MA_TIEUNHOM,
        TEN_TIEUNHOM,
        MA_NHOM,
        TEN_NHOM,
        MA_CTMTQG,
        TEN_CTMTQG,
        MA_KBNN,
        TEN_KBNN,
        MA_NGUON_NSNN,
        TEN_NGUON_NSNN,
        NGAY_KET_SO,
        NGAY_HIEU_LUC,
        SUM (GIA_TRI_HACH_TOAN),
        ATTRIBUTE8,
        P_USERID
      FROM PHA_HACHTOAN_CHI VC
      --WHERE VC.NGAY_HIEU_LUC BETWEEN P_BNGAY_HACHTOAN AND P_ENGAY_HACHTOAN
      --And VC.NGAY_KET_SO BETWEEN P_BNGAY_KETSO AND P_ENGAY_KETSO
      WHERE VC.NGAY_HIEU_LUC BETWEEN TO_DATE (''|| 0101 || V_TU_NAM  ||'', 'ddMMyyyy') AND P_ENGAY_HACHTOAN 
      AND VC.NGAY_KET_SO <= P_ENGAY_KETSO
      --and VC.NGAY_KET_SO <= to_date('31122016','ddMMyyyy')
      GROUP BY MA_NGHIEPVU,
        MA_TKTN,
        TEN_TKTN,
        MA_DVQHNS,
        TEN_DVQHNS,
        MA_DIABAN,
        TEN_DIABAN,
        MA_CAP,
        TEN_CAP,
        MA_CAPMLNS,
        TEN_CAPMLNS,
        MA_CHUONG,
        TEN_CHUONG,
        MA_NGANHKT,
        TEN_NGANHKT,
        MA_LOAI,
        TEN_LOAI,
        MA_TIEUMUC,
        TEN_TIEUMUC,
        MA_MUC,
        TEN_MUC,
        MA_TIEUNHOM,
        TEN_TIEUNHOM,
        MA_NHOM,
        TEN_NHOM,
        MA_CTMTQG,
        TEN_CTMTQG,
        MA_KBNN,
        TEN_KBNN,
        MA_NGUON_NSNN,
        TEN_NGUON_NSNN,
        NGAY_KET_SO,
        NGAY_HIEU_LUC,
        ATTRIBUTE8,
        P_USERID;
    End IF;
    IF INSTR(P_LOAI,'T') > 0 THEN
      INSERT INTO PHA_MLNS
      SELECT MA_NGHIEPVU,
        MA_TKTN,
        TEN_TKTN,
        MA_DVQHNS,
        TEN_DVQHNS,
        MA_DIABAN,
        TEN_DIABAN,
        MA_CAP,
        TEN_CAP,
        MA_CAP,
        TEN_CAP,
        --MA_CAPMLNS,
        --TEN_CAPMLNS,
        MA_CHUONG,
        TEN_CHUONG,
        MA_NGANHKT,
        TEN_NGANHKT,
        MA_LOAI,
        TEN_LOAI,
        MA_TIEUMUC,
        TEN_TIEUMUC,
        MA_MUC,
        TEN_MUC,
        MA_TIEUNHOM,
        TEN_TIEUNHOM,
        MA_NHOM,
        TEN_NHOM,
        MA_CTMTQG,
        TEN_CTMTQG,
        MA_KBNN,
        TEN_KBNN,
        MA_NGUON_NSNN,
        TEN_NGUON_NSNN,
        NGAY_KET_SO,
        NGAY_HIEU_LUC,
        SUM (GIA_TRI_HACH_TOAN),
        ATTRIBUTE8,
        P_USERID
      FROM PHA_HACHTOAN_THU VT
      --WHERE VT.NGAY_HIEU_LUC BETWEEN P_BNGAY_HACHTOAN AND P_ENGAY_HACHTOAN
      --And VT.NGAY_KET_SO BETWEEN P_BNGAY_KETSO AND P_ENGAY_KETSO
      WHERE VT.NGAY_HIEU_LUC BETWEEN TO_DATE (''|| 0101 || V_TU_NAM  ||'', 'ddMMyyyy') AND P_ENGAY_HACHTOAN 
      AND VT.NGAY_KET_SO <= P_ENGAY_KETSO
      GROUP BY MA_NGHIEPVU,
        MA_TKTN,
        TEN_TKTN,
        MA_DVQHNS,
        TEN_DVQHNS,
        MA_DIABAN,
        TEN_DIABAN,
        MA_CAPMLNS,
        TEN_CAPMLNS,
        MA_CAP,
        TEN_CAP,
        MA_CHUONG,
        TEN_CHUONG,
        MA_NGANHKT,
        TEN_NGANHKT,
        MA_LOAI,
        TEN_LOAI,
        MA_TIEUMUC,
        TEN_TIEUMUC,
        MA_MUC,
        TEN_MUC,
        MA_TIEUNHOM,
        TEN_TIEUNHOM,
        MA_NHOM,
        TEN_NHOM,
        MA_CTMTQG,
        TEN_CTMTQG,
        MA_KBNN,
        TEN_KBNN,
        MA_NGUON_NSNN,
        TEN_NGUON_NSNN,
        NGAY_KET_SO,
        NGAY_HIEU_LUC,
        ATTRIBUTE8,
        P_USERID;
    End If;
    
    IF INSTR(P_LOAI,'D') > 0 THEN
      INSERT INTO PHA_MLNS
      SELECT MA_NGHIEPVU,
        MA_TKTN,
        TEN_TKTN,
        MA_DVQHNS,
        TEN_DVQHNS,
        MA_DIABAN,
        TEN_DIABAN,
        MA_CAP,
        TEN_CAP,
        MA_CAP,
        TEN_CAP,
        --MA_CAPMLNS,
        --TEN_CAPMLNS,
        MA_CHUONG,
        TEN_CHUONG,
        MA_NGANHKT,
        TEN_NGANHKT,
        MA_LOAI,
        TEN_LOAI,
        MA_TIEUMUC,
        TEN_TIEUMUC,
        MA_MUC,
        TEN_MUC,
        MA_TIEUNHOM,
        TEN_TIEUNHOM,
        MA_NHOM,
        TEN_NHOM,
        MA_CTMTQG,
        TEN_CTMTQG,
        MA_KBNN,
        TEN_KBNN,
        MA_NGUON_NSNN,
        TEN_NGUON_NSNN,
        NGAY_KET_SO,
        NGAY_HIEU_LUC,
        SUM (GIA_TRI_HACH_TOAN),
        ATTRIBUTE8,
        P_USERID
      FROM PHA_DUTOAN VT
      --WHERE VT.NGAY_HIEU_LUC BETWEEN P_BNGAY_HACHTOAN AND P_ENGAY_HACHTOAN
      --And VT.NGAY_KET_SO BETWEEN P_BNGAY_KETSO AND P_ENGAY_KETSO
      WHERE VT.NGAY_HIEU_LUC BETWEEN TO_DATE (''|| 0101 || V_TU_NAM  ||'', 'ddMMyyyy') AND P_ENGAY_HACHTOAN 
      AND VT.NGAY_KET_SO <= P_ENGAY_KETSO
      GROUP BY MA_NGHIEPVU,
        MA_TKTN,
        TEN_TKTN,
        MA_DVQHNS,
        TEN_DVQHNS,
        MA_DIABAN,
        TEN_DIABAN,
        --MA_CAPMLNS,
        --TEN_CAPMLNS,
        MA_CAP,
        TEN_CAP,
        MA_CAP,
        TEN_CAP,
        MA_CHUONG,
        TEN_CHUONG,
        MA_NGANHKT,
        TEN_NGANHKT,
        MA_LOAI,
        TEN_LOAI,
        MA_TIEUMUC,
        TEN_TIEUMUC,
        MA_MUC,
        TEN_MUC,
        MA_TIEUNHOM,
        TEN_TIEUNHOM,
        MA_NHOM,
        TEN_NHOM,
        MA_CTMTQG,
        TEN_CTMTQG,
        MA_KBNN,
        TEN_KBNN,
        MA_NGUON_NSNN,
        TEN_NGUON_NSNN,
        NGAY_KET_SO,
        NGAY_HIEU_LUC,
        ATTRIBUTE8,
        P_USERID;
    End If;    
      
  END;
  
  PROCEDURE PRC_TH_TONQUY(
      P_BNGAY_HACHTOAN DATE,
      P_ENGAY_HACHTOAN DATE)
  IS
  BEGIN
    INSERT INTO TEMP_MLNS
      SELECT MA_NGHIEPVU,
        MA_TKTN,
        TEN_TKTN,
        MA_DVQHNS,
        TEN_DVQHNS,
        MA_DIABAN,
        TEN_DIABAN,
        MA_CAP,
        TEN_CAP,
        MA_CAP,
        TEN_CAP,
        --MA_CAPMLNS,
        --TEN_CAPMLNS,
        MA_CHUONG,
        TEN_CHUONG,
        MA_NGANHKT,
        TEN_NGANHKT,
        MA_LOAI,
        TEN_LOAI,
        MA_TIEUMUC,
        TEN_TIEUMUC,
        MA_MUC,
        TEN_MUC,
        MA_TIEUNHOM,
        TEN_TIEUNHOM,
        MA_NHOM,
        TEN_NHOM,
        MA_CTMTQG,
        TEN_CTMTQG,
        MA_KBNN,
        TEN_KBNN,
        MA_NGUON_NSNN,
        TEN_NGUON_NSNN,
        NGAY_KET_SO,
        NGAY_HIEU_LUC,
        SUM (GIA_TRI_HACH_TOAN),
        ATTRIBUTE8
      FROM (select * from PHA_HACHTOAN_THU WHERE NGAY_HIEU_LUC BETWEEN P_BNGAY_HACHTOAN AND P_ENGAY_HACHTOAN
            union ALL
            select * from PHA_HACHTOAN_CHI WHERE NGAY_HIEU_LUC BETWEEN P_BNGAY_HACHTOAN AND P_ENGAY_HACHTOAN) VT
      GROUP BY MA_NGHIEPVU,
        MA_TKTN,
        TEN_TKTN,
        MA_DVQHNS,
        TEN_DVQHNS,
        MA_DIABAN,
        TEN_DIABAN,
        MA_CAPMLNS,
        TEN_CAPMLNS,
        MA_CAP,
        TEN_CAP,
        MA_CHUONG,
        TEN_CHUONG,
        MA_NGANHKT,
        TEN_NGANHKT,
        MA_LOAI,
        TEN_LOAI,
        MA_TIEUMUC,
        TEN_TIEUMUC,
        MA_MUC,
        TEN_MUC,
        MA_TIEUNHOM,
        TEN_TIEUNHOM,
        MA_NHOM,
        TEN_NHOM,
        MA_CTMTQG,
        TEN_CTMTQG,
        MA_KBNN,
        TEN_KBNN,
        MA_NGUON_NSNN,
        TEN_NGUON_NSNN,
        NGAY_KET_SO,
        NGAY_HIEU_LUC,
        ATTRIBUTE8;
  END;
  PROCEDURE PRC_SUM_UP
  IS
    V_LV  NUMBER := 0;
    V_MAX NUMBER := 0;
    V_MD TEMPTB_DMCT%ROWTYPE;
  TYPE RC_MD
IS
  REF
  CURSOR;
    C_MD RC_MD;
  BEGIN
    SELECT MAX (LENGTH (tmp.MA_DONG)) INTO V_MAX FROM temptb_dmct tmp;
  WHILE (V_MAX > 0)
  LOOP
    OPEN C_MD FOR ('Select * From Temptb_dmct Tmp                        
Where Length(Tmp.MA_DONG) = :V_MAX') USING V_MAX;
    LOOP
      FETCH C_MD INTO V_MD;
      EXIT
    WHEN C_MD%NOTFOUND;
      IF(V_MD.DAU = 1) THEN
        UPDATE temptb_dmct tmp
          --
          --                Set tmp.TW_LK = select sum(TW_LK) from  tmp where tmp.MA_DONG like
        SET tmp.TW_LK     = NVL (tmp.TW_LK, 0)             + NVL (V_MD.TW_LK, 0),
          tmp.TINH_LK     = NVL (tmp.TINH_LK, 0)           + NVL (V_MD.TINH_LK, 0),
          tmp.HUYEN_LK    = NVL (tmp.HUYEN_LK, 0)          + NVL (V_MD.HUYEN_LK, 0),
          tmp.XA_LK       = NVL (tmp.XA_LK, 0)             + NVL (V_MD.XA_LK, 0),
          tmp.TW_PS       = NVL (tmp.TW_PS, 0)             + NVL (V_MD.TW_PS, 0),
          tmp.TINH_PS     = NVL (tmp.TINH_PS, 0)           + NVL (V_MD.TINH_PS, 0),
          tmp.HUYEN_PS    = NVL (tmp.HUYEN_PS, 0)          + NVL (V_MD.HUYEN_PS, 0),
          tmp.XA_PS       = NVL (tmp.XA_PS, 0)             + NVL (V_MD.XA_PS, 0)
        WHERE tmp.MA_DONG = SUBSTR (V_MD.MA_DONG, 0, V_MAX - 2);
      ElsIF (V_MD.DAU     =                                -1) THEN
        UPDATE temptb_dmct tmp
        SET tmp.TW_LK     = NVL (tmp.TW_LK, 0)             - NVL (V_MD.TW_LK, 0),
          tmp.TINH_LK     = NVL (tmp.TINH_LK, 0)           - NVL (V_MD.TINH_LK, 0),
          tmp.HUYEN_LK    = NVL (tmp.HUYEN_LK, 0)          - NVL (V_MD.HUYEN_LK, 0),
          tmp.XA_LK       = NVL (tmp.XA_LK, 0)             - NVL (V_MD.XA_LK, 0),
          tmp.TW_PS       = NVL (tmp.TW_PS, 0)             - NVL (V_MD.TW_PS, 0),
          tmp.TINH_PS     = NVL (tmp.TINH_PS, 0)           - NVL (V_MD.TINH_PS, 0),
          tmp.HUYEN_PS    = NVL (tmp.HUYEN_PS, 0)          - NVL (V_MD.HUYEN_PS, 0),
          tmp.XA_PS       = NVL (tmp.XA_PS, 0)             - NVL (V_MD.XA_PS, 0)
        WHERE tmp.MA_DONG = SUBSTR (V_MD.MA_DONG, 0, V_MAX - 2);
      END IF;
    END LOOP;
    V_MAX := V_MAX - 2;
  END LOOP;
/*Update temptb_dmct tmp
Set tmp.ILV =
(Select Sub.LV
From (
Select tmp1.MA_CHITIEU as MA_CHITIEU, tmp1.MA_CHITIEU_CHA as MA_CHITIEU_CHA, level as LV
from temptb_dmct tmp1
Start with tmp1.MA_CHITIEU_CHA is null
Connect by prior tmp1.MA_CHITIEU = tmp1.MA_CHITIEU_CHA) Sub
Where tmp.MA_CHITIEU = Sub.MA_CHITIEU_CHA);
Select max(tmp.ILV) Into V_LV From temptb_dmct tmp;
While V_LV > 1 loop
Update temptb_dmct tmp
Set tmp.TW_LK =
(Select v.TW_LK
From (Select tmp1.MA_CHITIEU_CHA,
nvl(sum(tmp1.TW_LK), 0) TW_LK
From temptb_dmct tmp1
Where tmp1.ILV = V_LV
Group by tmp1.MA_CHITIEU_CHA) v
Where tmp.MA_CHITIEU = v.MA_CHITIEU_CHA)
Where tmp.TW_LK is null;
Update temptb_dmct tmp
Set tmp.TINH_LK =
(Select v.TINH_LK
From (Select tmp1.MA_CHITIEU_CHA,
nvl(sum(tmp1.TINH_LK), 0) TINH_LK
From temptb_dmct tmp1
Where tmp1.ILV = V_LV
Group by tmp1.MA_CHITIEU_CHA) v
Where tmp.MA_CHITIEU = v.MA_CHITIEU_CHA)
Where tmp.TINH_LK is null;
Update temptb_dmct tmp
Set tmp.HUYEN_LK =
(Select v.HUYEN_LK
From (Select tmp1.MA_CHITIEU_CHA,
nvl(sum(tmp1.HUYEN_LK), 0) HUYEN_LK
From temptb_dmct tmp1
Where tmp1.ILV = V_LV
Group by tmp1.MA_CHITIEU_CHA) v
Where tmp.MA_CHITIEU = v.MA_CHITIEU_CHA)
Where tmp.HUYEN_LK is null;
Update temptb_dmct tmp
Set tmp.XA_LK =
(Select v.XA_LK
From (Select tmp1.MA_CHITIEU_CHA,
nvl(sum(tmp1.XA_LK), 0) XA_LK
From temptb_dmct tmp1
Where tmp1.ILV = V_LV
Group by tmp1.MA_CHITIEU_CHA) v
Where tmp.MA_CHITIEU = v.MA_CHITIEU_CHA)
Where tmp.XA_LK is null;
V_LV := V_LV - 1;
end loop;*/
END;
-- F_LOAIDATA : 0 Thu, 1 Chi
PROCEDURE PRC_GET_DATA(
    F_LOAIDATA NUMBER,
    F_TABLE_NAME NCHAR,
    F_TUNGAY  DATE,
    F_DENNGAY DATE,
    F_SHKB NCHAR)
IS
  CURSOR c_nv
  IS
    SELECT *
    FROM DM_NGHIEPVU nv
    WHERE 1            =1
    AND Lower(nv.LOAI) = (
      CASE F_LoaiData
        WHEN 0
        THEN 'thu'
        WHEN 1
        THEN 'chi'
        WHEN 2
        THEN 'dutoan'
        ELSE 'null'
      END) ;
  --r_nv          DM_NGHIEPVU%ROWTYPE;
  V_SQL    VARCHAR2(32000);
  V_INSERT VARCHAR2(32000);
  V_Del    VARCHAR2(500) := 'Delete '|| F_TABLE_NAME ||' where 1=1 And NGAY_KET_SO >= To_Date(' || Chr(39) || TO_CHAR(F_TUNGAY,'dd/MM/yyyy') || Chr(39) || ',' || Chr(39) || 'dd/MM/yyyy' || Chr(39) || ')' || ' And NGAY_KET_SO <= To_Date(' || Chr(39) || TO_CHAR(F_DENNGAY,'dd/MM/yyyy') || Chr(39) || ',' || Chr(39) || 'dd/MM/yyyy' || Chr(39) || ')' ;
BEGIN
  V_SQL := 'Insert Into '|| F_TABLE_NAME ||
  '                                     
(  SEGMENT1,                                       
MA_TKTN,                                       
MA_NHOM,                                       
MA_TIEUNHOM,                                       
MA_MUC,                                       
MA_TIEUMUC,                                       
MA_CAP,                                       
MA_DVQHNS,                                       
MA_DIABAN,                                       
MA_CAPMLNS,                                       
MA_CHUONG,                                       
MA_NGANHKT,                                       
MA_LOAI,                                       
MA_CTMTQG,                                       
MA_KBNN,                                       
MA_NGUON_NSNN,                                       
SEGMENT12,                                       
SET_OF_BOOKS_ID,                                       
SOB_NAME,                                       
PERIOD_NAME,                                       
NGAY_KET_SO,                                       
NGAY_HIEU_LUC,                                       
ENTERED_DR,                                       
ENTERED_CR,                                          
ACTUAL_FLAG,                                       
CURRENCY_CODE,                                        
TRANSFORM_DATE,                                       
VOUCHER_DATE,                                       
ATTRIBUTE8,                                                                                                         
MA_NGHIEPVU,                                                                          
GIA_TRI_HACH_TOAN,                                       
TEN_TKTN,                                       
TEN_NHOM,                                       
TEN_TIEUNHOM,                                       
TEN_MUC,                                       
TEN_TIEUMUC,                                       
TEN_CAP,                                       
TEN_CHUONG,                                       
TEN_CAPMLNS,                                       
TEN_CTMTQG,                                       
TEN_KBNN,                                       
TEN_NGUON_NSNN )                         
Select  SEGMENT1,                                 
SEGMENT2 AS MA_TKTN,                                
STC_PA_SYS.FNC_GET_CONVERT_NDKT2NHOM(SEGMENT3) AS MA_NHOM,                                
STC_PA_SYS.FNC_GET_CONVERT_NDKT2TNHOM(SEGMENT3) AS MA_TIEUNHOM,                                
STC_PA_SYS.FNC_GET_CONVERT_NDKT2MUC(SEGMENT3) AS MA_MUC ,                                
SEGMENT3 AS MA_TIEUMUC,                                
SEGMENT4 AS MA_CAP,                                
SEGMENT5 AS MA_DVQHNS,                                
SEGMENT6 AS MA_DIABAN,                                
STC_PA_SYS.FNC_GET_CONVERT_CHUONG2CAP(SEGMENT7) AS MA_CAPMLNS,                                
SEGMENT7 AS MA_CHUONG,
SEGMENT8 AS MA_NGANHKT,                                
STC_PA_SYS.FNC_GET_CONVERT_NKT2LOAI(SEGMENT8) AS MA_LOAI,
SEGMENT9 AS MA_CTMTQG,
SEGMENT10 AS MA_KBNN,
SEGMENT11 AS MA_NGUON_NSNN,
SEGMENT12,                                
SET_OF_BOOKS_ID,                                
SOB_NAME,                                
PERIOD_NAME,                                
POSTED_DATE AS NGAY_KET_SO,                                
EFFECTIVE_DATE AS NGAY_HIEU_LUC,                                
ENTERED_DR,                                
ENTERED_CR,                                
--ACCOUNTED_DR,                                
--ACCOUNTED_CR,                                
ACTUAL_FLAG,                                
CURRENCY_CODE,                                
TRANSFORM_DATE,                                
VOUCHER_DATE,                                
ATTRIBUTE8,                                 
LOAI_NV AS LOAI_NGHIEP_VU,                                 
DR_CR  AS TIEN_HACH_TOAN,                                
STC_PA_SYS.FNC_GET_TEN_DM ('
  || Chr(39) || 'DM_TKTN' || Chr(39) || ',' || Chr(39) || 'TEN_TKTN' || Chr(39) || ',' || Chr(39) || 'MA_TKTN' || Chr(39) || ',' || ' SEGMENT2,                                     
EFFECTIVE_DATE) AS TEN_TKTN,                                
STC_PA_SYS.FNC_GET_DICTIONARY(' || Chr(39) || 'MA_NHOM' || Chr(39) || ',STC_PA_SYS.FNC_GET_CONVERT_NDKT2NHOM(SEGMENT3),EFFECTIVE_DATE) AS TEN_NHOM,                                
STC_PA_SYS.FNC_GET_TEN_DM (' || Chr(39) || 'PHA_DM_TIEUNHOM' || Chr(39) || ',' || Chr(39) || 'TEN_TIEUNHOM' || Chr(39) || ',' || Chr(39) || 'MA_TIEUNHOM' || Chr(39) || ',' || ' STC_PA_SYS.FNC_GET_CONVERT_NDKT2TNHOM (SEGMENT3),                                     
EFFECTIVE_DATE) AS TEN_TIEUNHOM,                                     
STC_PA_SYS.FNC_GET_TEN_DM (' || Chr(39) || 'DM_MUC' || Chr(39) || ',' || Chr(39) || 'TEN_MUC' || Chr(39) || ',' || Chr(39) || 'MA_MUC' || Chr(39) || ',' ||
  ' STC_PA_SYS.FNC_GET_CONVERT_NDKT2MUC (SEGMENT3),                                     
POSTED_DATE)                                     
AS TEN_MUC,                                     
STC_PA_SYS.FNC_GET_TEN_DM (' || Chr(39) || 'DM_TIEUMUC' || Chr(39) || ',' || Chr(39) || 'TEN_TIEUMUC' || Chr(39) || ',' || Chr(39) || 'MA_TIEUMUC' || Chr(39) || ',' || ' SEGMENT3,                                                             
EFFECTIVE_DATE)                                     
AS TEN_TIEUMUC,                                     
STC_PA_SYS.FNC_GET_DICTIONARY (' || Chr(39) || 'MA_CAP' || Chr(39) || ',' || ' STC_PA_SYS.FNC_GET_CONVERT_CHUONG2CAP (SEGMENT7),                                     
EFFECTIVE_DATE)                                     
AS TEN_CAP,                                     
STC_PA_SYS.FNC_GET_TEN_DM (' || Chr(39) || 'DM_CHUONG'|| Chr(39) || ',' || Chr(39) || 'TEN_CHUONG'|| Chr(39) || ',' || Chr(39) || 'MA_CHUONG'|| Chr(39) || ',' ||
  ' SEGMENT7,                                                             
EFFECTIVE_DATE)                                     
AS TEN_CHUONG,                                     
STC_PA_SYS.FNC_GET_DICTIONARY (' || Chr(39) || 'MA_CAP'|| Chr(39) || ',' || ' STC_PA_SYS.FNC_GET_CONVERT_CHUONG2CAP (SEGMENT7),                                     
EFFECTIVE_DATE)                                     
AS TEN_CAPNS,                                     
STC_PA_SYS.FNC_GET_TEN_DM (' || Chr(39) || 'DM_CTMTQG'|| Chr(39) || ',' || Chr(39) || 'TEN_CTMTQG'|| Chr(39) || ',' || Chr(39) || 'MA_CTMTQG'|| Chr(39) || ',' || ' SEGMENT9,                                                             
EFFECTIVE_DATE)                                     
AS TEN_CTMTQG,                                     
STC_PA_SYS.FNC_GET_TEN_DM  (' || Chr(39) || 'DM_KBNN'|| Chr(39) || ',' || Chr(39) || 'TEN_KBNN' || Chr(39) || ',' || Chr(39) || 'MA_KBNN' || Chr(39) || ',' ||
  ' SEGMENT10,                                                             
EFFECTIVE_DATE)                                     
AS TEN_KBNN,                                     
STC_PA_SYS.FNC_GET_TEN_DM (' || Chr(39) || 'DM_NGUON_NSNN'|| Chr(39) || ',' || Chr(39) || 'TEN_NGUON_NSNN'|| Chr(39) || ',' || Chr(39) || 'MA_NGUON_NSNN'|| Chr(39) || ',' || ' SEGMENT11,                                                             
EFFECTIVE_DATE)                                     
AS TEN_NGUON_NSNN                                
From TABWH_JEL_FCT                                 
Where 1=1                                 
AND POSTED_DATE >= To_Date(' || Chr(39) || TO_CHAR(F_TUNGAY,'dd/MM/yyyy') || Chr(39) || ',' || Chr(39) || 'dd/MM/yyyy' || Chr(39) || ')' || ' And POSTED_DATE <= To_Date(' || Chr(39) || TO_CHAR(F_DENNGAY,'dd/MM/yyyy') || Chr(39) || ',' || Chr(39) || 'dd/MM/yyyy' || Chr(39) || ')';
  -- Neu nhan theo tung so hieu kho bac rieng
  IF(F_SHKB IS NOT NULL OR LENGTH(Trim(F_SHKB)) > 0) THEN
    V_Del   := V_Del || ' And Segment_10 = ' || Chr(39) || F_SHKB || Chr(39);
  END IF;
  CASE F_LOAIDATA
  WHEN 0 THEN -- Thu
    -- Delete data
    V_Del := REPLACE(V_Del,'PHA_HACHTOAN_CHI','PHA_HACHTOAN_THU');
    --DBMS_OUTPUT.PUT_LINE(V_Del);
    --Execute Immediate V_Del;
    --Lay Du lieu
    V_SQL := REPLACE(V_SQL,'PHA_HACHTOAN_CHI','PHA_HACHTOAN_THU');
    -- Lay du lieu theo nghiep vu
    FOR r_nv IN c_nv
    LOOP
      V_INSERT := ' ';
      V_INSERT := V_SQL;
      -- Lay nghiep vu
      V_INSERT := REPLACE(V_INSERT, 'LOAI_NV', Chr(39) || r_nv.ma_nghiepvu || Chr(39));
      -- Chuyen doi dieu kien so tien lay Dr - Cr hoac Cr - Dr ... tu bang DM_NghiepVu
      V_INSERT := REPLACE(V_INSERT, 'DR_CR', r_nv.DR_CR);
      -- Chuyen doi dieu kien tu cong thuc lay trong bang DM_NghiepVu
      V_INSERT := V_INSERT || ' And (' || FNC_CONVERT_FORMULA_FROM_TAB(r_nv.CONG_THUC) || ')';
      -- Lay them dieu kien tu bang DM_NghiepVu
      V_INSERT := V_INSERT || ' And ' || r_nv.Dieu_Kien;
      -- Thuc thi cau lenh
      --DBMS_OUTPUT.PUT_LINE(V_INSERT);
      EXECUTE Immediate V_INSERT;
    END LOOP;
  WHEN 1 THEN -- Chi
    -- Delete data
    --DBMS_OUTPUT.PUT_LINE(V_Del);
    EXECUTE Immediate V_Del;
    -- Lay du lieu theo nghiep vu
    FOR r_nv IN c_nv
    LOOP
      V_INSERT := ' ';
      V_INSERT := V_SQL;
      -- Lay nghiep vu
      V_INSERT := REPLACE(V_INSERT, 'LOAI_NV', Chr(39) || r_nv.ma_nghiepvu || Chr(39));
      -- Chuyen doi dieu kien so tien lay Dr - Cr hoac Cr - Dr ... tu bang DM_NghiepVu
      V_INSERT := REPLACE(V_INSERT, 'DR_CR', r_nv.DR_CR);
      -- Chuyen doi dieu kien tu cong thuc lay trong bang DM_NghiepVu
      V_INSERT := V_INSERT || ' And (' || FNC_CONVERT_FORMULA_FROM_TAB(r_nv.CONG_THUC) || ')';
      --V_INSERT :=REPLACE (V_INSERT, 'SEGMENT3 like ''340''', ' SEGMENT3 like ''34''');
      -- Lay them dieu kien tu bang DM_NghiepVu
      V_INSERT := V_INSERT || ' And ' || r_nv.Dieu_Kien;
      -- Thuc thi cau lenh
      --DBMS_OUTPUT.PUT_LINE(V_INSERT);
      EXECUTE Immediate V_INSERT;
    END LOOP;
  WHEN 2 THEN -- Du toan
    -- Delete data
    V_Del := REPLACE(V_Del,'PHA_HACHTOAN_CHI','PHA_DUTOAN');
    --DBMS_OUTPUT.PUT_LINE(V_Del);
    --Execute Immediate V_Del;
    --Lay Du lieu
    V_SQL := REPLACE(V_SQL,'PHA_HACHTOAN_CHI','PHA_DUTOAN');
    -- Lay du lieu theo nghiep vu
    FOR r_nv IN c_nv
    LOOP
      V_INSERT := ' ';
      V_INSERT := V_SQL;
      -- Lay nghiep vu
      V_INSERT := REPLACE(V_INSERT, 'LOAI_NV', Chr(39) || r_nv.ma_nghiepvu || Chr(39));
      -- Chuyen doi dieu kien so tien lay Dr - Cr hoac Cr - Dr ... tu bang DM_NghiepVu
      V_INSERT := REPLACE(V_INSERT, 'DR_CR', r_nv.DR_CR);
      -- Chuyen doi dieu kien tu cong thuc lay trong bang DM_NghiepVu
      V_INSERT := V_INSERT || ' And (' || FNC_CONVERT_FORMULA_FROM_TAB(r_nv.CONG_THUC) || ')';
      -- Lay them dieu kien tu bang DM_NghiepVu
      V_INSERT := V_INSERT || ' And ' || r_nv.Dieu_Kien;
      -- Thuc thi cau lenh
      DBMS_OUTPUT.PUT_LINE(V_INSERT);
      EXECUTE Immediate V_INSERT;
    END LOOP;
  ELSE
    NULL;
  END CASE;
END;
-- PROCERDURE Tá»”NG Há»¢P MLNS PHUC VU BAO CAO PL08-BM05, PL06-BM48, PL06-BM50, ...
    
    PROCEDURE PRC_TH_MLNS_BYFOMULAR(
      P_BNGAY_HACHTOAN      DATE,
      P_ENGAY_HACHTOAN      DATE,
      P_BNGAY_KETSO         DATE,
      P_ENGAY_KETSO         DATE,
      P_LOAI                NVARCHAR2,
      P_USERID              NVARCHAR2,
      P_CONGTHUC            NVARCHAR2
      )
    IS
    P_CT VARCHAR2(32767);
    SQL_QUERRY VARCHAR2(32767);
    INSERT_QUERRY VARCHAR2(32767);
    V_TU_NAM VARCHAR(4):= to_char(P_BNGAY_HACHTOAN,'yyyy');
    BEGIN 
     --Delete PHA_MLNS c where c.USERID = P_USERID;
        IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
            SELECT STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC) INTO P_CT FROM dual;
        END IF;
        IF INSTR(P_LOAI,'C') > 0 THEN
          SQL_QUERRY := 'SELECT MA_NGHIEPVU,MA_TKTN, TEN_TKTN,MA_DVQHNS,TEN_DVQHNS,MA_DIABAN,TEN_DIABAN,MA_CAP,TEN_CAP,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,TEN_NGANHKT,MA_LOAI,TEN_LOAI,MA_TIEUMUC,TEN_TIEUMUC,MA_MUC,
              TEN_MUC,MA_TIEUNHOM,TEN_TIEUNHOM,MA_NHOM,TEN_NHOM,MA_CTMTQG,TEN_CTMTQG,MA_KBNN,TEN_KBNN,MA_NGUON_NSNN,TEN_NGUON_NSNN,NGAY_KET_SO, NGAY_HIEU_LUC,SUM (GIA_TRI_HACH_TOAN),ATTRIBUTE8,'''||P_USERID||''' 
              FROM PHA_HACHTOAN_CHI VC WHERE VC.NGAY_HIEU_LUC >= '''|| '01-JAN-' || V_TU_NAM  ||''' 
              AND VC.NGAY_HIEU_LUC <= '''||P_ENGAY_HACHTOAN||''' AND ' ||' VC.NGAY_KET_SO <= '''||P_ENGAY_KETSO||''' AND '
              ||P_CT||
              ' GROUP BY MA_NGHIEPVU,MA_TKTN, TEN_TKTN,MA_DVQHNS,TEN_DVQHNS,MA_DIABAN,TEN_DIABAN,MA_CAP,TEN_CAP,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,
              TEN_NGANHKT,MA_LOAI,TEN_LOAI,MA_TIEUMUC,TEN_TIEUMUC,MA_MUC,TEN_MUC,MA_TIEUNHOM,TEN_TIEUNHOM,MA_NHOM,TEN_NHOM,MA_CTMTQG,TEN_CTMTQG,
              MA_KBNN,TEN_KBNN,MA_NGUON_NSNN,TEN_NGUON_NSNN,NGAY_KET_SO, NGAY_HIEU_LUC,ATTRIBUTE8,'''||P_USERID||''' ';     
              INSERT_QUERRY := 'INSERT INTO PHA_MLNS(LOAI_NS, MA_TKTN,TEN_TKTN, MA_DVQHNS, TEN_DVQHNS,MA_DIABAN, TEN_DIABAN,MA_CAP,TEN_CAP,MA_CHUONG,TEN_CHUONG, MA_NGANHKT, TEN_NGANHKT,MA_LOAI, 
              TEN_LOAI, MA_TIEUMUC, TEN_TIEUMUC, MA_MUC, TEN_MUC, MA_TIEUNHOM, 
              TEN_TIEUNHOM, MA_NHOM, TEN_NHOM, MA_CTMTQG, TEN_CTMTQG, MA_KBNN, TEN_KBNN, MA_NGUON_NSNN, TEN_NGUON_NSNN, NGAY_KET_SO,NGAY_HACH_TOAN,GIA_TRI_HACH_TOAN, LOAI_DU_TOAN,USERID)('||SQL_QUERRY||')';
           DBMS_OUTPUT.PUT_LINE(INSERT_QUERRY);        
            BEGIN 
                EXECUTE IMMEDIATE INSERT_QUERRY;
            END;
        END IF;
        
        IF INSTR(P_LOAI,'T') > 0 THEN
          SQL_QUERRY := 'SELECT MA_NGHIEPVU,MA_TKTN, TEN_TKTN,MA_DVQHNS,TEN_DVQHNS,MA_DIABAN,TEN_DIABAN,MA_CAP,TEN_CAP,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,TEN_NGANHKT,MA_LOAI,TEN_LOAI,MA_TIEUMUC,TEN_TIEUMUC,MA_MUC,
              TEN_MUC,MA_TIEUNHOM,TEN_TIEUNHOM,MA_NHOM,TEN_NHOM,MA_CTMTQG,TEN_CTMTQG,MA_KBNN,TEN_KBNN,MA_NGUON_NSNN,TEN_NGUON_NSNN,NGAY_KET_SO, NGAY_HIEU_LUC,SUM (GIA_TRI_HACH_TOAN),ATTRIBUTE8,'''||P_USERID||''' 
              FROM PHA_HACHTOAN_THU VC VC.NGAY_HIEU_LUC >= '''|| '01-JAN-' || V_TU_NAM  ||''' 
              AND VC.NGAY_HIEU_LUC <= '''||P_ENGAY_HACHTOAN||''' AND ' ||' VC.NGAY_KET_SO <= '''||P_ENGAY_KETSO||''' AND '
              ||P_CT||
              ' GROUP BY MA_NGHIEPVU,MA_TKTN, TEN_TKTN,MA_DVQHNS,TEN_DVQHNS,MA_DIABAN,TEN_DIABAN,MA_CAP,TEN_CAP,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,
              TEN_NGANHKT,MA_LOAI,TEN_LOAI,MA_TIEUMUC,TEN_TIEUMUC,MA_MUC,TEN_MUC,MA_TIEUNHOM,TEN_TIEUNHOM,MA_NHOM,TEN_NHOM,MA_CTMTQG,TEN_CTMTQG,
              MA_KBNN,TEN_KBNN,MA_NGUON_NSNN,TEN_NGUON_NSNN,NGAY_KET_SO, NGAY_HIEU_LUC,ATTRIBUTE8,'''||P_USERID||''' ';     
              INSERT_QUERRY := 'INSERT INTO PHA_MLNS(LOAI_NS, MA_TKTN,TEN_TKTN, MA_DVQHNS, TEN_DVQHNS,MA_DIABAN, TEN_DIABAN,MA_CAP,TEN_CAP,MA_CHUONG,TEN_CHUONG, MA_NGANHKT, TEN_NGANHKT,MA_LOAI, 
              TEN_LOAI, MA_TIEUMUC, TEN_TIEUMUC, MA_MUC, TEN_MUC, MA_TIEUNHOM, 
              TEN_TIEUNHOM, MA_NHOM, TEN_NHOM, MA_CTMTQG, TEN_CTMTQG, MA_KBNN, TEN_KBNN, MA_NGUON_NSNN, TEN_NGUON_NSNN, NGAY_KET_SO,NGAY_HACH_TOAN,GIA_TRI_HACH_TOAN, LOAI_DU_TOAN,USERID)('||SQL_QUERRY||')';
           DBMS_OUTPUT.PUT_LINE(INSERT_QUERRY);    
            BEGIN 
                EXECUTE IMMEDIATE INSERT_QUERRY;
            END;
        END IF;
        
        IF INSTR(P_LOAI,'D') > 0 THEN
          SQL_QUERRY := 'SELECT MA_NGHIEPVU,MA_TKTN, TEN_TKTN,MA_DVQHNS,TEN_DVQHNS,MA_DIABAN,TEN_DIABAN,MA_CAP,TEN_CAP,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,TEN_NGANHKT,MA_LOAI,TEN_LOAI,MA_TIEUMUC,TEN_TIEUMUC,MA_MUC,
              TEN_MUC,MA_TIEUNHOM,TEN_TIEUNHOM,MA_NHOM,TEN_NHOM,MA_CTMTQG,TEN_CTMTQG,MA_KBNN,TEN_KBNN,MA_NGUON_NSNN,TEN_NGUON_NSNN,NGAY_KET_SO, NGAY_HIEU_LUC,SUM (GIA_TRI_HACH_TOAN),ATTRIBUTE8,'''||P_USERID||''' 
              FROM PHA_DUTOAN VC WHERE VC.NGAY_HIEU_LUC >= '''|| '01-JAN-' || V_TU_NAM  ||''' 
              AND VC.NGAY_HIEU_LUC <= '''||P_ENGAY_HACHTOAN||''' AND ' ||' VC.NGAY_KET_SO <= '''||P_ENGAY_KETSO||''' AND '
              ||P_CT||
              ' GROUP BY MA_NGHIEPVU,MA_TKTN, TEN_TKTN,MA_DVQHNS,TEN_DVQHNS,MA_DIABAN,TEN_DIABAN,MA_CAP,TEN_CAP,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,
              TEN_NGANHKT,MA_LOAI,TEN_LOAI,MA_TIEUMUC,TEN_TIEUMUC,MA_MUC,TEN_MUC,MA_TIEUNHOM,TEN_TIEUNHOM,MA_NHOM,TEN_NHOM,MA_CTMTQG,TEN_CTMTQG,
              MA_KBNN,TEN_KBNN,MA_NGUON_NSNN,TEN_NGUON_NSNN,NGAY_KET_SO, NGAY_HIEU_LUC,ATTRIBUTE8,'''||P_USERID||''' ';     
              INSERT_QUERRY := 'INSERT INTO PHA_MLNS(LOAI_NS, MA_TKTN,TEN_TKTN, MA_DVQHNS, TEN_DVQHNS,MA_DIABAN, TEN_DIABAN,MA_CAP,TEN_CAP,MA_CHUONG,TEN_CHUONG, MA_NGANHKT, TEN_NGANHKT,MA_LOAI, 
              TEN_LOAI, MA_TIEUMUC, TEN_TIEUMUC, MA_MUC, TEN_MUC, MA_TIEUNHOM, 
              TEN_TIEUNHOM, MA_NHOM, TEN_NHOM, MA_CTMTQG, TEN_CTMTQG, MA_KBNN, TEN_KBNN, MA_NGUON_NSNN, TEN_NGUON_NSNN, NGAY_KET_SO,NGAY_HACH_TOAN,GIA_TRI_HACH_TOAN, LOAI_DU_TOAN,USERID)('||SQL_QUERRY||')';
           DBMS_OUTPUT.PUT_LINE(INSERT_QUERRY); 
           BEGIN 
                EXECUTE IMMEDIATE INSERT_QUERRY;
            END;
        END IF;
    END PRC_TH_MLNS_BYFOMULAR;
END STC_PA_SYS;

/
--------------------------------------------------------
--  DDL for Package Body STC_PHA_BCDMCT
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "BTSTC"."STC_PHA_BCDMCT" 
AS

PROCEDURE PROC_TESTDL (LOAI IN varchar:= 'thu', CUR OUT SYS_REFCURSOR)
as
    rowtype Test_DL%ROWTYPE;
begin
    delete TEMP_MLNS; 
    select * into rowtype from test_dl where name = loai and rownum = 1;
    STC_PA_SYS.PRC_TH_MLNS ( To_DATE('0101' || rowtype.nam,'ddMMyyyy'), To_DATE('3112' || rowtype.nam,'ddMMyyyy'),To_DATE('0101' || rowtype.nam,'ddMMyyyy'), To_DATE('3112' || rowtype.nam,'ddMMyyyy'), case Loai when 'thu' then 2 else 1 end); 
    open cur for select * from Table(FCN_TESTDL(LOAI)); 
end;

FUNCTION FCN_TESTDL (LOAI       varchar:= 'thu')
                                RETURN MEASURE_TABLE
      PIPELINED
Is
    rowtype Test_DL%ROWTYPE;
    GIATRI_TABLE   MEASURE_RECORD;
Begin
    -- Xoa DL
    
    -- TH SO LIEU
    select * into rowtype from test_dl where name = loai and rownum = 1;
    
    --DBMS_OUTPUT.put_line(rowtype.ct);
    --DBMS_OUTPUT.put_line(rowtype.nam);
    select * into GIATRI_TABLE from TABLE(STC_PHA_BCDMCT.FCN_GET_PHA_BCDMCT(case Loai when 'thu' then 2 else 1 end,rowtype.ct,'','',To_DATE('0101' || rowtype.nam,'ddMMyyyy'),To_DATE('3112'  || rowtype.nam,'ddMMyyyy'),To_DATE('01012011','ddMMyyyy'),To_DATE('31122016','ddMMyyyy'),1)); 
         PIPE ROW (GIATRI_TABLE);
         RETURN;
End;

   FUNCTION FCN_GET_PHA_BCDMCT (LOAI_BC       NVARCHAR2,
                                CONGTHUC      NVARCHAR2,
                                CONGTHUC_SEG1 NVARCHAR2,
                                CONGTHUC_SEG2 NVARCHAR2,
                                TUNGAY_HL     DATE,
                                DENNGAY_HL    DATE,
                                TUNGAY_KS     DATE,
                                DENNGAY_KS    DATE,
                                DONVI_TIEN    NUMBER)
      RETURN MEASURE_TABLE
      PIPELINED
   IS
      QUERY_STR      VARCHAR2 (30000);
      P_SQL_INSERT   VARCHAR2 (1000);
      P_CONGTHUC     VARCHAR2 (24000);
      P_CONGTHUC1     NVARCHAR2 (12000);
      cur            SYS_REFCURSOR;
      GIATRI_TABLE   MEASURE_RECORD;
   BEGIN
      IF CONGTHUC IS NULL
      THEN
         SELECT 0, 0, 0, 0, 0, 0, 0, 0 INTO GIATRI_TABLE FROM DUAL;
         PIPE ROW (GIATRI_TABLE);
         RETURN;
      END IF;

      IF LOAI_BC IS NULL
      THEN
         P_SQL_INSERT := 'TEMP_MLNS b';
      END IF;

      IF (TRIM (LOAI_BC) = 'C')
      THEN
         P_SQL_INSERT := 'TEMP_MLNS b';
      ELSIF (TRIM (LOAI_BC) = 'T')
      THEN
         P_SQL_INSERT := 'TEMP_MLNS b';
      ELSE
         P_SQL_INSERT := 'TEMP_MLNS b';
      END IF;
      
      /*if Length(Trim(CONGTHUC_SEG1)) > 0 then
        P_CONGTHUC := CONGTHUC || CONGTHUC_SEG1 || CONGTHUC_SEG2;
        end if;*/
        P_CONGTHUC1 := CONGTHUC || nvl(CONGTHUC_SEG1,'') || nvl(CONGTHUC_SEG2,'');
      
      /*SELECT STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC1)
        INTO P_CONGTHUC
        FROM DUAL;*/
        
        P_CONGTHUC := STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC1);

      QUERY_STR :=
            'select SUM(TW_PS)as TW_PS, NVL(SUM(TW_LK),0)as TW_LK,NVL(SUM(TINH_PS),0)as TINH_PS,NVL(SUM(TINH_LK),0)as TINH_LK,NVL(SUM(HUYEN_PS),0)as HUYEN_PS,NVL(SUM(HUYEN_LK),0)as HUYEN_LK,NVL(SUM(XA_PS),0)as XA_PS,NVL(SUM(XA_LK),0)as XA_LK 
            FROM (SELECT 
                 MA_CAP, MA_CAPNS,
                 NVL(SUM (
                    CASE
                       WHEN (    NGAY_HACH_TOAN >= TO_DATE ('''|| TO_CHAR (TUNGAY_HL, 'DD/MM/YY')|| ''', ''DD/MM/YY'')
                             AND NGAY_HACH_TOAN <= TO_DATE ('''|| TO_CHAR (DENNGAY_HL, 'DD/MM/YY')|| ''', ''DD/MM/YY''))
                       THEN
                          GIA_TRI_HACH_TOAN
                       ELSE
                          0
                    END) ,0)
                    AS PS,'||
                 /*NVL(SUM ( CASE
                       WHEN (    NGAY_HACH_TOAN >= TO_DATE ('''||TO_CHAR(TUNGAY_HL, 'YY')||'-01-01'|| ''', ''DD/MM/YY'')
                             AND NGAY_HACH_TOAN <= TO_DATE ('''|| TO_CHAR (DENNGAY_HL, 'DD/MM/YY')|| ''', ''DD/MM/YY''))
                       THEN
                          GIA_TRI_HACH_TOAN
                       ELSE
                          0
                    END ),0) AS LK*/
                    ' Sum(NVL(GIA_TRI_HACH_TOAN,0)) as LK' ||
            ' FROM '
         || P_SQL_INSERT
         || ' 
            WHERE 1=1 And '
         || P_CONGTHUC
         || ' 
        GROUP BY MA_CAP, MA_CAPNS
        )
        PIVOT ( sum(PS) as PS, Sum(LK) as LK
                  FOR MA_CAPNS
                  IN (''1'' AS TW, ''2'' AS TINH, ''3'' AS HUYEN, ''4'' AS XA)
                  )
        ';
        
        -- 13/05/2017 Kiem tra ty le dieu tiet 
--DBMS_OUTPUT.put_line ('abc:' || QUERY_STR);
      /*IF((TRIM (LOAI_BC) = '2'))
      Then
        QUERY_STR:= Replace (QUERY_STR, 'MA_CAPNS','MA_CAP');
      End If;*/
      BEGIN
         OPEN cur FOR QUERY_STR;
      EXCEPTION
         WHEN NO_DATA_FOUND
         THEN
            --DBMS_OUTPUT.put_line ('NoData:' || SQLERRM);
            return;
         WHEN OTHERS
         THEN            
            /*DBMS_OUTPUT.put_line ('Loi:' || QUERY_STR);
            DBMS_OUTPUT.put_line ('CT:' || CONGTHUC);
            DBMS_OUTPUT.put_line ('CT:' || CONGTHUC_SEG1);
            DBMS_OUTPUT.put_line ('CT:' || CONGTHUC_SEG2);
            */
            return;
      END;
      --return;
      OPEN cur FOR QUERY_STR;
      LOOP
         FETCH cur
            INTO GIATRI_TABLE.TW_PS,
                 GIATRI_TABLE.TW_LK, 
                 GIATRI_TABLE.TINH_PS,
                 GIATRI_TABLE.TINH_LK,
                 GIATRI_TABLE.HUYEN_PS,
                 GIATRI_TABLE.HUYEN_LK,
                 GIATRI_TABLE.XA_PS,
                 GIATRI_TABLE.XA_LK;

         IF cur%FOUND
         THEN
            PIPE ROW (GIATRI_TABLE);
         ELSE
            EXIT;
         END IF;
      END LOOP;

      RETURN;
   END FCN_GET_PHA_BCDMCT;
   
   FUNCTION FCN_GET_PHA_TONQUY (LOAI     NVARCHAR2,
                                CONGTHUC      NVARCHAR2,
                                CONGTHUC_SEG1 NVARCHAR2, 
                                CONGTHUC_SEG2 NVARCHAR2,
                                TUNGAY_HL     DATE,
                                DENNGAY_HL    DATE,
                                TUNGAY_KS     DATE,
                                DENNGAY_KS    DATE,
                                DONVI_TIEN    NUMBER)
      RETURN MEASURE_TABLE
      PIPELINED
   IS
      QUERY_STR      VARCHAR2 (30000);
      P_SQL_INSERT   VARCHAR2 (1000);
      P_CONGTHUC     VARCHAR2 (24000);
      P_CONGTHUC1     NVARCHAR2 (12000);
      cur            SYS_REFCURSOR;
      GIATRI_TABLE   MEASURE_RECORD;
   BEGIN
      IF CONGTHUC IS NULL
      THEN
         SELECT 0, 0, 0, 0, 0, 0, 0, 0 INTO GIATRI_TABLE FROM DUAL;
         PIPE ROW (GIATRI_TABLE);
         RETURN;
      END IF;

      IF LOAI IS NULL
      THEN
         P_SQL_INSERT := 'TEMP_TONQUY_CHI b';
      END IF;

      IF (TRIM (LOAI) = '1')
      THEN
         P_SQL_INSERT := 'TEMP_TONQUY_CHI b';
      ELSIF (TRIM (LOAI) = '2')
      THEN
         P_SQL_INSERT := 'TEMP_TONQUY_THU b';
      ELSE
         P_SQL_INSERT := 'TEMP_TONQUY_CHI b';
      END IF;
      
      /*if Length(Trim(CONGTHUC_SEG1)) > 0 then
        P_CONGTHUC := CONGTHUC || CONGTHUC_SEG1 || CONGTHUC_SEG2;
        end if;*/
        P_CONGTHUC1 := CONGTHUC || nvl(CONGTHUC_SEG1,'') || nvl(CONGTHUC_SEG2,'');
      
      /*SELECT STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC1)
        INTO P_CONGTHUC
        FROM DUAL;*/
        
        P_CONGTHUC := STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC1);

      QUERY_STR :=
            'select SUM(TW_PS)as TW_PS, NVL(SUM(TW_LK),0)as TW_LK,NVL(SUM(TINH_PS),0)as TINH_PS,NVL(SUM(TINH_LK),0)as TINH_LK,NVL(SUM(HUYEN_PS),0)as HUYEN_PS,NVL(SUM(HUYEN_LK),0)as HUYEN_LK,NVL(SUM(XA_PS),0)as XA_PS,NVL(SUM(XA_LK),0)as XA_LK 
            FROM (SELECT 
                 MA_CAP, MA_CAPNS,
                 NVL(SUM (
                    CASE
                       WHEN (    NGAY_HACH_TOAN >= TO_DATE ('''|| TO_CHAR (TUNGAY_HL, 'DD/MM/YY')|| ''', ''DD/MM/YY'')
                             AND NGAY_HACH_TOAN <= TO_DATE ('''|| TO_CHAR (DENNGAY_HL, 'DD/MM/YY')|| ''', ''DD/MM/YY'')
                             and NGAY_KET_SO >=TO_DATE ('''||to_char(TUNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'') 
                             and NGAY_KET_SO <=TO_DATE ('''||to_char(DENNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy''))
                       THEN
                          GIA_TRI_HACH_TOAN
                       ELSE
                          0
                    END) ,0)
                    AS PS,'||
                 /*NVL(SUM ( CASE
                       WHEN (    NGAY_HACH_TOAN >= TO_DATE ('''||TO_CHAR(TUNGAY_HL, 'YY')||'-01-01'|| ''', ''DD/MM/YY'')
                             AND NGAY_HACH_TOAN <= TO_DATE ('''|| TO_CHAR (DENNGAY_HL, 'DD/MM/YY')|| ''', ''DD/MM/YY''))
                       THEN
                          GIA_TRI_HACH_TOAN
                       ELSE
                          0
                    END ),0) AS LK*/
                    ' Sum(NVL(GIA_TRI_HACH_TOAN,0)) as LK' ||
            ' FROM '
         || P_SQL_INSERT
         || ' 
            WHERE 1=1 And NGAY_HACH_TOAN >= TO_DATE ('''|| TO_CHAR (TUNGAY_HL, 'DD/MM/YY')|| ''', ''DD/MM/YY'')
                             AND NGAY_HACH_TOAN <= TO_DATE ('''|| TO_CHAR (DENNGAY_HL, 'DD/MM/YY')|| ''', ''DD/MM/YY'') '
         || P_CONGTHUC
         || ' 
        GROUP BY MA_CAP, MA_CAPNS
        )
        PIVOT ( sum(PS) as PS, Sum(LK) as LK
                  FOR MA_CAPNS
                  IN (''1'' AS TW, ''2'' AS TINH, ''3'' AS HUYEN, ''4'' AS XA)
                  )
        ';
        
        -- 13/05/2017 Kiem tra ty le dieu tiet 
--DBMS_OUTPUT.put_line ('abc:' || QUERY_STR);
      /*IF((TRIM (LOAI_BC) = '2'))
      Then
        QUERY_STR:= Replace (QUERY_STR, 'MA_CAPNS','MA_CAP');
      End If;*/
      BEGIN
         OPEN cur FOR QUERY_STR;
      EXCEPTION
         WHEN NO_DATA_FOUND
         THEN
            --DBMS_OUTPUT.put_line ('NoData:' || SQLERRM);
            return;
         WHEN OTHERS
         THEN            
            /*DBMS_OUTPUT.put_line ('Loi:' || QUERY_STR);
            DBMS_OUTPUT.put_line ('CT:' || CONGTHUC);
            DBMS_OUTPUT.put_line ('CT:' || CONGTHUC_SEG1);
            DBMS_OUTPUT.put_line ('CT:' || CONGTHUC_SEG2);
            */
            return;
      END;
      --return;
      OPEN cur FOR QUERY_STR;
      LOOP
         FETCH cur
            INTO GIATRI_TABLE.TW_PS,
                 GIATRI_TABLE.TW_LK,
                 GIATRI_TABLE.TINH_PS,
                 GIATRI_TABLE.TINH_LK,
                 GIATRI_TABLE.HUYEN_PS,
                 GIATRI_TABLE.HUYEN_LK,
                 GIATRI_TABLE.XA_PS,
                 GIATRI_TABLE.XA_LK;

         IF cur%FOUND
         THEN
            PIPE ROW (GIATRI_TABLE);
         ELSE
            EXIT;
         END IF;
      END LOOP;

      RETURN;
   END FCN_GET_PHA_TONQUY;
   
   --===========================================================================
     FUNCTION FCN_GET_DATA_FROM_CT (CONGTHUC      NVARCHAR2)
      RETURN NUMBER 
      
   IS
      QUERY_STR      VARCHAR2 (30000);
      P_CONGTHUC     VARCHAR2 (24000); 
      GIA_TRI        NUMBER:=0;
      cur            SYS_REFCURSOR;
     
   BEGIN   
      P_CONGTHUC := STC_PA_SYS.FNC_CONVERT_FORMULA (CONGTHUC);
    
       QUERY_STR :='SELECT nvl( SUM (GIA_TRI_HACH_TOAN),0) as GIATRI FROM PHA_MLNS WHERE 1=1 and (' || P_CONGTHUC || ')';
       
       OPEN cur FOR QUERY_STR;
       LOOP
       FETCH cur 
        INTO
        GIA_TRI;
         IF cur%FOUND
         THEN
            RETURN GIA_TRI;
         ELSE
            EXIT;
         END IF;
      END LOOP;
       Close cur;
      RETURN GIA_TRI;
   END FCN_GET_DATA_FROM_CT;
   
END STC_PHA_BCDMCT;

/
--------------------------------------------------------
--  DDL for Package STC_PA_SYS
--------------------------------------------------------

--------------------------------------------------------
--  DDL for Package STC_PHA_BCDMCT
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "BTSTC"."STC_PHA_BCDMCT" AS 
    
    TYPE MEASURE_RECORD IS RECORD (
      TW_PS number(20,0),
      TW_LK number(20,0),
      TINH_PS number(20,0),
      TINH_LK number(20,0),
      HUYEN_PS number(20,0),
      HUYEN_LK number(20,0),
      XA_PS number(20,0),
      XA_LK number(20,0)
    );
    TYPE MEASURE_TABLE IS TABLE OF MEASURE_RECORD;
  FUNCTION FCN_GET_PHA_BCDMCT(LOAI_BC NVARCHAR2, CONGTHUC NVARCHAR2,CONGTHUC_SEG1 NVARCHAR2,CONGTHUC_SEG2 NVARCHAR2, TUNGAY_HL DATE, DENNGAY_HL DATE,TUNGAY_KS DATE, DENNGAY_KS DATE,DONVI_TIEN NUMBER)
  RETURN MEASURE_TABLE PIPELINED;
  FUNCTION FCN_GET_PHA_TONQUY(LOAI NVARCHAR2, CONGTHUC NVARCHAR2,CONGTHUC_SEG1 NVARCHAR2,CONGTHUC_SEG2 NVARCHAR2, TUNGAY_HL DATE, DENNGAY_HL DATE,TUNGAY_KS DATE, DENNGAY_KS DATE,DONVI_TIEN NUMBER)
  RETURN MEASURE_TABLE PIPELINED;
  FUNCTION FCN_GET_DATA_FROM_CT (CONGTHUC      NVARCHAR2)
      RETURN NUMBER;
   FUNCTION FCN_TESTDL (LOAI       varchar:= 'thu')
                               RETURN MEASURE_TABLE PIPELINED;
   PROCEDURE PROC_TESTDL (LOAI IN varchar:= 'thu', CUR OUT SYS_REFCURSOR);
    
END STC_PHA_BCDMCT;

/
--------------------------------------------------------
--  DDL for Function FCN_GET_DATA_PL08BM05_XP
--------------------------------------------------------

  CREATE OR REPLACE FUNCTION "BTSTC"."FCN_GET_DATA_PL08BM05_XP" (
    P_CONGTHUC      NVARCHAR2,
    P_USER        NVARCHAR2)
RETURN NUMBER 
IS 
        QUERY_STR       VARCHAR2 (30000);
        GIA_TRI         NUMBER:=0;
        P_CT VARCHAR2(32767);
        cur             SYS_REFCURSOR;
BEGIN   
         IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
            SELECT STC_PA_SYS.FNC_CONVERT_FORMULA (P_CONGTHUC) INTO P_CT FROM dual;
        END IF;
        QUERY_STR := 'SELECT nvl( SUM (GIA_TRI_HACH_TOAN),0) as GIATRI FROM PHA_MLNS WHERE 1=1 AND '|| P_CT ||' AND USERID = '''||P_USER||''' '; 
        DBMS_OUTPUT.PUT_LINE(QUERY_STR);
        OPEN cur FOR QUERY_STR;
        LOOP
        FETCH cur 
            INTO
            GIA_TRI;
                IF cur%FOUND
            THEN
                RETURN GIA_TRI;
            ELSE
            EXIT;
        END IF;
        END LOOP;
       Close cur;
      RETURN GIA_TRI;
END FCN_GET_DATA_PL08BM05_XP;

/
