create or replace procedure PHA_PL08_BM07(P_LOAI_BAOCAO IN VARCHAR2, P_CONGTHUC VARCHAR2, TUNGAY_KS IN DATE, DENNGAY_KS IN DATE, TUNGAY_HL IN DATE, DENNGAY_HL IN DATE, P_DBHC VARCHAR2, DONVI_TIEN number, cur OUT SYS_REFCURSOR) as
   QUERY_STR   CLOB; 
   P_CT  VARCHAR2(32767);   
   P_DIABAN  VARCHAR2(32767); 
   P_SQL_INSERT  VARCHAR2(32767); 
   
   --CHI DAU TU PHAT TRIEN
   CT_CDT_GD VARCHAR2(32767);   CT_CDT_VH VARCHAR2(32767);  CT_CDT_TDTT VARCHAR2(32767);   CT_CDT_QLNN VARCHAR2(32767);   CT_CDT_HDKT VARCHAR2(32767); 
   --CHI THUONG XUYEN
   CT_CTX_GD VARCHAR2(32767);   CT_CTX_VH VARCHAR2(32767);   CT_CTX_TDTT VARCHAR2(32767);    CT_CTX_QLNN VARCHAR2(32767);   CT_CTX_HDKT VARCHAR2(32767);

   BEGIN
    IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA_HCSN(P_CONGTHUC) INTO P_CT from dual;       
        P_CT:= ' AND ' || P_CT;
    END IF;
    IF TRIM(P_DBHC) IS NOT NULL THEN          
        P_DIABAN:= ' AND MA_DIABAN= ' || P_DBHC;
    END IF;
    
    
    --G�n c�ng th?c
    IF(UPPER(P_LOAI_BAOCAO) = 'DH') THEN 
        -------Chi ??u t?
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CDT_HDKT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI' AND MA_COT='CDT_HDKT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CDT_QLNN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI' AND MA_COT='CDT_QLNN';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CDT_VH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI' AND MA_COT='CDT_VH';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CDT_GD from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI' AND MA_COT='CDT_GD';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CDT_TDTT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI' AND MA_COT='CDT_TDTT';
        -------Chi th??ng xuy�n
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CTX_HDKT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI' AND MA_COT='CTX_HDKT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CTX_QLNN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI' AND MA_COT='CTX_QLNN';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CTX_VH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI' AND MA_COT='CTX_VH';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CTX_GD from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI' AND MA_COT='CTX_GD';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CTX_TDTT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI' AND MA_COT='CTX_TDTT';
    END IF;
    IF(UPPER(P_LOAI_BAOCAO) = 'QT') THEN 
         -------Chi ??u t?
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CDT_HDKT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI' AND MA_COT='CDT_HDKT';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CDT_QLNN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI' AND MA_COT='CDT_QLNN';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CDT_VH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI' AND MA_COT='CDT_VH';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CDT_GD from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI' AND MA_COT='CDT_GD';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CDT_TDTT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI' AND MA_COT='CDT_TDTT';
        -------Chi th??ng xuy�n
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CTX_HDKT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI' AND MA_COT='CTX_HDKT';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CTX_QLNN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI' AND MA_COT='CTX_QLNN';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CTX_VH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI' AND MA_COT='CTX_VH';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CTX_GD from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI' AND MA_COT='CTX_GD';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CTX_TDTT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI' AND MA_COT='CTX_TDTT';
    END IF;

    QUERY_STR:='SELECT HT.MA_DVQHNS, HT.TEN_DVQHNS
                ,NVL(HT.CDT_GD,0) as CDT_GD, NVL(HT.CDT_VH,0) as CDT_VH , NVL(HT.CDT_TDTT,0) as CDT_TDTT, NVL(HT.CDT_QLNN,0) as CDT_QLNN, NVL(HT.CDT_HDKT,0) as CDT_HDKT
                ,NVL(HT.CTX_GD,0) as CTX_GD, NVL(HT.CTX_VH,0) as CTX_VH , NVL(HT.CTX_TDTT,0) as CTX_TDTT, NVL(HT.CTX_QLNN,0) as CTX_QLNN, NVL(HT.CTX_HDKT,0) as CTX_HDKT
                FROM
                (
                SELECT A.MA_DVQHNS AS MA_DVQHNS, NS.TEN_DVQHNS
                        ,NVL(SUM (CASE WHEN ('|| CT_CDT_GD ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDT_GD
                        ,NVL(SUM (CASE WHEN ('|| CT_CDT_VH ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDT_VH
                        ,NVL(SUM (CASE WHEN ('|| CT_CDT_TDTT ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDT_TDTT
                        ,NVL(SUM (CASE WHEN ('|| CT_CDT_QLNN ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDT_QLNN
                        ,NVL(SUM (CASE WHEN ('|| CT_CDT_HDKT ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDT_HDKT
                        
                        ,NVL(SUM (CASE WHEN ('|| CT_CTX_GD ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_GD
                        ,NVL(SUM (CASE WHEN ('|| CT_CTX_VH ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_VH
                        ,NVL(SUM (CASE WHEN ('|| CT_CTX_TDTT ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_TDTT
                        ,NVL(SUM (CASE WHEN ('|| CT_CTX_QLNN ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_QLNN
                        ,NVL(SUM (CASE WHEN ('|| CT_CTX_HDKT ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_HDKT

                FROM PHA_HACHTOAN_CHI A LEFT JOIN SYS_DVQHNS NS ON A.MA_DVQHNS=NS.MA_DVQHNS
                WHERE A.NGAY_HIEU_LUC >= TO_DATE ('''||to_char(TUNGAY_HL,'ddMMyyyy')||''', ''ddMMyyyy'')
                        AND A.NGAY_HIEU_LUC <= TO_DATE ('''||to_char(DENNGAY_HL,'ddMMyyyy')||''', ''ddMMyyyy'')     
                        and A.NGAY_KET_SO <=TO_DATE ('''||to_char(DENNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'')
                        and A.NGAY_KET_SO >=TO_DATE ('''||to_char(TUNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'')
                        AND NOT A.MA_CAP like ''1'' AND NOT A.MA_CHUONG LIKE ''000''
                        AND NS.MA_CHUONG IN (''422'',''622'',''822'') AND LENGTH(NS.MA_DVQHNS_CHA) >0 '                     
                        || P_CT
                        || P_DIABAN
                        || ' GROUP BY A.MA_DVQHNS,NS.TEN_DVQHNS
                ) HT
                WHERE 1=1 ORDER BY HT.TEN_DVQHNS';


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
    --DBMS_OUTPUT.ENABLE(200000);
     DBMS_OUTPUT.put_line ('<your message>'  || SQLERRM); 
END;   
END PHA_PL08_BM07;